using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Presenter.AdminPages;
using Airport_Airplane_management_system.Presenter.AdminPagesPresenters;
using Airport_Airplane_management_system.Repositories;
using Airport_Airplane_management_system.View.Interfaces;
using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;
using Ticket_Booking_System_OOP.Model.Repositories;

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

        // ===== Repos (ONE source of truth) =====
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

        public AdminDashboard(INavigationService navigation)
        {
            InitializeComponent();

            _navigation = navigation;
            _presenter = new AdminDashboardPresenter(this, navigation);

            // ✅ Connection string (same one you used)
            string connStr = "server=localhost;port=3306;database=user;user=root;password=2006";

            // ✅ Create repositories (IMPORTANT: do NOT keep duplicate variables)
            _flightRepo = new MySqlFlightRepository(connStr);
            _userRepo = new MySqlUserRepository(connStr);
            _bookingRepo = new MySqlBookingRepository(connStr);
            _planeRepo = new MySqlPlaneRepository(connStr);
            _crewRepo = new MySqlCrewRepository(connStr);

            // ✅ Passenger Repo (make sure this class name matches your project)
            _passengerRepo = new PassengerRepository(connStr);

            // ✅ Create services
            _flightService = new FlightService(_flightRepo, _userRepo, _bookingRepo, _planeRepo);
            _crewService = new CrewService(_crewRepo, _flightRepo);
            _passengerService = new PassengerService(_passengerRepo);

            // ✅ Wire MVP presenters ONCE

            _flightPresenter = new FlightManagementPresenter(
                flightManagement1,
                _flightService,
                openCrewForFlight: (flightId) =>
                {
                    // 1) open crew page
                    CrewMangement();

                    // 2) set the crew page filter to the selected flight
                    crewManagement1.SetFilterFlight(flightId);

                    // 3) also sync the left form dropdown (optional but usually wanted)
                    crewManagement1.SetFormFlight(flightId);
                }
            );

            _crewPresenter = new CrewManagementPresenter(crewManagement1, _crewService);

            // ✅ Passenger MVP presenter (THIS is what was missing)
            // Passenger MVP
            _passengerRepo = new PassengerRepository(connStr);

            // PassengerService takes ONE arg in your project (repo)
            _passengerService = new PassengerService(_passengerRepo);

            // Passenger presenter requires the count function
            _passengerPresenter = new PassengerManagementPresenter(
                passengerMangement1,
                _passengerService,
                () => _flightRepo.CountUpcomingFlightsNotFullyBooked()
            );


            // UI init
            HideAllPanels();
            InitializeButtonEvents();
            MainA();
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
            panelToShow.BringToFront(); // REQUIRED so the selected page shows correctly

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

        // ===== IAdminDashboardView methods called by AdminDashboardPresenter =====
        public void Logout()
        {
            HideAllPanels();
            SetActiveButton(btnlogoutA);
        }

        public void MainA() => ShowOnly(maina1, btnMainA);
        public void NotrificationA() => ShowOnly(notrificationsa1, btnnotrificationA);
        public void FlightMangement() => ShowOnly(flightManagement1, btnFlight);
        public void CrewMangement() => ShowOnly(crewManagement1, btncrew);
        public void PassengerMangement() => ShowOnly(passengerMangement1, btnpasenger);
        public void PlaneMangement() => ShowOnly(planeManagements1, btnplane);
        public void Report() => ShowOnly(reports1, btnreport);

        // ===== Existing designer event handlers (keep) =====
        private void btnUpcomingFlights_Click(object sender, EventArgs e) { }
        private void btnSearchBook_Click(object sender, EventArgs e) { }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e) { }
        private void passengerMangement1_Load(object sender, EventArgs e) { }
        private void maina1_Load(object sender, EventArgs e) { }
    }
}
