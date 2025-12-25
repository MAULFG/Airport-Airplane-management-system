using System;
using System.Collections.Generic;
using System.Text;
namespace Airport_Airplane_management_system.Model.Core.Classes
{
    public class Booking
    {
        public int BookingID { get; private set; }       // Assigned by DB
        public User User { get; private set; }
        public Flight Flight { get; private set; }
        public FlightSeats Seat { get; private set; }
        public string Category { get; private set; }
        public string SeatCode => Seat?.SeatNumber;
        public string Status { get; private set; }

        #region Constructors

        // For new bookings (before saving to DB)
        public Booking(User user, Flight flight, FlightSeats seat, string category)
        {
            User = user ?? throw new ArgumentNullException(nameof(user));
            Flight = flight ?? throw new ArgumentNullException(nameof(flight));
            Seat = seat ?? throw new ArgumentNullException(nameof(seat));
            Category = category ?? throw new ArgumentNullException(nameof(category));

            Status = "Pending";
        }

        // For loading existing bookings from DB
        public Booking(int bookingId, User user, Flight flight, FlightSeats seat, string category, string status)
            : this(user, flight, seat, category)
        {
            BookingID = bookingId;
            Status = string.IsNullOrWhiteSpace(status) ? "Pending" : status;
        }

        #endregion

        #region Booking Actions

        // Call after inserting into DB to set the DB ID
        public void SetDbId(int bookingId)
        {
            if (BookingID != 0)
                throw new InvalidOperationException("Booking ID is already set.");

            BookingID = bookingId;
        }

        // Confirm the booking
        public bool Confirm()
        {
            if (Seat.IsBooked)
            {
                Status = "Cancelled";
                return false;
            }

            Seat.AssignPassenger(User);
            Status = "Confirmed";
            return true;
        }

        // Cancel the booking
        public void Cancel()
        {
            if (Status == "Confirmed")
            {
                Seat.ReleaseSeat();
                Status = "Cancelled";
            }
        }

        #endregion

        #region Convenience Properties

        public int FlightSeatId => Seat?.Id ?? 0;

        #endregion
    }
}
