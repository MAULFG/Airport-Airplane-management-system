using Airport_Airplane_management_system.Model.Core.Classes.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Presenter.AdminPages;
using Airport_Airplane_management_system.Presenter.AdminPagesPresenters;
using Airport_Airplane_management_system.Repositories;
using Airport_Airplane_management_system.View.Interfaces;
using Airport_Airplane_management_system.View.UserControls;
using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Ticket_Booking_System_OOP.Model.Repositories;

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

        // Navigation + Dashboard presenter

        private readonly AdminDashboardPresenter _presenter;

        // Page presenters (lazy-loaded)
        private CrewManagementPresenter _crewpresenter;
        private FlightManagementPresenter _flightmpresenter;
        private PassengerManagementPresenter _passengermanagementpresenter;
        private PlaneManagementPresenter _planepresenter;
        private ReportsPresenter _reportspresenter;
        private MainAPresenter _mainapresenter;

        // Repositories & services
        private readonly IFlightRepository flightRepo;
        private readonly IPlaneRepository planeRepo;





        private readonly NotificationWriterService notificationWriterService;
       
        private readonly IAppSession session;

        // Docked PlaneSchedule controls
        private PlaneScheduleControl _dockedScheduleOnPlanePage;
        private PlaneScheduleControl _dockedScheduleOnFlightPage;

        public AdminDashboard(INavigationService navigation)
        {
            InitializeComponent();

            _presenter = new AdminDashboardPresenter(this, navigation);

            session = new AppSession();

            // Repositories
            flightRepo = new MySqlFlightRepository("server=localhost;port=3306;database=user;user=root;password=2006"); 
            planeRepo = new MySqlPlaneRepository("server=localhost;port=3306;database=user;user=root;password=2006");


            _mainapresenter = new MainAPresenter(maina1);
           
            HookMainANavigation();
            HideAllPanels();
            InitializeButtonEvents();

            MainA(); // Default page
        }

        #region Lazy-Load Presenters

        private void EnsureCrewPresenter()
        {
            if (_crewpresenter == null)
                _crewpresenter = new CrewManagementPresenter(crewManagement1);
        }

        private void EnsureFlightPresenter()
        {
            if (_flightmpresenter == null)
                _flightmpresenter = new FlightManagementPresenter(
                    flightManagement1,
                    openCrewForFlight: null,
                    openScheduleForPlane: OpenPlaneScheduleDockedOnFlightPage
                );
        }

        private void EnsurePlanePresenter()
        {
            if (_planepresenter == null)
                _planepresenter = new PlaneManagementPresenter(
                    planeManagements1,
                    OpenPlaneScheduleDockedOnPlanePage
                );
        }

        private void EnsurePassengerPresenter()
        {

            if (_passengermanagementpresenter == null)
                _passengermanagementpresenter = new PassengerManagementPresenter(
                    passengerMangement1,

                    () => flightRepo.CountUpcomingFlightsNotFullyBooked()

                );
        }

        private void EnsureReportsPresenter()
        {
            if (_reportspresenter == null)
                _reportspresenter = new ReportsPresenter(reports1);
        }

        #endregion

        #region Button Events

        private void InitializeButtonEvents()
        {
            btnMainA.Click += (s, e) => MainAClicked?.Invoke(this, EventArgs.Empty);
            btnFlight.Click += (s, e) => FlightManagementClicked?.Invoke(this, EventArgs.Empty);
            btnplane.Click += (s, e) => PlaneManagementClicked?.Invoke(this, EventArgs.Empty);
            btncrew.Click += (s, e) => CrewManagementClicked?.Invoke(this, EventArgs.Empty);
            btnpasenger.Click += (s, e) => PassengerManagementClicked?.Invoke(this, EventArgs.Empty);
            btnreport.Click += (s, e) => ReportsClicked?.Invoke(this, EventArgs.Empty);
            btnlogoutA.Click += (s, e) => LogoutAClicked?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Main Navigation Cards

        private void HookMainANavigation()
        {
            maina1.GoToFlightsRequested += () => FlightMangement();
            maina1.GoToPlanesRequested += () => PlaneMangement();
            maina1.GoToCrewRequested += () => CrewMangement();
            maina1.GoToPassengersRequested += () => PassengerMangement();

        }

        #endregion

        #region Docked Plane Schedule

        private void OpenPlaneScheduleDockedOnPlanePage(int planeId)
        {
            PlaneMangement();
            CloseDockedScheduleOnPlanePage();

            var plane = planeRepo.GetAllPlanes().FirstOrDefault(p => p.PlaneID == planeId);
            if (plane == null)
            {
                MessageBox.Show("Plane not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var schedule = new PlaneScheduleControl
            {
                Dock = DockStyle.Fill
            };

            schedule.SetMode(false);
            schedule.SetAircraftTitle($"{plane.Model} Schedule");
            schedule.BindPlaneSchedule(planeId, flightRepo.GetAllFlights(), DateTime.Today.AddDays(-2), 360);
            schedule.CloseClicked += (_, __) => CloseDockedScheduleOnPlanePage();

            planeManagements1.Controls.Add(schedule);
            schedule.BringToFront();
            _dockedScheduleOnPlanePage = schedule;
        }

        private void OpenPlaneScheduleDockedOnFlightPage(int planeId)
        {
            if (!flightManagement1.Visible)
                FlightMangement();

            CloseDockedScheduleOnFlightPage();

            var plane = planeRepo.GetAllPlanes().FirstOrDefault(p => p.PlaneID == planeId);
            if (plane == null)
            {
                MessageBox.Show("Plane not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var schedule = new PlaneScheduleControl { Dock = DockStyle.Fill };
            schedule.SetMode(true);
            schedule.SetAircraftTitle($"{plane.Model} Schedule");
            schedule.BindPlaneSchedule(planeId, flightRepo.GetAllFlights(), DateTime.Today.AddDays(-2), 360);

            schedule.CloseClicked += (_, __) => CloseDockedScheduleOnFlightPage();

            schedule.SlotSelected += (_, info) =>
            {
                DateTime suggestedDate = info.date.Date;
                DateTime suggestedDeparture = suggestedDate.AddHours(info.hour);
                DateTime suggestedArrival = suggestedDeparture.AddHours(3);

                using var dlg = new SlotDialog(suggestedDate, suggestedDeparture, suggestedArrival);
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    flightManagement1.SetTimes(dlg.SelectedDeparture, dlg.SelectedArrival);
                    CloseDockedScheduleOnFlightPage();
                }
            };

            flightManagement1.ShowDockedSchedule(schedule);
            _dockedScheduleOnFlightPage = schedule;
        }

        private void CloseDockedScheduleOnFlightPage()
        {
            if (_dockedScheduleOnFlightPage == null || _dockedScheduleOnFlightPage.IsDisposed) return;
            try { flightManagement1.HideDockedSchedule(); _dockedScheduleOnFlightPage.Dispose(); } catch { }
            _dockedScheduleOnFlightPage = null;
        }

        private void CloseDockedScheduleOnPlanePage()
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

        #endregion

        #region Show / Hide Panels

        private void HideAllPanels()
        {
            maina1.Hide();
            flightManagement1.Hide();
            planeManagements1.Hide();
            crewManagement1.Hide();
            passengerMangement1.Hide();
            reports1.Hide();
            CloseDockedScheduleOnPlanePage();
            CloseDockedScheduleOnFlightPage();
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

        #endregion

        #region IAdminDashboardView Implementation
        public void FlightMangement()
        {
            EnsureFlightPresenter();
            _flightmpresenter.RefreshData();   // 🔥 refresh data
            ShowOnly(flightManagement1, btnFlight);
        }

        public void CrewMangement()
        {
            EnsureCrewPresenter();
            _crewpresenter.RefreshData();      // 🔥 refresh data
            ShowOnly(crewManagement1, btncrew);
        }

        public void PlaneMangement()
        {
            EnsurePlanePresenter();
            _planepresenter.RefreshData();     // 🔥 refresh data
            ShowOnly(planeManagements1, btnplane);
        }

        public void PassengerMangement()
        {
            EnsurePassengerPresenter();
            _passengermanagementpresenter.RefreshData(); // 🔥 refresh data
            ShowOnly(passengerMangement1, btnpasenger);
        }

        public void Report()
        {
            EnsureReportsPresenter();
            _reportspresenter.RefreshData();   // 🔥 refresh data
            ShowOnly(reports1, btnreport);
        }

        public void MainA()
        {
            // Usually MainAPresenter already loads summary data
            _mainapresenter.RefreshData();     // 🔥 refresh main page
            ShowOnly(maina1, btnMainA);
        }


        public void Logout()
        {
            session.Clear();
            maina1.Clear();
            flightManagement1.ClearView();
            crewManagement1.Clear();
            passengerMangement1.ClearView();
            planeManagements1.ClearView();
            reports1.ClearView();
            ClearPresenters();
            HideAllPanels();
            SetActiveButton(btnlogoutA);
        }

        private void ClearPresenters()
        {
            _mainapresenter = null;
            _crewpresenter = null;
            _flightmpresenter = null;
            _passengermanagementpresenter = null;
            _planepresenter = null;
            _reportspresenter = null;
        }

        #endregion

        private void btnFlight_Click(object sender, EventArgs e)
        {

        }

        private void maina1_Load(object sender, EventArgs e)
        {

        }
    }
}