using Airport_Airplane_management_system.Model.Core.Classes;
using System;
using System.Collections.Generic;

namespace Airport_Airplane_management_system.View.Interfaces
{
    public interface IBookingView
    {
        // Display
        void ShowSeats(Flight flight);
        void ShowFlightInfo(Flight flight);
        
        void ShowSelectedSeat(FlightSeats seat, decimal price);
        void ShowPrice(decimal price);
        void ShowMessage(string message);

        // Events (View → Presenter)
        event Action<FlightSeats> SeatSelected;
        event Action ConfirmBookingClicked;
    }
}
