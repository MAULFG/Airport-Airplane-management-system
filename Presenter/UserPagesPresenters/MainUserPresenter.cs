using Airport_Airplane_management_system.Model.Core.Classes;
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

            // 🔥 THIS WAS MISSING
            _bookingService.LoadBookingsForCurrentUser();

            var bookings = user.BookedFlights
                .Where(b => b.Status == "Confirmed")
                .ToList();

            var now = DateTime.Now;

            int totalBookings = bookings.Count;
            int upcomingFlights = bookings.Count(b => b.Flight.Departure > now);
            int completedFlights = bookings.Count(b => b.Flight.Arrival < now);

            string favoriteRoute = bookings
                .GroupBy(b => $"{b.Flight.From} → {b.Flight.To}")
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault() ?? "Beirut → Dubai";

            _view.AddStatCard("Upcoming Flights", upcomingFlights.ToString());
            _view.AddStatCard("Completed Flights", completedFlights.ToString());
            _view.AddStatCard("Total Bookings", totalBookings.ToString());
            _view.AddStatCard("Favorite Route", favoriteRoute);
        }


    }
}
