using Airport_Airplane_management_system.Model.Core.Classes; // Crew, Flight live here in your project
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;

namespace Ticket_Booking_System_OOP.Model.Repositories
{
    public class MySqlCrewRepository : ICrewRepository
    {
        private readonly string _connStr;

        public MySqlCrewRepository(string connStr)
        {
            if (string.IsNullOrWhiteSpace(connStr))
                throw new ArgumentException("Connection string is empty.", nameof(connStr));

            _connStr = connStr;
        }

        public List<Crew> GetAll()
        {
            var list = new List<Crew>();

            const string sql = @"
SELECT flight_id, employee_id, full_name, role, status, email, phone
FROM crew_members
ORDER BY employee_id;";

            using (var conn = new MySqlConnection(_connStr))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (var r = cmd.ExecuteReader())
                {
                    while (r.Read())
                    {
                        var crew = new Crew(
                            fullName: r.GetString("full_name"),
                            role: r.GetString("role"),
                            status: r.GetString("status"),
                            employeeId: r.GetString("employee_id"),
                            email: r["email"] == DBNull.Value ? "" : r.GetString("email"),
                            phone: r["phone"] == DBNull.Value ? "" : r.GetString("phone")
                        );

                        crew.FlightId = (r["flight_id"] == DBNull.Value) ? (int?)null : Convert.ToInt32(r["flight_id"]);
                        list.Add(crew);
                    }
                }
            }

            return list;
        }

        public bool Insert(Crew crew, out string err)
        {
            err = "";

            if (crew == null) { err = "Crew is null."; return false; }
            if (string.IsNullOrWhiteSpace(crew.EmployeeId)) { err = "EmployeeId is required."; return false; }
            if (string.IsNullOrWhiteSpace(crew.FullName)) { err = "FullName is required."; return false; }
            if (string.IsNullOrWhiteSpace(crew.Role)) { err = "Role is required."; return false; }
            if (string.IsNullOrWhiteSpace(crew.Status)) { err = "Status is required."; return false; }

            const string sql = @"
INSERT INTO crew_members
(flight_id, employee_id, full_name, role, status, email, phone)
VALUES
(@flight_id, @employee_id, @full_name, @role, @status, @email, @phone);";

            try
            {
                using (var conn = new MySqlConnection(_connStr))
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@employee_id", crew.EmployeeId);
                    cmd.Parameters.AddWithValue("@full_name", crew.FullName);
                    cmd.Parameters.AddWithValue("@role", crew.Role);
                    cmd.Parameters.AddWithValue("@status", crew.Status); // "active"/"inactive"
                    cmd.Parameters.AddWithValue("@email", (object)(crew.Email ?? ""));
                    cmd.Parameters.AddWithValue("@phone", (object)(crew.Phone ?? ""));

                    if (crew.FlightId == null || crew.FlightId <= 0)
                        cmd.Parameters.AddWithValue("@flight_id", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@flight_id", crew.FlightId.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (MySqlException ex)
            {
                err = $"MySQL Error {ex.Number}: {ex.Message}";
                return false;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        public bool Update(Crew crew, out string err)
        {
            err = "";

            if (crew == null) { err = "Crew is null."; return false; }
            if (string.IsNullOrWhiteSpace(crew.EmployeeId)) { err = "EmployeeId is required."; return false; }

            const string sql = @"
UPDATE crew_members
SET flight_id = @flight_id,
    full_name = @full_name,
    role = @role,
    status = @status,
    email = @email,
    phone = @phone
WHERE employee_id = @employee_id;";

            try
            {
                using (var conn = new MySqlConnection(_connStr))
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@employee_id", crew.EmployeeId);
                    cmd.Parameters.AddWithValue("@full_name", crew.FullName ?? "");
                    cmd.Parameters.AddWithValue("@role", crew.Role ?? "");
                    cmd.Parameters.AddWithValue("@status", crew.Status ?? "");
                    cmd.Parameters.AddWithValue("@email", (object)(crew.Email ?? ""));
                    cmd.Parameters.AddWithValue("@phone", (object)(crew.Phone ?? ""));

                    if (crew.FlightId == null || crew.FlightId <= 0)
                        cmd.Parameters.AddWithValue("@flight_id", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@flight_id", crew.FlightId.Value);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows == 0)
                    {
                        err = "No crew member found with this Employee ID.";
                        return false;
                    }

                    return true;
                }
            }
            catch (MySqlException ex)
            {
                err = $"MySQL Error {ex.Number}: {ex.Message}";
                return false;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        public bool DeleteByEmployeeId(string employeeId, out string err)
        {
            err = "";

            if (string.IsNullOrWhiteSpace(employeeId))
            {
                err = "Employee ID is empty.";
                return false;
            }

            const string sql = @"DELETE FROM crew_members WHERE employee_id = @employee_id;";

            try
            {
                using (var conn = new MySqlConnection(_connStr))
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@employee_id", employeeId);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows == 0)
                    {
                        err = "No crew member found with this Employee ID.";
                        return false;
                    }

                    return true;
                }
            }
            catch (MySqlException ex)
            {
                err = $"MySQL Error {ex.Number}: {ex.Message}";
                return false;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }

        public string GenerateNextEmployeeId()
        {
            const string sql = @"
SELECT MAX(CAST(SUBSTRING(employee_id, 4) AS UNSIGNED))
FROM crew_members
WHERE employee_id LIKE 'EMP%';";

            using (var conn = new MySqlConnection(_connStr))
            using (var cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                object result = cmd.ExecuteScalar();

                int maxNum = 0;
                if (result != DBNull.Value && result != null)
                    maxNum = Convert.ToInt32(result);

                int next = maxNum + 1;
                return $"EMP{next:000}";
            }
        }
    }
}
