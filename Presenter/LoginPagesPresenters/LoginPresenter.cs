using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.View.Forms.LoginPages;
using Airport_Airplane_management_system.View.Interfaces;
using MySqlX.XDevAPI;
using System;

namespace Airport_Airplane_management_system.Presenter.LoginPagesPresenters
{
    public class LoginPresenter
    {
        private readonly ILoginView _view;
        private readonly UserService _userService;
        private readonly FlightService _flightService;
        private readonly INavigationService _navigation;
        public LoginPresenter(ILoginView view,UserService userService,FlightService flightService,INavigationService navigation)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _flightService = flightService ?? throw new ArgumentNullException(nameof(flightService));
            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));

            _view.LoginClicked += OnLoginClicked;
            _view.ForgotPasswordClicked += (_, _) => _navigation.NavigateToForgotPassword();
            _view.SignUpClicked += (_, _) => _navigation.NavigateToSignUp();
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

            // 🔐 LOGIN (Authenticate + Session + Cache)
            if (!_userService.Login(_view.Username, _view.Password, out User user))
            {
                
                _view.HighlightFields(true, true);
                _view.ShowError("Invalid username or password.");
                _view.ClearFields();
                return;
            }

            // 🚀 PRELOAD HEAVY DATA
            _flightService.Preload();

            // 🧭 ROLE-BASED NAVIGATION
            if (user.UserID == 313)
            {
                _navigation.NavigateToAdmin();
            }
            else
            {
                _navigation.NavigateToUser();
            }

            _view.ClearFields();
        }
     
        private void OnSignUpClicked(object sender, EventArgs e) => _navigation.NavigateToSignUp();
        private void OnForgotPasswordClicked(object sender, EventArgs e) => _navigation.NavigateToForgotPassword();

    }
}