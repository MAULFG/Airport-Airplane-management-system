using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Core.Classes.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airport_Airplane_management_system.Model.Services
{
    public class PassengerService
    {
        private readonly IPassengerRepository _repo;
        private readonly IAppSession _session;

        public PassengerService(IPassengerRepository repo, IAppSession session)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _session = session ?? throw new ArgumentNullException(nameof(session));
        }

        // -----------------------------
        // ADD PASSENGER
        // -----------------------------
        public bool AddPassenger(
            string fullName,
            string email,
            string phone,
            out int passengerId,
            out string error)
        {
            passengerId = 0;
            error = "";

            if (string.IsNullOrWhiteSpace(fullName))
            {
                error = "Full name is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                error = "Email is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                error = "Phone number is required.";
                return false;
            }

            bool success = _repo.AddPassenger(fullName, email, phone, out passengerId, out error);

            if (success)
            {
                // Invalidate cached passenger summary
                _session.PassengersSummary = null;
            }

            return success;
        }
        public int? GetPassengerIdByPhone(string phone)
        {
            return _repo.GetPassengerIdByPhone(phone);
        }

        // -----------------------------
        // GET PASSENGERS SUMMARY (CACHED)
        // -----------------------------
        public List<PassengerSummaryRow> GetPassengersSummary()
        {
            if (_session.PassengersSummary != null)
                return _session.PassengersSummary;

            var summary = _repo.GetPassengersSummary();
            _session.PassengersSummary = summary;
            return summary;
        }

        // -----------------------------
        // GET BOOKINGS FOR A PASSENGER
        // -----------------------------
        public List<PassengerBookingRow> GetBookingsForPassenger(int passengerId)
        {
            return _repo.GetBookingsForPassenger(passengerId);
        }

        // -----------------------------
        // CANCEL BOOKING
        // -----------------------------
        public bool CancelBooking(int bookingId, out string error)
             => _repo.CancelBooking(bookingId, out error);
    }
}
