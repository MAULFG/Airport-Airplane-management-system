using Airport_Airplane_management_system.Model.Core.Classes;

using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Model.Interfaces.Repositories
{
    public interface IFlightRepository
    {
        
        List<Flight> GetAllFlights();
        Flight GetFlightById(int flightId);
        List<FlightSeats> GetSeatsForFlight(int flightId);
      

        int CountUpcomingFlightsNotFullyBooked();

        bool InsertFlight(Flight flight, out int newFlightId, out string error);

        // ✅ flight + create seats + set seat_price based on class_type
        bool InsertFlightWithSeats(
            Flight flight,
            decimal economyPrice,
            decimal businessPrice,
            decimal firstPrice,
            out int newFlightId,
            out string error
        );

        // ✅ used to decide which price rows to show depending on selected plane
        HashSet<string> GetSeatClassesForPlane(int planeId);

        Dictionary<string, decimal> GetSeatPricesForFlight(int flightId);
        bool UpdateSeatPricesForFlight(int flightId, decimal economy, decimal business, decimal firstOrVip, out string error);


        bool UpdateFlightDates(int flightId, DateTime newDeparture, DateTime newArrival, out string error);
        bool DeleteFlight(int flightId, out string error);

        bool PlaneHasTimeConflict(int planeId, DateTime dep, DateTime arr, int? excludeFlightId = null);
    }


}
