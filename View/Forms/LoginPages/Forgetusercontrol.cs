using Airport_Airplane_management_system.Model.Core.Classes.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.View.Interfaces;
using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.LoginPages
{
    public partial class ForgetUserControl : UserControl, IForgetPasswordView
    {
        private readonly IAppSession _session;
        private readonly ForgetPasswordPresenter _presenter;
        private readonly INavigationService _navigation;
        public ForgetUserControl(INavigationService navigation, IAppSession session)
        {
            InitializeComponent();
            _session = session;
            // Input reset visuals
            usernameTB.TextChanged += InputChanged;
            emailTB.TextChanged += InputChanged;
            newPasswordTB.TextChanged += InputChanged;
            confirmPasswordTB.TextChanged += InputChanged;

            CenterPanelVertically();
            this.Resize += ForgetUserControl_Resize;

            // Button events
            resetBtn.Click += (s, e) => ResetClicked?.Invoke(this, EventArgs.Empty);
            guna2HtmlLabel2.Click += (s, e) => ReturnToLoginClicked?.Invoke(this, EventArgs.Empty);

            // Setup MVP
            

            _presenter = new ForgetPasswordPresenter(this,_session, navigation);
            
        }

        #region IForgetPasswordView Implementation

        public string Username => usernameTB.Text.Trim();
        public string Email => emailTB.Text.Trim();
        public string NewPassword => newPasswordTB.Text.Trim();
        public string ConfirmPassword => confirmPasswordTB.Text.Trim();

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
       

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        

        public void ClearFields()
        {
            usernameTB.Text = "";
            emailTB.Text = "";
            newPasswordTB.Text = "";
            confirmPasswordTB.Text = "";

            usernameTB.BorderColor = Color.DarkGray;
            emailTB.BorderColor = Color.DarkGray;
            newPasswordTB.BorderColor = Color.DarkGray;
            confirmPasswordTB.BorderColor = Color.DarkGray;
        }
        public void HighlightFields2(bool newpassError, bool confirmpassError)
        {
            newPasswordTB.BorderColor = newpassError ? Color.Red : Color.DarkGray;
            confirmPasswordTB.BorderColor = confirmpassError ? Color.Red : Color.DarkGray;
        }
        public void HighlightFields1(bool usernameError, bool emailError)
        {
            usernameTB.BorderColor = usernameError ? Color.Red : Color.DarkGray;
            emailTB.BorderColor = emailError ? Color.Red : Color.DarkGray;
        }
        public event EventHandler ResetClicked;
        public event EventHandler ReturnToLoginClicked;

        #endregion

        private void ForgetUserControl_Resize(object sender, EventArgs e)
        {
            CenterPanelVertically();
        }

        private void CenterPanelVertically()
        {
            if (guna2CustomGradientPanel1 != null)
            {
                guna2CustomGradientPanel1.Top = (this.Height - guna2CustomGradientPanel1.Height) / 2;
            }
        }

        private void InputChanged(object sender, EventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2TextBox tb && !string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.BorderColor = Color.DarkGray;
                tb.BorderThickness = 2;
            }
        }

        private void ForgetUserControl_Load(object sender, EventArgs e)
        {

        }
    }
}
