using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Core.Classes.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airport_Airplane_management_system.Presenter.UserPagesPresenters
{
    public class MyTicketsPresenter
    {
        private readonly IMyTicketsView _view;
        private readonly MyTicketsService _service;
        private readonly NotificationWriterService notifWriter;
        private readonly IAppSession _session;
        private readonly NotificationWriterService _notifWriter;
        private readonly INotificationWriterRepository _notiRepo;
        private readonly IMyTicketsRepository _ticketsRepository;
        private List<MyTicketRow> _allTickets = new();

        public MyTicketsPresenter(IMyTicketsView view,  IAppSession session)
        {
            _view = view;
            
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _notiRepo = new MySqlNotificationWriterRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            _ticketsRepository = new MySqlMyTicketsRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            _service = new MyTicketsService(_ticketsRepository);
            _notifWriter = new NotificationWriterService(_notiRepo);

            _view.ViewLoaded += () => RefreshData();
            _view.RefreshClicked += () => RefreshData();

            _view.FilterChanged += ApplyFilter;
            _view.SearchChanged += ApplyFilter;
            _view.CancelClicked += OnCancel;
        }

        /// <summary>
        /// ✅ Public method to refresh all ticket data
        /// Call this whenever the My Tickets page is opened
        /// </summary>
        public void RefreshData()
        {
            try
            {
                if (_session.CurrentUser == null)
                {
                    _view.ShowError("No user is logged in.");
                    return;
                }

                // Load all tickets for current user
                _allTickets = _service.LoadTickets(_session.CurrentUser.UserID);

                // Apply filter/search and bind
                ApplyFilter();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        private void ApplyFilter()
        {
            var filtered = ApplyFilterAndSearch(_allTickets);
            _view.BindTickets(filtered);
        }

        private List<MyTicketRow> ApplyFilterAndSearch(List<MyTicketRow> rows)
        {
            var filter = (_view.Filter ?? "All").Trim();
            var search = (_view.SearchText ?? "").Trim();

            // Filter
            if (!string.Equals(filter, "All", StringComparison.OrdinalIgnoreCase))
            {
                if (string.Equals(filter, "Upcoming", StringComparison.OrdinalIgnoreCase))
                    rows = rows.Where(r => r.Departure >= DateTime.Now).ToList();
                else if (string.Equals(filter, "Past", StringComparison.OrdinalIgnoreCase))
                    rows = rows.Where(r => r.Departure < DateTime.Now).ToList();
                else
                    rows = rows.Where(r => string.Equals(r.Status, filter, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Search
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

            return rows;
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

                if (!_view.Confirm("Are you sure you want to cancel this ticket?"))
                    return;

                int userId = _session.CurrentUser.UserID;
                int bookingId = _view.SelectedBookingId.Value;

                bool ok = _service.CancelTicket(userId, bookingId, out string error);
                if (!ok)
                {
                    _view.ShowError(error);
                    return;
                }

                // Notify user
                _notifWriter.NotifyBookingCancelled(userId, bookingId);

                _view.ShowInfo("Ticket cancelled successfully.");

                // Refresh the list
                RefreshData();

                // Optional: update notifications badge
                _view.RequestBadgeRefresh();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }
    }
}
