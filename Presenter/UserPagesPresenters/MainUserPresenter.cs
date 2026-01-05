using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Exceptions;
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
        private readonly FlightService _flightService;
        private readonly NotificationWriterService notifWriter;
        private readonly BookingService _bookingService;
        private readonly INotificationWriterRepository _notiRepo;
        private readonly IUserRepository userRepo;
        private readonly IFlightRepository flightRepo;
        private readonly IBookingRepository bookingRepo;
        private readonly IPlaneRepository planeRepo;
        public MainUserPagePresenter( IMainUserPageView view, IAppSession session)
        {
            _view = view;
            _session = session;
            userRepo = new MySqlUserRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            flightRepo = new MySqlFlightRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            bookingRepo = new MySqlBookingRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            planeRepo = new MySqlPlaneRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            _notiRepo = new MySqlNotificationWriterRepository("server=localhost;port=3306;database=user;user=root;password=2006");

            notifWriter = new NotificationWriterService(_notiRepo);
            _flightService = new FlightService(flightRepo, userRepo, bookingRepo, planeRepo, _session, notifWriter);
            _bookingService = new BookingService(bookingRepo,_session);
        }

        public void RefreshData()
        {
            
                var user = _session.CurrentUser;
                if (user == null) return;

                _view.SetWelcomeText($"Welcome, {user.FullName}!");
                _view.ClearStatistics();

                // Preload flights and bookings
                _flightService.Preload();
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
