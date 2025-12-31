namespace Airport_Airplane_management_system.Model.Interfaces.Repositories
{
    public interface INotificationsCounterRepository
    {
        int GetUnreadCount(int userId);
    }
}
