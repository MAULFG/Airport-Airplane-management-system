using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Presenter;
using Airport_Airplane_management_system.View.Interfaces;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    public partial class UserAccount : UserControl, IUserAccountView
    {
        private INavigationService _navigation;
        private IUserAccountRepository _repo;
        private UserAccountService _service;
        private UserAccountPresenter _presenter;
        private bool _initialized;

        private bool _curVisible;
        private bool _newVisible;
        private bool _confirmVisible;

        public UserAccount()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;

            void Enter(KeyEventArgs e, Action action)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    action?.Invoke();
                }
            }

            txtCurrentPass.KeyDown += (s, e) => Enter(e, ChangePasswordClicked);
            txtNewPass.KeyDown += (s, e) => Enter(e, ChangePasswordClicked);
            txtConfirmPass.KeyDown += (s, e) => Enter(e, ChangePasswordClicked);

            txtNewEmail.KeyDown += (s, e) => Enter(e, UpdateEmailClicked);
            txtConfirmPasswordForEmail.KeyDown += (s, e) => Enter(e, UpdateEmailClicked);

            txtNewUsername.KeyDown += (s, e) => Enter(e, ConfirmUsernameChangeClicked);
            txtConfirmPassForUsername.KeyDown += (s, e) => Enter(e, ConfirmUsernameChangeClicked);


            btnChangePassword.Click += (_, __) => ChangePasswordClicked?.Invoke();
            btnUpdateEmail.Click += (_, __) => UpdateEmailClicked?.Invoke();
            btnShowChangeUsername.Click += (_, __) => ShowChangeUsernameClicked?.Invoke();
            btnConfirmUsernameChange.Click += (_, __) => ConfirmUsernameChangeClicked?.Invoke();
            btnCancelUsernameChange.Click += (_, __) => CancelUsernameChangeClicked?.Invoke();


        }

        public void Initialize(INavigationService navigation)
        {
            if (_initialized) return;

            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));

            var connStr = "server=localhost;port=3306;database=user;user=root;password=2006";

            _repo = new MySqlUserAccountRepository(connStr);
            _service = new UserAccountService(_repo);
            _presenter = new Presenter.UserAccountPresenter(this);

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

        public void SetHeader(string username, string email)
        {
            txtUsername.ReadOnly = true;
            txtEmail.ReadOnly = true;

            txtUsername.Text = username;
            txtEmail.Text = email;





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

        private void SetEye(Guna.UI2.WinForms.Guna2TextBox tb, bool visible)
        {
            tb.UseSystemPasswordChar = !visible;

            var src = visible
                ? Properties.Resources.icons8_eye_96
                : Properties.Resources.icons8_closed_eye_100;

            tb.IconRight = null;
            tb.IconRight = new Bitmap(src);
            tb.Invalidate();
        }

        private void UserAccount_Load(object sender, EventArgs e)
        {
            _curVisible = false;
            _newVisible = false;
            _confirmVisible = false;

            SetEye(txtCurrentPass, _curVisible);
            SetEye(txtNewPass, _newVisible);
            SetEye(txtConfirmPass, _confirmVisible);

            txtCurrentPass.IconRightClick += (s, e2) => { _curVisible = !_curVisible; SetEye(txtCurrentPass, _curVisible); };
            txtNewPass.IconRightClick += (s, e2) => { _newVisible = !_newVisible; SetEye(txtNewPass, _newVisible); };
            txtConfirmPass.IconRightClick += (s, e2) => { _confirmVisible = !_confirmVisible; SetEye(txtConfirmPass, _confirmVisible); };

            if (!_initialized) return;

            txtUsername.ReadOnly = true;
            txtEmail.ReadOnly = true;

            int userId = _navigation.GetCurrentUserId();
            if (userId <= 0) return;

            var header = _service.GetHeader(userId);
            if (header == null) return;

            txtUsername.Text = header.Value.Username;
            txtEmail.Text = header.Value.Email;
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            e.SuppressKeyPress = true;
            e.Handled = true;

            if (pnlChangeUsername.Visible)
            {
                ConfirmUsernameChangeClicked?.Invoke();
                return;
            }

            var focused = ActiveControl;

            if (IsInside(txtCurrentPass, focused) || IsInside(txtNewPass, focused) || IsInside(txtConfirmPass, focused))
            {
                ChangePasswordClicked?.Invoke();
                return;
            }

            if (IsInside(txtNewEmail, focused) || IsInside(txtConfirmPasswordForEmail, focused))
            {
                UpdateEmailClicked?.Invoke();
                return;
            }
        }

        private bool IsInside(Control container, Control focused)
        {
            if (focused == null) return false;
            if (focused == container) return true;
            return container.Contains(focused);
        }

        private void pnlRoot_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnShowChangeUsername_Click(object sender, EventArgs e)
        {

        }
    }
}