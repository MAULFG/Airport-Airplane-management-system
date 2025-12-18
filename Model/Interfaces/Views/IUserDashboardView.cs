using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface IUserDashboardView
    {
        // Method to display bookings in the UI
        void DisplayBookings(List<BookingRow> bookings);
    }

    // DTO to send booking data from Presenter to View
    public class BookingRow
    {
        public int BookingID { get; set; }
        public string FlightRoute { get; set; }
        public string Category { get; set; }
        public string Seat { get; set; }
        public string Status { get; set; }
    }
}
