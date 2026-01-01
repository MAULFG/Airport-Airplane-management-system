namespace Airport_Airplane_management_system.Model.Core.Classes
{
    public class MainAAlertsDto
    {
        public int UnassignedCrew { get; set; }
        public int InactivePlanes { get; set; }

        // ✅ NEW
        public int CrewAssignedToPastFlights { get; set; }
        public int PlanesNotAssignedToAnyFlight { get; set; }

        public int ActiveAlerts { get; set; }
    }
}
