using System;
using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface IUserNotificationsView
    {
        int UserId { get; }

        string Filter { get; }      // "All", "Unread", "Read", or type like "FlightDelayed"
        string SearchText { get; }

        List<int> SelectedNotificationIds { get; } // ctrl multi-select
        int? FocusedNotificationId { get; }        // last clicked / card menu target

        event Action ViewLoaded;
        event Action RefreshClicked;
        event Action ClearAllClicked;
        event Action DeleteSelectedClicked;
        event Action SelectAllClicked;

        event Action FilterChanged;
        event Action SearchChanged;

        event Action NotificationClicked;          // open = mark read, clear selection
        event Action MenuMarkReadClicked;
        event Action MenuMarkUnreadClicked;
        event Action MenuDeleteClicked;
        event Action MenuSeeTicketClicked;

        void BindNotifications(List<UserNotificationRow> rows);

        void SetUnreadCount(int count);            // for internal label, optional
        void ShowInfo(string message);
        void ShowError(string message);
        bool Confirm(string message);
        void SelectAllUI();
        void ClearSelectionPublic();

        // ask dashboard to refresh bell badge
        void RequestBadgeRefresh();
        event Action MarkSelectedReadClicked;
        event Action MarkSelectedUnreadClicked;

    }
}