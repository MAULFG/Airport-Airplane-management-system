using Airport_Airplane_management_system.Model.Core.Classes;
using Guna.UI2.WinForms;
using System;

namespace Airport_Airplane_management_system.View.Interfaces
{
    public interface IPassengerDetailsView
    {
        // Events
        event Action CompleteBookingClicked;
        event Action CancelClicked;

        string FirstName { get; }
        string MiddleName { get; }
        string LastName { get; }
        string FullName { get; }
        string Email { get; }
        string Phone { get; }

        // Display seat & price (from BookingPresenter)
        void ShowSelectedSeat(FlightSeats seat);
        void ShowPrice(decimal basePrice, decimal tax, decimal total);

        // UI feedback
        void ShowMessage(string message);
        void CloseView();
    }
}
