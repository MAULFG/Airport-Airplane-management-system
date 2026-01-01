using Airport_Airplane_management_system.Model.Core.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;

namespace Airport_Airplane_management_system.Model.Repositories
{
    public class MySqlFlightRepository : IFlightRepository
    {
        private readonly string _connStr;

        public MySqlFlightRepository(string connStr)
        {
            _connStr = connStr;
        }

        public List<Flight> GetAllFlights()
        {
            var flights = new List<Flight>();

            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            string query = "SELECT * FROM flights";
            using var cmd = new MySqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int flightId = reader.GetInt32("id");
                int planeId = reader.GetInt32("plane_id");
                string fromCity = reader.GetString("from_city");
                string toCity = reader.GetString("to_city");
                DateTime departure = reader.GetDateTime("departure");
                DateTime arrival = reader.GetDateTime("arrival");

                // Plane object should be loaded separately in service
                flights.Add(new Flight(flightId, null, fromCity, toCity, departure, arrival, new Dictionary<string, decimal>())
                {
                    PlaneIDFromDb = planeId
                });

            }

            return flights;
        }
        public Flight GetFlightById(int flightId)
        {
            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            const string sql = "SELECT * FROM flights WHERE id=@id;";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", flightId);

            using var r = cmd.ExecuteReader();
            if (!r.Read()) return null;

            return new Flight(
                r.GetInt32("id"),
                null,
                r.GetString("from_city"),
                r.GetString("to_city"),
                r.GetDateTime("departure"),
                r.GetDateTime("arrival"),
                new Dictionary<string, decimal>()
            )
            {
                PlaneIDFromDb = r.GetInt32("plane_id")
            };
        }
        public List<FlightSeats> GetSeatsForFlight(int flightId)
        {
            var seats = new List<FlightSeats>();

            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            string query = @"SELECT id, flight_id, plane_seat_index, seat_number, class_type, is_booked, passenger_id
                         FROM flight_seats
                         WHERE flight_id = @fid
                         ORDER BY plane_seat_index";
            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@fid", flightId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int seatId = reader.GetInt32("id");
                int planeSeatIndex = reader.GetInt32("plane_seat_index");
                string seatNum = reader.GetString("seat_number");
                string classType = reader.GetString("class_type");
                bool isBooked = reader.GetBoolean("is_booked");
                int? passengerId = reader.IsDBNull(reader.GetOrdinal("passenger_id")) ? null : reader.GetInt32("passenger_id");

                seats.Add(new FlightSeats(seatId, flightId, planeSeatIndex, seatNum, classType, isBooked, passengerId));
            }

            return seats;
        }
        public int CountUpcomingFlightsNotFullyBooked()
        {
            const string sql = @"
SELECT COUNT(DISTINCT f.id)
FROM flights f
JOIN flight_seats fs ON fs.flight_id = f.id
WHERE f.departure >= NOW()
  AND fs.is_booked = 0;
";

            using var conn = new MySqlConnection(_connStr);
            conn.Open();
            using var cmd = new MySqlCommand(sql, conn);
            object result = cmd.ExecuteScalar();
            return (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
        }
        

        

      

        

        
       
      
        public bool InsertFlight(Flight flight, out int newFlightId, out string error)
        {
            newFlightId = -1;
            error = "";

            const string sql = @"
INSERT INTO flights (plane_id, from_city, to_city, departure, arrival)
VALUES (@pid, @from, @to, @dep, @arr);
SELECT LAST_INSERT_ID();";

            try
            {
                using var conn = new MySqlConnection(_connStr);
                using var cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@pid", flight.Plane.PlaneID);
                cmd.Parameters.AddWithValue("@from", flight.From);
                cmd.Parameters.AddWithValue("@to", flight.To);
                cmd.Parameters.AddWithValue("@dep", flight.Departure);
                cmd.Parameters.AddWithValue("@arr", flight.Arrival);

                conn.Open();
                newFlightId = Convert.ToInt32(cmd.ExecuteScalar());
                return newFlightId > 0;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        // ✅ Used for "depends on selected plane"
        // ✅ FIX: VIP is treated like FIRST (Private Jet)
        // ✅ Used for "depends on selected plane"
        // ✅ FIX: return VIP as "vip" (so UI can display VIP instead of First)
        public HashSet<string> GetSeatClassesForPlane(int planeId)
        {
            static string NormalizeClass(string raw)
            {
                if (string.IsNullOrWhiteSpace(raw)) return "";

                var c = raw.Trim().ToLowerInvariant();

                // keep VIP distinct
                if (c.Contains("vip")) return "vip";

                // first class
                if (c.Contains("first") || c.Contains("premium")) return "first";

                if (c.Contains("bus") || c.Contains("business") || c.Contains("biz"))
                    return "business";

                if (c.Contains("eco") || c.Contains("economy"))
                    return "economy";

                return c;
            }

            var set = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            using var con = new MySqlConnection(_connStr);
            con.Open();

            using var cmd = con.CreateCommand();
            cmd.CommandText = @"
SELECT DISTINCT class_type
FROM seats
WHERE plane_id = @pid;";
            cmd.Parameters.AddWithValue("@pid", planeId);

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var raw = r["class_type"]?.ToString();
                var norm = NormalizeClass(raw);

                if (!string.IsNullOrWhiteSpace(norm))
                    set.Add(norm);
            }

            return set;
        }


        // ✅ Inserts flight + seats + seat_price based on admin inputs
        // ✅ FIX: VIP seats also use FIRST price
        public bool InsertFlightWithSeats(
            Flight flight,
            decimal economyPrice,
            decimal businessPrice,
            decimal firstPrice,
            out int newFlightId,
            out string error)
        {
            newFlightId = -1;
            error = "";

            try
            {
                using var conn = new MySqlConnection(_connStr);
                conn.Open();
                using var tx = conn.BeginTransaction();

                // 1) Insert flight
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tx;
                    cmd.CommandText = @"
INSERT INTO flights (plane_id, from_city, to_city, departure, arrival)
VALUES (@pid, @from, @to, @dep, @arr);
SELECT LAST_INSERT_ID();";

                    cmd.Parameters.AddWithValue("@pid", flight.Plane.PlaneID);
                    cmd.Parameters.AddWithValue("@from", flight.From);
                    cmd.Parameters.AddWithValue("@to", flight.To);
                    cmd.Parameters.AddWithValue("@dep", flight.Departure);
                    cmd.Parameters.AddWithValue("@arr", flight.Arrival);

                    newFlightId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                if (newFlightId <= 0)
                    throw new Exception("Failed to insert flight.");

                // 2) Create flight_seats from seats, set price by class_type
                using (var cmd = conn.CreateCommand())
                {
                    cmd.Transaction = tx;

                    // NOTE:
                    // - Handles class_type like: "Economy", "Business", "First", "First Class", "VIP"
                    cmd.CommandText = @"
INSERT INTO flight_seats
(flight_id, plane_seat_index, seat_number, class_type, is_booked, passenger_id, seat_price)
SELECT
    @fid,
    s.id,
    s.seat_number,
    s.class_type,
    0,
    NULL,
    CASE
        WHEN LOWER(TRIM(s.class_type)) LIKE '%eco%' THEN @eco
        WHEN LOWER(TRIM(s.class_type)) LIKE '%bus%' THEN @bus
        WHEN LOWER(TRIM(s.class_type)) LIKE '%first%' THEN @first
        WHEN LOWER(TRIM(s.class_type)) LIKE '%vip%' THEN @first
        ELSE 0.00
    END
FROM seats s
WHERE s.plane_id = @pid;";

                    cmd.Parameters.AddWithValue("@fid", newFlightId);
                    cmd.Parameters.AddWithValue("@pid", flight.Plane.PlaneID);
                    cmd.Parameters.AddWithValue("@eco", economyPrice);
                    cmd.Parameters.AddWithValue("@bus", businessPrice);
                    cmd.Parameters.AddWithValue("@first", firstPrice);

                    cmd.ExecuteNonQuery();
                }

                tx.Commit();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool UpdateFlightDates(int flightId, DateTime newDeparture, DateTime newArrival, out string error)
        {
            error = "";
            if (newArrival <= newDeparture)
            {
                error = "Arrival must be after departure.";
                return false;
            }

            const string sql = "UPDATE flights SET departure=@dep, arrival=@arr WHERE id=@id;";
            try
            {
                using var conn = new MySqlConnection(_connStr);
                using var cmd = new MySqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@dep", newDeparture);
                cmd.Parameters.AddWithValue("@arr", newArrival);
                cmd.Parameters.AddWithValue("@id", flightId);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool DeleteFlight(int flightId, out string error)
        {
            error = "";
            try
            {
                using var conn = new MySqlConnection(_connStr);
                conn.Open();
                using var tx = conn.BeginTransaction();

                using (var cmd = new MySqlCommand("DELETE FROM bookings WHERE flight_id=@fid;", conn, tx))
                {
                    cmd.Parameters.AddWithValue("@fid", flightId);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new MySqlCommand("DELETE FROM flight_seats WHERE flight_id=@fid;", conn, tx))
                {
                    cmd.Parameters.AddWithValue("@fid", flightId);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new MySqlCommand("DELETE FROM flights WHERE id=@fid;", conn, tx))
                {
                    cmd.Parameters.AddWithValue("@fid", flightId);
                    int affected = cmd.ExecuteNonQuery();
                    tx.Commit();
                    return affected > 0;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool PlaneHasTimeConflict(int planeId, DateTime dep, DateTime arr, int? excludeFlightId = null)
        {
            string sql = @"
SELECT COUNT(*)
FROM flights
WHERE plane_id = @pid
  AND (@dep < arrival AND @arr > departure)";

            if (excludeFlightId.HasValue)
                sql += " AND id <> @exclude";

            using var conn = new MySqlConnection(_connStr);
            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@pid", planeId);
            cmd.Parameters.AddWithValue("@dep", dep);
            cmd.Parameters.AddWithValue("@arr", arr);

            if (excludeFlightId.HasValue)
                cmd.Parameters.AddWithValue("@exclude", excludeFlightId.Value);

            conn.Open();
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
        public Dictionary<string, decimal> GetSeatPricesForFlight(int flightId)
        {
            // returns one price per class (economy/business/first/vip)
            var dict = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);

            using var con = new MySqlConnection(_connStr);
            con.Open();

            using var cmd = con.CreateCommand();
            cmd.CommandText = @"
        SELECT LOWER(TRIM(class_type)) AS ct, MIN(seat_price) AS p
        FROM flight_seats
        WHERE flight_id = @fid
        GROUP BY LOWER(TRIM(class_type));
    ";
            cmd.Parameters.AddWithValue("@fid", flightId);

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var ct = r["ct"]?.ToString() ?? "";
                var price = Convert.ToDecimal(r["p"]);

                // normalize to your 3 UI buckets
                if (ct.Contains("vip")) dict["vip"] = price;
                else if (ct.Contains("first")) dict["first"] = price;
                else if (ct.Contains("bus")) dict["business"] = price;
                else if (ct.Contains("eco")) dict["economy"] = price;
            }

            return dict;
        }
        public bool UpdateSeatPricesForFlight(int flightId, decimal economy, decimal business, decimal firstOrVip, out string error)
        {
            error = "";
            try
            {
                using var con = new MySqlConnection(_connStr);
                con.Open();

                using var cmd = con.CreateCommand();
                cmd.CommandText = @"
UPDATE flight_seats
SET seat_price =
    CASE
        WHEN LOWER(TRIM(class_type)) LIKE '%eco%' THEN @eco
        WHEN LOWER(TRIM(class_type)) LIKE '%bus%' THEN @bus
        WHEN LOWER(TRIM(class_type)) LIKE '%first%' THEN @first
        WHEN LOWER(TRIM(class_type)) LIKE '%vip%' THEN @first
        ELSE seat_price
    END
WHERE flight_id = @fid;
";
                cmd.Parameters.AddWithValue("@eco", economy);
                cmd.Parameters.AddWithValue("@bus", business);
                cmd.Parameters.AddWithValue("@first", firstOrVip);
                cmd.Parameters.AddWithValue("@fid", flightId);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

    }

}
