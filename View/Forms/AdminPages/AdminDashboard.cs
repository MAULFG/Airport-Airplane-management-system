
using Airport_Airplane_management_system.Model.Interfaces.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Presenter.AdminPages;
using Airport_Airplane_management_system.Presenter.AdminPagesPresenters;
using Airport_Airplane_management_system.Repositories;
using Airport_Airplane_management_system.View.Interfaces;
using Guna.UI2.WinForms;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using Ticket_Booking_System_OOP.Model.Repositories;

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
        private CrewManagementPresenter _crewpresenter;
        private FlightManagementPresenter _flightmpresenter;
        private PassengerManagementPresenter _passengermanagementpresenter;
        private PlaneManagementPresenter _planepresenter;
        private ReportsPresenter _reportspresenter;
        private MainAPresenter _mainapresenter;
        private readonly IFlightRepository flightRepo;
        private readonly IPlaneRepository planeRepo;
        private readonly IUserRepository userRepo;
        private readonly IBookingRepository bookRepo;
        private readonly ICrewRepository crewRepo;
        private readonly IPassengerRepository passRepo;
        private readonly ReportsService reportsService;
        private readonly IAppSession session;
        private readonly CrewService crewService;
        private readonly FlightService flightService;
        private readonly PlaneService planeService;
        private readonly PassengerService passService;
        private readonly BookingService bookingService;
        public AdminDashboard(INavigationService navigation)
        {
            
            InitializeComponent();
            session = new AppSession();
            _navigation = navigation;
            _presenter = new AdminDashboardPresenter(this, navigation);
            flightRepo = new MySqlFlightRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            userRepo =new MySqlUserRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            bookRepo =new MySqlBookingRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            planeRepo = new MySqlPlaneRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            crewRepo = new MySqlCrewRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            passRepo = new MySqlPassengerRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            crewService = new CrewService(crewRepo, flightRepo);
            passService = new PassengerService(passRepo, session);
            bookingService =new BookingService(bookRepo,session);
            planeService = new PlaneService(planeRepo, flightRepo);
            flightService = new FlightService(flightRepo, userRepo, bookRepo, planeRepo,session);
            reportsService = new ReportsService(flightRepo, planeRepo, crewRepo);
            _reportspresenter = new ReportsPresenter(reports1, reportsService);
            _mainapresenter = new MainAPresenter(maina1, flightRepo, planeRepo, crewRepo, passRepo, bookRepo);
            _crewpresenter = new CrewManagementPresenter(crewManagement1, crewService);
            _flightmpresenter = new FlightManagementPresenter(flightManagement1, flightService);
            _passengermanagementpresenter = new PassengerManagementPresenter(passengerMangement1, passService, () => flightRepo.CountUpcomingFlightsNotFullyBooked());
            _planepresenter = new PlaneManagementPresenter(planeManagements1, planeRepo);
            HookMainANavigation();
            HideAllPanels();
            InitializeButtonEvents();
            MainA();
        }
        private void HookMainANavigation()
        {
            // Flights cards + "View all flights →"
            maina1.GoToFlightsRequested += () =>
            {
                FlightMangement();
            };

            // Planes card
            maina1.GoToPlanesRequested += () =>
            {
                PlaneMangement();
            };

            // Crew card
            maina1.GoToCrewRequested += () =>
            {
                CrewMangement();
            };

            // Passengers card
            maina1.GoToPassengersRequested += () =>
            {
                PassengerMangement();
            };

            // Alerts card
            maina1.GoToNotificationsRequested += () =>
            {
                NotrificationA();
            };
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
            panelToShow.BringToFront();   // 🔥 REQUIRED

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
            // 1️⃣ Clear session
            session.Clear();

            // 2️⃣ Clear views
            maina1.Clear();
            flightManagement1.ClearView();
            crewManagement1.Clear();
            passengerMangement1.ClearView();
            planeManagements1.ClearView();
            reports1.ClearView();
            notrificationsa1.ClearView();

            // 3️⃣ Dispose presenters
            ClearPresenters();

            // 4️⃣ Hide UI
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

        private void maina1_Load(object sender, EventArgs e)
        {

        }
    }
}
