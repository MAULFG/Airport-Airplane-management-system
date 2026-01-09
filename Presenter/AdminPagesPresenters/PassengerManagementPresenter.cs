using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Core.Classes.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Collections.Specialized.BitVector32;
namespace Airport_Airplane_management_system.Presenter.AdminPages
{
    public class PassengerManagementPresenter
    {
        private readonly IPassengerManagementView _view;
        private readonly PassengerService _service;
        private readonly IBookingRepository bookRepo;
        private readonly IPassengerRepository passRepo;
        private readonly INotificationWriterRepository notirepo;
        private readonly IAppSession session;
        private readonly NotificationWriterService _notifWriter;
        private readonly Func<int> _countUpcomingFlightsNotFullyBooked;
        private List<PassengerSummaryRow> _all = new();

        public PassengerManagementPresenter(IPassengerManagementView view,Func<int> countUpcomingFlightsNotFullyBooked)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            session = new AppSession();
            _countUpcomingFlightsNotFullyBooked = countUpcomingFlightsNotFullyBooked ?? throw new ArgumentNullException(nameof(countUpcomingFlightsNotFullyBooked));
            bookRepo = new MySqlBookingRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            notirepo = new MySqlNotificationWriterRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            passRepo = new MySqlPassengerRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            _notifWriter =new NotificationWriterService(notirepo);
            _service = new PassengerService(passRepo, session);
            _view.ViewLoaded += OnLoad;
            _view.SearchChanged += OnSearch;
            _view.PassengerToggleRequested += OnTogglePassenger;
            _view.CancelBookingRequested += OnCancelBooking;
        }
        public void RefreshData()
        {
            RefreshAll();
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

            //  Before cancel, get booking owner + flight
            if (bookRepo.TryGetBookingNotificationInfo(
        bookingId,
        out int userId,
        out int flightId,
        out int passengerIdFromDb,
        out string passengerName,
        out string fromCity,
        out string toCity,
        out DateTime dep,
        out DateTime arr,
        out string infoErr))
            {
                _notifWriter.NotifyBookingCancelledByAdmin(userId, flightId, passengerId, passengerName);
            }


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
