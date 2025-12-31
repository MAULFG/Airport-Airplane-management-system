using System;
using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface IMainAView
    {
        // navigation
        event Action GoToFlightsRequested;
        event Action GoToPlanesRequested;
        event Action GoToCrewRequested;
        event Action GoToPassengersRequested;
        event Action GoToNotificationsRequested;

        // output
        void ShowKpis(MainAKpiDto dto);
        void ShowFlights(MainAFlightsDto dto);
        void ShowAlerts(MainAAlertsDto dto);
    }

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

    public class MainAFlightsDto
    {
        public List<Flight> InAir { get; set; } = new();
        public List<Flight> Upcoming48h { get; set; } = new();
        public List<Flight> Past { get; set; } = new();
        public DateTime Now { get; set; } = DateTime.Now;
    }

    public class MainAAlertsDto
    {
        public int UnassignedCrew { get; set; }
        public int InactivePlanes { get; set; }
        public int ActiveAlerts { get; set; }
    }
}
