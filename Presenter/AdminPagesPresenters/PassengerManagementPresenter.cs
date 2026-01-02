using System;
using System.Collections.Generic;
using System.Linq;
using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Services;

namespace Airport_Airplane_management_system.Presenter.AdminPages
{
    public class PassengerManagementPresenter
    {
        private readonly IPassengerManagementView _view;
        private readonly PassengerService _service;

        private readonly Func<int> _countUpcomingFlightsNotFullyBooked;
        private List<PassengerSummaryRow> _all = new();

        public PassengerManagementPresenter(
            IPassengerManagementView view,
            PassengerService service,
            Func<int> countUpcomingFlightsNotFullyBooked)
        {
            _view = view;
            _service = service;
            _countUpcomingFlightsNotFullyBooked = countUpcomingFlightsNotFullyBooked;

            _view.ViewLoaded += OnLoad;
            _view.SearchChanged += OnSearch;
            _view.PassengerToggleRequested += OnTogglePassenger;
            _view.CancelBookingRequested += OnCancelBooking;
        }
        

        private void OnLoad()
        {
            RefreshAll();
        }

        private void RefreshAll()
        {
            try
            {
                _all = _service.GetPassengersSummary();
                int upcomingFlights = _countUpcomingFlightsNotFullyBooked();

                _view.SetHeaderCounts(_all.Count, upcomingFlights);
                _view.RenderPassengers(_all);
            }
            catch (Exception ex)
            {
                _view.ShowError("Failed to load passengers.\n" + ex.Message);
                _view.SetHeaderCounts(0, 0);
                _view.RenderPassengers(new List<PassengerSummaryRow>());
            }
        }

        private void OnSearch(string query)
        {
            query = (query ?? "").Trim().ToLower();
            if (string.IsNullOrWhiteSpace(query))
            {
                _view.RenderPassengers(_all);
                return;
            }

            var filtered = _all.Where(p =>
                (p.FullName ?? "").ToLower().Contains(query) ||
                (p.Email ?? "").ToLower().Contains(query) ||
                (p.Phone ?? "").ToLower().Contains(query)
            ).ToList();

            _view.RenderPassengers(filtered);
        }

        private void OnTogglePassenger(int passengerId)
        {
            try
            {
                var list = _service.GetBookingsForPassenger(passengerId);

                var upcoming = list
                    .Where(b => b.Departure >= DateTime.Now &&
                                !string.Equals(b.Status, "Cancelled", StringComparison.OrdinalIgnoreCase))
                    .OrderBy(b => b.Departure)
                    .ToList();

                var past = list
                    .Where(b => b.Departure < DateTime.Now &&
                                !string.Equals(b.Status, "Cancelled", StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(b => b.Departure)
                    .ToList();

                _view.ShowPassengerBookings(passengerId, upcoming, past);
            }
            catch (Exception ex)
            {
                _view.ShowError("Failed to load passenger bookings.\n" + ex.Message);
                _view.ShowPassengerBookings(passengerId, new List<PassengerBookingRow>(), new List<PassengerBookingRow>());
            }
        }

        private void OnCancelBooking(int bookingId, int passengerId)
        {
            if (!_view.Confirm("Cancel this flight booking?", "Confirm"))
                return;

            if (_service.CancelBooking(bookingId, out var err))
            {
                RefreshAll();
                OnTogglePassenger(passengerId);
                _view.ShowInfo("Booking cancelled.");
            }
            else
            {
                _view.ShowError("Cancel failed.\n" + err);
            }
        }
    }
}
