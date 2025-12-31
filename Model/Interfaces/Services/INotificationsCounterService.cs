namespace Airport_Airplane_management_system.Model.Interfaces.Services
{
    public interface INotificationsCounterService
    {
        int GetUnreadCount(int userId);
    }
}
