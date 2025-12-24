using System;
using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;

namespace Airport_Airplane_management_system.View.Interfaces
{
    public interface IUpcomingFlightsView
    {
        event Action ViewLoaded;
        event Action<Flight> FlightSelected;

        void ShowFlights(IEnumerable<Flight> flights);
        void ShowError(string message);
    }
}
