using Airport_Airplane_management_system.Model.Core.Classes;
using System.Collections.Generic;
namespace Airport_Airplane_management_system.Model.Interfaces.Services
{
    public interface ICrewService
    {
        List<Crew> GetCrew();
        List<Flight> GetFlights();

        void AddCrew(string name, string role, string status, string email, string phone, int? flightId);
        void UpdateCrew(string employeeId, string name, string role, string status, string email, string phone, int? flightId);
        bool DeleteCrew(string employeeId);

        void ValidateStatusVsFlight(string status, int? flightId);
    }
}