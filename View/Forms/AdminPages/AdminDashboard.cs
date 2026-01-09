using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Core.Classes.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Presenter.AdminPagesPresenters;
using Airport_Airplane_management_system.View.Interfaces;
using Airport_Airplane_management_system.View.UserControls;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    public partial class AdminDashboard : UserControl, IAdminDashboardView
    {
        // Events
        public event EventHandler MainAClicked;
        public event EventHandler FlightManagementClicked;
        public event EventHandler PlaneManagementClicked;
        public event EventHandler CrewManagementClicked;
        public event EventHandler PassengerManagementClicked;
        public event EventHandler ReportsClicked;
        public event EventHandler NotrificationAClicked;
        public event EventHandler LogoutAClicked;

        // Expose child views
        public IMainAView MainAView => maina1;
        public IFlightManagementView FlightManagementView => flightManagement1;
        public IPlaneManagementView PlaneManagementView => planeManagements1;
        public ICrewManagementView CrewManagementView => crewManagement1;
        public IPassengerManagementView PassengerManagementView => passengerMangement1;
        public IReportsView ReportsView => reports1;
        public AdminDashboardPresenter adminpresenter;
        private readonly INavigationService _navigation;
        private readonly IAppSession _session;

        // Docked PlaneSchedule controls
        private PlaneScheduleControl _dockedScheduleOnPlanePage;
        private PlaneScheduleControl _dockedScheduleOnFlightPage;

        public AdminDashboard(INavigationService navigation,IAppSession session)
        {

            InitializeComponent();
            _session = session;
            _navigation = navigation;
            adminpresenter = new AdminDashboardPresenter(this, _navigation, _session);
            InitializeButtonEvents();
            HideAllPanels();

            // UI default only (presenter will refresh + call MainA())
            MainA();
        }

        private void InitializeButtonEvents()
        {
            btnMainA.Click += (s, e) => MainAClicked?.Invoke(this, EventArgs.Empty);
            btnplane.Click += (s, e) => PlaneManagementClicked?.Invoke(this, EventArgs.Empty);
            btncrew.Click += (s, e) => CrewManagementClicked?.Invoke(this, EventArgs.Empty);
            btnpasenger.Click += (s, e) => PassengerManagementClicked?.Invoke(this, EventArgs.Empty);
            btnreport.Click += (s, e) => ReportsClicked?.Invoke(this, EventArgs.Empty);
            btnlogoutA.Click += (s, e) => LogoutAClicked?.Invoke(this, EventArgs.Empty);
        }

        // Show / Hide Panels (UI only)
        private void HideAllPanels()
        {
            maina1.Hide();
            flightManagement1.Hide();
            planeManagements1.Hide();
            crewManagement1.Hide();
            passengerMangement1.Hide();
            reports1.Hide();

            HidePlaneScheduleOnPlanePage();
            HidePlaneScheduleOnFlightPage();
        }

        private void ShowOnly(Control panelToShow, Guna2Button activeButton)
        {
            HideAllPanels();
            panelToShow.Visible = true;
            panelToShow.BringToFront();
            SetActiveButton(activeButton);
        }

        private void SetActiveButton(Guna2Button activeBtn)
        {
            Guna2Button[] buttons = { btncrew, btnFlight, btnMainA, btnpasenger, btnplane, btnreport, btnlogoutA };
            foreach (var btn in buttons) btn.FillColor = Color.Transparent;
            activeBtn.FillColor = Color.DarkCyan;
        }

        // IAdminDashboardView (UI only)
        public void FlightMangement() => ShowOnly(flightManagement1, btnFlight);
        public void CrewMangement() => ShowOnly(crewManagement1, btncrew);
        public void PlaneMangement() => ShowOnly(planeManagements1, btnplane);
        public void PassengerMangement() => ShowOnly(passengerMangement1, btnpasenger);
        public void Report() => ShowOnly(reports1, btnreport);
        public void MainA() => ShowOnly(maina1, btnMainA);

        public void Logout()
        {
            maina1.Clear();
            flightManagement1.ClearView();
            crewManagement1.Clear();
            passengerMangement1.ClearView();
            planeManagements1.ClearView();
            reports1.ClearView();
            HideAllPanels();
            SetActiveButton(btnlogoutA);
        }

        public void ShowError(string message, string title = "Error")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Docked schedule (UI only; presenter supplies data)
        public void ShowPlaneScheduleOnPlanePage(string aircraftTitle, int planeId, IList<Flight> allFlights)
        {
            PlaneMangement();
            HidePlaneScheduleOnPlanePage();

            var schedule = new PlaneScheduleControl { Dock = DockStyle.Fill };
            schedule.SetMode(false);
            schedule.SetAircraftTitle(aircraftTitle);
            schedule.BindPlaneSchedule(planeId, allFlights, DateTime.Today.AddDays(-2), 360);
            schedule.CloseClicked += (_, __) => HidePlaneScheduleOnPlanePage();

            planeManagements1.Controls.Add(schedule);
            schedule.BringToFront();
            _dockedScheduleOnPlanePage = schedule;
        }

        public void ShowPlaneScheduleOnFlightPage(string aircraftTitle, int planeId, IList<Flight> allFlights)
        {
            FlightMangement();
            HidePlaneScheduleOnFlightPage();

            var schedule = new PlaneScheduleControl { Dock = DockStyle.Fill };
            schedule.SetMode(true);
            schedule.SetAircraftTitle(aircraftTitle);
            schedule.BindPlaneSchedule(planeId, allFlights, DateTime.Today.AddDays(-2), 360);
            schedule.CloseClicked += (_, __) => HidePlaneScheduleOnFlightPage();

            schedule.SlotSelected += (_, info) =>
            {
                DateTime suggestedDate = info.date.Date;
                DateTime suggestedDeparture = suggestedDate.AddHours(info.hour);
                DateTime suggestedArrival = suggestedDeparture.AddHours(3);

                using var dlg = new SlotDialog(suggestedDate, suggestedDeparture, suggestedArrival);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    flightManagement1.SetTimes(dlg.SelectedDeparture, dlg.SelectedArrival);
                    HidePlaneScheduleOnFlightPage();
                }
            };

            flightManagement1.ShowDockedSchedule(schedule);
            _dockedScheduleOnFlightPage = schedule;
        }

        public void HidePlaneScheduleOnPlanePage()
        {
            if (_dockedScheduleOnPlanePage == null || _dockedScheduleOnPlanePage.IsDisposed) return;
            try
            {
                if (planeManagements1.Controls.Contains(_dockedScheduleOnPlanePage))
                    planeManagements1.Controls.Remove(_dockedScheduleOnPlanePage);
                _dockedScheduleOnPlanePage.Dispose();
            }
            catch { }
            _dockedScheduleOnPlanePage = null;
        }

        public void HidePlaneScheduleOnFlightPage()
        {
            if (_dockedScheduleOnFlightPage == null || _dockedScheduleOnFlightPage.IsDisposed) return;
            try
            {
                flightManagement1.HideDockedSchedule();
                _dockedScheduleOnFlightPage.Dispose();
            }
            catch { }
            _dockedScheduleOnFlightPage = null;
        }
        // This is referenced by AdminDashboard.Designer.cs
        private void btnFlight_Click(object sender, EventArgs e)
        {
            FlightManagementClicked?.Invoke(this, EventArgs.Empty);
        }

        // This is referenced by AdminDashboard.Designer.cs
        private void maina1_Load(object sender, EventArgs e)
        {
        }

    }
}
