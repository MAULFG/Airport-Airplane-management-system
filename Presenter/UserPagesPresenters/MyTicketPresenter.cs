using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Services;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airport_Airplane_management_system.Presenter.UserPagesPresenters
{
    public class MyTicketsPresenter
    {
        private readonly IMyTicketsView _view;
        private readonly MyTicketsService _service;
        private readonly IAppSession _session;
        private readonly NotificationWriterService _notifWriter;
        private List<MyTicketRow> _allTickets = new();

        public MyTicketsPresenter(IMyTicketsView view, MyTicketsService service,IAppSession session, NotificationWriterService notifWriter)
        {
            _view = view;
            _service = service;
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _notifWriter = notifWriter ?? throw new ArgumentNullException(nameof(notifWriter));
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
                if (_session.CurrentUser == null)
                {
                    _view.ShowError("No user is logged in.");
                    return;
                }

                _allTickets = _service.LoadTickets(_session.CurrentUser.UserID);
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
                    rows = rows.Where(r => r.Departure >= DateTime.Now).ToList();
                }
                else if (string.Equals(filter, "Past", StringComparison.OrdinalIgnoreCase))
                {
                    rows = rows.Where(r => r.Departure < DateTime.Now).ToList();
                }
                else
                {
                    rows = rows.Where(r => string.Equals(r.Status, filter, StringComparison.OrdinalIgnoreCase)).ToList();
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
                if (_session.CurrentUser == null)
                {
                    _view.ShowError("No user is logged in.");
                    return;
                }

                if (_view.SelectedBookingId == null)
                {
                    _view.ShowError("Please select a ticket to cancel.");
                    return;
                }

                int userId = _session.CurrentUser.UserID;
                int bookingId = _view.SelectedBookingId.Value;

                if (!_view.Confirm("Are you sure you want to cancel this ticket?"))
                    return;

                bool ok = _service.CancelTicket(userId, bookingId, out string error);

                if (!ok)
                {
                    _view.ShowError(error);
                    return;
                }

                // ✅ THIS IS THE MISSING PART (insert into notifications)
                _notifWriter.NotifyBookingCancelled(userId, bookingId);

                _view.ShowInfo("Ticket cancelled successfully.");
                OnLoad();

                // ✅ optional but useful (if you add it)
                _view.RequestBadgeRefresh();
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
                    rows = rows.Where(r => r.Departure >= DateTime.Now).ToList();
                else if (string.Equals(filter, "Past", StringComparison.OrdinalIgnoreCase))
                    rows = rows.Where(r => r.Departure < DateTime.Now).ToList();
                else
                    rows = rows.Where(r => string.Equals(r.Status, filter, StringComparison.OrdinalIgnoreCase)).ToList();
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
