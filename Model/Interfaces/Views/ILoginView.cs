using System;

namespace Airport_Airplane_management_system.View.Forms.LoginPages
{
    public interface ILoginView
    {
        string Username { get; }
        string Password { get; }

        void ShowError(string message);
        void ClearFields();

        // <-- Add this method
        void HighlightFields(bool usernameError, bool passwordError);

        void ShowAdminPanel();
        void ShowUserPanel();
        public void ShowForgetpasswordpage();
        public void ShowSignUpPage();
        


        event EventHandler LoginClicked;
        event EventHandler SignUpClicked;
        event EventHandler ForgotPasswordClicked;
    }
}
