using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Services;

namespace Airport_Airplane_management_system.Model.Services
{
    public class UserNotificationsService : IUserNotificationsService
    {
        private readonly IUserNotificationsRepository _repo;

        public UserNotificationsService(IUserNotificationsRepository repo)
        {
            _repo = repo;
        }

        public List<UserNotificationRow> Load(int userId)
        {
            if (userId <= 0) return new List<UserNotificationRow>();
            return _repo.GetForUser(userId);
        }

        public int GetUnreadCount(int userId)
        {
            if (userId <= 0) return 0;
            return _repo.GetUnreadCount(userId);
        }

        public bool MarkRead(int userId, int notificationId)
        {
            if (userId <= 0 || notificationId <= 0) return false;
            return _repo.MarkRead(userId, notificationId);
        }

        public bool MarkUnread(int userId, int notificationId)
        {
            if (userId <= 0 || notificationId <= 0) return false;
            return _repo.MarkUnread(userId, notificationId);
        }

        public bool DeleteOne(int userId, int notificationId)
        {
            if (userId <= 0 || notificationId <= 0) return false;
            return _repo.DeleteOne(userId, notificationId);
        }

        public int DeleteMany(int userId, List<int> ids)
        {
            if (userId <= 0) return 0;
            return _repo.DeleteMany(userId, ids);
        }

        public int ClearAll(int userId)
        {
            if (userId <= 0) return 0;
            return _repo.ClearAll(userId);
        }
    }
}
