using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Repositories;
using System;
using System.Linq;
using Ticket_Booking_System_OOP.Model.Repositories;

namespace Airport_Airplane_management_system.Presenter.AdminPages
{
    public class CrewManagementPresenter
    {
        private readonly ICrewManagementView _view;
        private readonly CrewService _service;
        private readonly ICrewRepository crewRepo;
        private readonly IFlightRepository flightRepo;

        private bool _isEditMode;
        private string _editingEmployeeId;

        public CrewManagementPresenter(ICrewManagementView view)
        {
            
            _view = view;
            flightRepo = new MySqlFlightRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            crewRepo = new MySqlCrewRepository("server=localhost;port=3306;database=user;user=root;password=2006");


            _service = new CrewService(crewRepo,flightRepo);

            _view.LoadCrewRequested += OnLoad;
            _view.AddOrUpdateClicked += OnAddOrUpdate;
            _view.CancelEditClicked += ExitEditMode;
            _view.EditRequested += EnterEditMode;
            _view.DeleteRequested += DeleteCrew;
            _view.FilterChanged += RefreshCrew;
            

        }
        public void RefreshData()
        {
            LoadFlights();
            RefreshCrew();
             // reset edit mode whenever page opens
        }
        private void LoadFlights()
        {
            var flights = _service.GetFlights();
            _view.RenderFlights(flights);
            _view.RenderFilterFlights(flights);
        }
        private void OnLoad(object sender, EventArgs e)
        {
            _view.RenderFlights(_service.GetFlights());
            _view.RenderFilterFlights(_service.GetFlights());
  
            RefreshData();
        }

        private void RefreshCrew()
        {
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
                RefreshData();
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

                RefreshData();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }
    }
}
