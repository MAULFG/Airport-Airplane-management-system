using System;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface IAdminDashboardView
    {
        event EventHandler MainAClicked;
        event EventHandler FlightManagementClicked;
        event EventHandler PlaneManagementClicked;
        event EventHandler CrewManagementClicked;
        event EventHandler PassengerManagementClicked;
        event EventHandler ReportsClicked;
        event EventHandler NotrificationAClicked;
        event EventHandler LogoutAClicked;

        void MainA();
        void FlightMangement();
        void PlaneMangement();
        void CrewMangement();
        void PassengerMangement();
        void Report();

        void Logout();
    }
}
