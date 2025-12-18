using System;
using System.Collections.Generic;
using System.Linq;
using Airport_Airplane_management_system.Model.Core.Classes.Flights;

namespace Airport_Airplane_management_system.Model.Core.Classes.Users
{
    public class User
    {
        public int UserID { get; private set; }
        public string FName { get; private set; }
        public string LName { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
      
        public List<Booking> BookedFlights { get; private set; }

        public string FullName => $"{FName} {LName}";

        public User(int userID, string fname, string lname, string email, string username, string password, string phone)
        {
            UserID = userID;
            FName = fname ?? throw new ArgumentNullException(nameof(fname));
            LName = lname ?? throw new ArgumentNullException(nameof(lname));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            UserName = username ?? throw new ArgumentNullException(nameof(username));
            Password = password ?? throw new ArgumentNullException(nameof(password));
      
            BookedFlights = new List<Booking>();
        }

        public void UpdateProfile(string fname, string lname, string email, string username, string phone)
        {
            FName = fname ?? throw new ArgumentNullException(nameof(fname));
            LName = lname ?? throw new ArgumentNullException(nameof(lname));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            UserName = username ?? throw new ArgumentNullException(nameof(username));
          
        }
        public void ChangePassword(string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                throw new ArgumentException("Password cannot be empty.", nameof(newPassword));

            // You can add more validation here (length, complexity, etc.)
            Password = newPassword;
        }

        public void AddBooking(Booking booking)
        {
            if (booking == null) throw new ArgumentNullException(nameof(booking));
            if (!BookedFlights.Contains(booking))
                BookedFlights.Add(booking);
        }

        public void RemoveBooking(Booking booking)
        {
            if (booking == null) throw new ArgumentNullException(nameof(booking));
            BookedFlights.Remove(booking);
        }

        // DTOs for reporting
        public class PassengerSummaryRow
        {
            public int UserId { get; set; }
            public string FullName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
         
            public DateTime? MemberSince { get; set; }
            public int UpcomingCount { get; set; }
            public int PastCount { get; set; }
            public int TotalCount { get; set; }
        }

        public class PassengerBookingRow
        {
            public int BookingId { get; set; }
            public int UserId { get; set; }
            public int FlightId { get; set; }
            public string FromCity { get; set; } = string.Empty;
            public string ToCity { get; set; } = string.Empty;
            public DateTime Departure { get; set; }
            public DateTime Arrival { get; set; }
            public string SeatNumber { get; set; } = string.Empty;
            public string ClassType { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
        }
    }
}
