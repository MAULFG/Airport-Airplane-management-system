using Airport_Airplane_management_system.Model.Core.Classes;
using System;
using System.Collections.Generic;

namespace Airport_Airplane_management_system.Model.Interfaces.Repositories
{
    public interface IPlaneRepository
    {
        List<Plane> GetAllPlanesf();
        List<Plane> GetAllPlanes();

        bool SetPlaneStatus(int planeId, string status, out string error);
        bool PlaneHasTimeConflict(int planeId, DateTime dep, DateTime arr, int? excludeFlightId, out string error);
        int AddPlane(string model, string type, string status, out string error);
        List<Seat> GetSeatsByPlaneId(int planeId);
        bool InsertSeats(int planeId, List<Seat> seats, out string error);
        bool DeletePlane(int planeId, out string error);
        int CountPlanesNotAssignedToAnyFlight();
    }
}
