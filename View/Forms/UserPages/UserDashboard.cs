using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Presenter;
using Airport_Airplane_management_system.Presenter.UserPagesPresenters;
using Airport_Airplane_management_system.View.Interfaces;
using Guna.UI2.WinForms;
using System;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    public partial class UserDashboard : UserControl, IUserDashboardView
    {
        public event EventHandler UpcomingFlightsClicked;
        public event EventHandler SearchBookClicked;
        public event EventHandler MyTicketsClicked;
        public event EventHandler NotificationsClicked;
        public event EventHandler SettingsClicked;
        public event EventHandler AccountClicked;
        public event EventHandler LogoutClicked;
        public event EventHandler UserMainClicked;

        private readonly INavigationService _navigation;
        private readonly UserDashboardPresenter _presenter;
        private readonly FlightService _flightService;
        private UpcomingFlightsPresenter _upcomingFlightsPresenter;
        private SearchAndBookingPresenter _searchandbookingpresenter;
        private Panel panelMain;

        public UserDashboard(INavigationService navigation)
        {
            InitializeComponent();
            _navigation = navigation;
            _presenter = new UserDashboardPresenter(this, navigation);

            // Create a main panel to hold the content panels
            panelMain = new Panel { Dock = DockStyle.Fill };
            Controls.Add(panelMain);       // add main panel first
            Controls.Add(guna2Panel1);     // side menu added after


            // Initialize repositories & service
            var flightRepo = new MySqlFlightRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            var userRepo = new MySqlUserRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            var bookingRepo = new MySqlBookingRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            var planeRepo =new MySqlPlaneRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            _flightService = new FlightService(flightRepo, userRepo, bookingRepo,planeRepo);
            
            _upcomingFlightsPresenter =
                new UpcomingFlightsPresenter(upcomingFlights1, _flightService);
            _searchandbookingpresenter = new SearchAndBookingPresenter(searchAndBooking1,_flightService);
            // Initialize the designer panel at runtime


            // Add all designer panels to panelMain
            panelMain.Controls.Add(mainUserPage1);
            panelMain.Controls.Add(upcomingFlights1);
            panelMain.Controls.Add(searchAndBooking1);
            panelMain.Controls.Add(myTicketsBookingHistory1);
            panelMain.Controls.Add(notifications1);
            panelMain.Controls.Add(userSettings1);
            panelMain.Controls.Add(userAccount1);


            // Hide all panels initially
            HideAllPanels();
            ShowMainUser();

            // Set up button click events
            InitializeButtonEvents();
       

        }

        private void InitializeButtonEvents()
        {
            guna2Button1.Click += (s, e) => UserMainClicked?.Invoke(this, EventArgs.Empty);
            btnUpcomingFlights.Click += (s, e) => UpcomingFlightsClicked?.Invoke(this, EventArgs.Empty);
            btnSearchBook.Click += (s, e) => SearchBookClicked?.Invoke(this, EventArgs.Empty);
            btnMyTickets.Click += (s, e) => MyTicketsClicked?.Invoke(this, EventArgs.Empty);
            btnNotifications.Click += (s, e) => NotificationsClicked?.Invoke(this, EventArgs.Empty);
            Settings.Click += (s, e) => SettingsClicked?.Invoke(this, EventArgs.Empty);
            Account.Click += (s, e) => AccountClicked?.Invoke(this, EventArgs.Empty);

            logoutuser.Click += (s, e) => LogoutClicked?.Invoke(this, EventArgs.Empty);
        }


        private void HideAllPanels()
        {
            foreach (Control c in panelMain.Controls)
                c.Hide();
        }


        private void ShowOnly(Control panelToShow, Guna2Button activeButton)
        {
            HideAllPanels();
            panelToShow.Show();
            panelToShow.BringToFront();
            SetActiveButton(activeButton);
        }


        private void SetActiveButton(Guna2Button activeBtn)
        {
            Guna2Button[] buttons =
            {
                btnUpcomingFlights,
                btnSearchBook,
                btnMyTickets,
                btnNotifications,
                Settings,
                Account,
                logoutuser,
                guna2Button1
            };

            foreach (var btn in buttons)
                btn.FillColor = Color.Transparent;

            activeBtn.FillColor = Color.DarkCyan;
        }

        public void Logout()
        {
            HideAllPanels();

        }

        // Optional: you can use these if you need events from the presenter
        public void UpcomingFlights() => ShowOnly(upcomingFlights1, btnUpcomingFlights);
        public void SearchBook() => ShowOnly(searchAndBooking1, btnSearchBook);
        public void MyTickets()
        {
            myTicketsBookingHistory1.Initialize(_navigation);
            myTicketsBookingHistory1.Activate();
            ShowOnly(myTicketsBookingHistory1, btnMyTickets);
        }
        public void Notifications() => ShowOnly(notifications1, btnNotifications);
        public void UserSettings()
        {
            userSettings1.Initialize(_navigation);
            userSettings1.Activate();
            ShowOnly(userSettings1, Settings);
        }

        public void UserAccount() => ShowOnly(userAccount1, Account);
        public void ShowMainUser()
        {
            ShowOnly(mainUserPage1, guna2Button1);
        }


        private void UserDashboard_Load(object sender, EventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void myTicketsBookingHistory1_Load(object sender, EventArgs e)
        {

        }

        private void mainUserPage1_Load(object sender, EventArgs e)
        {

        }
    }
}
