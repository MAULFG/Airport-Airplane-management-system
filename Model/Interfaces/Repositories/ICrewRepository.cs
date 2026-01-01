using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;

namespace Airport_Airplane_management_system.Model.Interfaces.Repositories
{
    public interface ICrewRepository
    {
        // keep your existing methods...

        // Reports
        int CountCrewNotAssignedToAnyFlight();
        int CountCrewAssignedToPastFlightsOnly();

        List<Crew> GetCrewNotAssignedToAnyFlight();
        List<Crew> GetCrewAssignedToPastFlights();
    }
}
