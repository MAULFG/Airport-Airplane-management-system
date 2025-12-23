using System;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Interfaces
{
    public interface IUserDashboardView
    {
        event EventHandler UpcomingFlightsClicked;
        event EventHandler SearchBookClicked;
        event EventHandler MyTicketsClicked;
        event EventHandler NotificationsClicked;
        event EventHandler SettingsClicked;
        event EventHandler AccountClicked;
        event EventHandler LogoutClicked;
        event EventHandler Main;

        // Methods to show/hide pages, called by Presenter
        void UpcomingFlights();
        void SearchBook();
        void MyTickets();
        void Notifications();
        void UserSettings();
        void UserAccount();
        void Logout();

    }
}
