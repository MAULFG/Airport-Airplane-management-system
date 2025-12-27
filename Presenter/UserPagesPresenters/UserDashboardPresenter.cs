using System;
using Airport_Airplane_management_system.View.Interfaces;

namespace Airport_Airplane_management_system.Presenter.UserPagesPresenters
{
    public class UserDashboardPresenter
    {
        private readonly IUserDashboardView _view;
        private readonly INavigationService _navigationService;
        public UserDashboardPresenter(IUserDashboardView view, INavigationService navigationService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

            // Subscribe to view events
            _view.UserMainClicked += OnMainClicked;
            _view.UpcomingFlightsClicked += OnUpcomingFlightsClicked;
            _view.SearchBookClicked += OnSearchBookClicked;
            _view.MyTicketsClicked += OnMyTicketsClicked;
            _view.NotificationsClicked += OnNotificationsClicked;
            _view.SettingsClicked += OnSettingsClicked;
            _view.AccountClicked += OnAccountClicked;
            _view.LogoutClicked += OnLogoutClicked;
        }

        // Event handlers
        private void OnMainClicked(object sender, EventArgs e)
        {
            _view.ShowMainUser();
        }
        
        private void OnUpcomingFlightsClicked(object sender, EventArgs e)
        {
            _view.UpcomingFlights();
        }

        private void OnSearchBookClicked(object sender, EventArgs e)
        {
            _view.SearchBook();
        }
        public void OpenBookingp(int flightid)
        {
            _view.OpenBooking(flightid);
        }
        private void OnMyTicketsClicked(object sender, EventArgs e)
        {
            _view.MyTickets();
        }

        private void OnNotificationsClicked(object sender, EventArgs e)
        {
            _view.Notifications();
        }

        private void OnSettingsClicked(object sender, EventArgs e)
        {
            _view.UserSettings();
        }

        private void OnAccountClicked(object sender, EventArgs e)
        {
            _view.UserAccount();
        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            // Optional: any session cleanup here
            _view.Logout(); // clear UI
            _navigationService.NavigateToLogin(); // redirect to login page
        }
    }
}
