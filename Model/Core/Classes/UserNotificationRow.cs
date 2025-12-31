using System;

namespace Airport_Airplane_management_system.Model.Core.Classes
{
    public class UserNotificationRow
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }

        public int? BookingId { get; set; }   // used for "See ticket"

        public string Type { get; set; }      // BookingConfirmed, FlightDelayed, ...
        public string Title { get; set; }
        public string Message { get; set; }

        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
