using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airport_Airplane_management_system.Model.Repositories
{
    public class MySqlPlaneRepository : IPlaneRepository
    {
        private readonly string _connStr;

        public MySqlPlaneRepository(string connStr)
        {
            _connStr = connStr;
        }

        // -----------------------------
        // Helpers
        // -----------------------------
        private static Plane BuildPlaneFromType(int id, string type, string status)
        {
            return type switch
            {
                "HighLevel" => new HighLevel(id, status),
                "A320" => new MidRangeA320(id, status),
                "PrivateJet" => new PrivateJet(id, status),
                _ => new MidRangeA320(id, status)
            };
        }

        private List<Seat> LoadSeatsForPlane(int planeId, MySqlConnection conn)
        {
            var seats = new List<Seat>();

            using var cmd = new MySqlCommand(
                "SELECT seat_number, class_type FROM seats WHERE plane_id = @pid ORDER BY id",
                conn);
            cmd.Parameters.AddWithValue("@pid", planeId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string seatNumber = reader.GetString("seat_number");
                string classType = reader.GetString("class_type");

                // Seat ctor in your project: Seat(string classType, string seatNumber)
                seats.Add(new Seat(classType, seatNumber));
            }

            return seats;
        }

        // Backward-compat alias
        public List<Plane> GetAllPlanesf()
        {
            return GetAllPlanes();
        }

        public List<Plane> GetAllPlanes()
        {
            var planes = new List<Plane>();

            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            // 1) Read planes first (no nested readers)
            var rows = new List<(int Id, string Model, string Type, string Status)>();
            using (var cmd = new MySqlCommand("SELECT id, model, type, status FROM planes ORDER BY id", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    string model = reader.GetString("model");
                    string type = reader.GetString("type");
                    string status = reader.GetString("status");
                    rows.Add((id, model, type, status));
                }
            }

            // 2) Load seats for each plane
            foreach (var r in rows)
            {
                Plane p = BuildPlaneFromType(r.Id, r.Type, r.Status);
                p.Model = r.Model;

                var seats = LoadSeatsForPlane(r.Id, conn);
                if (seats.Count > 0)
                    p.Seats = seats;
                else
                    p.GenerateSeats(); // fallback for legacy DBs

                planes.Add(p);
            }

            if (planes.Count == 0)
            {
                // UI fallback (optional)
                Plane dummy = new MidRangeA320(-1, "Available");
                dummy.Model = "Demo";
                dummy.GenerateSeats();
                planes.Add(dummy);
            }

            return planes;
        }

        public bool SetPlaneStatus(int planeId, string status, out string error)
        {
            error = "";
            string sql = @"UPDATE planes SET status = @s WHERE id = @id";

            try
            {
                using var conn = new MySqlConnection(_connStr);
                conn.Open();

                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@s", status);
                cmd.Parameters.AddWithValue("@id", planeId);

                return cmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool PlaneHasTimeConflict(int planeId, DateTime dep, DateTime arr, int? excludeFlightId, out string error)
        {
            error = "";
            try
            {
                using var conn = new MySqlConnection(_connStr);
                conn.Open();

                // NOTE: adjust table/column names if yours differ (flights table structure)
                string sql = @"
                    SELECT COUNT(*)
                    FROM flights
                    WHERE plane_id = @pid
                      AND NOT (arrival <= @dep OR departure >= @arr)
                ";

                if (excludeFlightId.HasValue)
                    sql += " AND id <> @fid";

                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@pid", planeId);
                cmd.Parameters.AddWithValue("@dep", dep);
                cmd.Parameters.AddWithValue("@arr", arr);
                if (excludeFlightId.HasValue)
                    cmd.Parameters.AddWithValue("@fid", excludeFlightId.Value);

                long count = Convert.ToInt64(cmd.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return true; // safer to block if DB error
            }
        }

        public int AddPlane(string model, string type, string status, out string error)
        {
            error = "";
            try
            {
                using var conn = new MySqlConnection(_connStr);
                conn.Open();

                using var cmd = new MySqlCommand(
                    "INSERT INTO planes (model, type, status) VALUES (@m, @t, @s); SELECT LAST_INSERT_ID();",
                    conn);

                cmd.Parameters.AddWithValue("@m", model);
                cmd.Parameters.AddWithValue("@t", type);
                cmd.Parameters.AddWithValue("@s", status);

                object? result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return 0;
            }
        }

        public bool InsertSeats(int planeId, List<Seat> seats, out string error)
        {
            error = "";
            try
            {
                using var conn = new MySqlConnection(_connStr);
                conn.Open();

                using var tx = conn.BeginTransaction();

                // Clear existing seats (optional but prevents duplicates if re-adding)
                using (var del = new MySqlCommand("DELETE FROM seats WHERE plane_id = @pid", conn, tx))
                {
                    del.Parameters.AddWithValue("@pid", planeId);
                    del.ExecuteNonQuery();
                }

                string sql = @"INSERT INTO seats (plane_id, seat_number, class_type, is_booked, user_id)
                               VALUES (@pid, @sn, @ct, 0, NULL)";

                foreach (var s in seats)
                {
                    using var cmd = new MySqlCommand(sql, conn, tx);
                    cmd.Parameters.AddWithValue("@pid", planeId);
                    cmd.Parameters.AddWithValue("@sn", s.SeatNumber);
                    cmd.Parameters.AddWithValue("@ct", s.ClassType);
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
    }
}
