using System;
using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes.Flights;

public interface IUpcomingFlightsView
{
    event EventHandler LoadFlightsRequested;
    event EventHandler<int> FlightSelected;

    void DisplayFlights(IEnumerable<Flight> flights);
    void ClearFlights();
    void ShowError(string message);

    // Method to trigger loading when panel is shown
    void RefreshFlights();
}
