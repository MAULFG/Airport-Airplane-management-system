using System;

namespace Airport_Airplane_management_system.View.Interfaces
{
    public interface INavigationService
    {
        void NavigateToAdmin();
        void NavigateToUser();
        void NavigateToSignUp();
        void NavigateToForgotPassword();
        void NavigateToLogin();
        void ShowCrewManagement(int? flightIdFilter = null);

    }

}
