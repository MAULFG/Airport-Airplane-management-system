using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using MySql.Data.MySqlClient;
using System;

namespace Airport_Airplane_management_system.Model.Repositories
{
    public class MySqlUserSettingsRepository : IUserSettingsRepository
    {
        private readonly string _connStr;

        public MySqlUserSettingsRepository(string connStr)
        {
            _connStr = connStr;
        }

        public (string Username, string Email, DateTime CreatedAt, DateTime? LastLoginAt)? GetUserHeader(int userId)
        {
            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            const string q = @"
SELECT username, email, created_at, last_login_at
FROM `user`
WHERE id=@id
LIMIT 1;";

            using var cmd = new MySqlCommand(q, conn);
            cmd.Parameters.AddWithValue("@id", userId);

            using var r = cmd.ExecuteReader();
            if (!r.Read()) return null;

            string username = r.GetString("username");
            string email = r.GetString("email");
            DateTime createdAt = r["created_at"] == DBNull.Value ? DateTime.MinValue : r.GetDateTime("created_at");

            DateTime? lastLogin = r["last_login_at"] == DBNull.Value ? (DateTime?)null : r.GetDateTime("last_login_at");

            return (username, email, createdAt, lastLogin);
        }

        public string GetPassword(int userId)
        {
            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            const string q = "SELECT password FROM `user` WHERE id=@id LIMIT 1;";
            using var cmd = new MySqlCommand(q, conn);
            cmd.Parameters.AddWithValue("@id", userId);

            object result = cmd.ExecuteScalar();
            return result == null ? null : result.ToString();
        }

        public string GetUsername(int userId)
        {
            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            const string q = "SELECT username FROM `user` WHERE id=@id LIMIT 1;";
            using var cmd = new MySqlCommand(q, conn);
            cmd.Parameters.AddWithValue("@id", userId);

            object result = cmd.ExecuteScalar();
            return result == null ? null : result.ToString();
        }

        public bool UpdatePassword(int userId, string newPassword)
        {
            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            const string q = "UPDATE `user` SET password=@p WHERE id=@id;";
            using var cmd = new MySqlCommand(q, conn);
            cmd.Parameters.AddWithValue("@p", newPassword);
            cmd.Parameters.AddWithValue("@id", userId);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool UpdateEmail(int userId, string newEmail)
        {
            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            const string q = "UPDATE `user` SET email=@e WHERE id=@id;";
            using var cmd = new MySqlCommand(q, conn);
            cmd.Parameters.AddWithValue("@e", newEmail);
            cmd.Parameters.AddWithValue("@id", userId);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool UpdateUsername(int userId, string newUsername)
        {
            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            const string q = "UPDATE `user` SET username=@u WHERE id=@id;";
            using var cmd = new MySqlCommand(q, conn);
            cmd.Parameters.AddWithValue("@u", newUsername);
            cmd.Parameters.AddWithValue("@id", userId);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool EmailExists(string email, int excludeUserId)
        {
            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            const string q = "SELECT COUNT(*) FROM `user` WHERE email=@e AND id<>@id;";
            using var cmd = new MySqlCommand(q, conn);
            cmd.Parameters.AddWithValue("@e", email);
            cmd.Parameters.AddWithValue("@id", excludeUserId);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public bool UsernameExists(string username, int excludeUserId)
        {
            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            const string q = "SELECT COUNT(*) FROM `user` WHERE username=@u AND id<>@id;";
            using var cmd = new MySqlCommand(q, conn);
            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@id", excludeUserId);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

    }
}