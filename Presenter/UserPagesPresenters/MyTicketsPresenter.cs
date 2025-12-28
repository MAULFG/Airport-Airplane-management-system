using System;
using System.Collections.Generic;
using System.Linq;
using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Services;
using Airport_Airplane_management_system.Model.Interfaces.Views;

namespace Airport_Airplane_management_system.Presenter.UserPagesPresenters
{
    public class MyTicketsPresenter
    {
        private readonly IMyTicketsView _view;
        private readonly IMyTicketsService _service;

        private List<MyTicketRow> _allTickets = new();

        public MyTicketsPresenter(IMyTicketsView view, IMyTicketsService service)
        {
            _view = view;
            _service = service;

            _view.ViewLoaded += OnLoad;
            _view.RefreshClicked += OnLoad;
            _view.FilterChanged += ApplyFilter;
            _view.SearchChanged += ApplyFilter;
            _view.CancelClicked += OnCancel;
        }

        private void OnLoad()
        {
            try
            {
                _allTickets = _service.LoadTickets(_view.UserId);
                ApplyFilter();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        private void ApplyFilter()
        {
            var rows = _allTickets;

            var filter = (_view.Filter ?? "All").Trim();
            var search = (_view.SearchText ?? "").Trim();

            // ---------- FILTER ----------
            if (!string.Equals(filter, "All", StringComparison.OrdinalIgnoreCase))
            {
                if (string.Equals(filter, "Upcoming", StringComparison.OrdinalIgnoreCase))
                {
                    rows = rows
                        .Where(r => r.Departure >= DateTime.Now)
                        .ToList();
                }
                else if (string.Equals(filter, "Past", StringComparison.OrdinalIgnoreCase))
                {
                    rows = rows
                        .Where(r => r.Departure < DateTime.Now)
                        .ToList();
                }
                else
                {
                    // Confirmed / Pending / Cancelled
                    rows = rows
                        .Where(r => string.Equals(r.Status, filter, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }
            }

            // ---------- SEARCH ----------
            if (!string.IsNullOrWhiteSpace(search))
            {
                rows = rows.Where(r =>
                    (r.PassengerName ?? "").Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (r.FromCity ?? "").Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (r.ToCity ?? "").Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.BookingId.ToString().Contains(search) ||
                    r.FlightId.ToString().Contains(search)
                ).ToList();
            }

            _view.BindTickets(rows);
        }


        private void OnCancel()
        {
            try
            {
                if (_view.SelectedBookingId == null)
                {
                    _view.ShowError("Please select a ticket to cancel.");
                    return;
                }

                if (!_view.Confirm("Are you sure you want to cancel this ticket?"))
                    return;

                bool ok = _service.CancelTicket(
                    _view.UserId,
                    _view.SelectedBookingId.Value,
                    out string error);

                if (!ok)
                {
                    _view.ShowError(error);
                    return;
                }

                _view.ShowInfo("Ticket cancelled successfully.");
                OnLoad();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }
        private List<MyTicketRow> ApplyFilterAndSearch(List<MyTicketRow> rows)
        {
            var filter = (_view.Filter ?? "All").Trim();
            var search = (_view.SearchText ?? "").Trim();

            // ---- FILTER ----
            if (!string.Equals(filter, "All", StringComparison.OrdinalIgnoreCase))
            {
                if (string.Equals(filter, "Upcoming", StringComparison.OrdinalIgnoreCase))
                {
                    rows = rows.Where(r => r.Departure >= DateTime.Now).ToList();
                }
                else if (string.Equals(filter, "Past", StringComparison.OrdinalIgnoreCase))
                {
                    rows = rows.Where(r => r.Departure < DateTime.Now).ToList();
                }
                else
                {
                    // Confirmed / Pending / Cancelled
                    rows = rows.Where(r => string.Equals(r.Status, filter, StringComparison.OrdinalIgnoreCase)).ToList();
                }
            }

            // ---- SEARCH ----
            if (!string.IsNullOrWhiteSpace(search))
            {
                rows = rows.Where(r =>
                    (r.PassengerName ?? "").Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (r.FromCity ?? "").Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (r.ToCity ?? "").Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.FlightId.ToString().Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    r.BookingId.ToString().Contains(search, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            return rows;
        }
    
    }
}
