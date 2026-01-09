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

                // ✅ FIX: Seat ctor is Seat(string seatNumber, string classType)
                seats.Add(new Seat(seatNumber, classType));
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

                //  Ensure the plane remembers its DB type + DB model label
                p.Type = r.Type;
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
                return true;
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

                using (var del = new MySqlCommand("DELETE FROM seats WHERE plane_id = @pid", conn, tx))
                {
                    del.Parameters.AddWithValue("@pid", planeId);
                    del.ExecuteNonQuery();
                }

                string sql = @"INSERT INTO seats (plane_id, seat_number, class_type)
                               VALUES (@pid, @sn, @ct)";

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
        public List<Seat> GetSeatsByPlaneId(int planeId)
        {
            using var conn = new MySqlConnection(_connStr);
            conn.Open();

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
                seats.Add(new Seat(seatNumber, classType));
            }

            return seats;
        }

        public bool DeletePlane(int planeId, out string error)
        {
            error = "";
            try
            {
                using var conn = new MySqlConnection(_connStr);
                conn.Open();

                using var tx = conn.BeginTransaction();

                // delete seats first (FK-safe)
                using (var cmd1 = new MySqlCommand("DELETE FROM seats WHERE plane_id = @id", conn, tx))
                {
                    cmd1.Parameters.AddWithValue("@id", planeId);
                    cmd1.ExecuteNonQuery();
                }

                // delete plane
                using (var cmd2 = new MySqlCommand("DELETE FROM planes WHERE id = @id", conn, tx))
                {
                    cmd2.Parameters.AddWithValue("@id", planeId);
                    int affected = cmd2.ExecuteNonQuery();
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
        public int CountPlanesNotAssignedToAnyFlight()
        {
            const string sql = @"
        SELECT COUNT(*)
        FROM planes p
        LEFT JOIN flights f ON f.plane_id = p.id
        WHERE f.id IS NULL;
    ";

            using var con = new MySqlConnection(_connStr);
            con.Open();
            using var cmd = new MySqlCommand(sql, con);

            var obj = cmd.ExecuteScalar();
            if (obj == null || obj == DBNull.Value) return 0;
            return Convert.ToInt32(obj);
        }

    }
}
