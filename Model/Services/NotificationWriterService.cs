using Airport_Airplane_management_system.Model.Interfaces.Repositories;

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
            if (userId <= 0) return;
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
    }
}