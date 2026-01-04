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
        private readonly Action<int>? _openScheduleForPlane;

        // ✅ Track original plane in edit mode
        private int? _originalPlaneId;

        public FlightManagementPresenter(
            IFlightManagementView view,
            FlightService service,
            Action<int>? openCrewForFlight = null,
            Action<int>? openScheduleForPlane = null)
        {
            _view = view;
            _service = service;
            _openCrewForFlight = openCrewForFlight;
            _openScheduleForPlane = openScheduleForPlane;

            _view.ViewLoaded += (_, __) => OnLoad();
            _view.FilterChanged += (_, __) => RefreshFlights();

            _view.AddClicked += (_, __) => AddFlight();
            _view.UpdateClicked += (_, __) => UpdateFlight();
            _view.CancelEditClicked += (_, __) =>
            {
                _originalPlaneId = null;
                _view.ExitEditMode();
            };

            _view.EditRequested += flightId => EnterEditMode(flightId);
            _view.DeleteRequested += flightId => DeleteFlight(flightId);

            // ✅ When plane changes in the form, refresh seat class availability based on PLANE
            _view.PlaneChanged += planeId => OnPlaneChanged(planeId);

            _view.PlaneScheduleRequested += planeId =>
            {
                _openScheduleForPlane?.Invoke(planeId);
            };

            _view.ViewCrewRequested += flightId =>
            {
                _openCrewForFlight?.Invoke(flightId);
            };
        }

        private void OnLoad()
        {
            LoadPlanes();
            RefreshFlights();

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
            // ✅ Correct: seat classes are determined by PLANE here
            var classes = _service.GetSeatClassesForPlane(planeId)
                          ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);

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

            var classes = _service.GetSeatClassesForPlane(planeId)
                          ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            decimal eco = _view.EconomyPrice;
            decimal bus = _view.BusinessPrice;
            decimal top = _view.FirstPrice;

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

            var prices = _service.GetSeatPricesForFlight(flightId)
                         ?? new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);

            _view.EnterEditMode(flight, prices);

            // ✅ store original plane id for detecting plane change
            _originalPlaneId = flight.PlaneIDFromDb;

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
                _view.ShowError("Plane is required.");
                return;
            }

            int newPlaneId = _view.SelectedPlaneId.Value;

            if (_service.PlaneHasTimeConflict(newPlaneId, _view.Departure, _view.Arrival, excludeFlightId: flightId))
            {
                _view.ShowError("This plane already has another flight in the same time period.");
                return;
            }

            // Validate prices based on NEW plane
            var classes = _service.GetSeatClassesForPlane(newPlaneId)
                          ?? new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            decimal eco = _view.EconomyPrice;
            decimal bus = _view.BusinessPrice;
            decimal top = _view.FirstPrice;

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

            bool planeChanged = _originalPlaneId.HasValue && _originalPlaneId.Value != newPlaneId;

            if (planeChanged)
            {
                // ✅ Correct fix: update flight + plane + rebuild seats
                if (!_service.UpdateFlightWithPlaneAndSeats(
                    flightId,
                    newPlaneId,
                    _view.FromCity.Trim(),
                    _view.ToCity.Trim(),
                    _view.Departure,
                    _view.Arrival,
                    eco,
                    bus,
                    top,
                    out string err))
                {
                    _view.ShowError("Failed to update flight: " + err);
                    return;
                }
            }
            else
            {
                // ✅ Plane unchanged: keep your existing lightweight update
                if (!_service.UpdateFlightDates(flightId, _view.Departure, _view.Arrival, out string dateErr))
                {
                    _view.ShowError("Failed to update flight: " + dateErr);
                    return;
                }

                if (!_service.UpdateSeatPricesForFlight(flightId, eco, bus, top, out string priceErr))
                {
                    _view.ShowError("Failed to update seat prices: " + priceErr);
                    return;
                }
            }

            _view.ShowInfo("Flight updated.");
            _originalPlaneId = null;
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
