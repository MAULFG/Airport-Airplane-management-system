using System;
using System.Collections.Generic;
using System.Linq;
using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.View.Interfaces;

namespace Airport_Airplane_management_system.Presenter.AdminPagesPresenters
{
    public class FlightManagementPresenter
    {
        private readonly IFlightManagementView _view;
        private readonly FlightService _flightService;
        private readonly PlaneService _planeService;

        private List<Flight> _allFlights = new();

        public FlightManagementPresenter(
            IFlightManagementView view,
            FlightService flightService,
            PlaneService planeService)
        {
            _view = view;
            _flightService = flightService;
            _planeService = planeService;

            _view.ViewLoaded += (_, __) => Load();
            _view.FilterChanged += (_, __) => ApplyFilterAndRender();

            _view.AddClicked += (_, __) => Add();
            _view.UpdateClicked += (_, __) => Update();
            _view.CancelEditClicked += (_, __) => ExitEditMode();


            _view.EditRequested += id => StartEdit(id);
            _view.DeleteRequested += id => Delete(id);
            _view.ViewCrewRequested += id => OpenCrewForFlight(id);



        }
        public event Action<int> CrewForFlightRequested;

        private bool _isEditMode;
        private int? _editingFlightId;

        private void ExitEditMode()
        {
            _isEditMode = false;
            _editingFlightId = null;

            _view.ExitEditMode();     // just UI reset
            ApplyFilterAndRender();   // refresh list if needed (optional)
        }

        private void Load()
        {
            // keep planes statuses synced
            _planeService.LoadPlanes();
            _planeService.SyncPlaneStatusesWithFlights();

            _view.SetPlanes(_planeService.GetPlanes());

            _allFlights = _flightService.GetFlights() ?? new List<Flight>();
            ApplyFilterAndRender();
        }

        private void Refresh()
        {
            _planeService.LoadPlanes();
            _planeService.SyncPlaneStatusesWithFlights();
            _view.SetPlanes(_planeService.GetPlanes());

            _allFlights = _flightService.GetFlights() ?? new List<Flight>();
            ApplyFilterAndRender();
        }

        private void ApplyFilterAndRender()
        {
            var filter = _view.CurrentFilter ?? "All Flights";

            IEnumerable<Flight> q = _allFlights;

            if (filter == "Upcoming")
                q = q.Where(f => f.Departure >= DateTime.Now).OrderBy(f => f.Departure);
            else if (filter == "Past")
                q = q.Where(f => f.Departure < DateTime.Now).OrderByDescending(f => f.Departure);
            else if (filter.StartsWith("Plane #") && int.TryParse(filter.Replace("Plane #", "").Trim(), out int pid))
                q = q.Where(f => f.PlaneIDFromDb == pid).OrderByDescending(f => f.Departure);
            else
                q = q.OrderByDescending(f => f.Departure);

            _view.SetFlights(q.ToList());
        }

        private bool Validate(out string error)
        {
            error = "";

            if (string.IsNullOrWhiteSpace(_view.FromCity) || string.IsNullOrWhiteSpace(_view.ToCity))
            {
                error = "From and To are required.";
                return false;
            }

            if (_view.Arrival <= _view.Departure)
            {
                error = "Arrival must be after Departure.";
                return false;
            }

            if (_view.SelectedPlaneId == null)
            {
                error = "Plane is required.";
                return false;
            }

            return true;
        }

        private void Add()
        {
            if (!Validate(out var err))
            {
                _view.ShowError(err);
                return;
            }

            int planeId = _view.SelectedPlaneId!.Value;

            var flight = new Flight(
                flightID: 0,
                plane: null,
                from: _view.FromCity.Trim(),
                to: _view.ToCity.Trim(),
                departure: _view.Departure,
                arrival: _view.Arrival,
                categoryPrices: new Dictionary<string, decimal>()
            );

            if (!_flightService.AddFlight(flight, planeId, out var dbErr))
            {
                _view.ShowError(string.IsNullOrWhiteSpace(dbErr) ? "Insert failed." : dbErr);
                return;
            }

            _view.ShowInfo("Flight added.");
            _view.ClearForm();
            Refresh();
        }

        private void StartEdit(int flightId)
        {
            var f = _allFlights.FirstOrDefault(x => x.FlightID == flightId);
            if (f == null)
            {
                _view.ShowError("Flight not found.");
                return;
            }

            _view.EnterEditMode(f);
        }

        private void Update()
        {
            if (!_view.IsEditMode || _view.EditingFlightId == null)
            {
                _view.ShowError("Not in edit mode.");
                return;
            }

            if (!Validate(out var err))
            {
                _view.ShowError(err);
                return;
            }

            int flightId = _view.EditingFlightId.Value;
            int planeId = _view.SelectedPlaneId!.Value;

            if (!_flightService.UpdateFlightDates(flightId, _view.Departure, _view.Arrival, planeId, out var dbErr))
            {
                _view.ShowError(string.IsNullOrWhiteSpace(dbErr) ? "Update failed." : dbErr);
                return;
            }

            _view.ShowInfo("Flight updated.");
            _view.ExitEditMode();
            Refresh();
        }

        private void Delete(int flightId)
        {
            if (!_view.Confirm($"Delete Flight #{flightId}?"))
                return;

            if (!_flightService.DeleteFlight(flightId, out var dbErr))
            {
                _view.ShowError(string.IsNullOrWhiteSpace(dbErr) ? "Delete failed." : dbErr);
                return;
            }

            _view.ShowInfo("Flight deleted.");
            Refresh();
        }
        private void OpenCrewForFlight(int flightId)
        {
            // We will navigate to Crew page and pass the flightId
            CrewForFlightRequested?.Invoke(flightId);
        }


    }
}
