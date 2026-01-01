using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Airport_Airplane_management_system.Repositories
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly string _connectionString;

        public PassengerRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public List<PassengerSummaryRow> GetPassengersSummary()
        {
            // Uses your real flights columns: departure / arrival
            const string sql = @"
SELECT
  p.passenger_id,
  p.full_name,
  p.email,
  p.phone,
  p.created_at,

  SUM(CASE WHEN f.departure >= NOW() AND LOWER(b.status) <> 'cancelled' THEN 1 ELSE 0 END) AS upcoming_count,
  SUM(CASE WHEN f.departure <  NOW() AND LOWER(b.status) <> 'cancelled' THEN 1 ELSE 0 END) AS past_count,
  SUM(CASE WHEN LOWER(b.status) <> 'cancelled' THEN 1 ELSE 0 END) AS total_count

FROM passengers p
LEFT JOIN bookings b ON b.passenger_id = p.passenger_id
LEFT JOIN flights  f ON f.id = b.flight_id

GROUP BY p.passenger_id, p.full_name, p.email, p.phone, p.created_at
ORDER BY p.full_name;";

            var list = new List<PassengerSummaryRow>();

            using var con = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand(sql, con);
            con.Open();

            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new PassengerSummaryRow
                {
                    PassengerId = Convert.ToInt32(r["passenger_id"]),
                    FullName = r["full_name"]?.ToString() ?? "",
                    Email = r["email"] == DBNull.Value ? null : r["email"].ToString(),
                    Phone = r["phone"] == DBNull.Value ? null : r["phone"].ToString(),
                    CreatedAt = r["created_at"] == DBNull.Value ? null : (DateTime?)Convert.ToDateTime(r["created_at"]),
                    UpcomingCount = Convert.ToInt32(r["upcoming_count"]),
                    PastCount = Convert.ToInt32(r["past_count"]),
                    TotalCount = Convert.ToInt32(r["total_count"])
                });
            }

            return list;
        }

        public List<PassengerBookingRow> GetBookingsForPassenger(int passengerId)
        {
            const string sql = @"
SELECT
  b.booking_id,
  b.flight_id,
  b.flight_seat_id,
  b.category,
  b.status,
  fs.seat_number,
  f.from_city,
  f.to_city,
  f.departure,
  f.arrival
FROM bookings b
JOIN flights f       ON f.id = b.flight_id
JOIN flight_seats fs ON fs.id = b.flight_seat_id
WHERE b.passenger_id = @pid
ORDER BY f.departure DESC;";

            var list = new List<PassengerBookingRow>();

            using var con = new MySqlConnection(_connectionString);
            using var cmd = new MySqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@pid", passengerId);

            con.Open();
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new PassengerBookingRow
                {
                    BookingId = Convert.ToInt32(r["booking_id"]),
                    FlightId = Convert.ToInt32(r["flight_id"]),
                    FlightSeatId = Convert.ToInt32(r["flight_seat_id"]),
                    Category = r["category"]?.ToString() ?? "",
                    Status = r["status"]?.ToString() ?? "",
                    SeatNumber = r["seat_number"]?.ToString() ?? "",
                    FromCity = r["from_city"]?.ToString() ?? "",
                    ToCity = r["to_city"]?.ToString() ?? "",
                    Departure = Convert.ToDateTime(r["departure"]),
                    Arrival = Convert.ToDateTime(r["arrival"]),
                });
            }

            return list;
        }

        public bool CancelBooking(int bookingId, out string error)
        {
            error = "";

            using var con = new MySqlConnection(_connectionString);
            con.Open();
            using var tx = con.BeginTransaction();

            // ✅ keep these outside so we can use them after commit
            int userId = 0;
            int? passengerId = null;

            try
            {
                int seatId;
                string status;

                // ✅ modified: also read user_id and passenger_id
                const string readSql = @"
SELECT flight_seat_id, status, user_id, passenger_id
FROM bookings
WHERE booking_id=@bid
FOR UPDATE;";

                using (var cmd = new MySqlCommand(readSql, con, tx))
                {
                    cmd.Parameters.AddWithValue("@bid", bookingId);
                    using var r = cmd.ExecuteReader();

                    if (!r.Read())
                    {
                        error = "Booking not found.";
                        tx.Rollback();
                        return false;
                    }

                    seatId = Convert.ToInt32(r["flight_seat_id"]);
                    status = r["status"]?.ToString() ?? "Pending";

                    // ✅ capture who to notify
                    userId = Convert.ToInt32(r["user_id"]);
                    passengerId = (r["passenger_id"] == DBNull.Value) ? (int?)null : Convert.ToInt32(r["passenger_id"]);
                }

                if (string.Equals(status, "Cancelled", StringComparison.OrdinalIgnoreCase))
                {
                    tx.Commit();
                    return true;
                }

                const string cancelSql = @"UPDATE bookings SET status='Cancelled' WHERE booking_id=@bid;";
                using (var cmd = new MySqlCommand(cancelSql, con, tx))
                {
                    cmd.Parameters.AddWithValue("@bid", bookingId);
                    cmd.ExecuteNonQuery();
                }

                const string freeSeatSql = @"UPDATE flight_seats SET is_booked=0, passenger_id=NULL WHERE id=@sid;";
                using (var cmd = new MySqlCommand(freeSeatSql, con, tx))
                {
                    cmd.Parameters.AddWithValue("@sid", seatId);
                    cmd.ExecuteNonQuery();
                }

                tx.Commit();

                // ✅ notification AFTER commit (so booking is definitely cancelled)
                try
                {
                    var notifRepo = new MySqlNotificationWriterRepository(_connectionString);
                    var notifService = new NotificationWriterService(notifRepo);

                    // you can keep your own wording, but include passengerId
                    notifRepo.AddUserNotification(
                        userId,
                        type: "BookingCancelled",
                        title: "❌ Booking cancelled by admin",
                        message: passengerId.HasValue
                            ? $"Your booking #{bookingId} was cancelled by the admin for passenger #{passengerId.Value}."
                            : $"Your booking #{bookingId} was cancelled by the admin.",
                        bookingId: bookingId
                    );
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Notification insert failed: " + ex);
                }

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
