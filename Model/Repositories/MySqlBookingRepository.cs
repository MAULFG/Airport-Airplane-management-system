using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

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

            // 1) Lock seat so no one else books it
            const string lockSeat = @"
SELECT id
FROM flight_seats
WHERE id=@sid AND flight_id=@fid AND is_booked=0
FOR UPDATE;";

            // 2) Get passenger_id for this user (required by your DB when status='Confirmed')
            //    We map by email: user.Email == passengers.email
            const string getPassengerId = @"
SELECT p.passenger_id
FROM passengers p
JOIN `user` u ON u.Email = p.email
WHERE u.ID = @uid
LIMIT 1;";

            // 3) Insert booking (include passenger_id)
            const string insertBooking = @"
INSERT INTO bookings (user_id, flight_id, flight_seat_id, category, status, created_at, passenger_id)
VALUES (@uid, @fid, @sid, @cat, 'Confirmed', NOW(), @pid);";

            // 4) Mark seat booked + assign passenger_id
            const string bookSeat = @"
UPDATE flight_seats
SET is_booked=1, passenger_id=@pid
WHERE id=@sid;";

            try
            {
                using var conn = new MySqlConnection(_connStr);
                conn.Open();
                using var tx = conn.BeginTransaction();

                // Seat lock & availability check
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

                // Get passenger id
                int passengerId;
                using (var cmd = new MySqlCommand(getPassengerId, conn, tx))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    var result = cmd.ExecuteScalar();

                    if (result == null)
                    {
                        tx.Rollback();
                        error = "Cannot create booking: no passenger found for this user (email mapping user.Email -> passengers.email).";
                        return false;
                    }

                    passengerId = Convert.ToInt32(result);
                }

                // Insert booking
                using (var cmd = new MySqlCommand(insertBooking, conn, tx))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@fid", flightId);
                    cmd.Parameters.AddWithValue("@sid", flightSeatId);
                    cmd.Parameters.AddWithValue("@cat", category);
                    cmd.Parameters.AddWithValue("@pid", passengerId);

                    cmd.ExecuteNonQuery();
                    bookingId = (int)cmd.LastInsertedId;
                }

                // Mark seat booked
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

        public bool CancelBooking(int bookingId, out string error)
        {
            error = "";

            // bookings primary key is booking_id in your DB
            const string getSeat = @"
SELECT flight_seat_id
FROM bookings
WHERE booking_id=@bid
FOR UPDATE;";

            const string cancelBooking = @"
UPDATE bookings
SET status='Cancelled'
WHERE booking_id=@bid;";

            // IMPORTANT: flight_seats uses passenger_id not user_id
            const string freeSeat = @"
UPDATE flight_seats
SET is_booked=0, passenger_id=NULL
WHERE id=@sid;";

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

        public List<(int bookingId, int userId)> GetActiveBookingsForFlight(int flightId)
        {
            var list = new List<(int bookingId, int userId)>();

            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            // booking_id exists in your DB
            const string sql = @"
SELECT booking_id, user_id
FROM bookings
WHERE flight_id = @fid
  AND (status IS NULL OR status <> 'Cancelled');";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fid", flightId);

            using var r = cmd.ExecuteReader();
            while (r.Read())
                list.Add((Convert.ToInt32(r["booking_id"]), Convert.ToInt32(r["user_id"])));

            return list;
        }

        public List<int> GetActiveBookingIdsForFlight(int flightId)
        {
            var ids = new List<int>();

            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            const string sql = @"
SELECT booking_id
FROM bookings
WHERE flight_id = @fid
  AND (status IS NULL OR status <> 'Cancelled');";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fid", flightId);

            using var r = cmd.ExecuteReader();
            while (r.Read())
                ids.Add(Convert.ToInt32(r["booking_id"]));

            return ids;
        }
    }
}
