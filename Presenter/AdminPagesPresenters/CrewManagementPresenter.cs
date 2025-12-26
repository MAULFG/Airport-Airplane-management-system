using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using System;
using System.Linq;

namespace Airport_Airplane_management_system.Presenter.AdminPages
{
    public class CrewManagementPresenter
    {
        private readonly ICrewManagementView _view;
        private readonly CrewService _service;

        private bool _isEditMode = false;
        private string _editingEmployeeId = null;

        public CrewManagementPresenter(ICrewManagementView view, CrewService service)
        {
            _view = view;
            _service = service;

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

            var filterFlightId = _view.GetFlightFilter();
            if (filterFlightId.HasValue)
                crew = crew.Where(c => c.FlightId == filterFlightId).ToList();

            _view.RenderCrew(crew);

            // 🔴 IMPORTANT: sync ONLY when NOT editing
            if (!_isEditMode)
                _view.SyncFormFlightWithFilter(filterFlightId);
        }

        private void OnAddOrUpdate()
        {
            try
            {
                _service.ValidateStatusVsFlight(
                    _view.Status,
                    _view.SelectedFlightId
                );

                if (!_isEditMode)
                {
                    // ✅ ADD
                    _service.AddCrew(
                        _view.FullName,
                        _view.Role,
                        _view.Status,
                        _view.Email,
                        _view.Phone,
                        _view.SelectedFlightId
                    );
                }
                else
                {
                    // ✅ UPDATE (THIS WAS BROKEN BEFORE)
                    _service.UpdateCrew(
                        _editingEmployeeId,
                        _view.FullName,
                        _view.Role,
                        _view.Status,
                        _view.Email,
                        _view.Phone,
                        _view.SelectedFlightId
                    );
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

            _view.FillForm(
                c.FullName,
                c.Role,
                c.Status,
                c.Email,
                c.Phone,
                c.FlightId
            );

            // 🔴 DO NOT sync with filter while editing
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
