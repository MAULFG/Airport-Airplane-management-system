using Airport_Airplane_management_system.Model.Core.Classes.Users;
using Airport_Airplane_management_system.Model.Core.Enums;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.View.Interfaces;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Presenter.LoginPagesPresenters
{
    public class SignupPresenter
    {
        private readonly ISignupView _view;
        private readonly UserService _userService;
        private readonly INavigationService _navigationService;
        public SignupPresenter(ISignupView view, UserService userService, INavigationService navigation)
        {
            _view = view;
            _userService = userService;
            _navigationService = navigation;
            _view.SignupClicked += OnSignupClicked;
            _view.ReturnToLoginClicked += OnReturnToLoginClicked;
            
            
        }
       
        private void OnSignupClicked(object sender, EventArgs e)
        {
            bool hasError = false;
            _view.SetfnameError(false);
            _view.SetlnameError(false);
            _view.SetEmailError(false);
            _view.SetUsernameError(false);
            _view.SetPasswordError(false);
            _view.SetPassword2Error(false);
            bool fnameEmpty = string.IsNullOrWhiteSpace(_view.FName);
            bool lnameEmpty = string.IsNullOrWhiteSpace(_view.LName);
            bool emailEmpty = string.IsNullOrWhiteSpace(_view.Email);
            bool passwordEmpty = string.IsNullOrWhiteSpace(_view.Password);
            bool confirmpasswordEmpty = string.IsNullOrWhiteSpace(_view.ConfirmPassword);
            bool usernameEmpty = string.IsNullOrWhiteSpace(_view.Username);
            
            if (string.IsNullOrWhiteSpace(_view.FName))
            {
                _view.SetfnameError(true);
                hasError = true;
            }

            if (string.IsNullOrWhiteSpace(_view.LName))
            {
                _view.SetlnameError(true);
                hasError = true;
            }

            if (string.IsNullOrWhiteSpace(_view.Email))
            {
                _view.SetEmailError(true);
                hasError = true;
            }

            if (string.IsNullOrWhiteSpace(_view.Username))
            {
                _view.SetUsernameError(true);
                hasError = true;
            }

            if (string.IsNullOrWhiteSpace(_view.Password))
            {
                _view.SetPasswordError(true);
                hasError = true;
            }

            if (string.IsNullOrWhiteSpace(_view.ConfirmPassword))
            {
                _view.SetPassword2Error(true);
                hasError = true;
            }

            // cross-field validation
            if (!string.IsNullOrWhiteSpace(_view.Password) &&
                !string.IsNullOrWhiteSpace(_view.ConfirmPassword) &&
                _view.Password != _view.ConfirmPassword)
            {
                _view.SetPasswordError(true);
                _view.SetPassword2Error(true);
                _view.ShowError("Passwords do not match.");
                return;
            }

            // stop if any field missing
            if (hasError)
            {
                _view.ShowError("Please fill in all required fields.");
                return;
            }
            if (_view.Password != _view.ConfirmPassword)
            {
                _view.SetPasswordError(true);
                _view.SetPassword2Error(true);
                _view.ShowError("Passwords do not match.");
                return;
            }

            User user = new User(0, _view.FName, _view.LName, _view.Email, _view.Username, _view.Password);
            AddUserResult result = _userService.AddUser(user);
            switch (result)
            {
                case AddUserResult.Success:
                    _view.ClearFields();
                    _view.ShowError("Account created successfully 🎉");
                    _view.ClearFields();
                    _view.ClearFields();
                    _navigationService.NavigateToLogin(); 
                    break;

                case AddUserResult.UsernameExists:
                    _view.ShowError("Username is already taken.");
                    break;

                case AddUserResult.EmailExists:
                    _view.ShowError("Email is already registered.");
                    break;

                default:
                    _view.ShowError("Something went wrong. Please try again.");
                    break;
            }

            
        }

       
    
        private void OnReturnToLoginClicked(object sender, EventArgs e)
        {
            _view.ClearFields();
            _navigationService.NavigateToLogin(); ;
        }

    }

}