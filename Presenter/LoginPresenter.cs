using System;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.View.Forms.LoginPages;

namespace Airport_Airplane_management_system.Presenter
{
    public class LoginPresenter
    {
        private readonly ILoginView _view;
        private readonly UserService _userService;

        public LoginPresenter(ILoginView view, UserService userService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));

            _view.LoginClicked += OnLoginClicked;
        }

        private void OnLoginClicked(object sender, EventArgs e)
        {
            bool usernameEmpty = string.IsNullOrWhiteSpace(_view.Username);
            bool passwordEmpty = string.IsNullOrWhiteSpace(_view.Password);

            if (usernameEmpty || passwordEmpty)
            {
                _view.HighlightFields(usernameEmpty, passwordEmpty);
                _view.ShowError("Please enter both username and password.");
                return;
            }

            var user = _userService.Authenticate(_view.Username, _view.Password);

            if (user == null)
            {
                _view.HighlightFields(true, true); // highlight both fields for invalid login
                _view.ShowError("Invalid username or password.");
                _view.ClearFields();
                return;
            }

            // Successful login
            if (user.UserID == 313)
                _view.ShowAdminPanel();
            else
                _view.ShowUserPanel();
        }
    }
}
