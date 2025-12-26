using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.View.Forms.LoginPages;
using Airport_Airplane_management_system.View.Interfaces;
using System;

namespace Airport_Airplane_management_system.Presenter.LoginPagesPresenters
{
    public class LoginPresenter
    {
        private readonly ILoginView _view;
        private readonly UserService _userService;
        private readonly INavigationService _navigation;
        private readonly IUserSettingsRepository _userSettingsRepository;
        public LoginPresenter(ILoginView view, UserService userService, INavigationService navigation)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));

            _view.LoginClicked += OnLoginClicked;
            _view.ForgotPasswordClicked += OnForgotPasswordClicked;
            _view.SignUpClicked += OnSignUpClicked;
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

            _navigation.SetCurrentUserId(user.UserID);

            // Successful login
            if (user.UserID == 313)
            {
                _navigation.NavigateToAdmin();
                _view.ClearFields();
            }
            else
            {
                _navigation.NavigateToUser();
                _view.ClearFields();
            }
                
        }
        private void OnSignUpClicked(object sender, EventArgs e) => _navigation.NavigateToSignUp();
        private void OnForgotPasswordClicked(object sender, EventArgs e) => _navigation.NavigateToForgotPassword();

    }
}