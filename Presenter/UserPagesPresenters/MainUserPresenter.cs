using Airport_Airplane_management_system.Model.Interfaces.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Services;
using System;
using System.Linq;

namespace Airport_Airplane_management_system.Presenter.UserPagesPresenters
{
    public class MainUserPagePresenter
    {
        private readonly IMainUserPageView _view;
        private readonly IAppSession _session;
        private readonly FlightService _flightService;
        private readonly BookingService _bookingService;

        public MainUserPagePresenter(
            IMainUserPageView view,
            IAppSession session,
            FlightService flightService,
            BookingService bookingService)
        {
            _view = view;
            _session = session;
            _flightService = flightService;
            _bookingService = bookingService;

            LoadDashboard();
        }

        private void LoadDashboard()
        {
            var user = _session.CurrentUser;
            if (user == null) return;

            _view.SetWelcomeText($"Welcome, {user.FullName}!");
            _view.ClearStatistics();

            _flightService.Preload();
            _bookingService.LoadBookingsForCurrentUser();

            var now = DateTime.Now;

            var bookings = user.BookedFlights
                .Where(b => b.Status == "Confirmed")
                .ToList();

            int upcoming = bookings.Count(b => b.Flight.Departure > now);
            int completed = bookings.Count(b => b.Flight.Arrival < now);
            int total = bookings.Count;

            string favoriteRoute = bookings
                .GroupBy(b => $"{b.Flight.From} → {b.Flight.To}")
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault() ?? "-";

            _view.AddStatCard("Upcoming Flights", upcoming.ToString());
            _view.AddStatCard("Completed Flights", completed.ToString());
            _view.AddStatCard("Total Bookings", total.ToString());
            _view.AddStatCard("Favorite Route", favoriteRoute);
        }
    }
}
