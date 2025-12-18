using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface IForgetPasswordView
    {
        string Username { get; }
        string Email { get; }
        string NewPassword { get; }
        string ConfirmPassword { get; }

        void ShowError(string message);
        void ShowMessage(string message);
        void ClearFields();

        event EventHandler ResetClicked;
        event EventHandler ReturnToLoginClicked;
    }

}
