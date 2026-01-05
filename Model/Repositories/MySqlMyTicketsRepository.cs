using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Airport_Airplane_management_system.Model.Repositories
{
    public class MySqlMyTicketsRepository : IMyTicketsRepository
    {
        private readonly string _connStr;

        public MySqlMyTicketsRepository(string connStr)
        {
            _connStr = connStr;
        }

        public List<MyTicketRow> GetTicketsForUser(int userId)
        {
            const string sql = @"
SELECT
    b.id AS booking_id,
    b.flight_id,
    b.flight_seat_id,
    b.passenger_id,
    b.status,

    f.from_city,
    f.to_city,
    f.departure,
    f.arrival,

    fs.seat_number,
    fs.class_type,
    fs.seat_price,


    p.full_name AS passenger_name

FROM bookings b
JOIN flights f ON f.id = b.flight_id
JOIN flight_seats fs ON fs.id = b.flight_seat_id
LEFT JOIN passengers p ON p.passenger_id = b.passenger_id
WHERE b.user_id = @uid
ORDER BY f.departure DESC;
";

            var list = new List<MyTicketRow>();

            using var conn = new MySqlConnection(_connStr);
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uid", userId);

            conn.Open();
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                string classType = r["class_type"]?.ToString() ?? "Economy";

                list.Add(new MyTicketRow
                {
                    BookingId = Convert.ToInt32(r["booking_id"]),
                    FlightId = Convert.ToInt32(r["flight_id"]),
                    FlightSeatId = Convert.ToInt32(r["flight_seat_id"]),

                    PassengerId = r["passenger_id"] == DBNull.Value
                        ? (int?)null
                        : Convert.ToInt32(r["passenger_id"]),

                    PassengerName = r["passenger_name"] == DBNull.Value
                        ? "(Passenger not assigned)"
                        : r["passenger_name"].ToString(),

                    FromCity = r["from_city"].ToString(),
                    ToCity = r["to_city"].ToString(),
                    Departure = Convert.ToDateTime(r["departure"]),
                    Arrival = Convert.ToDateTime(r["arrival"]),

                    SeatNumber = r["seat_number"].ToString(),
                    ClassType = classType,
                    Category = classType,
                    Status = r["status"].ToString(),
                    Price = r["seat_price"] == DBNull.Value ? 0 : Convert.ToDecimal(r["seat_price"])
                });
            }

            return list;
        }

        public bool CancelTicket(int bookingId, int userId, out string error)
        {
            error = "";

            using var conn = new MySqlConnection(_connStr);
            conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                int seatId;

                const string readSql = @"
SELECT flight_seat_id
FROM bookings
WHERE id=@bid AND user_id=@uid
FOR UPDATE;";

                using (var cmd = new MySqlCommand(readSql, conn, tx))
                {
                    cmd.Parameters.AddWithValue("@bid", bookingId);
                    cmd.Parameters.AddWithValue("@uid", userId);

                    object result = cmd.ExecuteScalar();
                    if (result == null)
                    {
                        tx.Rollback();
                        error = "Ticket not found.";
                        return false;
                    }

                    seatId = Convert.ToInt32(result);
                }

                const string cancelBooking = @"UPDATE bookings SET status='Cancelled' WHERE id=@bid;";
                using (var cmd = new MySqlCommand(cancelBooking, conn, tx))
                {
                    cmd.Parameters.AddWithValue("@bid", bookingId);
                    cmd.ExecuteNonQuery();
                }

                const string freeSeat = @"UPDATE flight_seats SET is_booked=0, passenger_id=NULL WHERE id=@sid;";
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
                try { tx.Rollback(); } catch { }
                error = ex.Message;
                return false;
            }
        }

    }
}
