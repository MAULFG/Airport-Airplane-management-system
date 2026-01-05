using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Presenter
{
    public class UserAccountPresenter
    {
        private readonly IUserAccountView _view;
        private readonly IUserAccountRepository useraRepo;
        private readonly UserAccountService _service;

        private bool _usernamePanelVisible;

        public UserAccountPresenter(IUserAccountView view)
        {
            _view = view;
            useraRepo =new MySqlUserAccountRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            _service = new UserAccountService(useraRepo);

            _view.ViewLoaded += RefreshData;
            _view.ChangePasswordClicked += OnChangePassword;
            _view.UpdateEmailClicked += OnUpdateEmail;

            _view.ShowChangeUsernameClicked += OnToggleUsernamePanel;
            _view.ConfirmUsernameChangeClicked += OnConfirmUsernameChange;
            _view.CancelUsernameChangeClicked += OnCancelUsernameChange;
        }

        public void RefreshData()
        {
            try
            {
                var header = _service.GetHeader(_view.UserId);
                if (!header.HasValue)
                {
                    _view.ShowError("User not found.");
                    return;
                }

                _view.SetHeader(header.Value.Username, header.Value.Email);
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