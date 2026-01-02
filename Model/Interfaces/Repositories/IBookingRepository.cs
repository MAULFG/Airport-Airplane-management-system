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
    }


}
