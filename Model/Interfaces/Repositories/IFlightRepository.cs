using System;
using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;

namespace Airport_Airplane_management_system.Model.Interfaces.Repositories
{
    public interface IFlightRepository
    {
        List<Flight> GetAllFlights();

        // NOTE: your repo returns FlightSeats, keep it consistent
        List<FlightSeats> GetSeatsForFlight(int flightId);

        int CountUpcomingFlightsNotFullyBooked();

        bool DeleteFlight(int flightId, out string error);

        Flight GetFlightById(int flightId);

        // ✅ MVP/Admin methods (aligned with service)
        bool InsertFlight(Flight flight, int planeId, out int newFlightId, out string error);

        bool UpdateFlightDates(int flightId, DateTime newDeparture, DateTime newArrival, int planeId, out string error);

        bool PlaneHasTimeConflict(int planeId, DateTime dep, DateTime arr, int? excludeFlightId, out string error);
    }
}
