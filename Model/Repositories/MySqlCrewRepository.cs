using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;

namespace Airport_Airplane_management_system.Model.Repositories
{
    public class MySqlCrewRepository : ICrewRepository
    {
        private readonly string _connStr;

        public MySqlCrewRepository(string connStr)
        {
            _connStr = connStr;
        }

        // =========================================================
        // ICrewRepository - REQUIRED MEMBERS (fix your CS0535 errors)
        // =========================================================

        public List<Crew> GetAll()
        {
            const string sql = @"
                SELECT full_name, role, status, employee_id, email, phone, flight_id
                FROM crew_members;
            ";
            return QueryCrew(sql);
        }

        public string GenerateNextEmployeeId()
        {
            // Gets max employee_id and increments trailing digits.
            // Works for: EMP001, C-12, 12, etc.
            const string sql = @"
                SELECT employee_id
                FROM crew_members
                WHERE employee_id IS NOT NULL AND employee_id <> ''
                ORDER BY employee_id DESC
                LIMIT 1;
            ";

            using var con = new MySqlConnection(_connStr);
            con.Open();

            using var cmd = new MySqlCommand(sql, con);
            var lastObj = cmd.ExecuteScalar();
            var last = lastObj?.ToString() ?? "";

            if (string.IsNullOrWhiteSpace(last))
                return "EMP001";

            // Split prefix + trailing digits
            var m = Regex.Match(last, @"^(.*?)(\d+)$");
            if (!m.Success)
                return last + "001";

            var prefix = m.Groups[1].Value;
            var digits = m.Groups[2].Value;

            if (!int.TryParse(digits, out var n))
                return prefix + digits + "1";

            var next = (n + 1).ToString(new string('0', digits.Length));
            return prefix + next;
        }

        public bool Insert(Crew crew, out string error)
        {
            error = "";
            try
            {
                const string sql = @"
                    INSERT INTO crew_members (full_name, role, status, employee_id, email, phone, flight_id)
                    VALUES (@full_name, @role, @status, @employee_id, @email, @phone, @flight_id);
                ";

                using var con = new MySqlConnection(_connStr);
                con.Open();

                using var cmd = new MySqlCommand(sql, con);
                FillCrewParams(cmd, crew);

                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool Update(Crew crew, out string error)
        {
            error = "";
            try
            {
                const string sql = @"
                    UPDATE crew_members
                    SET full_name = @full_name,
                        role = @role,
                        status = @status,
                        email = @email,
                        phone = @phone,
                        flight_id = @flight_id
                    WHERE employee_id = @employee_id;
                ";

                using var con = new MySqlConnection(_connStr);
                con.Open();

                using var cmd = new MySqlCommand(sql, con);
                FillCrewParams(cmd, crew);

                var rows = cmd.ExecuteNonQuery();
                if (rows <= 0)
                {
                    error = "No crew row updated (employee_id not found).";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool DeleteByEmployeeId(string employeeId, out string error)
        {
            error = "";
            try
            {
                const string sql = @"DELETE FROM crew_members WHERE employee_id = @employee_id;";

                using var con = new MySqlConnection(_connStr);
                con.Open();

                using var cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@employee_id", employeeId);

                var rows = cmd.ExecuteNonQuery();
                if (rows <= 0)
                {
                    error = "No crew row deleted (employee_id not found).";
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        // =========================================================
        // REPORTS - PER PERSON LISTS
        // =========================================================

        public List<Crew> GetCrewNotAssignedToAnyFlight()
        {
            const string sql = @"
                SELECT full_name, role, status, employee_id, email, phone, flight_id
                FROM crew_members
                WHERE flight_id IS NULL OR flight_id = 0;
            ";
            return QueryCrew(sql);
        }

        public List<Crew> GetCrewAssignedToPastFlights()
        {
            const string sql = @"
                SELECT cm.full_name, cm.role, cm.status, cm.employee_id, cm.email, cm.phone, cm.flight_id
                FROM crew_members cm
                INNER JOIN flights f ON f.id = cm.flight_id
                WHERE cm.flight_id IS NOT NULL
                  AND cm.flight_id <> 0
                  AND f.arrival < NOW();
            ";
            return QueryCrew(sql);
        }

        // (Optional) counts if your ReportsService still calls them
        public int CountCrewNotAssignedToAnyFlight()
        {
            const string sql = @"SELECT COUNT(*) FROM crew_members WHERE flight_id IS NULL OR flight_id = 0;";
            return ExecuteScalarInt(sql);
        }

        public int CountCrewAssignedToPastFlightsOnly()
        {
            const string sql = @"
                SELECT COUNT(*)
                FROM crew_members cm
                INNER JOIN flights f ON f.id = cm.flight_id
                WHERE cm.flight_id IS NOT NULL
                  AND cm.flight_id <> 0
                  AND f.arrival < NOW();
            ";
            return ExecuteScalarInt(sql);
        }

        // =========================================================
        // Helpers
        // =========================================================

        private List<Crew> QueryCrew(string sql)
        {
            var list = new List<Crew>();

            using var con = new MySqlConnection(_connStr);
            con.Open();

            using var cmd = new MySqlCommand(sql, con);
            using var rd = cmd.ExecuteReader();

            while (rd.Read())
            {
                // Crew model in your project requires constructor:
                // Crew(string fullName, string role, string status, string employeeId, string email, string phone)
                var fullName = rd["full_name"].ToString();
                var role = rd["role"].ToString();
                var status = rd["status"].ToString();
                var employeeId = rd["employee_id"].ToString();
                var email = rd["email"].ToString();
                var phone = rd["phone"].ToString();

                var crew = new Crew(fullName, role, status, employeeId, email, phone);

                // FlightId is nullable in your model (and has public set)
                object flightObj = rd["flight_id"];
                if (flightObj == DBNull.Value) crew.FlightId = null;
                else
                {
                    var fid = Convert.ToInt32(flightObj);
                    crew.FlightId = fid <= 0 ? null : fid;
                }

                list.Add(crew);
            }

            return list;
        }

        private void FillCrewParams(MySqlCommand cmd, Crew crew)
        {
            cmd.Parameters.AddWithValue("@full_name", crew.FullName);
            cmd.Parameters.AddWithValue("@role", crew.Role);
            cmd.Parameters.AddWithValue("@status", crew.Status);
            cmd.Parameters.AddWithValue("@employee_id", crew.EmployeeId);
            cmd.Parameters.AddWithValue("@email", crew.Email);
            cmd.Parameters.AddWithValue("@phone", crew.Phone);

            if (crew.FlightId.HasValue && crew.FlightId.Value > 0)
                cmd.Parameters.AddWithValue("@flight_id", crew.FlightId.Value);
            else
                cmd.Parameters.AddWithValue("@flight_id", DBNull.Value);
        }

        private int ExecuteScalarInt(string sql)
        {
            using var con = new MySqlConnection(_connStr);
            con.Open();

            using var cmd = new MySqlCommand(sql, con);
            var obj = cmd.ExecuteScalar();
            if (obj == null || obj == DBNull.Value) return 0;

            return Convert.ToInt32(obj);
        }
    }
}
