using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface ISignupView
    {
        string FName { get; }
        string LName { get; }
        string Username { get; }
        string Email { get; }
        string Password { get; }
        string ConfirmPassword { get; }
        void ClearFields();
       
        void ShowError(string message);
        void SetErrorBorder(Guna2TextBox tb, bool isError);
        void SetUsernameError(bool isError);
        void SetEmailError(bool isError);
        void SetPasswordError(bool isError);
        void SetfnameError(bool isError);
        void SetlnameError(bool isError);
        void SetPassword2Error(bool isError);
    
        event EventHandler SignupClicked;
        event EventHandler ReturnToLoginClicked;

    }
}
