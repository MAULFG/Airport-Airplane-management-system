using Airport_Airplane_management_system.Model.Core.Classes.Users;
using Airport_Airplane_management_system.Model.Core.Enums;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Presenter
{
    public class SignupPresenter
    {
        private readonly ISignupView _view;
        private readonly UserService _userService;
        public SignupPresenter(ISignupView view, UserService userService)
        {
            _view = view;
            _userService = userService;
            _view.SignupClicked += OnSignupClicked;
            _view.ReturnToLoginClicked += OnReturnToLoginClicked;
        }
        private void OnSignupClicked(object sender, EventArgs e)
        {
            bool fnameEmpty = string.IsNullOrWhiteSpace(_view.FName);
            bool lnameEmpty = string.IsNullOrWhiteSpace(_view.LName);
            bool emailEmpty = string.IsNullOrWhiteSpace(_view.Email);
            bool passwordEmpty = string.IsNullOrWhiteSpace(_view.Password);
            bool confirmpasswordEmpty = string.IsNullOrWhiteSpace(_view.ConfirmPassword);
            bool usernameEmpty = string.IsNullOrWhiteSpace(_view.Username);
            if (string.IsNullOrEmpty(_view.FName))
            {
                _view.ShowError("Error");
                return;
            }
            if (string.IsNullOrEmpty(_view.LName))
            {
                _view.ShowError("Error");
                return;
            }
            if (string.IsNullOrEmpty(_view.Email))
            {
                _view.ShowError("Error");
                return;
            }
            if (string.IsNullOrEmpty(_view.Password))
            {
                _view.ShowError("Error");
                return;
            }
            if (string.IsNullOrEmpty(_view.ConfirmPassword))
            {
                _view.ShowError("Error");
                return;
            }
            if (string.IsNullOrEmpty(_view.Username))
            {
                _view.ShowError("Error");
                return;
            }
            if (_view.Password != _view.ConfirmPassword)
            {
                _view.HighlightFields1(true, true);
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
                    _view.Returnlogin();
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
            _view.Returnlogin();
        }

    }

}