using Airport_Airplane_management_system.Model.Core.Classes;
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
using Airport_Airplane_management_system.Model.Interfaces.Repositories;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    public partial class AdminDashboard : UserControl, IAdminDashboardView
    {
        // ===== IAdminDashboardView events =====
        public event EventHandler MainAClicked;
        public event EventHandler FlightManagementClicked;
        public event EventHandler PlaneManagementClicked;
        public event EventHandler CrewManagementClicked;
        public event EventHandler PassengerManagementClicked;
        public event EventHandler ReportsClicked;
        public event EventHandler NotrificationAClicked;
        public event EventHandler LogoutAClicked;

        // ===== Navigation & Presenter =====
        private readonly INavigationService _navigation;
        private readonly AdminDashboardPresenter _presenter;

        // ===== Repos =====
        private IFlightRepository _flightRepo;
        private IUserRepository _userRepo;
        private IBookingRepository _bookingRepo;
        private IPlaneRepository _planeRepo;
        private ICrewRepository _crewRepo;
        private IPassengerRepository _passengerRepo;

        // ===== Services =====
        private FlightService _flightService;
        private CrewService _crewService;
        private PassengerService _passengerService;

        // ===== Page Presenters =====
        private FlightManagementPresenter _flightPresenter;
        private CrewManagementPresenter _crewPresenter;
        private PassengerManagementPresenter _passengerPresenter;
        private PlaneManagementPresenter _planePresenter;
        private ReportsPresenter _reportsPresenter;


        // ✅ MainA MVP Presenter
        private MainAPresenter _mainAPresenter;

        // ===== Docked schedule refs =====
        private PlaneScheduleControl _dockedScheduleOnPlanePage;
        private PlaneScheduleControl _dockedScheduleOnFlightPage;

        private bool _returnToFlightAfterSchedule = false;
        private int _returnPlaneIdAfterSchedule = -1;

        public AdminDashboard(INavigationService navigation)
        {
            InitializeComponent();

            _navigation = navigation;
            _presenter = new AdminDashboardPresenter(this, navigation);

            string connStr = "server=localhost;port=3306;database=user;user=root;password=2006";

            _flightRepo = new MySqlFlightRepository(connStr);
            _userRepo = new MySqlUserRepository(connStr);
            _bookingRepo = new MySqlBookingRepository(connStr);
            _planeRepo = new MySqlPlaneRepository(connStr);
            _crewRepo = new MySqlCrewRepository(connStr);
            _passengerRepo = new PassengerRepository(connStr);

            _flightService = new FlightService(_flightRepo, _userRepo, _bookingRepo, _planeRepo);
            _crewService = new CrewService(_crewRepo, _flightRepo);
            _passengerService = new PassengerService(_passengerRepo);
            // =========================
            // Reports (REAL DATA)
            // =========================
            ReportsService reportsService =
                new ReportsService(_flightRepo, _planeRepo, _crewRepo);

            _reportsPresenter =
                new ReportsPresenter(reports1, reportsService);


            // ===== MVP presenters =====
            _planePresenter = new PlaneManagementPresenter(planeManagements1, _planeRepo);

            _flightPresenter = new FlightManagementPresenter(
                flightManagement1,
                _flightService,
                openCrewForFlight: (flightId) =>
                {
                    CrewMangement();
                    crewManagement1.SetFilterFlight(flightId);
                    crewManagement1.SetFormFlight(flightId);
                }
            );

            _crewPresenter = new CrewManagementPresenter(crewManagement1, _crewService);

            _passengerPresenter = new PassengerManagementPresenter(
                passengerMangement1,
                _passengerService,
                () => _flightRepo.CountUpcomingFlightsNotFullyBooked()
            );

            // =========================
            // DOCKED schedule everywhere
            // =========================
            if (flightManagement1 != null)
            {
                flightManagement1.PlaneScheduleRequested -= OpenPlaneScheduleDockedOnFlightPage;
                flightManagement1.PlaneScheduleRequested += OpenPlaneScheduleDockedOnFlightPage;
            }

            if (planeManagements1 != null)
            {
                planeManagements1.PlaneSelected -= OpenPlaneScheduleDockedOnPlanePage;
                planeManagements1.PlaneSelected += OpenPlaneScheduleDockedOnPlanePage;
            }

            // =========================
            // ✅ MainA MVP: Presenter owns repos + data logic
            // =========================
            _mainAPresenter = new MainAPresenter(
                maina1,
                _flightRepo,
                _planeRepo,
                _crewRepo,
                _passengerRepo,
                _bookingRepo
            );

            // ✅ KPI click navigation
            maina1.GoToFlightsRequested += () => FlightMangement();
            maina1.GoToPlanesRequested += () => PlaneMangement();
            maina1.GoToCrewRequested += () => CrewMangement();
            maina1.GoToPassengersRequested += () => PassengerMangement();
            maina1.GoToNotificationsRequested += () => NotrificationA();

            // UI init
            HideAllPanels();
            InitializeButtonEvents();
            WireReportsNavigation();   // ✅ ADD THIS
            MainA();
        }

        // =========================
        // Dock schedule on FLIGHT page
        // =========================
        private void OpenPlaneScheduleDockedOnFlightPage(int planeId)
        {
            FlightMangement();
            CloseDockedScheduleOnFlightPage();

            var plane = _planeRepo.GetAllPlanes().FirstOrDefault(p => p.PlaneID == planeId);
            if (plane == null)
            {
                MessageBox.Show("Plane not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var schedule = new PlaneScheduleControl();
            schedule.Dock = DockStyle.Fill;

            schedule.SetMode(true);
            schedule.SetAircraftTitle($"{plane.Model} Schedule");

            var flights = _flightRepo.GetAllFlights();

            schedule.BindPlaneSchedule(
                planeId,
                flights,
                DateTime.Today.AddDays(-2),
                360
            );

            schedule.CloseClicked += (_, __) => CloseDockedScheduleOnFlightPage();

            schedule.SlotSelected += (_, info) =>
            {
                DateTime suggestedDate = info.date.Date;
                DateTime suggestedDeparture = suggestedDate.AddHours(info.hour);
                DateTime suggestedArrival = suggestedDeparture.AddHours(3);

                using (var dlg = new SlotDialog(suggestedDate, suggestedDeparture, suggestedArrival))
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        flightManagement1.SetTimes(dlg.SelectedDeparture, dlg.SelectedArrival);
                        CloseDockedScheduleOnFlightPage();
                        FlightMangement();
                    }
                }
            };

            flightManagement1.ShowDockedSchedule(schedule);
            _dockedScheduleOnFlightPage = schedule;
        }

        private void CloseDockedScheduleOnFlightPage()
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

        // =========================
        // Dock schedule on PLANE page
        // =========================
        private void OpenPlaneScheduleDockedOnPlanePage(int planeId)
        {
            PlaneMangement();
            CloseDockedScheduleOnPlanePage();

            var plane = _planeRepo.GetAllPlanes().FirstOrDefault(p => p.PlaneID == planeId);
            if (plane == null)
            {
                MessageBox.Show("Plane not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var schedule = new PlaneScheduleControl();
            schedule.Dock = DockStyle.Fill;

            schedule.SetMode(false);
            schedule.SetAircraftTitle($"{plane.Model} Schedule");

            var flights = _flightRepo.GetAllFlights();

            schedule.BindPlaneSchedule(
                planeId,
                flights,
                DateTime.Today.AddDays(-2),
                360
            );

            schedule.CloseClicked += (_, __) => CloseDockedScheduleOnPlanePage();

            planeManagements1.Controls.Add(schedule);
            schedule.BringToFront();
            _dockedScheduleOnPlanePage = schedule;
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

        private void InitializeButtonEvents()
        {
            btnMainA.Click += (s, e) => MainAClicked?.Invoke(this, EventArgs.Empty);
            btnFlight.Click += (s, e) => FlightManagementClicked?.Invoke(this, EventArgs.Empty);
            btnplane.Click += (s, e) => PlaneManagementClicked?.Invoke(this, EventArgs.Empty);
            btncrew.Click += (s, e) => CrewManagementClicked?.Invoke(this, EventArgs.Empty);
            btnpasenger.Click += (s, e) => PassengerManagementClicked?.Invoke(this, EventArgs.Empty);
            btnreport.Click += (s, e) => ReportsClicked?.Invoke(this, EventArgs.Empty);
            btnnotrificationA.Click += (s, e) => NotrificationAClicked?.Invoke(this, EventArgs.Empty);
            btnlogoutA.Click += (s, e) => LogoutAClicked?.Invoke(this, EventArgs.Empty);
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
            panelToShow.Visible = true;
            panelToShow.BringToFront();
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
            SetActiveButton(btnlogoutA);
        }
        private void WireReportsNavigation()
        {
            reports1.NavigateRequested += pageKey =>
            {
                switch (pageKey)
                {
                    case "PlaneManagement":
                        PlaneMangement();
                        break;

                    case "CrewManagement":
                        CrewMangement();
                        break;

                    case "FlightMangement":
                        FlightMangement();
                        break;

                    case "PassengerMangement":
                        PassengerMangement();
                        break;
                }
            };
        }



        public void Reports() => ShowOnly(reports1, btnreport);

        public void MainA() => ShowOnly(maina1, btnMainA);
        public void NotrificationA() => ShowOnly(notrificationsa1, btnnotrificationA);
        public void FlightMangement() => ShowOnly(flightManagement1, btnFlight);
        public void CrewMangement() => ShowOnly(crewManagement1, btncrew);
        public void PassengerMangement() => ShowOnly(passengerMangement1, btnpasenger);
        public void PlaneMangement() => ShowOnly(planeManagements1, btnplane);

        private void btnUpcomingFlights_Click(object sender, EventArgs e) { }
        private void btnSearchBook_Click(object sender, EventArgs e) { }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e) { }
        private void passengerMangement1_Load(object sender, EventArgs e) { }
        private void maina1_Load(object sender, EventArgs e) { }
    }
}
