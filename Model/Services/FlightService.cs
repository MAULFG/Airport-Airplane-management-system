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

        public FlightService(IFlightRepository flightRepo, IUserRepository userRepo,
                             IBookingRepository bookingRepo, IPlaneRepository planeRepo)
        {
            _flightRepo = flightRepo ?? throw new ArgumentNullException(nameof(flightRepo));
            _userRepo = userRepo ?? throw new ArgumentNullException(nameof(userRepo));
            _bookingRepo = bookingRepo ?? throw new ArgumentNullException(nameof(bookingRepo));
            _planeRepo = planeRepo ?? throw new ArgumentNullException(nameof(planeRepo));
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

        public int GetUpcomingFlightsNotFullyBooked() => _flightRepo.CountUpcomingFlightsNotFullyBooked();

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

        public List<Plane> GetPlanes() => _planeRepo.GetAllPlanesf();

        public Flight GetFlightById(int flightId) => _flightRepo.GetFlightById(flightId);

        // ✅ for "depends on plane chosen"
        public HashSet<string> GetSeatClassesForPlane(int planeId)
            => _flightRepo.GetSeatClassesForPlane(planeId);

        // ✅ Flight + Seats + Prices
        public bool AddFlight(
            Flight flight,
            decimal economyPrice,
            decimal businessPrice,
            decimal firstPrice,
            out int newId,
            out string error)
        {
            error = "";
            newId = -1;

            if (flight == null)
            {
                error = "Invalid flight.";
                return false;
            }

            if (flight.Plane == null)
            {
                error = "Please select a plane.";
                return false;
            }

            if (flight.Arrival <= flight.Departure)
            {
                error = "Arrival must be after departure.";
                return false;
            }

            // conflict check
            if (_flightRepo.PlaneHasTimeConflict(flight.Plane.PlaneID, flight.Departure, flight.Arrival))
            {
                error = "Plane has a scheduling conflict.";
                return false;
            }

            return _flightRepo.InsertFlightWithSeats(
                flight,
                economyPrice,
                businessPrice,
                firstPrice,
                out newId,
                out error
            );
        }

        public bool UpdateFlightDates(int flightId, DateTime dep, DateTime arr, out string error)
            => _flightRepo.UpdateFlightDates(flightId, dep, arr, out error);

        public bool PlaneHasTimeConflict(int planeId, DateTime dep, DateTime arr, int? excludeFlightId)
            => _flightRepo.PlaneHasTimeConflict(planeId, dep, arr, excludeFlightId);
        public List<Flight> SearchFlights(string from, string to, int? year = null, int? month = null, int? day = null)
        {
            var flights = _flightRepo.GetAllFlights() ?? new List<Flight>();

            if (!string.IsNullOrWhiteSpace(from))
                flights = flights.Where(f => (f.From ?? "").Trim()
                    .Equals(from.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrWhiteSpace(to))
                flights = flights.Where(f => (f.To ?? "").Trim()
                    .Equals(to.Trim(), StringComparison.OrdinalIgnoreCase)).ToList();

            // If date parts are provided, filter by that date
            if (year.HasValue && month.HasValue && day.HasValue)
            {
                DateTime date;
                try
                {
                    date = new DateTime(year.Value, month.Value, day.Value);
                }
                catch
                {
                    // invalid date -> return empty
                    return new List<Flight>();
                }

                flights = flights.Where(f => f.Departure.Date == date.Date).ToList();
            }

            return flights;
        }
        public Dictionary<string, decimal> GetSeatPricesForFlight(int flightId)
            => _flightRepo.GetSeatPricesForFlight(flightId);
        public bool UpdateSeatPricesForFlight(int flightId, decimal economy, decimal business, decimal firstOrVip, out string error)
            => _flightRepo.UpdateSeatPricesForFlight(flightId, economy, business, firstOrVip, out error);



    }
}
