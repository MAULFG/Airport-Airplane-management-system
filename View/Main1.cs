using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces;
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
    private  UserDashboard userDashboard;
    private   AdminDashboard adminDashboard;
    private readonly Signupusercontrol signUpPage;
    private readonly ForgetUserControl forgotPasswordPage;
    // Flight-related services
    private readonly IFlightRepository flightRepo;
    private readonly IUserRepository userRepo;
    private readonly IBookingRepository bookingRepo;
    private readonly IPlaneRepository planeRepo;
    
    private readonly FlightService flightService;
    private readonly UpcomingFlights upcomingFlightsPage;
    private readonly AdminDashboardPresenter _adminPresenter;

    private int _currentUserId;
    public Main1()
    {
        InitializeComponent();

        flightRepo = new MySqlFlightRepository("server=localhost;port=3306;database=user;user=root;password=2006");
        userRepo = new MySqlUserRepository("server=localhost;port=3306;database=user;user=root;password=2006");
        bookingRepo = new MySqlBookingRepository("server=localhost;port=3306;database=user;user=root;password=2006");
        planeRepo = new MySqlPlaneRepository("server=localhost;port=3306;database=user;user=root;password=2006");
        
        flightService = new FlightService(flightRepo, userRepo, bookingRepo,planeRepo);
        
        // Initialize pages and pass this as INavigationService
        loginPage = new LoginPage(this);
        userDashboard = new UserDashboard(this);
        adminDashboard = new AdminDashboard(this);
        signUpPage = new Signupusercontrol(this);
        forgotPasswordPage = new ForgetUserControl(this);

       
        upcomingFlightsPage = new UpcomingFlights();
        upcomingFlightsPage.Dock = DockStyle.Fill;
        userDashboard.Controls.Add(upcomingFlightsPage);
        // Set docking
        loginPage.Dock = DockStyle.Fill;
        userDashboard.Dock = DockStyle.Fill;
        adminDashboard.Dock = DockStyle.Fill;
        signUpPage.Dock = DockStyle.Fill;
        forgotPasswordPage.Dock = DockStyle.Fill;

        // Add to form
        Controls.Add(loginPage);
        Controls.Add(userDashboard);
        Controls.Add(adminDashboard);
        Controls.Add(signUpPage);
        Controls.Add(forgotPasswordPage);
    
      
        // Show login page initially
        NavigateToLogin();
    }



    public void NavigateToLogin() => loginPage.BringToFront();
    public void NavigateToUser()
    {
        if (userDashboard != null)
            Controls.Remove(userDashboard);

        userDashboard = new UserDashboard(this)
        {
            Dock = DockStyle.Fill
        };

        Controls.Add(userDashboard);
        userDashboard.BringToFront();
    }

    public void NavigateToAdmin()
    {
        if (adminDashboard != null)
            Controls.Remove(adminDashboard);

        adminDashboard = new AdminDashboard(this)
        {
            Dock = DockStyle.Fill
        };

        Controls.Add(adminDashboard);
        adminDashboard.BringToFront();
    }
   


    public void NavigateToSignUp() => signUpPage.BringToFront();
    public void NavigateToForgotPassword() => forgotPasswordPage.BringToFront();

    // optional wrappers
    public void ShowAdmin() => NavigateToAdmin();
    public void ShowUser() => NavigateToUser();
    public void ShowSignUp() => NavigateToSignUp();
    public void ShowForget() => NavigateToForgotPassword();
    public void ReturnLogin() => NavigateToLogin();
   

    private void Main1_Load(object sender, EventArgs e)
    {

    }

    public void SetCurrentUserId(int userId)
    {
        _currentUserId = userId;
    }

    public int GetCurrentUserId()
    {
        return _currentUserId;
    }

}
