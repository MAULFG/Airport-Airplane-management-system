using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace Airport_Airplane_management_system.Model.Services
{
    public class NotificationWriterService
    {
        private readonly INotificationWriterRepository _repo;

        public NotificationWriterService(INotificationWriterRepository repo)
        {
            _repo = repo;
        }

        public void NotifyBookingConfirmed(int userId, int bookingId)
        {
            if (userId <= 0) return;

            _repo.AddUserNotification(
                userId,
                type: "BookingConfirmed",
                title: "✅ Booking confirmed",
                message: $"Your booking #{bookingId} has been confirmed.",
                bookingId: bookingId
            );
        }

        public void NotifyBookingCancelled(int userId, int bookingId)
        {
            _repo.AddUserNotification(
                userId,
                type: "BookingCancelled",
                title: "❌ Booking cancelled",
                message: $"Your booking #{bookingId} has been cancelled.",
                bookingId: bookingId
            );
        }

        public void NotifyFlightCancelled(int userId, int bookingId, int flightId)
        {
            if (userId <= 0) return;
            _repo.AddUserNotification(
                userId,
                type: "FlightCancelled",
                title: "⚠️ Flight cancelled",
                message: $"Flight #{flightId} related to booking #{bookingId} has been cancelled.",
                bookingId: bookingId
            );
        }

        public void NotifyFlightDelayed(int userId, int bookingId, int flightId, int minutesDelay)
        {
            if (userId <= 0) return;
            _repo.AddUserNotification(
                userId,
                type: "FlightDelayed",
                title: "⏱ Flight delayed",
                message: $"Flight #{flightId} for booking #{bookingId} is delayed by {minutesDelay} minutes.",
                bookingId: bookingId
            );
        }

        public void NotifySeatChanged(int userId, int bookingId, string oldSeat, string newSeat)
        {
            if (userId <= 0) return;
            _repo.AddUserNotification(
                userId,
                type: "SeatChanged",
                title: "💺 Seat changed",
                message: $"Booking #{bookingId}: seat changed from {oldSeat} to {newSeat}.",
                bookingId: bookingId
            );
        }
        public void NotifyFlightCancelledByAdmin(int userId, int flightId)
        {
            if (userId <= 0) return;

            _repo.AddUserNotification(
                userId,
                type: "FlightCancelled",
                title: "⚠️ Flight cancelled",
                message: $"Flight #{flightId} has been cancelled by the admin for all passengers.",
                bookingId: null // ✅ NO see ticket
            );
        }

        public void NotifyBookingCancelledByAdmin(int userId, int flightId, int passengerId, string passengerName)
        {
            if (userId <= 0) return;

            _repo.AddUserNotification(
                userId,
                type: "BookingCancelled",
                title: "❌ Booking cancelled",
                message: $"Your booking for flight #{flightId} (Passenger #{passengerId} - {passengerName}) has been cancelled by the admin.",
                bookingId: null
            );
        }


        public void NotifyBookingConfirmedForPassenger(
    int userId,
    int bookingId,
    int flightId,
    int passengerId,
    string passengerName,
    string fromCity,
    string toCity,
    DateTime departure,
    DateTime arrival)
        {
            if (userId <= 0) return;

            _repo.AddUserNotification(
                userId,
                type: "BookingConfirmed",
                title: "✅ Booking confirmed",
                message: $"Passenger #{passengerId} ({passengerName}) — {fromCity} → {toCity} | Dep: {departure:yyyy-MM-dd HH:mm} | Arr: {arrival:yyyy-MM-dd HH:mm}",
                bookingId: bookingId
            );
        }

        public void NotifyFlightDatesUpdated(
            int userId,
            int flightId,
            DateTime? newDeparture,
            DateTime? newArrival)
        {
            if (userId <= 0) return;

            // build message ONLY with the changed values
            var parts = new List<string>();
            if (newDeparture.HasValue) parts.Add($"New departure: {newDeparture.Value:yyyy-MM-dd HH:mm}");
            if (newArrival.HasValue) parts.Add($"New arrival: {newArrival.Value:yyyy-MM-dd HH:mm}");

            if (parts.Count == 0) return; // nothing changed

            _repo.AddUserNotification(
                userId,
                type: "FlightDatesUpdated",
                title: "🕒 Flight schedule updated",
                message: $"Flight #{flightId} updated. " + string.Join(" | ", parts),
                bookingId: null // NO "See Ticket" for this type if you want
            );
        }


    }
}