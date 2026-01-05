using System;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface IUserAccountView
    {
        int UserId { get; }

        string CurrentPassword { get; }
        string NewPassword { get; }
        string ConfirmPassword { get; }

        string NewEmail { get; }
        string ConfirmPasswordForEmail { get; }

        string NewUsername { get; }
        string ConfirmPasswordForUsername { get; }

        event Action ViewLoaded;
        event Action ChangePasswordClicked;
        event Action UpdateEmailClicked;
        event Action ShowChangeUsernameClicked;
        event Action ConfirmUsernameChangeClicked;
        event Action CancelUsernameChangeClicked;

        void Activate();

        void SetHeader(string username, string email);

        void SetUsername(string username);
        void SetEmail(string email);

        void ToggleUsernamePanel(bool visible);

        void ClearPasswordFields();
        void ClearEmailFields();
        void ClearUsernameFields();

        void ShowError(string message);
        void ShowInfo(string message);
    }
}