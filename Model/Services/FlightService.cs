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
        public FlightService(
            IFlightRepository flightRepo,
            IUserRepository userRepo,
            IBookingRepository bookingRepo, IPlaneRepository planeRepo)
        {
            _flightRepo = flightRepo;
            _userRepo = userRepo;
            _bookingRepo = bookingRepo;
            _planeRepo = planeRepo;
        }

        // -----------------------------
        // FLIGHTS
        // -----------------------------
        public List<Flight> LoadFlightsWithSeats()
        {
            // 1️⃣ Load all planes from repository
            var planes = _planeRepo.GetAllPlanesf();

            // 2️⃣ Load all flights
            var flights = _flightRepo.GetAllFlights();

            // 3️⃣ Load all users (for seat assignment)
            var users = _userRepo.GetAllUsers();

            foreach (var flight in flights)
            {
                // --- Assign the correct Plane object ---
                // Make sure Flight class has a property PlaneIDFromDb that holds the plane_id from DB
                flight.Plane = planes.FirstOrDefault(p => p.PlaneID == flight.PlaneIDFromDb);

                // --- Load seats for this flight ---
                flight.FlightSeats.Clear();
                var seats = _flightRepo.GetSeatsForFlight(flight.FlightID);

                foreach (var seat in seats)
                {
                    // Assign passenger if booked
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
                if (seat.UserId.HasValue)
                {
                    var passenger = users.FirstOrDefault(u => u.UserID == seat.UserId.Value);
                    if (passenger != null)
                        seat.AssignPassenger(passenger);
                }

                flight.FlightSeats.Add(seat);
            }
        }

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







        public int GetUpcomingFlightsNotFullyBooked()
        {
            return _flightRepo.CountUpcomingFlightsNotFullyBooked();
        }

        // -----------------------------
        // CANCEL FLIGHT (PROFESSIONAL)
        // -----------------------------

        public bool CancelFlight(int flightID, out string error)
        {
            error = "";

            // 1) Get booking IDs
            var bookingIds = _bookingRepo.GetActiveBookingIdsForFlight(flightID);

            // 2) Cancel bookings in DB
            foreach (var bookingId in bookingIds)
            {
                if (!_bookingRepo.CancelBooking(bookingId, out error))
                    return false;
            }

            // 3) Delete flight
            if (!_flightRepo.DeleteFlight(flightID, out error))
                return false;

            return true;
        }


    }
}
