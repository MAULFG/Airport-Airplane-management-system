using System;
using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using MySql.Data.MySqlClient;

namespace Airport_Airplane_management_system.Model.Repositories
{
    public class MySqlUserNotificationsRepository : IUserNotificationsRepository
    {
        private readonly string _connStr;

        public MySqlUserNotificationsRepository(string connStr)
        {
            _connStr = connStr;
        }

        public List<UserNotificationRow> GetForUser(int userId)
        {
            const string sql = @"
SELECT
  notification_id,
  user_id,
  booking_id,
  type,
  title,
  message,
  is_read,
  created_at
FROM notifications
WHERE user_id = @uid
ORDER BY created_at DESC;";

            var list = new List<UserNotificationRow>();

            using var conn = new MySqlConnection(_connStr);
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uid", userId);

            conn.Open();
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                list.Add(new UserNotificationRow
                {
                    NotificationId = Convert.ToInt32(r["notification_id"]),
                    UserId = Convert.ToInt32(r["user_id"]),
                    BookingId = r["booking_id"] == DBNull.Value ? (int?)null : Convert.ToInt32(r["booking_id"]),
                    Type = r["type"]?.ToString() ?? "",
                    Title = r["title"]?.ToString() ?? "",
                    Message = r["message"]?.ToString() ?? "",
                    IsRead = Convert.ToInt32(r["is_read"]) == 1,
                    CreatedAt = Convert.ToDateTime(r["created_at"])
                });
            }

            return list;
        }

        public int GetUnreadCount(int userId)
        {
            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            const string sql = @"SELECT COUNT(*) FROM notifications WHERE user_id=@uid AND is_read=0;";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uid", userId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public bool MarkRead(int userId, int notificationId)
        {
            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            const string sql = @"UPDATE notifications SET is_read=1 WHERE notification_id=@nid AND user_id=@uid;";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nid", notificationId);
            cmd.Parameters.AddWithValue("@uid", userId);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool MarkUnread(int userId, int notificationId)
        {
            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            const string sql = @"UPDATE notifications SET is_read=0 WHERE notification_id=@nid AND user_id=@uid;";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nid", notificationId);
            cmd.Parameters.AddWithValue("@uid", userId);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool DeleteOne(int userId, int notificationId)
        {
            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            const string sql = @"DELETE FROM notifications WHERE notification_id=@nid AND user_id=@uid;";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nid", notificationId);
            cmd.Parameters.AddWithValue("@uid", userId);

            return cmd.ExecuteNonQuery() > 0;
        }

        public int DeleteMany(int userId, List<int> notificationIds)
        {
            if (notificationIds == null || notificationIds.Count == 0) return 0;

            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            // build IN (@p0,@p1,...)
            var paramNames = new List<string>();
            for (int i = 0; i < notificationIds.Count; i++)
                paramNames.Add($"@p{i}");

            string sql = $@"DELETE FROM notifications 
                            WHERE user_id=@uid AND notification_id IN ({string.Join(",", paramNames)});";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uid", userId);

            for (int i = 0; i < notificationIds.Count; i++)
                cmd.Parameters.AddWithValue(paramNames[i], notificationIds[i]);

            return cmd.ExecuteNonQuery();
        }

        public int ClearAll(int userId)
        {
            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            const string sql = @"DELETE FROM notifications WHERE user_id=@uid;";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uid", userId);

            return cmd.ExecuteNonQuery();
        }
    }
}
