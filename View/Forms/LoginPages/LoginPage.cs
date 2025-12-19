using System;
using System.Drawing;
using System.Windows.Forms;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.View.Forms.LoginPages;
using Airport_Airplane_management_system.Presenter;

namespace Airport_Airplane_management_system.View.Forms.LoginPages
{
    public partial class LoginPage : UserControl, ILoginView
    {
        private readonly LoginPresenter _presenter;

        public LoginPage()
        {
            InitializeComponent();

            // Enter key triggers login
            UsernameTB.KeyDown += TextBox_KeyDown;
            PasswordTB.KeyDown += TextBox_KeyDown;

            // Reset borders on typing
            UsernameTB.TextChanged += (s, e) => UsernameTB.BorderColor = Color.DarkGray;
            PasswordTB.TextChanged += (s, e) => PasswordTB.BorderColor = Color.DarkGray;

            // Connect events to MVP
            loginbtn.Click += (s, e) => LoginClicked?.Invoke(this, EventArgs.Empty);
            guna2HtmlLabel2.Click += (s, e) => SignUpClicked?.Invoke(this, EventArgs.Empty);
            lb2.Click += (s, e) => ForgotPasswordClicked?.Invoke(this, EventArgs.Empty);

            // Initialize repository and service
            var userRepo = new MySqlUserRepository(
                "server=localhost;port=3306;database=user;user=root;password=2006"
            );
            var userService = new UserService(userRepo);

            // Initialize presenter
            _presenter = new LoginPresenter(this, userService);
            // Center the panel horizontally initially
            CenterGradientPanelVertically();

            // Re-center when panel2 resizes horizontally
            panel2.Resize += (s, e) => CenterGradientPanelVertically();
        }
        private void CenterGradientPanelVertically()
        {
            if (guna2CustomGradientPanel1 != null && panel2 != null)
            {
                // Keep horizontal (Left) as-is
                // Center vertically
                guna2CustomGradientPanel1.Top = (panel2.ClientSize.Height - guna2CustomGradientPanel1.Height) / 2;
            }
        }


        // ILoginView implementation
        public string Username => UsernameTB.Text.Trim();
        public string Password => PasswordTB.Text.Trim();

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ClearFields()
        {
            UsernameTB.Text = "";
            PasswordTB.Text = "";
            UsernameTB.BorderColor = Color.DarkGray;
            PasswordTB.BorderColor = Color.DarkGray;
        }

        public void HighlightFields(bool usernameError, bool passwordError)
        {
            UsernameTB.BorderColor = usernameError ? Color.Red : Color.DarkGray;
            PasswordTB.BorderColor = passwordError ? Color.Red : Color.DarkGray;
        }

        public void ShowAdminPanel()
        {
            var main = (Main1)this.ParentForm;
            main.ShowAdmin();
        }

        public void ShowUserPanel()
        {
            var main = (Main1)this.ParentForm;
            main.ShowUser();
        }
        public void ShowForgetpasswordpage()
        {
            var main = (Main1)this.ParentForm;
            main.ShowForget();
        }
        public void ShowSignUpPage()
        {
            var main = (Main1)this.ParentForm;
            main.ShowSignUp();
        }
        // Events
        public event EventHandler LoginClicked;
        public event EventHandler SignUpClicked;
        public event EventHandler ForgotPasswordClicked;

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                loginbtn.PerformClick();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
