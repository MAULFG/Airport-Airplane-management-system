using Airport_Airplane_management_system.Model.Interfaces.Views;
using System;
using System.Linq;
using Airport_Airplane_management_system.Model.Core.Classes.Exceptions;

namespace Airport_Airplane_management_system.Presenter.UserPagesPresenters
{
    public class MainUserPagePresenter
    {
        private readonly IMainUserPageView _view;
        private readonly IAppSession _session;

        public MainUserPagePresenter(IMainUserPageView view, IAppSession session)
        {
            _view = view;
            _session = session;
        }

        public void RefreshData()
        {
            var user = _session.CurrentUser;
            if (user == null) return;

            // ===== Welcome =====
            _view.SetWelcomeText($"Welcome, {user.FullName}!");
            _view.ClearStatistics();

            var bookings = user.BookedFlights
                .Where(b => b.Status == "Confirmed")
                .ToList();

            var now = DateTime.Now;

            // ===== Main Stats =====
            _view.AddStatCard("Upcoming Flights", bookings.Count(b => b.Flight.Departure > now).ToString());
            _view.AddStatCard("Completed Flights", bookings.Count(b => b.Flight.Arrival < now).ToString());
            _view.AddStatCard("Total Bookings", bookings.Count.ToString());

            // ===== Favorite Route =====
            var favorite = bookings
                .GroupBy(b => $"{b.Flight.From} → {b.Flight.To}")
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault();

            if (favorite != null)
                _view.AddStatCard("Favorite Route", favorite);

            // ===== Next Flight Panel =====
            var next = bookings
                .Where(b => b.Flight.Departure > now)
                .OrderBy(b => b.Flight.Departure)
                .FirstOrDefault();

            if (next != null)
            {
                _view.SetNextFlight(
                    $"{next.Flight.From} → {next.Flight.To}",
                    $"Departs {next.Flight.Departure:g} "
                );

                // Optional: add "Next Check-in" card (24h before departure)
                var checkInStatus = next.Flight.Departure.AddHours(-24) <= now ?
                    "Available Now" :
                    $"Opens {(next.Flight.Departure.AddHours(-24) - now).Hours}h {(next.Flight.Departure.AddHours(-24) - now).Minutes}m";

                _view.AddStatCard("Next Check-in", checkInStatus);
            }
            else
            {
                _view.HideNextFlight();
                _view.AddStatCard("Next Check-in", "No upcoming flights");
            }

           
            

            // ===== Optional extra filler cards for balance =====
            // Example: Loyalty Points, Frequent Route, or just dummy info
            if (favorite != null)
                _view.AddStatCard("Most Frequent Route", favorite);
            else
                _view.AddStatCard("Most Frequent Route", "Beirut → Dubai");

            
        }
    }
}
