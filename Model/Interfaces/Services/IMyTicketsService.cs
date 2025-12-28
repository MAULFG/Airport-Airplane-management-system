using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;

namespace Airport_Airplane_management_system.Model.Interfaces.Services
{
    public interface IMyTicketsService
    {
        List<MyTicketRow> LoadTickets(int userId);
        bool CancelTicket(int userId, int bookingId, out string error);
    }
}
