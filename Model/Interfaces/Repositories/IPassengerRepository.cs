using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;

namespace Airport_Airplane_management_system.Model.Interfaces.Repositories
{
    public interface IPassengerRepository
    {
        List<PassengerSummaryRow> GetPassengersSummary();
        List<PassengerBookingRow> GetBookingsForPassenger(int passengerId);

        bool CancelBooking(int bookingId, out string error);
    }
}
