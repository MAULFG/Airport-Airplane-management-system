using Airport_Airplane_management_system.Model.Core.Classes.Planes;
using System;
using System.Collections.Generic;

namespace Airport_Airplane_management_system.Model.Interfaces.Repositories
{
    public interface IPlaneRepository
    {
        List<Plane> GetAllPlanes();
        bool SetPlaneStatus(int planeId, string status, out string error);
        bool PlaneHasTimeConflict(int planeId, DateTime dep, DateTime arr, int? excludeFlightId, out string error);
    }
}
