using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Services;
using System;
using System;
using System.Collections.Generic;
using System.Text;

public class ForgetPasswordPresenter
{
    private readonly IForgetPasswordView _view;
    private readonly UserService _userService;

    public ForgetPasswordPresenter(IForgetPasswordView view, UserService userService)
    {
        _view = view;
        _userService = userService;

        _view.ResetClicked += OnResetClicked;
        _view.ReturnToLoginClicked += OnReturnToLoginClicked;
    }

    private void OnResetClicked(object sender, EventArgs e)
    {
        // Basic validation
        if (string.IsNullOrWhiteSpace(_view.Username))
        {
            _view.ShowError("Username is required.");
            return;
        }

        if (string.IsNullOrWhiteSpace(_view.Email))
        {
            _view.ShowError("Email is required.");
            return;
        }

        if (string.IsNullOrWhiteSpace(_view.NewPassword))
        {
            _view.ShowError("New password is required.");
            return;
        }

        if (_view.NewPassword != _view.ConfirmPassword)
        {
            _view.ShowError("Passwords do not match.");
            return;
        }

        // Check if user exists
        var user = _userService.GetUserByUsername(_view.Username);
        if (user == null || !user.Email.Equals(_view.Email, StringComparison.OrdinalIgnoreCase))
        {
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
        // Could navigate to Login page
        _view.ClearFields();
    }
}

