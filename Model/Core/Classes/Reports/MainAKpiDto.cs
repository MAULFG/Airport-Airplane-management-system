namespace Airport_Airplane_management_system.Model.Core.Classes
{
    public class MainAKpiDto
    {
        public int TotalFlights { get; set; }
        public int FlightsInAir { get; set; }
        public int FlightsUpcoming { get; set; }
        public int FlightsPast { get; set; }

        public int TotalPlanes { get; set; }
        public int ActivePlanes { get; set; }
        public int InactivePlanes { get; set; }

        public int TotalCrew { get; set; }
        public int CrewAssigned { get; set; }
        public int CrewUnassigned { get; set; }

        public int TotalPassengers { get; set; }
        public int PassengersUpcoming { get; set; }
        public int PassengersPast { get; set; }

        public int ActiveAlerts { get; set; }
        public string OpsText { get; set; } = "0.0%";
    }
}
