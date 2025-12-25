using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Services;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.View.Interfaces;
using System;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    public partial class UserSettings : UserControl, IUserSettingsView
    {
        private INavigationService _navigation;
        private IUserSettingsRepository _repo;
        private IUserSettingsService _service;
        private Presenter.UserSettingsPresenter _presenter;
        private bool _initialized;

        public UserSettings()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;

            btnChangePassword.Click += (_, __) => ChangePasswordClicked?.Invoke();
            btnUpdateEmail.Click += (_, __) => UpdateEmailClicked?.Invoke();
            btnShowChangeUsername.Click += (_, __) => ShowChangeUsernameClicked?.Invoke();
            btnConfirmUsernameChange.Click += (_, __) => ConfirmUsernameChangeClicked?.Invoke();
            btnCancelUsernameChange.Click += (_, __) => CancelUsernameChangeClicked?.Invoke();

            txtCurrentPass.IconRightClick += (_, __) => txtCurrentPass.UseSystemPasswordChar = !txtCurrentPass.UseSystemPasswordChar;
            txtNewPass.IconRightClick += (_, __) => txtNewPass.UseSystemPasswordChar = !txtNewPass.UseSystemPasswordChar;
            txtConfirmPass.IconRightClick += (_, __) => txtConfirmPass.UseSystemPasswordChar = !txtConfirmPass.UseSystemPasswordChar;

           // Load += (_, __) => ViewLoaded?.Invoke();
        }

        public void Initialize(INavigationService navigation)
        {
            if (_initialized) return;

            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));

            var connStr = "server=localhost;port=3306;database=user;user=root;password=2006";

            _repo = new MySqlUserSettingsRepository(connStr);
            _service = new UserSettingsService(_repo);
            _presenter = new Presenter.UserSettingsPresenter(this, _service);

            _initialized = true;
        }

        public int UserId => _navigation?.GetCurrentUserId() ?? 0;

        public string CurrentPassword => txtCurrentPass.Text;
        public string NewPassword => txtNewPass.Text;
        public string ConfirmPassword => txtConfirmPass.Text;

        public string NewEmail => txtNewEmail.Text;
        public string ConfirmPasswordForEmail => txtConfirmPasswordForEmail.Text;

        public string NewUsername => txtNewUsername.Text;
        public string ConfirmPasswordForUsername => txtConfirmPassForUsername.Text;

        public void SetHeader(string username, string email, DateTime createdAt, DateTime? lastLoginAt)
        {
            txtUsername.Text = username;
            txtEmail.Text = email;

            lblCreatedAtValue.Text = createdAt.ToString("yyyy-MM-dd HH:mm");
            lblLastLoginValue.Text = lastLoginAt.HasValue ? lastLoginAt.Value.ToString("yyyy-MM-dd HH:mm") : "-";
        }

        public void SetUsername(string username) => txtUsername.Text = username;
        public void SetEmail(string email) => txtEmail.Text = email;

        public void ToggleUsernamePanel(bool visible)
        {
            pnlChangeUsername.Visible = visible;
            if (visible)
            {
                pnlChangeUsername.BringToFront();
                txtNewUsername.Focus();
            }
        }

        public void ClearPasswordFields()
        {
            txtCurrentPass.Text = "";
            txtNewPass.Text = "";
            txtConfirmPass.Text = "";
        }

        public void ClearEmailFields()
        {
            txtNewEmail.Text = "";
            txtConfirmPasswordForEmail.Text = "";
        }

        public void ClearUsernameFields()
        {
            txtNewUsername.Text = "";
            txtConfirmPassForUsername.Text = "";
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowInfo(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public event Action ViewLoaded;
        public event Action ChangePasswordClicked;
        public event Action UpdateEmailClicked;
        public event Action ShowChangeUsernameClicked;
        public event Action ConfirmUsernameChangeClicked;
        public event Action CancelUsernameChangeClicked;
        public void Activate()
        {
            ViewLoaded?.Invoke();
        }

    }
}
