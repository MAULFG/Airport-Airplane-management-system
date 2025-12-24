using Airport_Airplane_management_system.Model.Core.Classes.Flights;
using Airport_Airplane_management_system.View.Forms.AdminPages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface IAdminDashboardView
    {
        public event EventHandler MainAClicked;
        public event EventHandler FlightManagementClicked;
        public event EventHandler PlaneManagementClicked;
        public event EventHandler CrewManagementClicked;
        public event EventHandler PassengerManagementClicked;
        public event EventHandler ReportsClicked;
        public event EventHandler NotrificationAClicked;
        public event EventHandler LogoutAClicked;
        void Logout();
        void MainA();
        void NotrificationA();
        void FlightMangement();
        void CrewMangement();
        void PassengerMangement();
        void PlaneMangement();
        void Report();


    }
}
