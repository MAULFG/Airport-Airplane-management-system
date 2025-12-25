using System;
using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;

public interface IUpcomingFlightsView
{
    event EventHandler LoadFlightsRequested;
    event EventHandler<FlightEventArgs> FlightGoClicked;

    void DisplayFlights(IEnumerable<Flight> flights);
    void ShowError(string message);
}
public class FlightEventArgs : EventArgs
{
    public Flight Flight { get; }

    public FlightEventArgs(Flight flight)
    {
        Flight = flight;
    }
}