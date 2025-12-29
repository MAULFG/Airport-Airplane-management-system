using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.View.Interfaces;
using System;
using System.Linq;

namespace Airport_Airplane_management_system.Presenter
{
    public class BookingPresenter
    {
        private readonly IBookingView _view;
        private readonly FlightService _flightService;
        private readonly BookingService _bookingService;

        private Flight _flight;
        private FlightSeats _selectedSeat;

        public BookingPresenter(IBookingView view, FlightService flightService, int flightId)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _flightService = flightService ?? throw new ArgumentNullException(nameof(flightService));

            _view.SeatSelected += OnSeatSelected;
            _view.ConfirmBookingClicked += OnConfirmBooking;

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
            decimal price = _flight.GetSeatPrice(seat.ClassType);

            _view.ShowSelectedSeat(seat, price);
           
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

            // Reload seats after booking
            LoadFlight(_flight.FlightID);
        }
    }
}
