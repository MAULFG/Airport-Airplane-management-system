using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using System.Linq;

namespace Airport_Airplane_management_system.Model.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _repo;

        public BookingService(IBookingRepository repo)
        {
            _repo = repo;
        }

        public bool MakeBooking(
            User user,
            Flight flight,
            string category,
            string seatNumber,
            out Booking booking,
            out string error)
        {
            booking = null;
            error = "";

            var seat = flight.GetAvailableSeats(category)
                             .FirstOrDefault(s => s.SeatNumber == seatNumber);

            if (seat == null)
            {
                error = "Seat not available.";
                return false;
            }

            if (!_repo.CreateBooking(
                user.UserID,
                flight.FlightID,
                seat.Id,
                category,
                out int bookingId,
                out error))
            {
                return false;
            }

            booking = new Booking(user, flight, seat, category);
            booking.SetDbId(bookingId);
            booking.Confirm();

            return true;
        }

        public bool CancelBooking(Booking booking, out string error)
        {
            error = "";

            if (!_repo.CancelBooking(booking.BookingID, out error))
                return false;

            booking.Cancel();
            return true;
        }
    }
}
