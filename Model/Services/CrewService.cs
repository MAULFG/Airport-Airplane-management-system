using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace Airport_Airplane_management_system.Model.Services
{
    public class CrewService
    {
        private readonly ICrewRepository _crewRepo;
        private readonly IFlightRepository _flightRepo;

        public CrewService(ICrewRepository crewRepo, IFlightRepository flightRepo)
        {
            _crewRepo = crewRepo;
            _flightRepo = flightRepo;
        }

        public List<Crew> GetCrew() => _crewRepo.GetAll();
        public List<Flight> GetFlights() => _flightRepo.GetAllFlights();

        public void AddCrew(string name, string role, string status, string email, string phone, int? flightId)
        {
            ValidateStatusVsFlight(status, flightId);

            string emp = _crewRepo.GenerateNextEmployeeId();
            var crew = new Crew(name, role, Normalize(status), emp, email, phone)
            {
                FlightId = flightId
            };

            if (!_crewRepo.Insert(crew, out var err))
                throw new Exception(err);
        }

        public void UpdateCrew(string employeeId, string name, string role, string status, string email, string phone, int? flightId)
        {
            ValidateStatusVsFlight(status, flightId);

            var existing = _crewRepo.GetById(employeeId);
            if (existing == null)
                throw new Exception("Crew member not found");

            var updatedCrew = new Crew(
                fullName: name,
                role: role,
                status: Normalize(status),
                employeeId: existing.EmployeeId,
                email: email,
                phone: phone
            );
            updatedCrew.FlightId = flightId;

            if (!_crewRepo.Update(updatedCrew, out var err))
                throw new Exception(err);
        }

        public void DeleteCrew(string employeeId)
        {
            if (!_crewRepo.DeleteByEmployeeId(employeeId, out var err))
                throw new Exception(err);
        }

        public void ValidateStatusVsFlight(string status, int? flightId)
        {
            if (status.Equals("Inactive", StringComparison.OrdinalIgnoreCase) && flightId != null)
                throw new Exception("Inactive crew members cannot be assigned to a flight.");
        }

        private static string Normalize(string ui) =>
            ui.Equals("Active", StringComparison.OrdinalIgnoreCase) ? "active" : "inactive";
    }
}
