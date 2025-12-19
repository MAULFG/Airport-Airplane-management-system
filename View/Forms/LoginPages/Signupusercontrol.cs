using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Presenter;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection.Metadata;
using System.Text;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.LoginPages
{
    public partial class Signupusercontrol : UserControl, ISignupView
    {
        private readonly SignupPresenter _presenter;
        public Signupusercontrol()
        {
            InitializeComponent();
            fnameTB.TextChanged += InputChanged;
            lnameTB.TextChanged += InputChanged;
            usernameTB.TextChanged += InputChanged;
            EmailTB.TextChanged += InputChanged;
            PasswordTB.TextChanged += InputChanged;
            PasswordTB2.TextChanged += InputChanged;

            CenterPanelVertically();
            this.Resize += ForgetUserControl_Resize;
            signupbtn.Click += (s, e) => SignupClicked?.Invoke(this, EventArgs.Empty);
            guna2HtmlLabel2.Click += (s, e) => ReturnToLoginClicked?.Invoke(this, EventArgs.Empty);
            IUserRepository userRepo = new MySqlUserRepository(
                "server=localhost;port=3306;database=user;user=root;password=2006"
            );
            UserService userService = new UserService(userRepo);

            _presenter = new SignupPresenter(this, userService);
        }
        private void InputChanged(object sender, EventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2TextBox tb && !string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.BorderColor = Color.DarkGray;
                tb.BorderThickness = 2;
            }
        }
        private void Signupusercontrol_Load(object sender, EventArgs e)
        {

        }
       

        public void ClearFields()
        {
           
            fnameTB.Text = "";
            lnameTB.Text = "";
            usernameTB.Text = "";
            EmailTB.Text = "";
            PasswordTB.Text = "";
            PasswordTB2.Text = "";

            fnameTB.BorderColor = Color.DarkGray;
            lnameTB.BorderColor = Color.DarkGray;
            usernameTB.BorderColor = Color.DarkGray;
            EmailTB.BorderColor = Color.DarkGray;
            PasswordTB.BorderColor = Color.DarkGray;
            PasswordTB2.BorderColor = Color.DarkGray;
            
        }
        public string FName => fnameTB.Text;
        public string LName => lnameTB.Text;
        public string Username => usernameTB.Text;
        public string Email => EmailTB.Text;
        public string Password => PasswordTB.Text;
        public string ConfirmPassword => PasswordTB2.Text;
       
        public void HighlightFields1(bool newpassError, bool confirmpassError)
        {
            PasswordTB.BorderColor = newpassError ? Color.Red : Color.DarkGray;
            PasswordTB2.BorderColor = confirmpassError ? Color.Red : Color.DarkGray;
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void ForgetUserControl_Resize(object sender, EventArgs e)
        {
            CenterPanelVertically();
        }
        public void Returnlogin()
        {
            var main = (Main1)this.ParentForm;
            main.ShowLogin();
        }
        public event EventHandler SignupClicked;
        public event EventHandler ReturnToLoginClicked;
        private void CenterPanelVertically()
        {
            if (guna2CustomGradientPanel1 != null)
            {
                guna2CustomGradientPanel1.Top = (this.Height - guna2CustomGradientPanel1.Height) / 2;
            }
        }
    }
}
