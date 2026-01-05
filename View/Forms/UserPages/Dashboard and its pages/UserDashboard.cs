using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Presenter;
using Airport_Airplane_management_system.Presenter.UserPagesPresenters;
using Airport_Airplane_management_system.View.Interfaces;
using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    public partial class UserDashboard : UserControl, IUserDashboardView
    {
        // =======================
        // EVENTS
        // =======================
        public event EventHandler UpcomingFlightsClicked;
        public event EventHandler SearchBookClicked;
        public event EventHandler MyTicketsClicked;
        public event EventHandler NotificationsClicked;
        public event EventHandler SettingsClicked;
        public event EventHandler AccountClicked;
        public event EventHandler LogoutClicked;
        public event EventHandler UserMainClicked;

        // =======================
        // SERVICES & NAVIGATION
        // =======================
        private readonly INavigationService _navigation;
        private readonly IAppSession _session;
        private readonly NotificationsCounterService _notifCounterService;
        private readonly INotificationsCounterRepository _notifCounterRepo;

        // =======================
        // PANELS
        // =======================
        private Panel panelMain;

        // =======================
        // PRESENTERS (LAZY LOADED)
        // =======================
        private UserDashboardPresenter _dashboardPresenter;
        private MainUserPagePresenter _mainUserPagePresenter;
        private UpcomingFlightsPresenter _upcomingFlightsPresenter;
        private SearchAndBookingPresenter _searchAndBookingPresenter;
        private UserNotificationsPresenter _notificationsPresenter;
        private MyTicketsPresenter _myTicketsPresenter;
        private BookingPresenter _bookingPresenter;
        private UserAccountPresenter _userAccountPresenter;

        // =======================
        // NOTIFICATION BADGE
        // =======================
        private Guna2CircleButton _notifBadge;

        // =======================
        // CONSTRUCTOR
        // =======================
        public UserDashboard(IAppSession session, INavigationService navigation)
        {
            InitializeComponent();

            _navigation = navigation;
            _session = session ?? throw new ArgumentNullException(nameof(session));

            // Notifications
            _notifCounterRepo = new MySqlNotificationsCounterRepository( "server=localhost;port=3306;database=user;user=root;password=2006");
            _notifCounterService = new NotificationsCounterService(_notifCounterRepo);
            CreateNotificationsBadge();

            // Main panel for switching views
            panelMain = new Panel { Dock = DockStyle.Fill };
            Controls.Add(panelMain);
            guna2Panel1.Dock = DockStyle.Left;
            Controls.Add(guna2Panel1);

            // Dashboard presenter
            _dashboardPresenter = new UserDashboardPresenter(this, _navigation);

            // Initialize MainUserPage immediately
            _mainUserPagePresenter = new MainUserPagePresenter(mainUserPage1, _session);

            panelMain.Controls.Add(mainUserPage1);
            panelMain.Controls.Add(upcomingFlights1);
            panelMain.Controls.Add(searchAndBooking1);
            panelMain.Controls.Add(myTicketsBookingHistory1);
            panelMain.Controls.Add(notifications1);
            panelMain.Controls.Add(userAccount1);

            // Initialize button events
            InitializeButtonEvents();

            // Badge refresh events
            notifications1.BadgeRefreshRequested += () => RefreshNotificationsBadge();
            myTicketsBookingHistory1.BadgeRefreshRequested += () => RefreshNotificationsBadge();
            notifications1.SeeTicketRequested += (bookingId) =>
            {
                MyTickets();
                myTicketsBookingHistory1.FocusBooking(bookingId);
            };

            // Show default panel
            HideAllPanels();
            ShowMainUser();
            RefreshNotificationsBadge();
        }

        // =======================
        // BUTTON CLICK HANDLERS
        // =======================
        private void InitializeButtonEvents()
        {
            guna2Button1.Click += (s, e) => ShowMainUser();
            btnUpcomingFlights.Click += (s, e) => UpcomingFlights();
            btnSearchBook.Click += (s, e) => SearchBook();
            btnMyTickets.Click += (s, e) => MyTickets();
            btnNotifications.Click += (s, e) => Notifications();
            Account.Click += (s, e) => UserAccount();
            logoutuser.Click += (s, e) => LogoutClicked?.Invoke(this, EventArgs.Empty);
        }

        // =======================
        // PANEL SWITCHING
        // =======================
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
                Account,
                logoutuser,
                guna2Button1
            };

            foreach (var btn in buttons)
                btn.FillColor = Color.Transparent;

            activeBtn.FillColor = Color.DarkCyan;
        }

        // =======================
        // SHOW PANELS WITH LAZY PRESENTER LOADING
        // =======================
        public void ShowMainUser()
        {
            if (_mainUserPagePresenter == null)
                _mainUserPagePresenter = new MainUserPagePresenter(mainUserPage1, _session);

            _mainUserPagePresenter.RefreshData();
            ShowOnly(mainUserPage1, guna2Button1);
        }

        public void UpcomingFlights()
        {
            if (_upcomingFlightsPresenter == null)
                _upcomingFlightsPresenter = new UpcomingFlightsPresenter(upcomingFlights1, _session, _dashboardPresenter);

            _upcomingFlightsPresenter.RefreshData();
            ShowOnly(upcomingFlights1, btnUpcomingFlights);
        }

        public void SearchBook()
        {
            if (_searchAndBookingPresenter == null)
                _searchAndBookingPresenter = new SearchAndBookingPresenter(searchAndBooking1, _session, _dashboardPresenter);

            _searchAndBookingPresenter.RefreshData();
            ShowOnly(searchAndBooking1, btnSearchBook);
        }

        public void Notifications()
        {
            if (_notificationsPresenter == null)
                _notificationsPresenter = new UserNotificationsPresenter(notifications1);

            _notificationsPresenter.RefreshData();
            notifications1.Initialize(_navigation);
            notifications1.Activate();
            RefreshNotificationsBadge();
            ShowOnly(notifications1, btnNotifications);
        }

        public void MyTickets()
        {
            if (_myTicketsPresenter == null)
                _myTicketsPresenter = new MyTicketsPresenter(myTicketsBookingHistory1, _session);

            _myTicketsPresenter.RefreshData();
            myTicketsBookingHistory1.Initialize(_navigation, _session);
            myTicketsBookingHistory1.Activate();
            ShowOnly(myTicketsBookingHistory1, btnMyTickets);
        }

        public void UserAccount()
        {
            if (_userAccountPresenter == null)
                _userAccountPresenter = new UserAccountPresenter(userAccount1);

            _userAccountPresenter.RefreshData();
            userAccount1.Initialize(_navigation);
            userAccount1.Activate();
            ShowOnly(userAccount1, Account);
        }

        // =======================
        // LOGOUT
        // =======================
        public void Logout()
        {
            HideAllPanels();
        }

        // =======================
        // BOOKING
        // =======================
        public void OpenBooking(int flightId)
        {
            var bookingForm = new BookingPage(flightId, _session);
            _bookingPresenter = new BookingPresenter(bookingForm, _session, flightId);
            bookingForm.ShowDialog();
            RefreshNotificationsBadge();
        }

        // =======================
        // NOTIFICATIONS BADGE
        // =======================
        private void CreateNotificationsBadge()
        {
            _notifBadge = new Guna.UI2.WinForms.Guna2CircleButton
            {
                Size = new Size(14, 14),       // small circle
                FillColor = Color.Red,
                BackColor = Color.Transparent,
                Visible = false,               // hide until you need it
                Enabled = true,                // keep it active
                BorderThickness = 0,
                ShadowDecoration = { Enabled = false }
            };


            // put it on side panel
            guna2Panel1.Controls.Add(_notifBadge);
            _notifBadge.BringToFront();
            _notifBadge.BringToFront();

            // keep it positioned even if UI resizes
            btnNotifications.LocationChanged += (_, _) => PositionNotificationsBadge();
            btnNotifications.SizeChanged += (_, _) => PositionNotificationsBadge();
            guna2Panel1.SizeChanged += (_, _) => PositionNotificationsBadge();

            PositionNotificationsBadge();
        }

        private void PositionNotificationsBadge()
        {
            if (_notifBadge == null) return;

            // Place it at the TOP-RIGHT corner of the Notifications button
            int x = btnNotifications.Right - _notifBadge.Width - 18;
            int y = btnNotifications.Top + 6;

            _notifBadge.Location = new Point(x, y);
            _notifBadge.BringToFront();
        }

        public void SetUnreadNotificationsCount(int count)
        {
            if (_notifBadge == null) return;

            if (count <= 0)
            {
                _notifBadge.Visible = false;
                return;
            }

            _notifBadge.Text = count > 99 ? "99+" : count.ToString();
            _notifBadge.Visible = true;
            _notifBadge.BringToFront();
        }
        private void RefreshNotificationsBadge()
        {
            if (_notifCounterService == null) return;

            int count = _notifCounterService.GetUnreadCount(_navigation.GetCurrentUserId());
            SetUnreadNotificationsCount(count);
        }

        private void UserDashboard_Load(object sender, EventArgs e)
        {

        }
    }
}
