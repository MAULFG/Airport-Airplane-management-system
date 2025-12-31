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

        void Logout();
        void MainA();
        void NotrificationA();
        void FlightMangement();
        void CrewMangement();
        void PassengerMangement();
        void PlaneMangement();
        void Reports();   // ✅ was Report()
    }
}
