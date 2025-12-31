namespace Airport_Airplane_management_system.Model.Interfaces.Services
{
    public interface INotificationWriterService
    {
        void NotifyBookingConfirmed(int userId, int bookingId);
        void NotifyBookingCancelled(int userId, int bookingId);
        void NotifyFlightCancelled(int userId, int bookingId, int flightId);
        void NotifyFlightDelayed(int userId, int bookingId, int flightId, int minutesDelay);
        void NotifySeatChanged(int userId, int bookingId, string oldSeat, string newSeat);
    }
}
