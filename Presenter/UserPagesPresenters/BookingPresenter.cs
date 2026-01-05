using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Repositories;
using Airport_Airplane_management_system.View.Interfaces;
using System;
using System.Linq;

namespace Airport_Airplane_management_system.Presenter
{
    public class BookingPresenter
    {
        private readonly IFlightRepository _flightrepo;
        private readonly IUserRepository _userRepo;
        private readonly IBookingRepository _bookRepo;
        private readonly IPlaneRepository _planeRepo;
        private readonly INotificationWriterRepository _notiRepo;
        private IAppSession _session;
        private readonly IBookingView _view;
        private readonly FlightService _flightService;
        private readonly BookingService _bookingService;
        private readonly NotificationWriterService _notifWriter;
        private Flight _flight;
        private FlightSeats _selectedSeat;
        //("server=localhost;port=3306;database=user;user=root;password=2006");
        public BookingPresenter(IBookingView view, IAppSession session, int flightId)
        {
            _session = session;
            _view = view ?? throw new ArgumentNullException(nameof(view));

            _userRepo = new MySqlUserRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            _flightrepo = new MySqlFlightRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            _bookRepo = new MySqlBookingRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            _notiRepo = new MySqlNotificationWriterRepository("server=localhost;port=3306;database=user;user=root;password=2006");

            _notifWriter = new NotificationWriterService(_notiRepo);
            _flightService = new FlightService(_flightrepo,_userRepo,_bookRepo,_planeRepo,session, _notifWriter);
            _view.BookingCompleted += OnBookingCompleted;

            _view.SeatSelected += OnSeatSelected;
            _view.ConfirmBookingClicked += OnConfirmBooking;

            LoadFlight(flightId);
        }
        private void OnBookingCompleted()
        {
            _selectedSeat = null;

            // reload flight + seats
            LoadFlight(_flight.FlightID);
        }

        public void Refresh(int flightId)
        {
            // Clear current seat selection
            _selectedSeat = null;

            // Reload the flight info
            LoadFlight(flightId);
        }

        private void LoadFlight(int flightId)
        {
            _flight = _flightService.LoadFlightsWithSeats().FirstOrDefault(f => f.FlightID == flightId);

            if (_flight == null)
            {
                _view.ShowMessage("Flight not found.");
                return;
            }

            if (_flight.Plane == null)
            {
                _view.ShowMessage("Flight plane is not assigned.");
                return;
            }

            // Now flight is fully loaded, with plane and seats
            _view.ShowFlightInfo(_flight);
            _view.ShowSeats(_flight);
        }

        private void OnSeatSelected(FlightSeats seat)
        {
            if (seat.IsBooked)
            {
                _view.ShowMessage("This seat is already booked.");
                return;
            }

            _selectedSeat = seat;

            // Base price from flight by class
            decimal basePrice = _flight.GetSeatPrice(seat.ClassType);

            // Optional: add window seat surcharge (+20%)
            decimal priceWithSurcharge = basePrice;
            if (IsWindowSeat(seat, _flight))
                priceWithSurcharge += basePrice * 0.20m;

            _view.ShowSelectedSeat(seat, priceWithSurcharge);
        }

        // Helper: detect window seat
        private bool IsWindowSeat(FlightSeats seat, Flight flight)
        {
            string seatLetter = new string(seat.SeatNumber.SkipWhile(char.IsDigit).ToArray()).ToUpper();
            string model = flight.Plane.Model.ToLower();

            if (model.Contains("a320"))
                return seatLetter == "A" || seatLetter == "F";

            if (model.Contains("777"))
            {
                return seat.ClassType switch
                {
                    "First" => seatLetter == "A" || seatLetter == "D",
                    "Business" => seatLetter == "A" || seatLetter == "F",
                    "Economy" => seatLetter == "A" || seatLetter == "J",
                    _ => false
                };
            }

            if (model.Contains("g650"))
                return seatLetter == "A" || seatLetter == "B";

            return false;
        }


        private void OnConfirmBooking()
        {
            if (_selectedSeat == null)
            {
                _view.ShowMessage("Please select a seat.");
                return;
            }

            decimal price = _flight.GetSeatPrice(_selectedSeat.ClassType);

            _view.ShowPassengerDetails(_flight, _selectedSeat, price);



        }

    }
}
