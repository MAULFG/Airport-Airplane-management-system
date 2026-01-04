using Airport_Airplane_management_system.Model.Core.Classes;
using System;
using System.Collections.Generic;

namespace Airport_Airplane_management_system.Model.Interfaces.Repositories
{
    public interface IFlightRepository
    {
        // -----------------------------
        // FLIGHTS
        // -----------------------------
        List<Flight> GetAllFlights();
        Flight GetFlightById(int flightId);

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

        bool UpdateFlightDates(int flightId, DateTime newDeparture, DateTime newArrival, out string error);
        bool DeleteFlight(int flightId, out string error);

        bool PlaneHasTimeConflict(int planeId, DateTime dep, DateTime arr, int? excludeFlightId = null);

        // ✅ FULL update when plane changes: update flight + rebuild flight_seats (transaction)
        bool UpdateFlightWithPlaneAndSeats(
            int flightId,
            int newPlaneId,
            string fromCity,
            string toCity,
            DateTime departure,
            DateTime arrival,
            decimal economyPrice,
            decimal businessPrice,
            decimal firstOrVipPrice,
            out string error
        );

        // -----------------------------
        // SEATS (FLIGHT)
        // -----------------------------
        List<FlightSeats> GetSeatsForFlight(int flightId);

        // ✅ classes from flight_seats (what the flight currently has)
        HashSet<string> GetSeatClassesForFlight(int flightId);

        Dictionary<string, decimal> GetSeatPricesForFlight(int flightId);
        bool UpdateSeatPricesForFlight(int flightId, decimal economy, decimal business, decimal firstOrVip, out string error);

        // -----------------------------
        // SEATS (PLANE)  ✅ MISSING BEFORE
        // -----------------------------
        // ✅ classes from seats table for a plane (used when selecting/changing plane)
        HashSet<string> GetSeatClassesForPlane(int planeId);
    }
}