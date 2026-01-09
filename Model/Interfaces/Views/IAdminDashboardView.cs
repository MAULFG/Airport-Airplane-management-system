using System;
using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface IAdminDashboardView
    {
        // menu events
        event EventHandler MainAClicked;
        event EventHandler FlightManagementClicked;
        event EventHandler PlaneManagementClicked;
        event EventHandler CrewManagementClicked;
        event EventHandler PassengerManagementClicked;
        event EventHandler ReportsClicked;
        event EventHandler NotrificationAClicked;
        event EventHandler LogoutAClicked;

        IMainAView MainAView { get; }
        IFlightManagementView FlightManagementView { get; }
        IPlaneManagementView PlaneManagementView { get; }
        ICrewManagementView CrewManagementView { get; }
        IPassengerManagementView PassengerManagementView { get; }
        IReportsView ReportsView { get; }

        // show/hide pages (UI only)
        void MainA();
        void FlightMangement();
        void PlaneMangement();
        void CrewMangement();
        void PassengerMangement();
        void Report();

        // shell utilities
        void Logout(); // UI clear only (presenter clears session + navigates)
        void ShowError(string message, string title = "Error");

        // schedule docking (UI only; presenter supplies data)
        void ShowPlaneScheduleOnPlanePage(string aircraftTitle, int planeId, IList<Flight> allFlights);
        void ShowPlaneScheduleOnFlightPage(string aircraftTitle, int planeId, IList<Flight> allFlights);
        void HidePlaneScheduleOnPlanePage();
        void HidePlaneScheduleOnFlightPage();
    }
}
