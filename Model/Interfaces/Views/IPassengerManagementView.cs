using System;
using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface IPassengerManagementView
    {
        // Events (Form -> Presenter)
        event Action ViewLoaded;
        event Action<string> SearchChanged;
        event Action<int> PassengerToggleRequested;      // passengerId
        event Action<int, int> CancelBookingRequested;   // bookingId, passengerId

        // Methods (Presenter -> Form)
        void SetHeaderCounts(int totalPassengers, int upcomingFlightsNotFullyBooked);
        void RenderPassengers(List<PassengerSummaryRow> passengers);
        void ShowPassengerBookings(int passengerId, List<PassengerBookingRow> upcoming, List<PassengerBookingRow> past);
        void ClearView();

        bool Confirm(string message, string title);
        void ShowError(string message);
        void ShowInfo(string message);
    }
}
