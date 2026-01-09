using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airport_Airplane_management_system.Model.Services
{
    public class CrewService
    {
        private readonly ICrewRepository _crewRepo;
        private readonly IFlightRepository _flightRepo;

        public CrewService(ICrewRepository crewRepo, IFlightRepository flightRepo)
        {
            _crewRepo = crewRepo ?? throw new ArgumentNullException(nameof(crewRepo));
            _flightRepo = flightRepo ?? throw new ArgumentNullException(nameof(flightRepo));
        }

        public List<Crew> GetCrew() => _crewRepo.GetAll();

        public List<Flight> GetFlights() => _flightRepo.GetAllFlights();

        public void ValidateStatusVsFlight(string status, int? flightId)
        {
            bool inactive = string.Equals(status, "Inactive", StringComparison.OrdinalIgnoreCase);

            // If inactive, must be unassigned
            if (inactive && flightId.HasValue)
                throw new Exception("Inactive crew cannot be assigned to a flight.");

            // If active and assigned, ensure flight exists (optional but good)
            if (!inactive && flightId.HasValue)
            {
                var exists = _flightRepo.GetAllFlights().Any(f => f.FlightID == flightId.Value);
                if (!exists)
                    throw new Exception("Selected flight not found.");
            }
        }

        public void AddCrew(string fullName, string role, string status, string email, string phone, int? flightId)
        {
            // normalize: if inactive => force null
            if (string.Equals(status, "Inactive", StringComparison.OrdinalIgnoreCase))
                flightId = null;

            var employeeId = _crewRepo.GenerateNextEmployeeId();

            var crew = new Crew(fullName, role, status, employeeId, email ?? "", phone ?? "")
            {
                // ✅ THIS is the missing line in most cases
                FlightId = flightId
            };

            if (!_crewRepo.Insert(crew, out var err))
                throw new Exception(err);
        }

        public void UpdateCrew(string employeeId, string fullName, string role, string status, string email, string phone, int? flightId)
        {
            if (string.Equals(status, "Inactive", StringComparison.OrdinalIgnoreCase))
                flightId = null;

            var crew = new Crew(fullName, role, status, employeeId, email ?? "", phone ?? "")
            {
                // ✅ same here
                FlightId = flightId
            };

            if (!_crewRepo.Update(crew, out var err))
                throw new Exception(err);
        }

        public void DeleteCrew(string employeeId)
        {
            if (!_crewRepo.DeleteByEmployeeId(employeeId, out var err))
                throw new Exception(err);
        }
    }
}
