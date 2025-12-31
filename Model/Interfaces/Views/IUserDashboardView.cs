using System;
using Airport_Airplane_management_system.View.Forms.UserPages;

namespace Airport_Airplane_management_system.View.Interfaces
{
    public interface IUserDashboardView
    {
        // Events triggered by button clicks
        event EventHandler UpcomingFlightsClicked;
        event EventHandler SearchBookClicked;
        event EventHandler MyTicketsClicked;
        event EventHandler NotificationsClicked;
        event EventHandler SettingsClicked;
        event EventHandler AccountClicked;
        event EventHandler LogoutClicked;
        event EventHandler UserMainClicked;

        // Methods called by Presenter to show specific panels
        void UpcomingFlights();
        void SearchBook();
        void MyTickets();
        void Notifications();
        void UserSettings();
        void UserAccount();
        void ShowMainUser();
        // Logout action
        void Logout();
        void SetUnreadNotificationsCount(int count); // for bell

    }
}
