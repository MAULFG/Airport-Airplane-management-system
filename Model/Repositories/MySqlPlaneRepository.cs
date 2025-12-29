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

        public List<Plane> GetAllPlanesf()
        {
            var planes = new List<Plane>();
            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            using var cmd = new MySqlCommand("SELECT * FROM planes", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32("id");
                string type = reader.GetString("type");
                string status = reader.GetString("status");

                Plane p = type switch
                {
                    "HighLevel" => new HighLevel(id, status),
                    "A320" => new MidRangeA320(id, status),
                    "PrivateJet" => new PrivateJet(id, status),
                    _ => new MidRangeA320(id, status)
                };

                // TODO: once seats are stored in DB, load them from DB instead of generating
                p.GenerateSeats();
                planes.Add(p);
            }

            if (!planes.Any())
            {
                Plane dummy = new MidRangeA320(-1, "Available");
                dummy.GenerateSeats();
                planes.Add(dummy);
            }

            return planes;
        }

        public List<Plane> GetAllPlanes()
        {
            var planes = new List<Plane>();
            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            string sql = "SELECT * FROM planes";
            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32("id");
                string type = reader.GetString("type");
                string status = reader.GetString("status");

                Plane p = type switch
                {
                    "HighLevel" => new HighLevel(id, status),
                    "A320" => new MidRangeA320(id, status),
                    "PrivateJet" => new PrivateJet(id, status),
                    _ => new MidRangeA320(id, status)
                };

                // TODO: once seats are stored in DB, load them from DB instead of generating
                p.GenerateSeats();
                planes.Add(p);
            }

            return planes;
        }

        public bool SetPlaneStatus(int planeId, string status, out string error)
        {
            error = "";
            string sql = @"UPDATE planes SET status=@st WHERE id=@id;";
            try
            {
                using var conn = new MySqlConnection(_connStr);
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@st", status);
                cmd.Parameters.AddWithValue("@id", planeId);

                conn.Open();
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
            string sql = @"
SELECT COUNT(*)
FROM flights
WHERE plane_id = @pid
  AND (@newDep < arrival AND @newArr > departure)";
            if (excludeFlightId.HasValue)
                sql += " AND id <> @excludeId";

            try
            {
                using var conn = new MySqlConnection(_connStr);
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@pid", planeId);
                cmd.Parameters.AddWithValue("@newDep", dep);
                cmd.Parameters.AddWithValue("@newArr", arr);
                if (excludeFlightId.HasValue)
                    cmd.Parameters.AddWithValue("@excludeId", excludeFlightId.Value);

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return true;
            }
        }

        public int AddPlane(string type, string status, out string error)
        {
            error = "";
            try
            {
                using var conn = new MySqlConnection(_connStr);
                conn.Open();

                using var cmd = new MySqlCommand(
                    "INSERT INTO planes (type, status) VALUES (@t, @s); SELECT LAST_INSERT_ID();",
                    conn);

                cmd.Parameters.AddWithValue("@t", type);
                cmd.Parameters.AddWithValue("@s", status);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return -1;
            }
        }

        public bool InsertSeats(int planeId, List<Seat> seats, out string error)
        {
            error = "";

            if (seats == null || seats.Count == 0)
            {
                error = "No seats to insert.";
                return false;
            }

            try
            {
                using var conn = new MySqlConnection(_connStr);
                conn.Open();

                // Safety: avoid duplicate insert if seats already exist for this plane
                using (var checkCmd = new MySqlCommand("SELECT COUNT(*) FROM seats WHERE plane_id=@pid;", conn))
                {
                    checkCmd.Parameters.AddWithValue("@pid", planeId);
                    int existing = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (existing > 0)
                        return true; // already generated
                }

                using var tx = conn.BeginTransaction();

                string sql = @"INSERT INTO seats
(plane_id, seat_number, class_type, is_booked)
VALUES (@pid, @sn, @ct, 0)";

                using var cmd = new MySqlCommand(sql, conn, tx);

                foreach (var seat in seats)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@pid", planeId);
                    cmd.Parameters.AddWithValue("@sn", seat.SeatNumber);
                    cmd.Parameters.AddWithValue("@ct", seat.ClassType);
                    cmd.ExecuteNonQuery();
                }

                tx.Commit();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                try { /* best effort */ } catch { }
                return false;
            }
        }
    }
}
