using Airport_Airplane_management_system.Model.Core;
using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Services;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.View.Forms.LoginPages;
using Airport_Airplane_management_system.View.Interfaces;
using System;

namespace Airport_Airplane_management_system.Presenter.AdminPages
{
    public class CrewManagementPresenter
    {
        private readonly ICrewManagementView _view;
        private readonly CrewService _service;

        private bool _isEditMode;
        private string _editingEmployeeId;

        public CrewManagementPresenter(
            ICrewManagementView view,
            CrewService service)
        {
            _view = view;
            _service = service;

            // Wire events
            _view.ViewLoaded += OnLoad;
            _view.AddOrUpdateClicked += OnAddOrUpdate;
            _view.CancelEditClicked += ExitEditMode;
            _view.EditRequested += EnterEditMode;
            _view.DeleteRequested += DeleteCrew;
            _view.FilterChanged += Refresh;
        }

        private void OnLoad()
        {
            _view.RenderFlights(_service.GetFlights());
            Refresh();
        }

        private void Refresh()
        {
            var crew = _service.GetCrew();
            _view.RenderCrew(crew);
        }

        private void OnAddOrUpdate()
        {
            try
            {
                _service.ValidateStatusVsFlight(
                    _view.Status,
                    _view.SelectedFlightId);

                if (!_isEditMode)
                {
                    _service.AddCrew(
                        _view.FullName,
                        _view.Role,
                        _view.Status,
                        _view.Email,
                        _view.Phone,
                        _view.SelectedFlightId);
                }
                else
                {
                    _service.UpdateCrew(
                        _editingEmployeeId,
                        _view.FullName,
                        _view.Role,
                        _view.Status,
                        _view.Email,
                        _view.Phone,
                        _view.SelectedFlightId);
                }

                ExitEditMode();
                Refresh();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        private void EnterEditMode(Crew c)
        {
            _isEditMode = true;
            _editingEmployeeId = c.EmployeeId;

            _view.SetEditMode(true);

            // fill UI (safe cast)
            if (_view is Airport_Airplane_management_system.View.Forms.AdminPages.CrewManagement v)
                v.FillFormFromCrew(c);
        }

        private void ExitEditMode()
        {
            _isEditMode = false;
            _editingEmployeeId = null;
            _view.SetEditMode(false);
        }

        private void DeleteCrew(Crew c)
        {
            if (_service.DeleteCrew(c.EmployeeId))
            {
                if (_editingEmployeeId == c.EmployeeId)
                    ExitEditMode();

                Refresh();
            }
        }
    }
}