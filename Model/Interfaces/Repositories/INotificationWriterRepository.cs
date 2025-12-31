using System;

namespace Airport_Airplane_management_system.Model.Interfaces.Repositories
{
    public interface INotificationWriterRepository
    {
        void AddUserNotification(
            int userId,
            string type,
            string title,
            string message,
            int? bookingId = null
        );
    }
}
