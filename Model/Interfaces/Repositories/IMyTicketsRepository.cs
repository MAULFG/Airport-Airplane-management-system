using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;

namespace Airport_Airplane_management_system.Model.Interfaces.Repositories
{
    public interface IMyTicketsRepository
    {
        List<MyTicketRow> GetTicketsForUser(int userId);
        bool CancelTicket(int bookingId, int userId, out string error);
    }
}
