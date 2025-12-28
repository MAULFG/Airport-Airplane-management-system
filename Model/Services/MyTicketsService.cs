using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Services;

namespace Airport_Airplane_management_system.Model.Services
{
    public class MyTicketsService : IMyTicketsService
    {
        private readonly IMyTicketsRepository _repo;

        public MyTicketsService(IMyTicketsRepository repo)
        {
            _repo = repo;
        }

        public List<MyTicketRow> LoadTickets(int userId)
        {
            if (userId <= 0)
                return new List<MyTicketRow>();

            return _repo.GetTicketsForUser(userId);
        }

        public bool CancelTicket(int userId, int bookingId, out string error)
        {
            error = "";

            if (userId <= 0)
            {
                error = "Invalid user.";
                return false;
            }

            if (bookingId <= 0)
            {
                error = "Invalid ticket.";
                return false;
            }

            return _repo.CancelTicket(bookingId, userId, out error);
        }
    }
}
