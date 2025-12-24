using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;

namespace Airport_Airplane_management_system.Model.Repositories
{
    public class MySqlUserRepository : IUserRepository
    {
        private readonly string connStr;

        // Parameterless constructor
        public MySqlUserRepository()
        {
            connStr = "server=localhost;port=3306;database=user;user=root;password=2006";
        }

        // Constructor with custom connection string
        public MySqlUserRepository(string connectionString)
        {
            connStr = connectionString;
        }
        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            using var conn = new MySqlConnection(connStr);
            conn.Open();

            const string sql = "SELECT * FROM user;";
            using var cmd = new MySqlCommand(sql, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                users.Add(new User(
                    userID: reader.GetInt32("id"),
                    fname: reader.GetString("firstname"),
                    lname: reader.GetString("lastname"),
                    email: reader.GetString("email"),
                    username: reader.GetString("username"),
                    password: reader.GetString("password")
               
                ));
            }

            return users;
        }

        public User GetUserById(int userId)
        {
            using var conn = new MySqlConnection(connStr);
            conn.Open();

            const string sql = "SELECT * FROM user WHERE id=@id;";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", userId);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User(
                    userID: reader.GetInt32("id"),
                    fname: reader.GetString("firstname"),
                    lname: reader.GetString("lastname"),
                    email: reader.GetString("email"),
                    username: reader.GetString("username"),
                    password: reader.GetString("password")
                );
            }

            return null;
        }

        public User GetUserByUsername(string username)
        {
            using var conn = new MySqlConnection(connStr);
            conn.Open();

            const string sql = "SELECT * FROM user WHERE username=@username;";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new User(
                    userID: reader.GetInt32("id"),
                    fname: reader.GetString("firstname"),
                    lname: reader.GetString("lastname"),
                    email: reader.GetString("email"),
                    username: reader.GetString("username"),
                    password: reader.GetString("password")
                );
            }

            return null;
        }

        public void AddUser(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            using var conn = new MySqlConnection(connStr);
            conn.Open();

            const string sql = @"INSERT INTO user 
                                (firstname, lastname, email, username, password)
                                VALUES (@fname, @lname, @email, @username, @password);";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@fname", user.FName);
            cmd.Parameters.AddWithValue("@lname", user.LName);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@username", user.UserName);
            cmd.Parameters.AddWithValue("@password", user.Password);
        

            cmd.ExecuteNonQuery();
        }
        public bool UsernameExists(string username)
        {
            using var conn = new MySqlConnection(connStr);
            conn.Open();

            const string sql = "SELECT COUNT(*) FROM user WHERE username = @username";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public bool EmailExists(string email)
        {
            using var conn = new MySqlConnection(connStr);
            conn.Open();

            const string sql = "SELECT COUNT(*) FROM user WHERE email = @email";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@email", email);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public void UpdatePassword(int userId, string newPassword)
        {
            using var conn = new MySqlConnection(connStr);
            conn.Open();

            const string sql = "UPDATE user SET password=@password WHERE id=@id;";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@password", newPassword);
            cmd.Parameters.AddWithValue("@id", userId);

            cmd.ExecuteNonQuery();
        }

        public bool RemoveUser(int userId)
        {
            using var conn = new MySqlConnection(connStr);
            conn.Open();

            const string sql = "DELETE FROM user WHERE id=@id;";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", userId);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
