using Airport_Airplane_management_system.Model.Core.Classes.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Presenter.AdminPagesPresenters;
using Airport_Airplane_management_system.Repositories;
using Airport_Airplane_management_system.View.Forms.AdminPages;
using Airport_Airplane_management_system.View.Forms.LoginPages;
using Airport_Airplane_management_system.View.Forms.UserPages;
using Airport_Airplane_management_system.View.Interfaces;
using System;
using System.Windows.Forms;
using Ticket_Booking_System_OOP.Model.Repositories;

public partial class Main1 : Form, INavigationService
{
    private readonly LoginPage loginPage;
    private UserDashboard userDashboard;
    private readonly AdminDashboard adminDashboard;
    private readonly Signupusercontrol signUpPage;
    private readonly ForgetUserControl forgotPasswordPage;

    private readonly IAppSession session;

    // Repos created once
    private readonly IFlightRepository flightRepo;
    private readonly IPlaneRepository planeRepo;
    private readonly ICrewRepository crewRepo;
    private readonly IPassengerRepository passRepo;

    // Presenters created once
    private readonly AdminDashboardPresenter adminDashboardPresenter;

    private int _currentUserId;

    public Main1()
    {
        InitializeComponent();

        session = new AppSession();
        this.AutoScaleMode = AutoScaleMode.Dpi;

        string cs = "server=localhost;port=3306;database=user;user=root;password=2006";

        flightRepo = new MySqlFlightRepository(cs);
        planeRepo = new MySqlPlaneRepository(cs);
        crewRepo = new MySqlCrewRepository(cs);
        passRepo = new MySqlPassengerRepository(cs);

        // Views
        loginPage = new LoginPage(this, session);
        userDashboard = new UserDashboard(session, this);
        adminDashboard = new AdminDashboard();
        signUpPage = new Signupusercontrol(this);
        forgotPasswordPage = new ForgetUserControl(this);

        // Wire presenters
        adminDashboardPresenter = new AdminDashboardPresenter(
            adminDashboard,
            this,
            session,
            flightRepo,
            planeRepo,
            crewRepo,
            passRepo
        );

        // Dock pages
        loginPage.Dock = DockStyle.Fill;
        userDashboard.Dock = DockStyle.Fill;
        adminDashboard.Dock = DockStyle.Fill;
        signUpPage.Dock = DockStyle.Fill;
        forgotPasswordPage.Dock = DockStyle.Fill;

        Controls.Add(loginPage);
        Controls.Add(userDashboard);
        Controls.Add(adminDashboard);
        Controls.Add(signUpPage);
        Controls.Add(forgotPasswordPage);

        NavigateToLogin();
    }

    public void SetCurrentUserId(int userId) => _currentUserId = userId;
    public int GetCurrentUserId() => _currentUserId;

    public void NavigateToUser() => userDashboard.BringToFront();

    public void NavigateToAdmin()
    {
        adminDashboard.BringToFront();
        adminDashboardPresenter.ShowMain(); // refresh main each time you enter admin
    }

    public void NavigateToLogin() => loginPage.BringToFront();
    public void NavigateToSignUp() => signUpPage.BringToFront();
    public void NavigateToForgotPassword() => forgotPasswordPage.BringToFront();

    public void ShowAdmin() => NavigateToAdmin();
    public void ShowUser() => NavigateToUser();
    public void ShowSignUp() => NavigateToSignUp();
    public void ShowForget() => NavigateToForgotPassword();
    public void ReturnLogin() => NavigateToLogin();
    // This is referenced by Main1.Designer.cs
    private void Main1_Load(object sender, EventArgs e)
    {
        // keep empty
    }

}
