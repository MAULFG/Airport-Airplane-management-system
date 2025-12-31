using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;

namespace Airport_Airplane_management_system.Model.Interfaces.Services
{
    public interface IUserNotificationsService
    {
        List<UserNotificationRow> Load(int userId);

        int GetUnreadCount(int userId);

        bool MarkRead(int userId, int notificationId);
        bool MarkUnread(int userId, int notificationId);

        bool DeleteOne(int userId, int notificationId);
        int DeleteMany(int userId, List<int> ids);

        int ClearAll(int userId);
    }
}
