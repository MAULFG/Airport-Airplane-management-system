using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;

namespace Airport_Airplane_management_system.Model.Repositories
{
    public partial class MySqlCrewRepository : ICrewRepository
    {
        private readonly string _connStr;

        public MySqlCrewRepository(string connStr)
        {
            _connStr = connStr;
        }

        // =========================================================
        // REPORTS: lists (for per-person cards)
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

        // (optional) keep your count methods too if used elsewhere
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
                // ✅ match your Crew constructor exactly :contentReference[oaicite:1]{index=1}
                string fullName = rd["full_name"].ToString();
                string role = rd["role"].ToString();
                string status = rd["status"].ToString();
                string employeeId = rd["employee_id"].ToString();
                string email = rd["email"].ToString();
                string phone = rd["phone"].ToString();

                var crew = new Crew(fullName, role, status, employeeId, email, phone);

                // FlightId is nullable and has a public setter in your model :contentReference[oaicite:2]{index=2}
                object flightObj = rd["flight_id"];
                if (flightObj == DBNull.Value) crew.FlightId = null;
                else
                {
                    int fid = Convert.ToInt32(flightObj);
                    crew.FlightId = (fid <= 0) ? null : fid;
                }

                list.Add(crew);
            }

            return list;
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
