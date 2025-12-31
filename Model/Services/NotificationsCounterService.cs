using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Services;

namespace Airport_Airplane_management_system.Model.Services
{
    public class NotificationsCounterService : INotificationsCounterService
    {
        private readonly INotificationsCounterRepository _repo;

        public NotificationsCounterService(INotificationsCounterRepository repo)
        {
            _repo = repo;
        }

        public int GetUnreadCount(int userId)
        {
            if (userId <= 0) return 0;
            return _repo.GetUnreadCount(userId);
        }
    }
}
