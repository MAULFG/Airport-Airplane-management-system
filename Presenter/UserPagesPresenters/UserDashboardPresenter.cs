using Airport_Airplane_management_system.Model.Core.Classes.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.View.Interfaces;
using System;

namespace Airport_Airplane_management_system.Presenter.UserPagesPresenters
{
    public class UserDashboardPresenter
    {
        private readonly IUserDashboardView _view;
        private readonly INavigationService _navigation;
        private readonly IAppSession _session;

        private readonly NotificationsCounterService _notifService;

        public UserDashboardPresenter(
            IUserDashboardView view,
            INavigationService navigation,
            IAppSession session)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
            _session = session ?? throw new ArgumentNullException(nameof(session));

            INotificationsCounterRepository repo =
                new MySqlNotificationsCounterRepository("server=localhost;port=3306;database=user;user=root;password=2006");

            _notifService = new NotificationsCounterService(repo);

            HookViewEvents();
            RefreshNotifications();
        }

        private void HookViewEvents()
        {
            _view.UserMainClicked += (_, _) => _view.ShowMainUser();
            _view.UpcomingFlightsClicked += (_, _) => _view.UpcomingFlights();
            _view.SearchBookClicked += (_, _) => _view.SearchBook();
            _view.MyTicketsClicked += (_, _) => _view.MyTickets();
            _view.NotificationsClicked += (_, _) =>
            {
                _view.Notifications();
                RefreshNotifications();
            };
            _view.AccountClicked += (_, _) => _view.UserAccount();
            _view.SettingsClicked += (_, _) => _view.UserAccount();
            _view.LogoutClicked += (_, _) =>
            {
                _view.Logout();
                _navigation.NavigateToLogin();
            };
        }

        public void OpenBooking(int flightId)
        {
            _view.OpenBooking(flightId);
            RefreshNotifications();
        }

        public void RefreshNotifications()
        {
            int userId = _navigation.GetCurrentUserId();
            int count = _notifService.GetUnreadCount(userId);
            _view.SetUnreadNotificationsCount(count);
        }
    }
}
