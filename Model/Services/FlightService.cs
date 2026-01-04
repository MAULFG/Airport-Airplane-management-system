using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using MySqlX.XDevAPI;
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
        private readonly NotificationWriterService _notifWriter;
        public FlightService(IFlightRepository flightRepo, IUserRepository userRepo,IBookingRepository bookingRepo,IPlaneRepository planeRepo, IAppSession session, NotificationWriterService notifWriter) 
        {
            _flightRepo = flightRepo;
            _userRepo = userRepo;
            _bookingRepo = bookingRepo;
            _planeRepo = planeRepo;
            _session = session;
            _notifWriter = notifWriter;
        }

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

        public Flight GetFlightById(int flightId) => _flightRepo.GetFlightById(flightId);

        public List<Flight> GetFlights()
        {
            return _flightRepo.GetAllFlights();
        }

        public void LoadSeatsForFlight(Flight flight)
        {
            if (flight == null) return;

            flight.FlightSeats.Clear();

            var seats = _flightRepo.GetSeatsForFlight(flight.FlightID);
            var users = _userRepo.GetAllUsers();

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
        public int GetUpcomingFlightsNotFullyBooked() => _flightRepo.CountUpcomingFlightsNotFullyBooked();
        public bool CancelFlight(int flightID, out string error)
        {
            error = "";

            // ✅ notify all users with bookings on this flight (NO bookingId => no See Ticket)
            var userIds = _bookingRepo.GetUserIdsForFlight(flightID);
            foreach (var uid in userIds.Distinct())
                _notifWriter.NotifyFlightCancelledByAdmin(uid, flightID);

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

 

        // ✅ for "depends on plane chosen"
        public HashSet<string> GetSeatClassesForFlight(int planeId)
            => _flightRepo.GetSeatClassesForFlight(planeId);
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
        public bool PlaneHasTimeConflict(int planeId, DateTime dep, DateTime arr, int? excludeFlightId)
            => _flightRepo.PlaneHasTimeConflict(planeId, dep, arr, excludeFlightId);
        //  public bool UpdateFlightDates(int flightId, DateTime dep, DateTime arr, out string error)
        //    => _flightRepo.UpdateFlightDates(flightId, dep, arr, out error);

        public bool UpdateFlightDates(int flightId, DateTime dep, DateTime arr, out string error)
        {
            error = "";

            var oldFlight = _flightRepo.GetFlightById(flightId);
            if (oldFlight == null)
            {
                error = "Flight not found.";
                return false;
            }

            bool depChanged = oldFlight.Departure != dep;
            bool arrChanged = oldFlight.Arrival != arr;

            if (!_flightRepo.UpdateFlightDates(flightId, dep, arr, out error))
                return false;

            // notify ONLY if something changed
            if (depChanged || arrChanged)
            {
                var userIds = _bookingRepo.GetUserIdsForFlight(flightId);
                foreach (var uid in userIds.Distinct())
                {
                    _notifWriter.NotifyFlightDatesUpdated(
                        uid,
                        flightId,
                        depChanged ? dep : (DateTime?)null,
                        arrChanged ? arr : (DateTime?)null
                    );
                }
            }

            return true;
        }







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



    
        public List<Flight> SearchFlights(
            string from = null,
            string to = null,
            int? year = null,
            int? month = null,
            int? day = null,
            string seatClass = null,
            int? passengers = null)
        {
            // Load all flights with seats assigned
            var flights = LoadFlightsWithSeats();
            var query = flights.AsEnumerable();

            // Filter FROM
            if (!string.IsNullOrWhiteSpace(from))
                query = query.Where(f => f.From.Equals(from, StringComparison.OrdinalIgnoreCase));

            // Filter TO
            if (!string.IsNullOrWhiteSpace(to))
                query = query.Where(f => f.To.Equals(to, StringComparison.OrdinalIgnoreCase));

            // Filter DATE
            if (year.HasValue)
                query = query.Where(f => f.Departure.Year == year.Value);
            if (month.HasValue)
                query = query.Where(f => f.Departure.Month == month.Value);
            if (day.HasValue)
                query = query.Where(f => f.Departure.Day == day.Value);

            // Filter by class + passengers
            if (!string.IsNullOrWhiteSpace(seatClass) && passengers.HasValue)
            {
                query = query.Where(f => f.GetAvailableSeats(seatClass).Count >= passengers.Value);
            }

            return query.ToList();
        }







      

    }
}
