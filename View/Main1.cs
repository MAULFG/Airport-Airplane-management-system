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
    private AdminDashboard adminDashboard;
    private readonly Signupusercontrol signUpPage;
    private readonly ForgetUserControl forgotPasswordPage;

    private readonly IAppSession session;

    private int _currentUserId;

    // Initializes the main form and all child pages
    public Main1()
    {
        InitializeComponent();

        session = new AppSession();
        this.AutoScaleMode = AutoScaleMode.Dpi;

        loginPage = new LoginPage(this, session);
        userDashboard = new UserDashboard(session, this);
        adminDashboard = new AdminDashboard(this, session);
        signUpPage = new Signupusercontrol(this,session);
        forgotPasswordPage = new ForgetUserControl(this,session);

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

    // Sets the current user ID
    public void SetCurrentUserId(int userId) => _currentUserId = userId;

    // Gets the current user ID
    public int GetCurrentUserId() => _currentUserId;

    // Navigates to the user dashboard
    public void NavigateToUser()
    {
        if (userDashboard != null)
            Controls.Remove(userDashboard);
        userDashboard = new UserDashboard(session, this)
        {
            Dock = DockStyle.Fill
        };
        Controls.Add(userDashboard);
        userDashboard.BringToFront();
    }

    // Brings the admin dashboard to the front
    public void NavigateToAdmin()
    {
        adminDashboard.BringToFront();
    }

    // Brings the login page to the front
    public void NavigateToLogin() => loginPage.BringToFront();

    // Brings the sign-up page to the front
    public void NavigateToSignUp() => signUpPage.BringToFront();

    // Brings the forgot password page to the front
    public void NavigateToForgotPassword() => forgotPasswordPage.BringToFront();

    // Shows the admin dashboard
    public void ShowAdmin() => NavigateToAdmin();

    // Shows the user dashboard
    public void ShowUser() => NavigateToUser();

    // Shows the sign-up page
    public void ShowSignUp() => NavigateToSignUp();

    // Shows the forgot password page
    public void ShowForget() => NavigateToForgotPassword();

    // Returns to the login page
    public void ReturnLogin() => NavigateToLogin();

    // Handles the form load event
    private void Main1_Load(object sender, EventArgs e)
    {
       
    }

}
