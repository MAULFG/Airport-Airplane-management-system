using Airport_Airplane_management_system.Model.Core.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Model.Interfaces.Repositories
{
    public interface IBookingRepository
    {
        bool CreateBooking(int userId, int flightId, int flightSeatId, int passengerId, out int bookingId, out string error);


        bool CancelBooking(int bookingId, out string error);
        List<Booking> GetBookingsForUser(User user);

        List<int> GetActiveBookingIdsForFlight(int flightId);
        List<int> GetUserIdsForFlight(int flightId);

        bool TryGetBookingNotificationInfo(int bookingId, out int userId, out int flightId, out int passengerId, out string passengerName, out string fromCity, out string toCity, out DateTime departure, out DateTime arrival, out string error);


    }


}
