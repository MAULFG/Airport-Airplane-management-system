using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Exceptions;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.View.Interfaces;
using MySqlX.XDevAPI;
using System;
using System.Text.RegularExpressions;
using System.Text.RegularExpressions;
namespace Airport_Airplane_management_system.Presenter
{
    public class PassengerDetailsPresenter
    {
        private readonly IPassengerDetailsView _view;
        private readonly PassengerService _passengerService;
        private readonly BookingService _bookingService;
        private readonly IAppSession _session;
        private readonly Flight _flight;
        private readonly FlightSeats _seat;
        private readonly decimal _price;


        public PassengerDetailsPresenter(IPassengerDetailsView view, PassengerService passengerService, BookingService bookingService, IAppSession session, Flight flight,
            FlightSeats selectedSeat, decimal seatPrice)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _passengerService = passengerService ?? throw new ArgumentNullException(nameof(passengerService));
            _bookingService = bookingService ?? throw new ArgumentNullException(nameof(bookingService));
            _session = session ?? throw new ArgumentNullException(nameof(session)); // assign it

            _flight = flight;
            _seat = selectedSeat;
            _price = seatPrice;

            _view.CompleteBookingClicked += OnCompleteBooking;
            _view.CancelClicked += OnCancel;

            LoadSummary();
        }

        private void LoadSummary()
        {
            _view.ShowSelectedSeat(_seat);

            decimal seatPrice = _seat.SeatPrice; // base seat price

            // Optional: add window seat surcharge (+20%)
            if (IsWindowSeat(_seat, _flight))
                seatPrice += seatPrice * 0.20m;

            decimal tax = seatPrice * 0.10m; // 10% tax
            decimal total = seatPrice + tax;

            _view.ShowPrice(seatPrice, tax, total);
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

        private void OnCompleteBooking()
        {
            if (!ValidatePassenger())
                return;

            // 1️⃣ Get or create passenger
            int? passengerId = _passengerService.GetPassengerIdByPhone(_view.Phone);
            if (passengerId == null)
            {
                if (!_passengerService.AddPassenger(
                    _view.FullName,
                    _view.Email,
                    _view.Phone,
                    out int newPassengerId,
                    out string passengerError))
                {
                    _view.ShowMessage(passengerError);
                    return;
                }
                passengerId = newPassengerId;
            }

            var loggedInUser = _session.CurrentUser;

            // 2️⃣ Compute final seat price (surcharge + tax)
            decimal seatPrice = _seat.SeatPrice;
            if (IsWindowSeat(_seat, _flight))
                seatPrice += seatPrice * 0.20m;
            decimal tax = seatPrice * 0.10m;
            decimal totalPrice = seatPrice + tax;

            // 3️⃣ Make booking
            if (!_bookingService.MakeBooking(
                loggedInUser,
                passengerId.Value,
                _flight,
                _seat,
                out Booking booking,
                out string bookingError))
            {
                _view.ShowMessage(bookingError);
                return;
            }

            // 4️⃣ Show success and close
            _view.ShowMessage($"Booking completed successfully.\nTotal: ${totalPrice:0.00}");
            PassengerDetailsClosed?.Invoke();
            _view.CloseView();
        }


        public event Action PassengerDetailsClosed;

        private bool ValidatePassenger()
        {
            // First name validation
            if (string.IsNullOrWhiteSpace(_view.FirstName) || _view.FirstName.Length < 2)
            {
                _view.ShowMessage("First name must be at least 2 characters.");
                return false;
            }

            // Middle name (optional)
            if (!string.IsNullOrWhiteSpace(_view.MiddleName) && _view.MiddleName.Length < 2)
            {
                _view.ShowMessage("Middle name must be at least 2 characters if provided.");
                return false;
            }

            // Last name validation
            if (string.IsNullOrWhiteSpace(_view.LastName) || _view.LastName.Length < 2)
            {
                _view.ShowMessage("Last name must be at least 2 characters.");
                return false;
            }

            // Email validation
            if (string.IsNullOrWhiteSpace(_view.Email))
            {
                _view.ShowMessage("Email is required.");
                return false;
            }

            if (!Regex.IsMatch(_view.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                _view.ShowMessage("Invalid email format.");
                return false;
            }

            // Phone number validation
            if (string.IsNullOrWhiteSpace(_view.Phone))
            {
                _view.ShowMessage("Phone number is required.");
                return false;
            }

            if (!Regex.IsMatch(_view.Phone, @"^\d{8,}$"))
            {
                _view.ShowMessage("Phone number must be numeric and at least 8 digits.");
                return false;
            }

            return true; // all checks passed
        }


        private void OnCancel()
        {
            PassengerDetailsClosed?.Invoke();  // Notify BookingPage even if canceled
            _view.CloseView();

        }
    }
}
