using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Core.Classes.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Presenter;
using Airport_Airplane_management_system.Presenter.AdminPages;
using Airport_Airplane_management_system.Presenter.AdminPagesPresenters;
using Airport_Airplane_management_system.Repositories;
using Airport_Airplane_management_system.View.Forms.AdminPages;
using Airport_Airplane_management_system.View.Forms.LoginPages;
using Airport_Airplane_management_system.View.Forms.UserPages;
using Airport_Airplane_management_system.View.Interfaces;
using Microsoft.VisualBasic.ApplicationServices;
using Ticket_Booking_System_OOP.Model.Repositories;


public partial class Main1 : Form, INavigationService
{
    private readonly LoginPage loginPage;
    private UserDashboard userDashboard;
    private AdminDashboard adminDashboard;
    private readonly Signupusercontrol signUpPage;
    private readonly ForgetUserControl forgotPasswordPage;

  
    private readonly IAppSession session;
    
    private int _currentUserId;
    public Main1()
    {
        InitializeComponent();

        // Session
        session = new AppSession();

        // Repositories

        this.AutoScaleMode = AutoScaleMode.Dpi;



        // Pages
        loginPage = new LoginPage(this,  session);
        userDashboard = new UserDashboard(session, this);
        adminDashboard = new AdminDashboard(this);
        signUpPage = new Signupusercontrol(this);
        forgotPasswordPage = new ForgetUserControl(this);

        // Dock pages
        loginPage.Dock = DockStyle.Fill;
        userDashboard.Dock = DockStyle.Fill;
        adminDashboard.Dock = DockStyle.Fill;
        signUpPage.Dock = DockStyle.Fill;
        forgotPasswordPage.Dock = DockStyle.Fill;

        // Add pages to form
        Controls.Add(loginPage);
        Controls.Add(userDashboard);
        Controls.Add(adminDashboard);
        Controls.Add(signUpPage);
        Controls.Add(forgotPasswordPage);

        // Show login page initially
        NavigateToLogin();
    }


    public void SetCurrentUserId(int userId)
    {
        _currentUserId = userId;
    }

    public void NavigateToUser()
    {
        if (userDashboard != null)
            Controls.Remove(userDashboard);

        // Recreate with same service instances
        userDashboard = new UserDashboard(session, this )
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

    public void NavigateToLogin() => loginPage.BringToFront();
    public void NavigateToSignUp() => signUpPage.BringToFront();
    public void NavigateToForgotPassword() => forgotPasswordPage.BringToFront();

    public void ShowAdmin() => NavigateToAdmin();
    public void ShowUser() => NavigateToUser();
    public void ShowSignUp() => NavigateToSignUp();
    public void ShowForget() => NavigateToForgotPassword();
    public void ReturnLogin() => NavigateToLogin();


    private void Main1_Load(object sender, EventArgs e)
    {

    }
    public int GetCurrentUserId()
    {
        
        return _currentUserId;
    }
    
}
