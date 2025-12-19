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
        void Returnlogin();
       void HighlightFields2(bool newpassError, bool confirmpassError);

       void HighlightFields1(bool usernameError, bool emailError);

        event EventHandler ResetClicked;
        event EventHandler ReturnToLoginClicked;
    }

}
