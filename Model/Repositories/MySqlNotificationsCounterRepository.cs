using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using MySql.Data.MySqlClient;
using System;

namespace Airport_Airplane_management_system.Model.Repositories
{
    public class MySqlNotificationsCounterRepository : INotificationsCounterRepository
    {
        private readonly string _connStr;

        public MySqlNotificationsCounterRepository(string connStr)
        {
            _connStr = connStr;
        }

        public int GetUnreadCount(int userId)
        {
            using var conn = new MySqlConnection(_connStr);
            conn.Open();

            const string sql = @"SELECT COUNT(*) 
                                 FROM notifications 
                                 WHERE user_id=@uid AND is_read=0;";

            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uid", userId);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }
}
    