using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Services;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Presenter;
using Airport_Airplane_management_system.Presenter.AdminPages;
using Airport_Airplane_management_system.Presenter.AdminPagesPresenters;
using Airport_Airplane_management_system.View.Forms.AdminPages;
using Airport_Airplane_management_system.View.Forms.LoginPages;
using Airport_Airplane_management_system.View.Forms.UserPages;
using Airport_Airplane_management_system.View.Interfaces;
using Ticket_Booking_System_OOP.Model.Repositories;

public partial class Main1 : Form, INavigationService
{
    private readonly LoginPage loginPage;
    private readonly UserDashboard userDashboard;
    private readonly AdminDashboard adminDashboard;
    private readonly Signupusercontrol signUpPage;
    private readonly ForgetUserControl forgotPasswordPage;

    private readonly AdminDashboardPresenter _adminPresenter;

    public Main1()
    {
        InitializeComponent();

        loginPage = new LoginPage(this);
        userDashboard = new UserDashboard(this);

        // ONE instance فقط
        adminDashboard = new AdminDashboard();

        signUpPage = new Signupusercontrol(this);
        forgotPasswordPage = new ForgetUserControl(this);

        // Docking
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

        // Presenter uses the SAME adminDashboard instance + SAME pages inside it
        _adminPresenter = new AdminDashboardPresenter(
            adminDashboard,
            adminDashboard.dashboard1,        // if you have dashboard1 use it here instead
            adminDashboard.crewManagment1,
            adminDashboard.flightManagment1,
            adminDashboard.planeMangement1,
            adminDashboard.passengerManagement1,
            adminDashboard.reports1,
            adminDashboard.accountSettings1
        );
        string connStr = "server=localhost;port=3306;database=user;user=root;password=2006";

        ICrewRepository crewRepo = new MySqlCrewRepository(connStr);

        // create flight repo too (because CrewService needs flights)
        IFlightRepository flightRepo = new MySqlFlightRepository(connStr);

        ICrewService crewService = new CrewService(crewRepo, flightRepo);

        // keep presenter as a FIELD (recommended), not local var
        var _crewPresenter = new CrewManagementPresenter(adminDashboard.crewManagment1, crewService);

        NavigateToLogin();
    }

    public void NavigateToLogin() => loginPage.BringToFront();
    public void NavigateToUser() => userDashboard.BringToFront();
    public void NavigateToAdmin() => adminDashboard.BringToFront();
    public void NavigateToSignUp() => signUpPage.BringToFront();
    public void NavigateToForgotPassword() => forgotPasswordPage.BringToFront();
}
