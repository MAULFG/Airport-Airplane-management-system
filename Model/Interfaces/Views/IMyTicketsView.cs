using System;
using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface IMyTicketsView
    {
        int UserId { get; }

        string Filter { get; }
        string SearchText { get; }
        int? SelectedBookingId { get; }

        event Action ViewLoaded;
        event Action RefreshClicked;
        event Action CancelClicked;
        event Action FilterChanged;
        event Action SearchChanged;

        void BindTickets(List<MyTicketRow> rows);
        void ShowInfo(string message);
        void ShowError(string message);
        bool Confirm(string message);

    }
}