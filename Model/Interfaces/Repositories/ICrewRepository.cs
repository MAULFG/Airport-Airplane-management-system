using Airport_Airplane_management_system.Model.Core.Classes.Crew;
using Airport_Airplane_management_system.Model.Core.Classes.Flights;
using System.Collections.Generic;

namespace Airport_Airplane_management_system.Model.Interfaces.Repositories
{
    public interface ICrewRepository
    {
        List<Crew> GetAllCrew();
        List<Crew> GetCrewForFlight(int flightId);
        List<Crew> GetUnassignedCrew();
        bool InsertCrew(Crew crew);
        bool UpdateCrew(Crew crew);
        bool DeleteCrew(string employeeId);
        string GenerateNextEmployeeId();
    }
}
