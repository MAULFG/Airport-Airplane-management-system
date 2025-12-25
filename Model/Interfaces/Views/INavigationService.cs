using System;

namespace Airport_Airplane_management_system.View.Interfaces
{
    public interface INavigationService
    {
        // added for usersettings page(IDs) :
        void SetCurrentUserId(int userId);
        int GetCurrentUserId();

        void NavigateToAdmin();
        void NavigateToUser();
        void NavigateToSignUp();
        void NavigateToForgotPassword();
        void NavigateToLogin();
    }

}
