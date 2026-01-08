using Airport_Airplane_management_system.Model.Core.Classes.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using System;
using System.Linq;

namespace Airport_Airplane_management_system.Presenter.UserPagesPresenters
{
    public class MainUserPagePresenter
    {
        private readonly IMainUserPageView _view;
        private readonly IAppSession _session;
        private readonly IBookingRepository _bookRepo;
        private readonly BookingService _bookingService;

        public MainUserPagePresenter(
            IMainUserPageView view,
            IAppSession session)
        {
            _view = view;
            _session = session;
            _bookRepo = new MySqlBookingRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            _bookingService = new BookingService(_bookRepo, _session);
        }

        public void RefreshData()
        {
            var user = _session.CurrentUser;
            if (user == null)
                return;

            _bookingService.LoadBookingsForCurrentUser();

            var bookings = user.BookedFlights
                .Where(b => b.Status == "Confirmed")
                .ToList();

            var now = DateTime.Now;
            _view.SetWelcomeText($"Welcome, {user.FullName}!");
            _view.ClearStatistics();

            int upcoming = bookings.Count(b => b.Flight.Departure > now);
            int completed = bookings.Count(b => b.Flight.Arrival < now);
            int total = bookings.Count;

            _view.AddStatCard("Upcoming Flights", upcoming.ToString());
            _view.AddStatCard("Completed Flights", completed.ToString());
            _view.AddStatCard("Total Bookings", total.ToString());


            var favoriteRoute = bookings
                .GroupBy(b => $"{b.Flight.From} → {b.Flight.To}")
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            _view.AddStatCard(
                "Favorite Route",
                favoriteRoute ?? "No data"
            );


            var nextFlight = bookings
                .Where(b => b.Flight.Departure > now)
                .OrderBy(b => b.Flight.Departure)
                .FirstOrDefault();

            if (nextFlight != null)
            {
                _view.SetNextFlight(
                    $"{nextFlight.Flight.From} → {nextFlight.Flight.To}",
                    $"Departs {nextFlight.Flight.Departure:g}"
                );

                // Check-in logic (24h before)
                var checkInTime = nextFlight.Flight.Departure.AddHours(-24);

                var timeLeft = checkInTime - now;

                string checkInStatus;

                if (timeLeft <= TimeSpan.Zero)
                {
                    checkInStatus = "Available Now";
                }
                else
                {
                    checkInStatus =
                        $"{timeLeft.Days}d {timeLeft.Hours}h {timeLeft.Minutes}m";
                }


                _view.AddStatCard("Next Check-in", checkInStatus);
            }
            else
            {
                _view.HideNextFlight();
                _view.AddStatCard("Next Check-in", "No upcoming flights");
            }
        }
    }
}
