using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;

namespace Airport_Airplane_management_system.Model.Repositories
{
    public class MySqlBookingRepository : IBookingRepository
    {
        private readonly string _connStr;

        public MySqlBookingRepository(string connStr)
        {
            _connStr = connStr;
        }

        public bool CreateBooking(
    int userId,
    int flightId,
    int flightSeatId,
    string category,
    int passengerId,
    decimal seatPrice,   // new parameter for seat price
    out int bookingId,
    out string error)
        {
            bookingId = 0;
            error = "";

            const string lockSeat = @"
SELECT id, passenger_id FROM flight_seats
WHERE id=@sid AND flight_id=@fid AND is_booked=0
FOR UPDATE;";

            const string insertBooking = @"
INSERT INTO bookings (user_id, flight_id, seat_id, flight_seat_id, passenger_id, status, seat_price)
VALUES (@uid, @fid, @sid, @sid, @pid, 'Confirmed', @price);";

            const string bookSeat = @"
UPDATE flight_seats SET is_booked=1, passenger_id=@pid WHERE id=@sid;";

            try
            {
                using var conn = new MySqlConnection(_connStr);
                conn.Open();
                using var tx = conn.BeginTransaction();

                // 1️⃣ Lock seat
                using (var cmd = new MySqlCommand(lockSeat, conn, tx))
                {
                    cmd.Parameters.AddWithValue("@sid", flightSeatId);
                    cmd.Parameters.AddWithValue("@fid", flightId);

                    var result = cmd.ExecuteScalar();
                    if (result == null)
                    {
                        tx.Rollback();
                        error = "Seat is already booked.";
                        return false;
                    }
                }

                // 2️⃣ Insert booking with seat price
                using (var cmd = new MySqlCommand(insertBooking, conn, tx))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@fid", flightId);
                    cmd.Parameters.AddWithValue("@sid", flightSeatId);
                    cmd.Parameters.AddWithValue("@pid", passengerId);
                    cmd.Parameters.AddWithValue("@price", seatPrice); // ✅ correct

                    cmd.ExecuteNonQuery();
                    bookingId = (int)cmd.LastInsertedId;
                }

                // 3️⃣ Mark seat as booked
                using (var cmd = new MySqlCommand(bookSeat, conn, tx))
                {
                    cmd.Parameters.AddWithValue("@sid", flightSeatId);
                    cmd.Parameters.AddWithValue("@pid", passengerId);
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


        public List<Booking> GetBookingsForUser(User user)
        {
            var list = new List<Booking>();

            const string sql = @"
SELECT 
    b.id AS id,    -- ← change this if the column is 'id'
    b.status,
    f.id AS flight_id,
    f.from_city,
    f.to_city,
    f.departure,
    f.arrival,
    fs.id AS seat_id,
    fs.seat_number,
    fs.class_type
FROM bookings b
JOIN flights f ON f.id = b.flight_id
JOIN flight_seats fs ON fs.id = b.flight_seat_id
WHERE b.user_id = @uid;
";


            using var conn = new MySqlConnection(_connStr);
            conn.Open();
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uid", user.UserID);

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var flight = new Flight(
                    r.GetInt32("flight_id"),
                    null,
                    r.GetString("from_city"),
                    r.GetString("to_city"),
                    r.GetDateTime("departure"),
                    r.GetDateTime("arrival"),
                    new Dictionary<string, decimal>()
                );

                var seat = new FlightSeats(
                    r.GetInt32("seat_id"),
                    flight.FlightID,
                    0,
                    r.GetString("seat_number"),
                    r.GetString("class_type"),
                    true,
                    user.UserID
                );

                var booking = new Booking(
                    r.GetInt32("id"),
                    user,          // ✅ injected properly
                    flight,
                    seat,
                    seat.ClassType,
                    r.GetString("status")
                );

                list.Add(booking);
            }

            return list;
        }

        public bool CancelBooking(int bookingId, out string error)
        {
            error = "";

            const string getSeat = @"
SELECT flight_seat_id FROM bookings WHERE id=@bid FOR UPDATE;";

            const string cancelBooking = @"
UPDATE bookings SET status='Cancelled' WHERE id=@bid;";

            const string freeSeat = @"
UPDATE flight_seats SET is_booked=0, passenger_id=NULL WHERE id=@sid;";


            try
            {
                using var conn = new MySqlConnection(_connStr);
                conn.Open();
                using var tx = conn.BeginTransaction();

                int seatId;
                using (var cmd = new MySqlCommand(getSeat, conn, tx))
                {
                    cmd.Parameters.AddWithValue("@bid", bookingId);
                    var result = cmd.ExecuteScalar();
                    if (result == null)
                    {
                        tx.Rollback();
                        error = "Booking not found.";
                        return false;
                    }
                    seatId = Convert.ToInt32(result);
                }

                using (var cmd = new MySqlCommand(cancelBooking, conn, tx))
                {
                    cmd.Parameters.AddWithValue("@bid", bookingId);
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new MySqlCommand(freeSeat, conn, tx))
                {
                    cmd.Parameters.AddWithValue("@sid", seatId);
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
        public List<int> GetActiveBookingIdsForFlight(int flightId)
        {
            var list = new List<int>();

            const string sql = @"
SELECT id
FROM bookings
WHERE flight_id = @fid AND status <> 'Cancelled';";

            using var conn = new MySqlConnection(_connStr);
            conn.Open();
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fid", flightId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(reader.GetInt32("id"));

            return list;
        }

    }
}
