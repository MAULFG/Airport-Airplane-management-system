public interface IUserDashboardView
{
    // Events
    event EventHandler UpcomingFlightsClicked;
    event EventHandler SearchBookClicked;
    event EventHandler MyTicketsClicked;
    event EventHandler NotificationsClicked;
    event EventHandler SettingsClicked;
    event EventHandler AccountClicked;
    event EventHandler LogoutClicked;
    event EventHandler UserMainClicked;

    // Navigation / UI
    void ShowMainUser();
    void UpcomingFlights();
    void SearchBook();
    void MyTickets();
    void Notifications();
    void UserAccount();
    void Logout();

    // Booking
    void OpenBooking(int flightId);

    // 🔹 Notification badge (Presenter controls this)
    void SetUnreadNotificationsCount(int count);
}
