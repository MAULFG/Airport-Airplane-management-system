using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using MySql.Data.MySqlClient;
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
            out int bookingId,
            out string error)
        {
            bookingId = 0;
            error = "";

            const string lockSeat = @"
SELECT id FROM flight_seats
WHERE id=@sid AND flight_id=@fid AND is_booked=0
FOR UPDATE;";

            const string insertBooking = @"
INSERT INTO bookings (user_id, flight_id, flight_seat_id, category, status)
VALUES (@uid, @fid, @sid, @cat, 'Confirmed');";

            const string bookSeat = @"
UPDATE flight_seats SET is_booked=1, user_id=@uid WHERE id=@sid;";

            try
            {
                using var conn = new MySqlConnection(_connStr);
                conn.Open();
                using var tx = conn.BeginTransaction();

                using (var cmd = new MySqlCommand(lockSeat, conn, tx))
                {
                    cmd.Parameters.AddWithValue("@sid", flightSeatId);
                    cmd.Parameters.AddWithValue("@fid", flightId);

                    if (cmd.ExecuteScalar() == null)
                    {
                        tx.Rollback();
                        error = "Seat not available.";
                        return false;
                    }
                }

                using (var cmd = new MySqlCommand(insertBooking, conn, tx))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@fid", flightId);
                    cmd.Parameters.AddWithValue("@sid", flightSeatId);
                    cmd.Parameters.AddWithValue("@cat", category);
                    cmd.ExecuteNonQuery();
                    bookingId = (int)cmd.LastInsertedId;
                }

                using (var cmd = new MySqlCommand(bookSeat, conn, tx))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@sid", flightSeatId);
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

        public bool CancelBooking(int bookingId, out string error)
        {
            error = "";

            const string getSeat = @"
SELECT flight_seat_id FROM bookings WHERE booking_id=@bid FOR UPDATE;";

            const string cancelBooking = @"
UPDATE bookings SET status='Cancelled' WHERE booking_id=@bid;";

            const string freeSeat = @"
UPDATE flight_seats SET is_booked=0, user_id=NULL WHERE id=@sid;";

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
SELECT booking_id
FROM bookings
WHERE flight_id = @fid AND status <> 'Cancelled';";

            using var conn = new MySqlConnection(_connStr);
            conn.Open();
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fid", flightId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
                list.Add(reader.GetInt32("booking_id"));

            return list;
        }

    }
}
