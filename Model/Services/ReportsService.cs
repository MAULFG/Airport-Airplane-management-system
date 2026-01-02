using System;
using System.Collections.Generic;
using System.Linq;
using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;

namespace Airport_Airplane_management_system.Model.Services
{
    public class ReportsService
    {
        private readonly IFlightRepository _flightRepo;
        private readonly IPlaneRepository _planeRepo;
        private readonly ICrewRepository _crewRepo;

        public ReportsService(IFlightRepository flightRepo, IPlaneRepository planeRepo, ICrewRepository crewRepo)
        {
            _flightRepo = flightRepo;
            _planeRepo = planeRepo;
            _crewRepo = crewRepo;
        }

        public List<ReportItemRow> GetReportItems()
        {
            var items = new List<ReportItemRow>();

            var flights = _flightRepo.GetAllFlights() ?? new List<Flight>();
            var planes = _planeRepo.GetAllPlanes() ?? new List<Plane>();

            // =======================
            // PLANES: per-plane card (unassigned to any flight)
            // =======================
            var planeIdsWithFlights = new HashSet<int>(
                flights.Select(f => GetPlaneId(f)).Where(id => id > 0)
            );

            foreach (var p in planes)
            {
                if (p == null || p.PlaneID <= 0) continue;

                if (!planeIdsWithFlights.Contains(p.PlaneID))
                {
                    items.Add(new ReportItemRow
                    {
                        Title = $"Plane #{p.PlaneID} not assigned to any flight",
                        SubTitle = $"{p.Model} has no flights linked.",
                        BadgeText = "Plane",
                        IsWarning = true,
                        TargetPageKey = "PlaneManagement",
                        PlaneId = p.PlaneID
                    });
                }
            }

            // =======================
            // CREW: per-crew card
            // =======================
            var crewNotAssigned = _crewRepo.GetCrewNotAssignedToAnyFlight() ?? new List<Crew>();
            foreach (var c in crewNotAssigned)
            {
                items.Add(new ReportItemRow
                {
                    Title = $"{c.FullName} not assigned to any flight",
                    SubTitle = $"{c.Role} • {c.EmployeeId}",
                    BadgeText = "Crew",
                    IsWarning = true,
                    TargetPageKey = "CrewManagement",
                    CrewEmployeeId = c.EmployeeId
                });
            }

            var crewPast = _crewRepo.GetCrewAssignedToPastFlights() ?? new List<Crew>();
            foreach (var c in crewPast)
            {
                items.Add(new ReportItemRow
                {
                    Title = $"{c.FullName} assigned to a past flight",
                    SubTitle = $"{c.Role} • {c.EmployeeId}",
                    BadgeText = "Crew",
                    IsWarning = false,
                    TargetPageKey = "CrewManagement",
                    CrewEmployeeId = c.EmployeeId
                });
            }

            // warnings first
            return items.OrderByDescending(x => x.IsWarning).ThenBy(x => x.BadgeText).ThenBy(x => x.Title).ToList();
        }

        private static int GetPlaneId(Flight f)
        {
            if (f == null) return 0;
            if (f.PlaneIDFromDb > 0) return f.PlaneIDFromDb;
            if (f.PlaneIDFromDb > 0) return f.PlaneIDFromDb;
            return 0;
        }
    }
}
