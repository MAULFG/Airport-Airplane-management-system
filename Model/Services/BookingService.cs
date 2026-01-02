using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using MySqlX.XDevAPI;
using System.Linq;

namespace Airport_Airplane_management_system.Model.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _repo;
        private readonly IAppSession _session;

        public BookingService(IBookingRepository repo, IAppSession session)
        {
            _repo = repo;
            _session = session;
        }
        public void LoadBookingsForCurrentUser()
        {
            var user = _session.CurrentUser;
            if (user == null) return;

            var bookings = _repo.GetBookingsForUser(user);

            user.BookedFlights.Clear();
            foreach (var booking in bookings)
                user.AddBooking(booking);
        }


        public bool MakeBooking(User user, int passengerId, Flight flight, FlightSeats seat, out Booking booking, out string error)
        {
            booking = null;
            error = "";

            if (seat.IsBooked)
            {
                error = "Seat already booked.";
                return false;
            }

            bool success = _repo.CreateBooking(
    user.UserID,
    flight.FlightID,
    seat.Id,
    seat.ClassType,
    passengerId,
    seat.SeatPrice,   // ✅ pass the seat-specific price
    out int bookingId,
    out error);


            if (success)
            {
                seat.AssignPassenger(user); // mark seat as booked in memory
                seat.Book(user);
                booking = new Booking(user, flight, seat, seat.ClassType);
                booking.SetDbId(bookingId);
                booking.Confirm();
            }

            return success;
        }

        public bool CancelBooking(Booking booking, out string error)
        {
            error = "";

            if (!_repo.CancelBooking(booking.BookingID, out error))
                return false;

            booking.Cancel();

            // update cached flight
            var cachedFlight = _session.Flights?.FirstOrDefault(f => f.FlightID == booking.Flight.FlightID);
            if (cachedFlight != null)
            {
                var cachedSeat = cachedFlight.FlightSeats.FirstOrDefault(s => s.Id == booking.Seat.Id);
                if (cachedSeat != null)
                    cachedSeat.RemovePassenger();
            }

            return true;
        }
    }
}
