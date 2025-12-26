using Airport_Airplane_management_system.View.Forms.AdminPages;
using Airport_Airplane_management_system.View.Forms.LoginPages;
using Airport_Airplane_management_system.View.Forms.UserPages;
using Airport_Airplane_management_system.View.Interfaces;

public partial class Main1 : Form, INavigationService
{
    
    private int _currentUserId; // added for settings panel

    private readonly LoginPage loginPage;
    private readonly UserDashboard userDashboard;
    private readonly AdminDashboard adminDashboard;
    private readonly Signupusercontrol signUpPage;
    private readonly ForgetUserControl forgotPasswordPage;

    public Main1()
    {
        InitializeComponent();

        // Initialize pages and pass this as INavigationService
        loginPage = new LoginPage(this);
        userDashboard = new UserDashboard(this);
        adminDashboard = new AdminDashboard();
        signUpPage = new Signupusercontrol(this);
        forgotPasswordPage = new ForgetUserControl(this);

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

    public void SetCurrentUserId(int userId) => _currentUserId = userId; // added for settings panel
    public int GetCurrentUserId() => _currentUserId; // added for settings panel

    public void NavigateToLogin() => loginPage.BringToFront();
    public void NavigateToUser() => userDashboard.BringToFront();
    public void NavigateToAdmin() => adminDashboard.BringToFront();
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
}
