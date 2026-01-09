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
        private readonly IFlightRepository flightRepo;
        private readonly IPlaneRepository planeRepo;
        private readonly ICrewRepository crewRepo;
        private readonly IPassengerRepository passRepo;

        public MainAPresenter(
            IMainAView view,
            IFlightRepository flightRepo,
            IPlaneRepository planeRepo,
            ICrewRepository crewRepo,
            IPassengerRepository passRepo)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            this.flightRepo = flightRepo ?? throw new ArgumentNullException(nameof(flightRepo));
            this.planeRepo = planeRepo ?? throw new ArgumentNullException(nameof(planeRepo));
            this.crewRepo = crewRepo ?? throw new ArgumentNullException(nameof(crewRepo));
            this.passRepo = passRepo ?? throw new ArgumentNullException(nameof(passRepo));
        }

        public void RefreshData()
        {
            var flights = InvokeList<Flight>(flightRepo, "GetAllFlights", "GetFlights", "GetAll") ?? new List<Flight>();
            var planes = InvokeList<Plane>(planeRepo, "GetAllPlanes", "GetPlanes", "GetAll") ?? new List<Plane>();
            var crew = InvokeList<Crew>(crewRepo, "GetAllCrew", "GetAllCrewMembers", "GetCrew", "GetAll") ?? new List<Crew>();

            var passengerSummary = passRepo.GetPassengersSummary();
            int totalPassengers = passengerSummary?.Count ?? 0;

            var now = DateTime.Now;

            int inAir = flights.Count(f => f.Departure <= now && now < f.Arrival);
            int upcoming = flights.Count(f => f.Departure > now);
            int past = flights.Count(f => f.Arrival <= now);

            int totalPlanes = planes.Count;
            int activePlanes = planes.Count(IsPlaneActive);
            int inactivePlanes = totalPlanes - activePlanes;

            var planeIdsWithAnyFlight = new HashSet<int>(
                flights.Select(GetPlaneIdFromFlight).Where(id => id > 0)
            );

            int planesNotAssignedToAnyFlight = planes.Count(p =>
                p != null && p.PlaneID > 0 && !planeIdsWithAnyFlight.Contains(p.PlaneID)
            );

            int assigned = crew.Count(c => c.FlightId.HasValue);
            int unassigned = crew.Count - assigned;

            var flightById = flights
                .Select(f => new { Flight = f, Id = GetFlightId(f) })
                .Where(x => x.Id > 0)
                .GroupBy(x => x.Id)
                .ToDictionary(g => g.Key, g => g.First().Flight);

            int crewAssignedToPastFlights = crew.Count(c =>
            {
                if (c?.FlightId == null) return false;
                if (!flightById.TryGetValue(c.FlightId.Value, out var f)) return false;
                return f.Arrival <= now;
            });

            int alertsCount = 0;
            if (unassigned > 0) alertsCount++;
            if (inactivePlanes > 0) alertsCount++;
            if (crewAssignedToPastFlights > 0) alertsCount++;
            if (planesNotAssignedToAnyFlight > 0) alertsCount++;

            _view.ShowKpis(new MainAKpiDto
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
            });

            _view.ShowFlights(new MainAFlightsDto
            {
                Now = now,
                InAir = flights.Where(f => f.Departure <= now && now < f.Arrival)
                               .OrderBy(f => f.Departure).Take(3).ToList(),
                Upcoming48h = flights.Where(f => f.Departure > now && f.Departure <= now.AddHours(48))
                                     .OrderBy(f => f.Departure).Take(8).ToList(),
                Past = flights.Where(f => f.Arrival <= now)
                              .OrderByDescending(f => f.Arrival).Take(5).ToList(),
            });

            _view.ShowAlerts(new MainAAlertsDto
            {
                UnassignedCrew = unassigned,
                InactivePlanes = inactivePlanes,
                CrewAssignedToPastFlights = crewAssignedToPastFlights,
                PlanesNotAssignedToAnyFlight = planesNotAssignedToAnyFlight,
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
            if (string.IsNullOrWhiteSpace(p.Status)) return false;
            return p.Status.Trim().Equals("Active", StringComparison.OrdinalIgnoreCase);
        }

        private static int GetFlightId(Flight f)
        {
            if (f == null) return 0;
            var idObj = ReadAny(f, "FlightID", "Id", "ID", "id");
            return idObj == null ? 0 : ToInt(idObj);
        }

        private static int GetPlaneIdFromFlight(Flight f)
        {
            if (f == null) return 0;
            var pidObj = ReadAny(f, "PlaneIDFromDb", "PlaneID", "plane_id", "PlaneId", "planeId");
            return pidObj == null ? 0 : ToInt(pidObj);
        }

        private static int ToInt(object v)
        {
            try
            {
                if (v is int i) return i;
                if (v is long l) return (int)l;
                return Convert.ToInt32(v);
            }
            catch { return 0; }
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
