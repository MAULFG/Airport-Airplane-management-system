using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Exceptions;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Presenter;
using Airport_Airplane_management_system.Presenter.UserPagesPresenters;
using Airport_Airplane_management_system.View.Interfaces;
using Guna.UI2.WinForms;
using MySqlX.XDevAPI;
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
        
        private  BookingPage _bookForm;
        private readonly INavigationService _navigation;
        private readonly UserDashboardPresenter _presenter;
        private readonly FlightService _flightService;
        private readonly PassengerService _passengerService;
        private readonly MyTicketsService _myTicketsService;
        private readonly BookingService _bookingService;
        private UpcomingFlightsPresenter _upcomingFlightsPresenter;
        private SearchAndBookingPresenter _searchandbookingpresenter;
        private MainUserPagePresenter _mainUserPagePresenter;
        private MyTicketsPresenter _myTicketsPresenter;
        private BookingPresenter _bookingpresenter;
        private Panel panelMain;
        private readonly IAppSession _session;
        public UserDashboard(MyTicketsService myticketservice ,INavigationService navigation, FlightService flightService, BookingService bookingService, PassengerService passengerService, IAppSession session)
        {
            InitializeComponent();

            _navigation = navigation;
            _flightService = flightService;
            _bookingService = bookingService;
            _passengerService = passengerService ?? throw new ArgumentNullException(nameof(passengerService));
            _myTicketsService = myticketservice;
            _session = session ?? throw new ArgumentNullException(nameof(session));

            // Initialize main panel
            panelMain = new Panel();
            panelMain.Dock = DockStyle.Fill;

            // Side menu panel
            guna2Panel1.Dock = DockStyle.Left;

            // Add in correct order
            Controls.Add(panelMain);
            Controls.Add(guna2Panel1); // buttons panel on top
            _presenter = new UserDashboardPresenter(this, _navigation);



            
 

            _mainUserPagePresenter =new MainUserPagePresenter(mainUserPage1, _session, _flightService,_bookingService);

            _upcomingFlightsPresenter = new UpcomingFlightsPresenter(upcomingFlights1, _flightService,_presenter);
            _searchandbookingpresenter = new SearchAndBookingPresenter(searchAndBooking1, _flightService, _navigation, _presenter);
            _myTicketsPresenter = new MyTicketsPresenter(myTicketsBookingHistory1, _myTicketsService);
            // Add all designer panels to panelMain
            panelMain.Controls.Add(mainUserPage1);
            panelMain.Controls.Add(upcomingFlights1);
            panelMain.Controls.Add(searchAndBooking1);
            panelMain.Controls.Add(myTicketsBookingHistory1);
            panelMain.Controls.Add(notifications1);
            panelMain.Controls.Add(userSettings1);
            panelMain.Controls.Add(userAccount1);

            HideAllPanels();
            ShowMainUser();
            InitializeButtonEvents();
        }


        public void OpenBooking(int flightId)
        {
            var bookingForm = new BookingPage(flightId, _bookingService, _passengerService, _session);
            _bookingpresenter = new BookingPresenter(bookingForm, _flightService, flightId);
            bookingForm.ShowDialog();
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
        public void MyTickets() => ShowOnly(myTicketsBookingHistory1, btnMyTickets);
        public void Notifications() => ShowOnly(notifications1, btnNotifications);
        public void UserSettings() => ShowOnly(userSettings1, Settings);
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
