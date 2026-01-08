using Airport_Airplane_management_system.Model.Core.Classes.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.View.Interfaces;
using MySqlX.XDevAPI;
using System;
using System;
using System.Collections.Generic;
using System.Text;

public class ForgetPasswordPresenter
{
    private readonly IForgetPasswordView _view;
    private readonly UserService _userService;
    private readonly INavigationService _navigationService;
    private readonly IUserRepository userRepo;
    private readonly IAppSession _session;
    public ForgetPasswordPresenter(IForgetPasswordView view,IAppSession session, INavigationService navigation)
    {
        _view = view;
        _session= session;
        userRepo = new MySqlUserRepository("server=localhost;port=3306;database=user;user=root;password=2006");
      
        _userService = new UserService(userRepo, session);
        _navigationService = navigation;

        _view.ResetClicked += OnResetClicked;
        _view.ReturnToLoginClicked += OnReturnToLoginClicked;
    }

    private void OnResetClicked(object sender, EventArgs e)
    {
        bool usernameEmpty = string.IsNullOrWhiteSpace(_view.Username);
        bool emailEmpty = string.IsNullOrWhiteSpace(_view.Email);
        bool newpassEmpty = string.IsNullOrWhiteSpace(_view.NewPassword);
        bool confirmpassEmpty = string.IsNullOrWhiteSpace(_view.ConfirmPassword);

        // Basic validation
        if (string.IsNullOrWhiteSpace(_view.Username))
        {
            _view.HighlightFields1(usernameEmpty, emailEmpty);
            _view.ShowError("Username is required.");
            return;
        }

        if (string.IsNullOrWhiteSpace(_view.Email))
        {
            _view.HighlightFields1(usernameEmpty, emailEmpty);
            _view.ShowError("Email is required.");
            return;
        }

        if (string.IsNullOrWhiteSpace(_view.NewPassword))
        {
            _view.HighlightFields2(newpassEmpty, confirmpassEmpty);
            _view.ShowError("New password is required.");
            return;
        }

        if (_view.NewPassword != _view.ConfirmPassword)
        {
            _view.HighlightFields2(true, true);
            _view.ShowError("Passwords do not match.");
            return;
        }

        // Check if user exists
        var user = _userService.GetUserByUsername(_view.Username);
        if (user == null || !user.Email.Equals(_view.Email, StringComparison.OrdinalIgnoreCase))
        {
            _view.HighlightFields1(true, true);
            _view.ShowError("Invalid username or email.");
            return;
        }

        // Update password
        _userService.UpdatePassword(user.UserID, _view.NewPassword);
        _view.ShowMessage("Password has been reset successfully.");
        _view.ClearFields();
    }

    private void OnReturnToLoginClicked(object sender, EventArgs e)
    {
        _view.ClearFields();
        _navigationService.NavigateToLogin();
    }
}

