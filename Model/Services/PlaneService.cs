using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airport_Airplane_management_system.Model.Services
{
    public class PlaneService
    {
        private readonly IPlaneRepository _planeRepo;
        private readonly IFlightRepository _flightRepo; // needed to sync plane status
        private List<Plane> _planes;

        public PlaneService(IPlaneRepository planeRepo, IFlightRepository flightRepo)
        {
            _planeRepo = planeRepo;
            _flightRepo = flightRepo;
            _planes = new List<Plane>();
        }

        public List<Plane> GetPlanes() => _planes;

        public void LoadPlanes()
        {
            _planes = _planeRepo.GetAllPlanes();
            if (!_planes.Any())
            {
                var dummy = new MidRangeA320(-1, "Available");
                dummy.GenerateSeats();
                _planes.Add(dummy);
            }
        }

        public void SyncPlaneStatusesWithFlights()
        {
            var flights = _flightRepo.GetAllFlights();
            var now = DateTime.Now;

            var busyPlaneIds = flights
                .Where(f => f.Plane != null && now < f.Arrival)
                .Select(f => f.Plane.PlaneID)
                .Distinct()
                .ToHashSet();

            foreach (var p in _planes)
            {
                string desiredStatus = busyPlaneIds.Contains(p.PlaneID) ? "inactive" : "active";
                if (!string.Equals(p.Status, desiredStatus, StringComparison.OrdinalIgnoreCase))
                {
                    _planeRepo.SetPlaneStatus(p.PlaneID, desiredStatus, out _);
                    p.Status = desiredStatus;
                }
            }
        }
    }
}
