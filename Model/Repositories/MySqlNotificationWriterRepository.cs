using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using MySql.Data.MySqlClient;

namespace Airport_Airplane_management_system.Model.Repositories
{
    public class MySqlNotificationWriterRepository : INotificationWriterRepository
    {
        private readonly string _connStr;

        public MySqlNotificationWriterRepository(string connStr)
        {
            _connStr = connStr;
        }

        public void AddUserNotification(int userId, string type, string title, string message, int? bookingId = null)
        {
            const string sql = @"
INSERT INTO notifications
(user_id, booking_id, type, title, message, is_read, created_at)
VALUES
(@uid, @bid, @type, @title, @msg, 0, NOW());";

            using var conn = new MySqlConnection(_connStr);
            using var cmd = new MySqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@uid", userId);
            cmd.Parameters.AddWithValue("@bid", (object?)bookingId ?? System.DBNull.Value);
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@msg", message);

            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
