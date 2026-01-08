using Airport_Airplane_management_system.Model.Core.Classes.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using System;

public class UserAccountPresenter
{
    private readonly IUserAccountView _view;
    private readonly IUserAccountRepository _repo;
    private readonly UserAccountService _service;
    private readonly IAppSession _session;
    

    private bool _usernamePanelVisible;

    public UserAccountPresenter(IUserAccountView view, IAppSession session)
    {
        _view = view;
        _session = session;

        _repo = new MySqlUserAccountRepository("server=localhost;port=3306;database=user;user=root;password=2006"
        );
        _service = new UserAccountService(_repo);

        _view.ViewLoaded += RefreshData;
        _view.ChangePasswordClicked += OnChangePassword;
        _view.UpdateEmailClicked += OnUpdateEmail;
        _view.ShowChangeUsernameClicked += OnToggleUsernamePanel;
        _view.ConfirmUsernameChangeClicked += OnConfirmUsernameChange;
        _view.CancelUsernameChangeClicked += OnCancelUsernameChange;
    }

    private int UserId => _session.CurrentUser?.UserID ?? 0;

    public void RefreshData()
    {
        if (!_session.IsLoggedIn)
        {
            _view.ShowError("No active session.");
            return;
        }

        _view.SetHeader(
            _session.CurrentUser.UserName,
            _session.CurrentUser.Email
        );

        _usernamePanelVisible = false;
        _view.ToggleUsernamePanel(false);
    }

    private void OnChangePassword()
    {
        try
        {
            _service.ChangePassword(
                UserId,
                _view.CurrentPassword,
                _view.NewPassword,
                _view.ConfirmPassword
            );

            _view.ShowInfo("Password changed successfully.");
            _view.ClearPasswordFields();
            var updatedUser = _repo.GetUserById(UserId);

            if (updatedUser != null)
            {
                _session.SetUser(updatedUser);

            }

            RefreshData();
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
                UserId,
                _view.NewEmail,
                _view.ConfirmPasswordForEmail
            );

            var updatedUser = _repo.GetUserById(UserId);

            if (updatedUser != null)
            {
                _session.SetUser(updatedUser);
               
            }

            RefreshData();

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
                UserId,
                _view.NewUsername,
                _view.ConfirmPasswordForUsername
            );



            var updatedUser = _repo.GetUserById(UserId);


            if (updatedUser != null)
            {
                _session.SetUser(updatedUser);

            }


            RefreshData();

            _view.ShowInfo("Username updated successfully.");
            _view.ClearUsernameFields();
        }
        catch (Exception ex)
        {
            _view.ShowError(ex.Message);
        }
    }
}
