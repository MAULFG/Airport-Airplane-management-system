using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airport_Airplane_management_system.Presenter.AdminPages
{
    public class FlightManagementPresenter
    {
        private readonly IFlightManagementView _view;
        private readonly FlightService _service;

        private bool _isEditMode;
        private int? _editingFlightId;

        public FlightManagementPresenter(IFlightManagementView view, FlightService service)
        {
            _view = view;
            _service = service;

            // Wire events
            _view.LoadFlightsRequested += OnLoadFlights;
            _view.AddOrUpdateClicked += OnAddOrUpdateFlight;
            _view.CancelEditClicked += ExitEditMode;
            _view.EditRequested += EnterEditMode;
            _view.DeleteRequested += DeleteFlight;
        }

        private void OnLoadFlights(object sender, EventArgs e)
        {
            RefreshFlights();
        }

        private void RefreshFlights()
        {
            var flights = _service.GetFlights();
            _view.RenderFlights(flights);
        }

        private void OnAddOrUpdateFlight()
        {
            try
            {
                if (!_isEditMode)
                {
                    // Add new flight
                    _service.AddFlight(
                        _view.FlightNumber,
                        _view.Origin,
                        _view.Destination,
                        _view.Date,
                        _view.Arrival,
                        _view.PlaneID
                    );
                }
                else
                {
                    // Update existing flight
                    if (_editingFlightId.HasValue)
                    {
                        _service.UpdateFlight(
                            _editingFlightId.Value,
                            _view.FlightNumber,
                            _view.Origin,
                            _view.Destination,
                            _view.Departure,
                            _view.Arrival,
                            _view.PlaneID
                        );
                    }
                }

                ExitEditMode();
                RefreshFlights();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        private void EnterEditMode(Flight f)
        {
            if (f == null) return;

            _isEditMode = true;
            _editingFlightId = f.FlightID;

            _view.SetEditMode(true);
            _view.FillForm(f);
        }

        private void ExitEditMode()
        {
            _isEditMode = false;
            _editingFlightId = null;
            _view.SetEditMode(false);
        }

        private void DeleteFlight(Flight f)
        {
            if (f == null) return;

            try
            {
                _service.(f.FlightID);

                if (_editingFlightId == f.FlightID)
                    ExitEditMode();

                RefreshFlights();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }
    }
}
