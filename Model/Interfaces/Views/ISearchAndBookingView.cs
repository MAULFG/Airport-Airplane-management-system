using System;
using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface ISearchAndBookingView
    {
        string From { get; }
        string To { get; }
        DateTime? DepartureDate { get; } // <-- nullable
        int Passengers { get; }
        string Class { get; }
        bool IsDateSelected { get; }

        event EventHandler SearchClicked;
        event Action<int> BookFlightRequested;
        void DisplayFlights(List<Flight> flights);

        void ShowMessage(string message);
    }
}
