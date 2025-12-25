using Airport_Airplane_management_system.Model.Interfaces.Services;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Presenter
{
    public class UserSettingsPresenter
    {
        private readonly IUserSettingsView _view;
        private readonly IUserSettingsService _service;

        private bool _usernamePanelVisible;

        public UserSettingsPresenter(IUserSettingsView view, IUserSettingsService service)
        {
            _view = view;
            _service = service;

            _view.ViewLoaded += OnLoaded;
            _view.ChangePasswordClicked += OnChangePassword;
            _view.UpdateEmailClicked += OnUpdateEmail;

            _view.ShowChangeUsernameClicked += OnToggleUsernamePanel;
            _view.ConfirmUsernameChangeClicked += OnConfirmUsernameChange;
            _view.CancelUsernameChangeClicked += OnCancelUsernameChange;
        }

        private void OnLoaded()
        {
            try
            {
                var header = _service.GetHeader(_view.UserId);
                if (!header.HasValue)
                {
                    _view.ShowError("User not found.");
                    return;
                }

                _view.SetHeader(header.Value.Username, header.Value.Email, header.Value.CreatedAt, header.Value.LastLoginAt);
                _usernamePanelVisible = false;
                _view.ToggleUsernamePanel(false);
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        private void OnChangePassword()
        {
            try
            {
                _service.ChangePassword(
                    _view.UserId,
                    _view.CurrentPassword,
                    _view.NewPassword,
                    _view.ConfirmPassword);

                _view.ShowInfo("Password changed successfully.");
                _view.ClearPasswordFields();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        private void OnUpdateEmail()
        {
            try
            {
                _service.ChangeEmail(
                    _view.UserId,
                    _view.NewEmail,
                    _view.ConfirmPasswordForEmail);

                var header = _service.GetHeader(_view.UserId);
                if (header.HasValue)
                    _view.SetEmail(header.Value.Email);

                _view.ShowInfo("Email updated successfully.");
                _view.ClearEmailFields();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        private void OnToggleUsernamePanel()
        {
            _usernamePanelVisible = !_usernamePanelVisible;
            _view.ToggleUsernamePanel(_usernamePanelVisible);
        }

        private void OnCancelUsernameChange()
        {
            _usernamePanelVisible = false;
            _view.ToggleUsernamePanel(false);
            _view.ClearUsernameFields();
        }

        private void OnConfirmUsernameChange()
        {
            try
            {
                _service.ChangeUsername(
                    _view.UserId,
                    _view.NewUsername,
                    _view.ConfirmPasswordForUsername);

                var header = _service.GetHeader(_view.UserId);
                if (header.HasValue)
                    _view.SetUsername(header.Value.Username);

                _view.ShowInfo("Username updated successfully.");
                _usernamePanelVisible = false;
                _view.ToggleUsernamePanel(false);
                _view.ClearUsernameFields();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }
    }
}
