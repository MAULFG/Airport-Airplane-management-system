using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;

namespace Airport_Airplane_management_system.Model.Services
{
    public class PassengerService
    {
        private readonly IPassengerRepository _repo;

        public PassengerService(IPassengerRepository repo)
        {
            _repo = repo;
        }

        public List<PassengerSummaryRow> GetPassengersSummary()
            => _repo.GetPassengersSummary();

        public List<PassengerBookingRow> GetBookingsForPassenger(int passengerId)
            => _repo.GetBookingsForPassenger(passengerId);

        public bool CancelBooking(int bookingId, out string error)
            => _repo.CancelBooking(bookingId, out error);
    }
}
