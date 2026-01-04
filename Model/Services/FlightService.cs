using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Exceptions;
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
        private readonly IAppSession _session;

        public FlightService(
            IFlightRepository flightRepo,
            IUserRepository userRepo,
            IBookingRepository bookingRepo,
            IPlaneRepository planeRepo,
            IAppSession session)
        {
            _flightRepo = flightRepo;
            _userRepo = userRepo;
            _bookingRepo = bookingRepo;
            _planeRepo = planeRepo;
            _session = session;
        }

        // -----------------------------
        // PRELOAD / SESSION
        // -----------------------------
        public void Preload()
        {
            if (_session.Flights != null)
                return;

            var planes = _planeRepo.GetAllPlanesf();
            var flights = LoadFlightsWithSeats();

            _session.SetPlanes(planes);
            _session.SetFlights(flights);
        }

        // -----------------------------
        // FLIGHTS
        // -----------------------------
        public List<Flight> LoadFlightsWithSeats()
        {
            if (_session.Flights != null)
                return _session.Flights;

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
                    if (seat.PassengerId.HasValue)
                    {
                        var passenger = users.FirstOrDefault(u => u.UserID == seat.PassengerId.Value);
                        if (passenger != null)
                            seat.AssignPassenger(passenger);
                    }
                    flight.FlightSeats.Add(seat);
                }
            }

            _session.SetPlanes(planes);
            _session.SetFlights(flights);
            return flights;
        }

        public Flight GetFlightById(int flightId)
            => _flightRepo.GetFlightById(flightId);

        public List<Flight> GetFlights()
            => _flightRepo.GetAllFlights();

        public List<Plane> GetPlanes()
            => _planeRepo.GetAllPlanesf();

        // -----------------------------
        // SEATS / CLASSES
        // -----------------------------

        // ✅ Correct meaning: classes depend on PLANE
        public HashSet<string> GetSeatClassesForPlane(int planeId)
            => _flightRepo.GetSeatClassesForPlane(planeId);
        public bool PlaneHasTimeConflict(int planeId, DateTime dep, DateTime arr, int? excludeFlightId)
    => _flightRepo.PlaneHasTimeConflict(planeId, dep, arr, excludeFlightId);



        public Dictionary<string, decimal> GetSeatPricesForFlight(int flightId)
            => _flightRepo.GetSeatPricesForFlight(flightId);

        public bool UpdateSeatPricesForFlight(
            int flightId,
            decimal economy,
            decimal business,
            decimal firstOrVip,
            out string error)
            => _flightRepo.UpdateSeatPricesForFlight(
                flightId, economy, business, firstOrVip, out error);

        // -----------------------------
        // ADD FLIGHT
        // -----------------------------
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

            if (_flightRepo.PlaneHasTimeConflict(
                flight.Plane.PlaneID,
                flight.Departure,
                flight.Arrival))
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
                out error);
        }

        // -----------------------------
        // UPDATE FLIGHT (NO PLANE CHANGE)
        // -----------------------------
        public bool UpdateFlightDates(
            int flightId,
            DateTime dep,
            DateTime arr,
            out string error)
            => _flightRepo.UpdateFlightDates(flightId, dep, arr, out error);

        // -----------------------------
        // UPDATE FLIGHT (PLANE CHANGED)
        // -----------------------------
        public bool UpdateFlightWithPlaneAndSeats(
            int flightId,
            int newPlaneId,
            string fromCity,
            string toCity,
            DateTime departure,
            DateTime arrival,
            decimal economyPrice,
            decimal businessPrice,
            decimal firstOrVipPrice,
            out string error)
        {
            return _flightRepo.UpdateFlightWithPlaneAndSeats(
                flightId,
                newPlaneId,
                fromCity,
                toCity,
                departure,
                arrival,
                economyPrice,
                businessPrice,
                firstOrVipPrice,
                out error);
        }

        // -----------------------------
        // DELETE / CANCEL
        // -----------------------------
        public bool CancelFlight(int flightID, out string error)
        {
            error = "";

            var bookingIds = _bookingRepo.GetActiveBookingIdsForFlight(flightID);
            foreach (var bookingId in bookingIds)
            {
                if (!_bookingRepo.CancelBooking(bookingId, out error))
                    return false;
            }

            return _flightRepo.DeleteFlight(flightID, out error);
        }

        // -----------------------------
        // SEARCH / FILTER
        // -----------------------------
        public List<Flight> SearchFlights(
            string from = null,
            string to = null,
            int? year = null,
            int? month = null,
            int? day = null,
            string seatClass = null,
            int? passengers = null)
        {
            var flights = LoadFlightsWithSeats();
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

            if (!string.IsNullOrWhiteSpace(seatClass) && passengers.HasValue)
            {
                query = query.Where(f =>
                    f.GetAvailableSeats(seatClass).Count >= passengers.Value);
            }

            return query.ToList();
        }
    }
}
