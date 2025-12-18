using Airport_Airplane_management_system.Model.Core.Classes.Planes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Airport_Airplane_management_system.Model.Repositories
{
    public class MySqlPlaneRepository : IPlaneRepository
    {
        private readonly string _connStr;

        public MySqlPlaneRepository(string connStr)
        {
            _connStr = connStr;
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
    }
}
