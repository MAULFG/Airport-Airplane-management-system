using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Airport_Airplane_management_system.Presenter.AdminPages
{
    public class MainAPresenter
    {
        private readonly IMainAView _view;
        private readonly IFlightRepository _flightRepo;
        private readonly IPlaneRepository _planeRepo;
        private readonly ICrewRepository _crewRepo;
        private readonly IPassengerRepository _passengerRepo;
        private readonly IBookingRepository _bookingRepo;

        public MainAPresenter(
            IMainAView view,
            IFlightRepository flightRepo,
            IPlaneRepository planeRepo,
            ICrewRepository crewRepo,
            IPassengerRepository passengerRepo,
            IBookingRepository bookingRepo)
        {
            _view = view;
            _flightRepo = flightRepo;
            _planeRepo = planeRepo;
            _crewRepo = crewRepo;
            _passengerRepo = passengerRepo;
            _bookingRepo = bookingRepo;

            Load();
        }

        public void Load()
        {
            var flights = InvokeList<Flight>(_flightRepo, "GetAllFlights", "GetFlights", "GetAll") ?? new List<Flight>();
            var planes = InvokeList<Plane>(_planeRepo, "GetAllPlanes", "GetPlanes", "GetAll") ?? new List<Plane>();
            var crew = InvokeList<Crew>(_crewRepo, "GetAllCrew", "GetAllCrewMembers", "GetCrew", "GetAll") ?? new List<Crew>();

            var passengerSummary = _passengerRepo.GetPassengersSummary();
            int totalPassengers = passengerSummary?.Count ?? 0;

            var now = DateTime.Now;

            int inAir = flights.Count(f => f.Departure <= now && now < f.Arrival);
            int upcoming = flights.Count(f => f.Departure > now);
            int past = flights.Count(f => f.Arrival <= now);

            int totalPlanes = planes.Count;
            int activePlanes = planes.Count(IsPlaneActive);    // ✅ centralized logic
            int inactivePlanes = totalPlanes - activePlanes;

            int assigned = crew.Count(c => c.FlightId.HasValue);
            int unassigned = crew.Count - assigned;

            int alertsCount = 0;
            if (unassigned > 0) alertsCount++;
            if (inactivePlanes > 0) alertsCount++;

            var kpi = new MainAKpiDto
            {
                TotalFlights = flights.Count,
                FlightsInAir = inAir,
                FlightsUpcoming = upcoming,
                FlightsPast = past,

                TotalPlanes = totalPlanes,
                ActivePlanes = activePlanes,
                InactivePlanes = inactivePlanes,

                TotalCrew = crew.Count,
                CrewAssigned = assigned,
                CrewUnassigned = unassigned,

                TotalPassengers = totalPassengers,
                PassengersUpcoming = passengerSummary?.Sum(p => p.UpcomingCount) ?? 0,
                PassengersPast = passengerSummary?.Sum(p => p.PastCount) ?? 0,

                ActiveAlerts = alertsCount,
                OpsText = flights.Count == 0 ? "0.0%" : "100.0%"
            };

            _view.ShowKpis(kpi);

            var flightsDto = new MainAFlightsDto
            {
                Now = now,
                InAir = flights.Where(f => f.Departure <= now && now < f.Arrival)
                               .OrderBy(f => f.Departure).Take(3).ToList(),
                Upcoming48h = flights.Where(f => f.Departure > now && f.Departure <= now.AddHours(48))
                                     .OrderBy(f => f.Departure).Take(8).ToList(),
                Past = flights.Where(f => f.Arrival <= now)
                              .OrderByDescending(f => f.Arrival).Take(5).ToList(),
            };

            _view.ShowFlights(flightsDto);

            _view.ShowAlerts(new MainAAlertsDto
            {
                UnassignedCrew = unassigned,
                InactivePlanes = inactivePlanes,
                ActiveAlerts = alertsCount
            });
        }

        private static List<T>? InvokeList<T>(object repo, params string[] methods)
        {
            if (repo == null) return null;

            foreach (var m in methods)
            {
                var mi = repo.GetType().GetMethod(m, BindingFlags.Public | BindingFlags.Instance);
                if (mi == null) continue;

                var res = mi.Invoke(repo, null);
                if (res is List<T> list) return list;
                if (res is IEnumerable<T> en) return en.ToList();
            }
            return null;
        }

        private static bool IsPlaneActive(Plane p)
        {
            if (p == null) return false;

            if (string.IsNullOrWhiteSpace(p.Status))
                return false;

            return p.Status.Trim()
                           .Equals("Active", StringComparison.OrdinalIgnoreCase);
        }



        private static object? ReadAny(object obj, params string[] props)
        {
            foreach (var p in props)
            {
                var pi = obj.GetType().GetProperty(p, BindingFlags.Public | BindingFlags.Instance);
                if (pi == null) continue;
                var v = pi.GetValue(obj);
                if (v != null) return v;
            }
            return null;
        }
    }
}
