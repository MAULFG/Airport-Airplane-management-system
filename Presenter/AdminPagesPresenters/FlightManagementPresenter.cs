using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airport_Airplane_management_system.Presenter.AdminPagesPresenters
{
    public class FlightManagementPresenter
    {
        private readonly IFlightManagementView _view;
        private readonly FlightService _service;

        private List<Plane> _planes = new List<Plane>();

        // Optional hook to open Crew page from dashboard
        private readonly Action<int>? _openCrewForFlight;

        public FlightManagementPresenter(
            IFlightManagementView view,
            FlightService service,
            Action<int>? openCrewForFlight = null)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _openCrewForFlight = openCrewForFlight;

            _view.ViewLoaded += OnLoad;
            _view.FilterChanged += (_, __) => Refresh();

            _view.AddClicked += (_, __) => Add();
            _view.UpdateClicked += (_, __) => Update();
            _view.CancelEditClicked += (_, __) => _view.ExitEditMode();

            _view.EditRequested += id => EnterEdit(id);
            _view.DeleteRequested += id => Delete(id);

            _view.ViewCrewRequested += id => ViewCrew(id);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            LoadPlanes();
            Refresh();
        }

        private void LoadPlanes()
        {
            // You already have _planeRepo.GetAllPlanesf() in FlightService.LoadFlightsWithSeats
            // So we just expose a minimal getter below (see service patch)
            _planes = _service.GetPlanes() ?? new List<Plane>();
            _view.SetPlanes(_planes);
        }

        private void Refresh()
        {
            var flights = _service.GetFlights() ?? new List<Flight>();
            flights = ApplyFilter(flights, _view.CurrentFilter ?? "All Flights");
            _view.SetFlights(flights);
        }

        private List<Flight> ApplyFilter(List<Flight> flights, string filter)
        {
            if (filter == "All Flights") return flights;

            var now = DateTime.Now;

            if (filter == "Upcoming")
                return flights.Where(f => f.Departure > now).ToList();

            if (filter == "Past")
                return flights.Where(f => f.Arrival < now).ToList();

            if (filter.StartsWith("Plane #"))
            {
                var num = filter.Replace("Plane #", "").Trim();
                if (int.TryParse(num, out int planeId))
                    return flights.Where(f => f.PlaneIDFromDb == planeId).ToList();
            }

            return flights;
        }

        private void Add()
        {
            if (string.IsNullOrWhiteSpace(_view.FromCity) || string.IsNullOrWhiteSpace(_view.ToCity))
            {
                _view.ShowError("From and To are required.");
                return;
            }

            if (_view.Arrival <= _view.Departure)
            {
                _view.ShowError("Arrival must be after Departure.");
                return;
            }

            if (!_view.SelectedPlaneId.HasValue)
            {
                _view.ShowError("Please select a plane.");
                return;
            }

            int planeId = _view.SelectedPlaneId.Value;

            if (_service.PlaneHasTimeConflict(planeId, _view.Departure, _view.Arrival, excludeFlightId: null))
            {
                _view.ShowError("This plane already has another flight in the same time period.");
                return;
            }

            // ✅ Plane is abstract => MUST pick an existing concrete plane
            var plane = _planes.FirstOrDefault(p => p.PlaneID == planeId);
            if (plane == null)
            {
                _view.ShowError("Selected plane not found. Reload planes.");
                return;
            }

            // ✅ Match your constructor style used in MySqlFlightRepository
            var flight = new Flight(
                0,                       // flightId
                plane,                   // plane object (concrete)
                _view.FromCity.Trim(),
                _view.ToCity.Trim(),
                _view.Departure,
                _view.Arrival,
                new Dictionary<string, decimal>()
            )
            {
                PlaneIDFromDb = planeId
            };

            if (!_service.AddFlight(flight, out int newId, out string err))
            {
                _view.ShowError(err);
                return;
            }

            _view.ShowInfo($"Flight added (ID #{newId}).");
            _view.ClearForm();
            Refresh();
        }

        private void EnterEdit(int flightId)
        {
            var f = _service.GetFlightById(flightId);
            if (f == null)
            {
                _view.ShowError("Flight not found.");
                return;
            }

            _view.EnterEditMode(f);
        }

        private void Update()
        {
            if (!_view.IsEditMode || !_view.EditingFlightId.HasValue)
            {
                _view.ShowError("Not in edit mode.");
                return;
            }

            int flightId = _view.EditingFlightId.Value;

            if (_view.Arrival <= _view.Departure)
            {
                _view.ShowError("Arrival must be after Departure.");
                return;
            }

            // if you allow plane change, validate conflict too
            int? planeId = _view.SelectedPlaneId;
            if (planeId.HasValue &&
                _service.PlaneHasTimeConflict(planeId.Value, _view.Departure, _view.Arrival, excludeFlightId: flightId))
            {
                _view.ShowError("This plane already has another flight in the same time period.");
                return;
            }

            if (!_service.UpdateFlightDates(flightId, _view.Departure, _view.Arrival, out string err))
            {
                _view.ShowError(err);
                return;
            }

            _view.ShowInfo("Flight updated.");
            _view.ExitEditMode();
            Refresh();
        }

        private void Delete(int flightId)
        {
            if (!_view.Confirm("Delete this flight? This will cancel bookings and remove seats."))
                return;

            if (!_service.CancelFlight(flightId, out string err))
            {
                _view.ShowError(err);
                return;
            }

            _view.ShowInfo("Flight deleted.");
            Refresh();
        }

        private void ViewCrew(int flightId)
        {
            if (_openCrewForFlight != null)
            {
                _openCrewForFlight(flightId);
                return;
            }

            _view.ShowInfo($"Crew navigation is not wired yet. Flight ID: {flightId}");
        }
    }
}
