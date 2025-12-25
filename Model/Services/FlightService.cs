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
            // 1. Load planes
            var planes = _planeRepo.GetAllPlanes();

            // 2. Load flights
            var flights = _flightRepo.GetAllFlights();

            // 3. Load seats and assign passengers
            var users = _userRepo.GetAllUsers();

            foreach (var flight in flights)
            {
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

            return flights; // ready to pass to presenter/view
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
            int? day = null)
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
