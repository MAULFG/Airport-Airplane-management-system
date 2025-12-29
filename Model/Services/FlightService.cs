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
        public FlightService(IFlightRepository flightRepo, IUserRepository userRepo,IBookingRepository bookingRepo,IPlaneRepository planeRepo, IAppSession session) 
        {
            _flightRepo = flightRepo;
            _userRepo = userRepo;
            _bookingRepo = bookingRepo;
            _planeRepo = planeRepo;
            _session = session; 
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
