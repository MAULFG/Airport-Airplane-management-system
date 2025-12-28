using System;

namespace Airport_Airplane_management_system.Model.Core.Classes
{
    public class MyTicketRow
    {
        public int BookingId { get; set; }
        public int FlightId { get; set; }
        public int FlightSeatId { get; set; }

        public int? PassengerId { get; set; }
        public string PassengerName { get; set; } = "";

        public string FromCity { get; set; } = "";
        public string ToCity { get; set; } = "";
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }

        public string SeatNumber { get; set; } = "";
        public string ClassType { get; set; } = "";
        public string Category { get; set; } = "";
        public string Status { get; set; } = "";

        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
