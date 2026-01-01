using System;
using Airport_Airplane_management_system.Model.Core.Classes;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface IMainAView
    {
        event Action GoToFlightsRequested;
        event Action GoToPlanesRequested;
        event Action GoToCrewRequested;
        event Action GoToPassengersRequested;
        event Action GoToNotificationsRequested;

        void ShowKpis(MainAKpiDto dto);
        void ShowFlights(MainAFlightsDto dto);
        void ShowAlerts(MainAAlertsDto dto);
    }
}
