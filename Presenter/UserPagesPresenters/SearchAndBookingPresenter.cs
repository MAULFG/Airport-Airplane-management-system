using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Core.Classes.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Presenter.UserPagesPresenters;
using Airport_Airplane_management_system.View.Forms.UserPages;
using Airport_Airplane_management_system.View.Interfaces;
using MySqlX.XDevAPI;
using System;
using System.Configuration;
using System.Linq;

namespace Airport_Airplane_management_system.Presenter
{
    public class SearchAndBookingPresenter
    {
        private readonly ISearchAndBookingView _view;
        private readonly FlightService _flightService;
        private readonly NotificationWriterService notifWriter;

        private readonly INotificationWriterRepository _notiRepo;
        private readonly IUserRepository userRepo;
        private readonly IFlightRepository flightRepo;
        private readonly IBookingRepository bookingRepo;
        private readonly IPlaneRepository planeRepo;
        private readonly UserDashboardPresenter _userdashpresenter;
        private readonly IAppSession _session;
        public SearchAndBookingPresenter( ISearchAndBookingView view, IAppSession Session, UserDashboardPresenter userDashboardPresenter)  // Add this parameter
        {
            _session = Session;
            _view = view;
            userRepo = new MySqlUserRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            flightRepo = new MySqlFlightRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            bookingRepo = new MySqlBookingRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            planeRepo = new MySqlPlaneRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            _notiRepo = new MySqlNotificationWriterRepository("server=localhost;port=3306;database=user;user=root;password=2006");

            notifWriter = new NotificationWriterService(_notiRepo);
            _flightService = new FlightService(flightRepo, userRepo, bookingRepo, planeRepo, _session, notifWriter);


            _userdashpresenter = userDashboardPresenter;

            _view.BookFlightRequested += OpenBookingPage;
            _view.SearchClicked += OnSearchClicked;
        }
        public void RefreshData()
        {
            
                var flights = _flightService.LoadFlightsWithSeats();

                var fromList = flights.Select(f => f.From).Distinct().OrderBy(f => f).ToList();
                var toList = flights.Select(f => f.To).Distinct().OrderBy(f => f).ToList();
                var classList = new List<string> { "Economy", "Business", "First" }; 



                _view.DisplayFlights(new List<Flight>());
            
            
        }
        private void OnSearchClicked(object sender, EventArgs e)
        {

            _view.DisplayFlights(new List<Flight>()); 


            var flights = _flightService.LoadFlightsWithSeats().AsEnumerable();

            if (!string.IsNullOrWhiteSpace(_view.From))
                flights = flights.Where(f => f.From.Equals(_view.From, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(_view.To))
                flights = flights.Where(f => f.To.Equals(_view.To, StringComparison.OrdinalIgnoreCase));


            if (_view.IsDateSelected && _view.DepartureDate.HasValue)
            {
                var date = _view.DepartureDate.Value.Date;
                flights = flights.Where(f => f.Departure.Date == date);
            }

            if (!string.IsNullOrWhiteSpace(_view.Class))
            {
                flights = flights.Where(f => f.GetAvailableSeats(_view.Class).Count >= _view.Passengers);
            }

            var finalFlights = flights.ToList();

            if (!finalFlights.Any())
            {
                _view.ShowMessage("No flights match your criteria.");
                return;
            }

            _view.DisplayFlights(finalFlights);
        }


        private void OpenBookingPage(int flightId)
        {
            _userdashpresenter.OpenBooking(flightId);
        }

    }
}
