using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;

namespace Airport_Airplane_management_system.Model.Interfaces.Repositories
{
    public interface IUserNotificationsRepository
    {
        List<UserNotificationRow> GetForUser(int userId);

        int GetUnreadCount(int userId);

        bool MarkRead(int userId, int notificationId);
        bool MarkUnread(int userId, int notificationId);

        bool DeleteOne(int userId, int notificationId);
        int DeleteMany(int userId, List<int> notificationIds);

        int ClearAll(int userId);
        void MarkRead(int userId, List<int> notificationIds);
        void MarkUnread(int userId, List<int> notificationIds);
        void InsertNotification(int userId, int? bookingId, string type, string title, string message);
        void InsertNotificationsBulk(List<(int userId, int? bookingId, string type, string title, string message)> items);

    }
}