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

        private List<Plane> _planes = new List<Plane>();

        private readonly Action<int>? _openCrewForFlight;

        public FlightManagementPresenter(IFlightManagementView view, FlightService service, Action<int>? openCrewForFlight = null)
        {
            _view = view;
            _service = service;
            _openCrewForFlight = openCrewForFlight;
            _view.ViewLoaded += (_, __) => OnLoad();

            _view.FilterChanged += (_, __) => RefreshFlights();

            _view.AddClicked += (_, __) => AddFlight();
            _view.UpdateClicked += (_, __) => UpdateFlight();
            _view.CancelEditClicked += (_, __) => _view.ExitEditMode();

            _view.EditRequested += flightId => EnterEditMode(flightId);
            _view.DeleteRequested += flightId => DeleteFlight(flightId);

            _view.PlaneChanged += planeId => OnPlaneChanged(planeId);
        }

        private void OnLoad()
        {
            LoadPlanes();
            RefreshFlights();

            // apply seat class availability once on load (if plane selected)
            if (_view.SelectedPlaneId.HasValue)
                OnPlaneChanged(_view.SelectedPlaneId.Value);
        }

        private void LoadPlanes()
        {
            _planes = _service.GetPlanes() ?? new List<Plane>();
            _view.SetPlanes(_planes);
        }

        private void RefreshFlights()
        {
            var flights = _service.GetFlights() ?? new List<Flight>();
            flights = ApplyFilter(flights, _view.CurrentFilter);
            _view.SetFlights(flights);
        }

        private List<Flight> ApplyFilter(List<Flight> flights, string filter)
        {
            filter ??= "All Flights";
            var now = DateTime.Now;

            if (filter == "All Flights") return flights;

            if (filter == "Upcoming")
                return flights.Where(f => f.Departure > now).ToList();

            if (filter == "Past")
                return flights.Where(f => f.Arrival < now).ToList();

            // optional: "Plane #ID" filter pattern (if you use it)
            if (filter.StartsWith("Plane #"))
            {
                var s = filter.Replace("Plane #", "").Trim();
                if (int.TryParse(s, out int planeId))
                    return flights.Where(f => f.PlaneIDFromDb == planeId).ToList();
            }

            return flights;
        }

        private void OnPlaneChanged(int planeId)
        {
            var classes = _service.GetSeatClassesForFlight(planeId)
                          ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // view decides what to show/hide + VIP label logic
            _view.SetSeatClassAvailability(classes);
        }

        private void AddFlight()
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

            // time conflict
            if (_service.PlaneHasTimeConflict(planeId, _view.Departure, _view.Arrival, excludeFlightId: null))
            {
                _view.ShowError("This plane already has another flight in the same time period.");
                return;
            }

            var plane = _planes.FirstOrDefault(p => p.PlaneID == planeId);
            if (plane == null)
            {
                _view.ShowError("Selected plane not found. Reload planes.");
                return;
            }

            // validate prices only for classes that exist
            var classes = _service.GetSeatClassesForFlight(planeId)
                          ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            decimal eco = _view.EconomyPrice;
            decimal bus = _view.BusinessPrice;
            decimal top = _view.FirstPrice; // First OR VIP

            if (classes.Contains("economy") && eco <= 0)
            {
                _view.ShowError("Please set Economy price.");
                return;
            }

            if (classes.Contains("business") && bus <= 0)
            {
                _view.ShowError("Please set Business price.");
                return;
            }

            bool needsTop = classes.Contains("first") || classes.Contains("vip");
            if (needsTop && top <= 0)
            {
                _view.ShowError("Please set VIP/First price.");
                return;
            }

            var flight = new Flight(
                0,
                plane,
                _view.FromCity.Trim(),
                _view.ToCity.Trim(),
                _view.Departure,
                _view.Arrival,
                new Dictionary<string, decimal>()
            )
            {
                PlaneIDFromDb = planeId
            };


            if (!_service.AddFlight(flight, eco, bus, top, out int newId, out string err))
            {
                _view.ShowError(err);
                return;
            }

            _view.ShowInfo($"Flight added (ID #{newId}).");
            _view.ClearForm();
            RefreshFlights();
        }

        private void EnterEditMode(int flightId)
        {
            var flight = _service.GetFlightById(flightId);
            if (flight == null)
            {
                _view.ShowError("Flight not found.");
                return;
            }

            // ✅ load prices from DB and pass them to the view
            var prices = _service.GetSeatPricesForFlight(flightId)
                         ?? new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);

            _view.EnterEditMode(flight, prices);

            // refresh availability + VIP label for selected plane
            if (_view.SelectedPlaneId.HasValue)
                OnPlaneChanged(_view.SelectedPlaneId.Value);
        }

        private void UpdateFlight()
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

            if (!_view.SelectedPlaneId.HasValue)
            {
                _view.ShowError("Plane is required.");
                return;
            }

            int planeId = _view.SelectedPlaneId.Value;

            // time conflict (exclude this flight)
            if (_service.PlaneHasTimeConflict(planeId, _view.Departure, _view.Arrival, excludeFlightId: flightId))
            {
                _view.ShowError("This plane already has another flight in the same time period.");
                return;
            }

            // update dates
            if (!_service.UpdateFlightDates(flightId, _view.Departure, _view.Arrival, out string dateErr))
            {
                _view.ShowError("Failed to update flight: " + dateErr);
                return;
            }

            // ✅ update seat prices too
            var classes = _service.GetSeatClassesForFlight(planeId)
                          ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            decimal eco = _view.EconomyPrice;
            decimal bus = _view.BusinessPrice;
            decimal top = _view.FirstPrice; // First OR VIP

            if (classes.Contains("economy") && eco <= 0)
            {
                _view.ShowError("Please set Economy price.");
                return;
            }
            if (classes.Contains("business") && bus <= 0)
            {
                _view.ShowError("Please set Business price.");
                return;
            }
            if ((classes.Contains("first") || classes.Contains("vip")) && top <= 0)
            {
                _view.ShowError("Please set VIP/First price.");
                return;
            }

            if (!_service.UpdateSeatPricesForFlight(flightId, eco, bus, top, out string priceErr))
            {
                _view.ShowError("Failed to update seat prices: " + priceErr);
                return;
            }

            _view.ShowInfo("Flight updated.");
            _view.ExitEditMode();
            RefreshFlights();
        }

        private void DeleteFlight(int flightId)
        {
            if (!_view.Confirm("Delete this flight? This will remove its seats and bookings."))
                return;

            if (!_service.CancelFlight(flightId, out string err))
            {
                _view.ShowError("Delete failed: " + err);
                return;
            }

            _view.ShowInfo("Flight deleted.");
            RefreshFlights();
        }
    }
}
