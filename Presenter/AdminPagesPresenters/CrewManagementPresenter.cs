using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Services;
using System;
using System.Linq;

namespace Airport_Airplane_management_system.Presenter.AdminPages
{
    public class CrewManagementPresenter
    {
        private readonly ICrewManagementView _view;
        private readonly CrewService _service;

        private bool _isEditMode;
        private string _editingEmployeeId;

        public CrewManagementPresenter(ICrewManagementView view, CrewService service)
        {
            _view = view;
            _service = service;

            _view.LoadCrewRequested += OnLoad;
            _view.AddOrUpdateClicked += OnAddOrUpdate;
            _view.CancelEditClicked += ExitEditMode;
            _view.EditRequested += EnterEditMode;
            _view.DeleteRequested += DeleteCrew;
            _view.FilterChanged += RefreshCrew;
            

        }

        private void OnLoad(object sender, EventArgs e)
        {
            _view.RenderFlights(_service.GetFlights());
            _view.RenderFilterFlights(_service.GetFlights());
            RefreshCrew();
        }

        private void RefreshCrew()
        {
            SyncFormWithFilter();
            var crew = _service.GetCrew();
            var filterFlightId = _view.GetFlightFilter();

            if (filterFlightId.HasValue)
                crew = crew.Where(c => c.FlightId == filterFlightId).ToList();

            _view.RenderCrew(crew);
        }


        private void OnAddOrUpdate()
        {
            try
            {
                _service.ValidateStatusVsFlight(_view.Status, _view.SelectedFlightId);

                if (!_isEditMode)
                {
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
                RefreshCrew();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        private void EnterEditMode(Crew crew)
        {
            if (crew == null) return;

            _isEditMode = true;
            _editingEmployeeId = crew.EmployeeId;

            _view.SetEditMode(true);
            _view.FillForm(
                crew.FullName,
                crew.Role,
                crew.Status,
                crew.Email,
                crew.Phone,
                crew.FlightId
            );
        }

        private void ExitEditMode()
        {
            _isEditMode = false;
            _editingEmployeeId = null;
            _view.SetEditMode(false);
        }

        private void DeleteCrew(Crew crew)
        {
            if (crew == null) return;

            try
            {
                _service.DeleteCrew(crew.EmployeeId);

                if (_editingEmployeeId == crew.EmployeeId)
                    ExitEditMode();

                RefreshCrew();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }
        // string filter = _view.CurrentFilter;

        private void SyncFormWithFilter()
        {
            var filter = _view.CurrentFilter ?? "All Flights";

            if (filter.StartsWith("Flight #"))
            {
                var num = filter.Replace("Flight #", "").Trim();
                if (int.TryParse(num, out int flightId))
                    _view.SetFormFlight(flightId);
                else
                    _view.SetFormFlight(null);
            }
            else if (filter == "Unassigned")
            {
                _view.SetFormFlight(null);
            }
            else if (filter == "All Flights")
            {
                // ✅ your requested behavior:
                // when filter is "All Flights", reset Add/Edit panel flight to Unassigned
                _view.SetFormFlight(null);
            }
        }


    }
}
