using Airport_Airplane_management_system.Model.Core.Classes;
using System.Collections.Generic;
using static Airport_Airplane_management_system.Model.Core.Classes.User;

namespace Airport_Airplane_management_system.Model.Interfaces.Repositories
{
    public interface IPassengerRepository
    {
        List<PassengerSummaryRow> GetPassengersSummary();
        List<PassengerBookingRow> GetBookingsForPassenger(int passengerId);
        bool AddPassenger(string fullName,string email,string phone,out int passengerId,out string error);
        int? GetPassengerIdByPhone(string phone);
        bool CancelBooking(int bookingId, out string error);
    }
}