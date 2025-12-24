using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Presenter.AdminPagesPresenters;
using Airport_Airplane_management_system.View.Interfaces;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    public partial class AdminDashboard : UserControl, IAdminDashboardView
    {
        public event EventHandler MainAClicked;
        public event EventHandler FlightManagementClicked;
        public event EventHandler PlaneManagementClicked;
        public event EventHandler CrewManagementClicked;
        public event EventHandler PassengerManagementClicked;
        public event EventHandler ReportsClicked;
        public event EventHandler NotrificationAClicked;
        public event EventHandler LogoutAClicked;
        private readonly INavigationService _navigation;
        private readonly AdminDashboardPresenter _presenter;
        public AdminDashboard(INavigationService navigation)
        {
            InitializeComponent();
            _navigation = navigation;
            _presenter = new AdminDashboardPresenter(this, navigation);
            HideAllPanels();
            InitializeButtonEvents();
        }
        private void InitializeButtonEvents()
        {

            btnlogoutA.Click += (s, e) => Logout();
            btnMainA.Click += (s, e) => ShowOnly(maina1, btnMainA);
            btnnotrificationA.Click += (s, e) => ShowOnly(notrificationsa1, btnnotrificationA);
            btnFlight.Click += (s, e) => ShowOnly(flightManagement1, btnFlight);
            btncrew.Click += (s, e) => ShowOnly(crewManagement1, btncrew);
            btnpasenger.Click += (s, e) => ShowOnly(passengerMangement1, btnpasenger);
            btnplane.Click += (s, e) => ShowOnly(planeManagements1, btnplane);
            btnreport.Click += (s, e) => ShowOnly(reports1, btnreport);
        }

        private void HideAllPanels()
        {
            maina1.Hide();
            flightManagement1.Hide();
            planeManagements1.Hide();
            crewManagement1.Hide();
            passengerMangement1.Hide();
            reports1.Hide();
            notrificationsa1.Hide();


        }
        private void ShowOnly(Control panelToShow, Guna2Button activeButton)
        {
            HideAllPanels();
            panelToShow.Show();
            SetActiveButton(activeButton);
        }
        private void SetActiveButton(Guna2Button activeBtn)
        {
            Guna2Button[] buttons =
            {
               btncrew,
               btnFlight,
               btnMainA,
               btnnotrificationA,
               btnpasenger,
               btnplane,
               btnreport,
               btnlogoutA 
               

            };

            foreach (var btn in buttons)
                btn.FillColor = Color.Transparent;

            activeBtn.FillColor = Color.DarkCyan;
        }
        public void Logout()
        {
            HideAllPanels();
            SetActiveButton(btnlogoutA); // Reset highlight
            LogoutAClicked?.Invoke(this, EventArgs.Empty);
        }
        public void MainA() => ShowOnly(maina1, btnMainA);
        public void NotrificationA() => ShowOnly(notrificationsa1, btnnotrificationA);
        public void FlightMangement() => ShowOnly(flightManagement1, btnFlight);
        public void CrewMangement() => ShowOnly(crewManagement1, btncrew);
        public void PassengerMangement() => ShowOnly(passengerMangement1, btnpasenger);
        public void PlaneMangement() => ShowOnly(planeManagements1, btnplane);
        public void Report() => ShowOnly(reports1, btnreport);
        private void btnUpcomingFlights_Click(object sender, EventArgs e)
        {

        }

        private void btnSearchBook_Click(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void passengerMangement1_Load(object sender, EventArgs e)
        {

        }
    }
}
