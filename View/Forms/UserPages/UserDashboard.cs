using Airport_Airplane_management_system.Presenters;
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
        public event EventHandler Main;

        private readonly INavigationService _navigation;
        private readonly UserDashboardPresenter _presenter;
        public UserDashboard(INavigationService navigation)
        {
            InitializeComponent();

            _navigation = navigation;

            userSettings1.Initialize(_navigation);

            InitializePanelContent();
            InitializeButtonEvents();

            _presenter = new UserDashboardPresenter(this, _navigation);
        }


        private void InitializeButtonEvents()
        {
            guna2Button1.Click += (s, e) =>
            {
                SetActiveButton(guna2Button1);
                Main?.Invoke(this, EventArgs.Empty);
            };
            btnUpcomingFlights.Click += (s, e) =>
            {
                SetActiveButton(btnUpcomingFlights);
                UpcomingFlightsClicked?.Invoke(this, EventArgs.Empty);
            };
            btnSearchBook.Click += (s, e) =>
            {
                SetActiveButton(btnSearchBook);
                SearchBookClicked?.Invoke(this, EventArgs.Empty);
            };
            btnMyTickets.Click += (s, e) =>
            {
                SetActiveButton(btnMyTickets);
                MyTicketsClicked?.Invoke(this, EventArgs.Empty);
            };
            btnNotifications.Click += (s, e) =>
            {
                SetActiveButton(btnNotifications);
                NotificationsClicked?.Invoke(this, EventArgs.Empty);
            };
            Settings.Click += (s, e) =>
            {
                SetActiveButton(Settings);
                SettingsClicked?.Invoke(this, EventArgs.Empty);
            };
            Account.Click += (s, e) =>
            {
                SetActiveButton(Account);
                AccountClicked?.Invoke(this, EventArgs.Empty);
            };
            logoutuser.Click += (s, e) =>
            {
                SetActiveButton(logoutuser);
                LogoutClicked?.Invoke(this, EventArgs.Empty);
            };
        }
        private void InitializePanelContent()
        {

        }

        private void SetActiveButton(Guna2Button activeBtn)
        {
            Guna2Button[] buttons = { btnUpcomingFlights, btnSearchBook, btnMyTickets, btnNotifications, Settings, Account, logoutuser,guna2Button1 };

            foreach (var btn in buttons)
            {
                btn.FillColor = Color.Transparent;
               
            }

            activeBtn.FillColor = Color.DarkCyan;
          
        }

        public void UpcomingFlights()
        {
            upcomingFlights1.Show(); // was 2 make it 1
            searchAndBooking1.Hide();
            myTicketsBookingHistory1.Hide();
            notifications1.Hide();
            userSettings1.Hide();
            userAccount1.Hide();

        }
        public void SearchBook()
        {
            upcomingFlights1.Hide(); // was 2 make it 1
            searchAndBooking1.Show();
            myTicketsBookingHistory1.Hide();
            notifications1.Hide();
            userSettings1.Hide();
            userAccount1.Hide();
        }
        public void MyTickets()
        {
            upcomingFlights1.Hide(); // was 2 make it 1
            searchAndBooking1.Hide();
            myTicketsBookingHistory1.Show();
            notifications1.Hide();
            userSettings1.Hide();
            userAccount1.Hide();
        }
        public void Notifications()
        {
            upcomingFlights1.Hide(); // was 2 make it 1
            searchAndBooking1.Hide();
            myTicketsBookingHistory1.Hide();
            notifications1.Show();
            userSettings1.Hide();
            userAccount1.Hide();
        }
        public void UserSettings()
        {
            upcomingFlights1.Hide();
            searchAndBooking1.Hide();
            myTicketsBookingHistory1.Hide();
            notifications1.Hide();
            userAccount1.Hide();

            userSettings1.Show();
            userSettings1.BringToFront();
        }



        public void UserAccount()
        {
            upcomingFlights1.Hide(); // was 2 make it 1
            searchAndBooking1.Hide();
            myTicketsBookingHistory1.Hide();
            notifications1.Hide();
            userSettings1.Hide();
            userAccount1.Show();
        }
        public void Logout()
        {
            
        }

        private void upcomingFlights2_Load(object sender, EventArgs e)
        {

        } 

        private void UserDashboard_Load(object sender, EventArgs e)
        {

        }

        private void userAccount1_Load(object sender, EventArgs e)
        {

        }

        private void myTicketsBookingHistory1_Load(object sender, EventArgs e)
        {

        }
    }
}
