using Airport_Airplane_management_system.Model.Core.Classes.Exceptions;
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
        // ================= EVENTS =================
        public event EventHandler UpcomingFlightsClicked;
        public event EventHandler SearchBookClicked;
        public event EventHandler MyTicketsClicked;
        public event EventHandler NotificationsClicked;
        public event EventHandler SettingsClicked;
        public event EventHandler AccountClicked;
        public event EventHandler LogoutClicked;
        public event EventHandler UserMainClicked;

        // ================= UI =================
        private Panel panelMain;
        private Guna2CircleButton _notifBadge;

        // ================= PRESENTERS =================
        private UserDashboardPresenter _dashboardPresenter;
        private MainUserPagePresenter _mainPresenter;
        private UpcomingFlightsPresenter _upcomingPresenter;
        private SearchAndBookingPresenter _searchPresenter;
        private UserNotificationsPresenter _notificationsPresenter;
        private MyTicketsPresenter _ticketsPresenter;
        private UserAccountPresenter _accountPresenter;

        private readonly INavigationService _navigation;
        private readonly IAppSession _session;

        // ================= CONSTRUCTOR =================
        public UserDashboard(IAppSession session, INavigationService navigation)
        {
            InitializeComponent();

            _session = session;
            _navigation = navigation;

            panelMain = new Panel { Dock = DockStyle.Fill };
            Controls.Add(panelMain);
            guna2Panel1.Dock = DockStyle.Left;
            Controls.Add(guna2Panel1);

            CreateNotificationsBadge();
            InitializeButtonEvents();

            _dashboardPresenter = new UserDashboardPresenter(this, _navigation, _session);

            // Add all user controls to the main panel
            panelMain.Controls.Add(mainUserPage1);
            panelMain.Controls.Add(upcomingFlights1);
            panelMain.Controls.Add(searchAndBooking1);
            panelMain.Controls.Add(myTicketsBookingHistory1);
            panelMain.Controls.Add(notifications1);
            panelMain.Controls.Add(userAccount1);

            // Badge refresh triggers
            notifications1.BadgeRefreshRequested += () => _dashboardPresenter.RefreshNotifications();
            myTicketsBookingHistory1.BadgeRefreshRequested += () => _dashboardPresenter.RefreshNotifications();

            ShowMainUser();
        }

        // ================= BUTTON EVENTS =================
        private void InitializeButtonEvents()
        {
            guna2Button1.Click += (_, _) => UserMainClicked?.Invoke(this, EventArgs.Empty);
            btnUpcomingFlights.Click += (_, _) => UpcomingFlightsClicked?.Invoke(this, EventArgs.Empty);
            btnSearchBook.Click += (_, _) => SearchBookClicked?.Invoke(this, EventArgs.Empty);
            btnMyTickets.Click += (_, _) => MyTicketsClicked?.Invoke(this, EventArgs.Empty);
            btnNotifications.Click += (_, _) => NotificationsClicked?.Invoke(this, EventArgs.Empty);
            Account.Click += (_, _) => AccountClicked?.Invoke(this, EventArgs.Empty);
            logoutuser.Click += (_, _) => LogoutClicked?.Invoke(this, EventArgs.Empty);
        }

        // ================= PANEL MANAGEMENT =================
        private void HideAll()
        {
            foreach (Control c in panelMain.Controls)
                c.Hide();
        }

        private void Show(Control control, Guna2Button btn)
        {
            HideAll();
            control.Show();
            control.BringToFront();
            Highlight(btn);
        }

        private void Highlight(Guna2Button active)
        {
            foreach (var b in new[]
            {
                guna2Button1, btnUpcomingFlights, btnSearchBook,
                btnMyTickets, btnNotifications, Account
            })
                b.FillColor = Color.Transparent;

            active.FillColor = Color.DarkCyan;
        }

        // ================= VIEW METHODS =================
        public void ShowMainUser()
        {
            _mainPresenter ??= new MainUserPagePresenter(mainUserPage1, _session);
            _mainPresenter.RefreshData();
            Show(mainUserPage1, guna2Button1);
        }

        public void UpcomingFlights()
        {
            _upcomingPresenter ??= new UpcomingFlightsPresenter(upcomingFlights1, _session, _dashboardPresenter);
            _upcomingPresenter.RefreshData();
            Show(upcomingFlights1, btnUpcomingFlights);
        }

        public void SearchBook()
        {
            _searchPresenter ??= new SearchAndBookingPresenter(searchAndBooking1, _session, _dashboardPresenter);
            _searchPresenter.RefreshData();
            Show(searchAndBooking1, btnSearchBook);
        }

        public void MyTickets()
        {
            _ticketsPresenter ??= new MyTicketsPresenter(myTicketsBookingHistory1, _session);
            _ticketsPresenter.RefreshData();
            Show(myTicketsBookingHistory1, btnMyTickets);
        }

        public void Notifications()
        {
            notifications1.Initialize(_navigation, _session);

            _notificationsPresenter ??= new UserNotificationsPresenter(notifications1, _session);
            _notificationsPresenter.RefreshData();

            Show(notifications1, btnNotifications);
            _dashboardPresenter.RefreshNotifications();
        }

        public void UserAccount()
        {
            _accountPresenter ??= new UserAccountPresenter(userAccount1, _session);
            _accountPresenter.RefreshData();
            Show(userAccount1, Account);
        }

        public void Logout() => HideAll();

        public void OpenBooking(int flightId)
        {
            var form = new BookingPage(flightId, _session);
            new BookingPresenter(form, _session, flightId);
            form.ShowDialog();
        }

        // ================= NOTIFICATION BADGE =================
        private void CreateNotificationsBadge()
        {
            _notifBadge = new Guna2CircleButton
            {
                Size = new Size(14, 14),
                FillColor = Color.Red,
                BackColor = Color.Transparent,
                Visible = false
            };

            guna2Panel1.Controls.Add(_notifBadge);
            btnNotifications.LocationChanged += (_, _) => PositionBadge();
            btnNotifications.SizeChanged += (_, _) => PositionBadge();
            PositionBadge();
        }

        private void PositionBadge()
        {
            _notifBadge.Location = new Point(
                btnNotifications.Right - _notifBadge.Width - 18,
                btnNotifications.Top + 6);
        }

        public void SetUnreadNotificationsCount(int count)
        {
            _notifBadge.Visible = count > 0;
            _notifBadge.Text = count > 99 ? "99+" : count.ToString();

            if (_notifBadge.Visible)
            {
                _notifBadge.BringToFront();
                PositionBadge();
            }
        }

        private void UserDashboard_Load(object sender, EventArgs e) { }
    }
}
