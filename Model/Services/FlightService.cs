using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airport_Airplane_management_system.Model.Services
{
    public class FlightService
    {
        private readonly IFlightRepository _flightRepo;
        private readonly IUserRepository _userRepo;
        private readonly IBookingRepository _bookingRepo;
        private readonly IPlaneRepository _planeRepo;

        public FlightService(IFlightRepository flightRepo, IUserRepository userRepo, IBookingRepository bookingRepo, IPlaneRepository planeRepo)
        {
            _flightRepo = flightRepo;
            _userRepo = userRepo;
            _bookingRepo = bookingRepo;
            _planeRepo = planeRepo;
        }

        public List<Flight> LoadFlightsWithSeats()
        {
            var planes = _planeRepo.GetAllPlanesf();
            var flights = _flightRepo.GetAllFlights();
            var users = _userRepo.GetAllUsers();

            foreach (var flight in flights)
            {
                flight.Plane = planes.FirstOrDefault(p => p.PlaneID == flight.PlaneIDFromDb);

                flight.FlightSeats.Clear();
                var seats = _flightRepo.GetSeatsForFlight(flight.FlightID);

                foreach (var seat in seats)
                {
                    if (seat.UserId.HasValue)
                    {
                        var passenger = users.FirstOrDefault(u => u.UserID == seat.UserId.Value);
                        if (passenger != null)
                            seat.AssignPassenger(passenger);
                    }

                    flight.FlightSeats.Add(seat);
                }
            }

            return flights;
        }

        public List<Flight> GetFlights() => _flightRepo.GetAllFlights();

        public void LoadSeatsForFlight(Flight flight)
        {
            if (flight == null) return;

            flight.FlightSeats.Clear();

            var seats = _flightRepo.GetSeatsForFlight(flight.FlightID);
            var users = _userRepo.GetAllUsers();

            foreach (var seat in seats)
            {
                if (seat.UserId.HasValue)
                {
                    var passenger = users.FirstOrDefault(u => u.UserID == seat.UserId.Value);
                    if (passenger != null)
                        seat.AssignPassenger(passenger);
                }

                flight.FlightSeats.Add(seat);
            }
        }

        public List<Flight> SearchFlights(string from = null, string to = null, int? year = null, int? month = null, int? day = null)
        {
            var flights = _flightRepo.GetAllFlights();
            var query = flights.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(from))
                query = query.Where(f => f.From.Equals(from, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(to))
                query = query.Where(f => f.To.Equals(to, StringComparison.OrdinalIgnoreCase));

            if (year.HasValue)
                query = query.Where(f => f.Departure.Year == year.Value);

            if (month.HasValue)
                query = query.Where(f => f.Departure.Month == month.Value);

            if (day.HasValue)
                query = query.Where(f => f.Departure.Day == day.Value);

            return query.ToList();
        }

        public int GetUpcomingFlightsNotFullyBooked() => _flightRepo.CountUpcomingFlightsNotFullyBooked();

        // ✅ NEW: admin add
        public bool AddFlight(Flight flight, int planeId, out string error)
        {
            error = "";

            if (flight == null) { error = "Flight is null."; return false; }
            if (string.IsNullOrWhiteSpace(flight.From) || string.IsNullOrWhiteSpace(flight.To))
            {
                error = "From/To are required.";
                return false;
            }
            if (flight.Arrival <= flight.Departure)
            {
                error = "Arrival must be after Departure.";
                return false;
            }

            if (_flightRepo.PlaneHasTimeConflict(planeId, flight.Departure, flight.Arrival, excludeFlightId: null, out var conflictErr))
            {
                error = string.IsNullOrWhiteSpace(conflictErr)
                    ? "This plane already has a flight overlapping that time."
                    : conflictErr;
                return false;
            }

            return _flightRepo.InsertFlight(flight, planeId, out _, out error);
        }

        // ✅ NEW: admin update
        public bool UpdateFlightDates(int flightId, DateTime departure, DateTime arrival, int planeId, out string error)
        {
            error = "";

            if (arrival <= departure)
            {
                error = "Arrival must be after Departure.";
                return false;
            }

            if (_flightRepo.PlaneHasTimeConflict(planeId, departure, arrival, excludeFlightId: flightId, out var conflictErr))
            {
                error = string.IsNullOrWhiteSpace(conflictErr)
                    ? "This plane already has a flight overlapping that time."
                    : conflictErr;
                return false;
            }

            return _flightRepo.UpdateFlightDates(flightId, departure, arrival, planeId, out error);
        }

        public bool DeleteFlight(int flightId, out string error) => _flightRepo.DeleteFlight(flightId, out error);

        public bool CancelFlight(int flightID, out string error)
        {
            error = "";

            var bookingIds = _bookingRepo.GetActiveBookingIdsForFlight(flightID);

            foreach (var bookingId in bookingIds)
            {
                if (!_bookingRepo.CancelBooking(bookingId, out error))
                    return false;
            }

            if (!_flightRepo.DeleteFlight(flightID, out error))
                return false;

            return true;
        }
    }
}
