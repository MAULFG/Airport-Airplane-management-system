using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Presenter.AdminPagesPresenters;
using Airport_Airplane_management_system.View.Interfaces;
using Guna.UI2.WinForms;
using System;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using System.Drawing;
using System.Windows.Forms;
using Ticket_Booking_System_OOP.Model.Repositories;
using Airport_Airplane_management_system.Presenter.AdminPages;
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

        // Repos
        private readonly IFlightRepository _flightRepo;
        private readonly ICrewRepository _crewRepo;
        private readonly IPlaneRepository _planeRepo;
        private readonly IUserRepository _userRepo;
        private readonly IBookingRepository _bookingRepo;

        // Services
        private readonly CrewService _crewService;
        private readonly FlightService _flightService;
        private readonly PlaneService _planeService;

        // Presenters (wired to DESIGNER controls)
        private readonly CrewManagementPresenter _crewPresenter;
        private readonly FlightManagementPresenter _flightPresenter;

        private const string ConnStr = "server=localhost;port=3306;database=user;user=root;password=2006";

        public AdminDashboard(INavigationService navigation)
        {
            InitializeComponent();

            _navigation = navigation;
            _presenter = new AdminDashboardPresenter(this, navigation);

            // Repositories
            _flightRepo = new MySqlFlightRepository(ConnStr);
            _crewRepo = new MySqlCrewRepository(ConnStr);
            _planeRepo = new MySqlPlaneRepository(ConnStr);
            _userRepo = new MySqlUserRepository(ConnStr);
            _bookingRepo = new MySqlBookingRepository(ConnStr);

            // Services
            _crewService = new CrewService(_crewRepo, _flightRepo);
            _flightService = new FlightService(_flightRepo, _userRepo, _bookingRepo, _planeRepo);
            _planeService = new PlaneService(_planeRepo, _flightRepo);

            // Presenters (IMPORTANT: use the existing designer controls)
            _crewPresenter = new CrewManagementPresenter(crewManagement1, _crewService);
            _flightPresenter = new FlightManagementPresenter(flightManagement1, _flightService, _planeService);

            // ✅ Bridge: when user clicks "See crew" in flight card → open crew page with filter
            _flightPresenter.CrewForFlightRequested += flightId => OpenCrewManagement(flightId);

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

        // IAdminDashboardView methods
        public void Logout()
        {
            HideAllPanels();
            SetActiveButton(btnlogoutA);
        }

        // ✅ This is what Main1 should call (or the presenter bridge uses)
        public void OpenCrewManagement(int? flightIdFilter = null)
        {
            ShowOnly(crewManagement1, btncrew);

            if (flightIdFilter.HasValue)
                crewManagement1.SetFlightFilter(flightIdFilter.Value); // you must implement this in CrewManagement
        }

        public void MainA() => ShowOnly(maina1, btnMainA);
        public void NotrificationA() => ShowOnly(notrificationsa1, btnnotrificationA);
        public void FlightMangement() => ShowOnly(flightManagement1, btnFlight);
        public void CrewMangement() => ShowOnly(crewManagement1, btncrew);
        public void PassengerMangement() => ShowOnly(passengerMangement1, btnpasenger);
        public void PlaneMangement() => ShowOnly(planeManagements1, btnplane);
        public void Report() => ShowOnly(reports1, btnreport);

        // unused designer events (keep if designer references them)
        private void btnUpcomingFlights_Click(object sender, EventArgs e) { }
        private void btnSearchBook_Click(object sender, EventArgs e) { }
        private void guna2Panel1_Paint(object sender, PaintEventArgs e) { }
        private void passengerMangement1_Load(object sender, EventArgs e) { }
        private void maina1_Load(object sender, EventArgs e) { }
    }
}
