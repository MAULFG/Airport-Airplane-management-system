using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Repositories;
using Airport_Airplane_management_system.View.Interfaces;
using System;
using System.Text.RegularExpressions;

namespace Airport_Airplane_management_system.Presenter
{
    public class PassengerDetailsPresenter
    {
        private readonly IBookingRepository bookingRepo;
        private readonly IPassengerRepository passengerRepo;

        private readonly IPassengerDetailsView _view;
        private readonly PassengerService _passengerService;
        private readonly BookingService _bookingService;
        private readonly IAppSession _session;

        private Flight _flight;
        private FlightSeats _seat;
        private decimal _price;

        private bool _isClosed; // 🔒 prevents double execution

        public PassengerDetailsPresenter(
            IPassengerDetailsView view,
            IAppSession session,
            Flight flight,
            FlightSeats selectedSeat,
            decimal seatPrice)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _session = session;

            bookingRepo = new MySqlBookingRepository(
                "server=localhost;port=3306;database=user;user=root;password=2006");
            passengerRepo = new MySqlPassengerRepository(
                "server=localhost;port=3306;database=user;user=root;password=2006");

            _passengerService = new PassengerService(passengerRepo, session);
            _bookingService = new BookingService(bookingRepo, session);

            _flight = flight;
            _seat = selectedSeat;
            _price = seatPrice;

            _view.CompleteBookingClicked += OnCompleteBooking;
            _view.CancelClicked += OnCancel;

            LoadSummary();
        }

        // ================= SUMMARY =================
        private void LoadSummary()
        {
            _view.ShowSelectedSeat(_seat);

            decimal basePrice = _seat.SeatPrice;
            decimal window = IsWindowSeat(_seat, _flight) ? basePrice * 0.20m : 0m;
            decimal tax = (basePrice + window) * 0.10m;
            decimal total = basePrice + window + tax;

            _view.ShowPrice(basePrice, window, tax, total);
        }

        // ================= BOOKING =================

        private void OnCompleteBooking()
        {
            if (_isClosed) return;

            if (!ValidatePassenger())
                return;

            int? passengerId = _passengerService.GetPassengerIdByPhone(_view.Phone);

            if (passengerId == null)
            {
                if (!_passengerService.AddPassenger(
                    _view.FullName,
                    _view.Email,
                    _view.Phone,
                    out int newPassengerId,
                    out string error))
                {
                    _view.ShowMessage(error);
                    return;
                }

                passengerId = newPassengerId;
            }

            var user = _session.CurrentUser;

            if (!_bookingService.MakeBooking(
                user,
                passengerId.Value,
                _flight,
                _seat,
                out Booking booking,
                out string bookingError))
            {
                _view.ShowMessage(bookingError);
                return;
            }

            _view.ShowMessage("Booking completed successfully.");
            Close();
            BookingCompleted?.Invoke();
        }

        // ================= CANCEL =================

        private void OnCancel()
        {
            if (_isClosed) return;
            Close();
            PassengerDetailsClosed?.Invoke();
        }

        // ================= CLEANUP =================

        private void Close()
        {
            _isClosed = true;

            _view.CompleteBookingClicked -= OnCompleteBooking;
            _view.CancelClicked -= OnCancel;

            _view.ClearInputs();
        }

        // ================= VALIDATION =================

        private bool ValidatePassenger()
        {
            if (string.IsNullOrWhiteSpace(_view.FirstName) || _view.FirstName.Length < 2)
            {
                _view.ShowMessage("First name must be at least 2 characters.");
                return false;
            }

            if (!string.IsNullOrWhiteSpace(_view.MiddleName) && _view.MiddleName.Length < 2)
            {
                _view.ShowMessage("Middle name must be at least 2 characters.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(_view.LastName) || _view.LastName.Length < 2)
            {
                _view.ShowMessage("Last name must be at least 2 characters.");
                return false;
            }

            if (!Regex.IsMatch(_view.Email ?? "", @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                _view.ShowMessage("Invalid email.");
                return false;
            }

            if (!Regex.IsMatch(_view.Phone ?? "", @"^\d{8,}$"))
            {
                _view.ShowMessage("Invalid phone number.");
                return false;
            }

            return true;
        }

        // ================= HELPERS =================

        private bool IsWindowSeat(FlightSeats seat, Flight flight)
        {
            string letter = new string(seat.SeatNumber.SkipWhile(char.IsDigit).ToArray()).ToUpper();
            string model = flight.Plane.Model.ToLower();

            if (model.Contains("a320")) return letter == "A" || letter == "F";
            if (model.Contains("777"))
                return seat.ClassType switch
                {
                    "First" => letter == "A" || letter == "D",
                    "Business" => letter == "A" || letter == "F",
                    "Economy" => letter == "A" || letter == "I",
                    _ => false
                };
            if (model.Contains("g650")) return letter == "A" || letter == "B";

            return false;
        }

        // ================= EVENTS =================

        public event Action BookingCompleted;
        public event Action PassengerDetailsClosed;
    }
}
