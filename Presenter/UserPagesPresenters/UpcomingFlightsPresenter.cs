using Airport_Airplane_management_system.Model.Interfaces.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Repositories;
using Airport_Airplane_management_system.View.Forms.UserPages;
using System;

namespace Airport_Airplane_management_system.Presenter.UserPagesPresenters
{
    public class UpcomingFlightsPresenter
    {
        private readonly UpcomingFlights _view;
        private readonly FlightService _service;
        private readonly NotificationWriterService notifWriter;
        private readonly UserDashboardPresenter _userdashpresenter;

        private readonly INotificationWriterRepository _notiRepo;
        private readonly IUserRepository userRepo;
        private readonly IFlightRepository flightRepo;
        private readonly IBookingRepository bookingRepo;
        private readonly IPlaneRepository planeRepo;

        private readonly IAppSession _session;
        public UpcomingFlightsPresenter(UpcomingFlights view,IAppSession Session,  UserDashboardPresenter userDashboardPresenter)
        {
            _session = Session;
            _view = view;
            userRepo = new MySqlUserRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            flightRepo = new MySqlFlightRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            bookingRepo = new MySqlBookingRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            planeRepo = new MySqlPlaneRepository("server=localhost;port=3306;database=user;user=root;password=2006");
           _notiRepo =new MySqlNotificationWriterRepository("server=localhost;port=3306;database=user;user=root;password=2006");

            notifWriter = new NotificationWriterService(_notiRepo);
            _service = new FlightService(flightRepo,userRepo,bookingRepo,planeRepo,_session,notifWriter);
            _userdashpresenter = userDashboardPresenter;
            _view.BookFlightRequested += OpenBookingPage;
            _view.LoadFlightsRequested += OnLoadFlights;
        }
        public void RefreshData()
        {
            
                var flights = _service.LoadFlightsWithSeats();
                _view.LoadFlights(flights);
          
        }
        private void OnLoadFlights(object sender, EventArgs e)
        {
            RefreshData();
        }
        private void OpenBookingPage(int flightId)
        {
            _userdashpresenter.OpenBookingp(flightId);
        }
    }
}
