using Airport_Airplane_management_system.Model.Core.Classes.Crew;
using Airport_Airplane_management_system.Model.Core.Classes.Flights;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Airport_Airplane_management_system.Model.Services
{
    public class CrewService
    {
        private readonly ICrewRepository _crewRepo;
        private readonly List<Crew> _crewMembers;

        public CrewService(ICrewRepository crewRepo)
        {
            _crewRepo = crewRepo;
            _crewMembers = _crewRepo.GetAllCrew();
        }

        public List<Crew> GetCrew() => _crewMembers;

        public List<Crew> GetCrewForFlight(int flightId)
            => _crewRepo.GetCrewForFlight(flightId);

        public List<Crew> GetUnassignedCrew()
            => _crewRepo.GetUnassignedCrew();

        public bool AssignCrewToFlight(Crew crew, int flightId)
        {
            crew.AssignToFlight(flightId);

            bool result = _crewRepo.UpdateCrew(crew);
            if (result)
            {
                var c = _crewMembers.FirstOrDefault(x => x.EmployeeId == crew.EmployeeId);
                if (c != null) c.AssignToFlight(flightId);
            }
            return result;
        }

        public bool UnassignCrew(Crew crew)
        {
            crew.UnassignFromFlight();

            bool result = _crewRepo.UpdateCrew(crew);
            if (result)
            {
                var c = _crewMembers.FirstOrDefault(x => x.EmployeeId == crew.EmployeeId);
                if (c != null) c.UnassignFromFlight();
            }
            return result;
        }


        public bool AddCrew(Crew crew)
        {
            bool result = _crewRepo.InsertCrew(crew);
            if (result)
            {
                _crewMembers.Add(crew);
            }
            return result;
        }

        public bool RemoveCrew(string employeeId)
        {
            bool result = _crewRepo.DeleteCrew(employeeId);
            if (result)
            {
                var c = _crewMembers.FirstOrDefault(x => x.EmployeeId == employeeId);
                if (c != null) _crewMembers.Remove(c);
            }
            return result;
        }

        public string GetNextEmployeeId() => _crewRepo.GenerateNextEmployeeId();
    }
}
