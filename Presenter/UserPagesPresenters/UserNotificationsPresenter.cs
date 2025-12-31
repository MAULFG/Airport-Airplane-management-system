using System;
using System.Collections.Generic;
using System.Linq;
using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Services;
using Airport_Airplane_management_system.Model.Interfaces.Views;

namespace Airport_Airplane_management_system.Presenter.UserPagesPresenters
{
    public class UserNotificationsPresenter
    {
        private readonly IUserNotificationsView _view;
        private readonly IUserNotificationsService _service;

        private List<UserNotificationRow> _all = new();

        public UserNotificationsPresenter(IUserNotificationsView view, IUserNotificationsService service)
        {
            _view = view;
            _service = service;

            _view.ViewLoaded += OnLoad;
            _view.RefreshClicked += OnLoad;

            _view.FilterChanged += ApplyFilter;
            _view.SearchChanged += ApplyFilter;

            _view.NotificationClicked += OnNotificationClicked;

            _view.MenuMarkReadClicked += OnMenuMarkRead;
            _view.MenuMarkUnreadClicked += OnMenuMarkUnread;
            _view.MenuDeleteClicked += OnMenuDelete;

            _view.DeleteSelectedClicked += OnDeleteSelected;
            _view.ClearAllClicked += OnClearAll;

            _view.MarkSelectedReadClicked += OnMarkSelectedRead;
            _view.MarkSelectedUnreadClicked += OnMarkSelectedUnread;

           

        }

        private void OnLoad()
        {
            try
            {
                _all = _service.Load(_view.UserId);
                ApplyFilter();

                RefreshUnread();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        private void RefreshUnread()
        {
            int unread = _service.GetUnreadCount(_view.UserId);
            _view.SetUnreadCount(unread);
            _view.RequestBadgeRefresh();
        }

        private void ApplyFilter()
        {
            var rows = _all;

            string filter = (_view.Filter ?? "All").Trim();
            string search = (_view.SearchText ?? "").Trim();

            // Filter: All / Unread / Read / By Type
            if (!string.Equals(filter, "All", StringComparison.OrdinalIgnoreCase))
            {
                if (string.Equals(filter, "Unread", StringComparison.OrdinalIgnoreCase))
                    rows = rows.Where(n => !n.IsRead).ToList();
                else if (string.Equals(filter, "Read", StringComparison.OrdinalIgnoreCase))
                    rows = rows.Where(n => n.IsRead).ToList();
                else
                    rows = rows.Where(n => string.Equals(n.Type, filter, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Search in title/message/type
            if (!string.IsNullOrWhiteSpace(search))
            {
                rows = rows.Where(n =>
                    (n.Title ?? "").Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (n.Message ?? "").Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (n.Type ?? "").Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (n.BookingId?.ToString() ?? "").Contains(search)
                ).ToList();
            }

            _view.BindNotifications(rows);
        }

        private void OnNotificationClicked()
        {
            try
            {
                // clicking notification => mark read (only if unread)
                if (_view.FocusedNotificationId == null) return;

                var n = _all.FirstOrDefault(x => x.NotificationId == _view.FocusedNotificationId.Value);
                if (n == null) return;

                if (!n.IsRead)
                {
                    bool ok = _service.MarkRead(_view.UserId, n.NotificationId);
                    if (ok) n.IsRead = true;
                    RefreshUnread();
                    ApplyFilter();
                }
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        private void OnMenuMarkRead()
        {
            if (_view.FocusedNotificationId == null) return;

            var n = _all.FirstOrDefault(x => x.NotificationId == _view.FocusedNotificationId.Value);
            if (n == null) return;

            if (!n.IsRead)
            {
                _service.MarkRead(_view.UserId, n.NotificationId);
                n.IsRead = true;
                RefreshUnread();
                ApplyFilter();
            }
        }

        private void OnMenuMarkUnread()
        {
            if (_view.FocusedNotificationId == null) return;

            var n = _all.FirstOrDefault(x => x.NotificationId == _view.FocusedNotificationId.Value);
            if (n == null) return;

            if (n.IsRead)
            {
                _service.MarkUnread(_view.UserId, n.NotificationId);
                n.IsRead = false;
                RefreshUnread();
                ApplyFilter();
            }
        }

        private void OnMenuDelete()
        {
            if (_view.FocusedNotificationId == null) return;

            if (!_view.Confirm("Delete this notification?")) return;

            int id = _view.FocusedNotificationId.Value;

            bool ok = _service.DeleteOne(_view.UserId, id);
            if (!ok) { _view.ShowError("Delete failed."); return; }

            _all.RemoveAll(x => x.NotificationId == id);
            RefreshUnread();
            ApplyFilter();
        }

        private void OnDeleteSelected()
        {
            var ids = _view.SelectedNotificationIds;
            if (ids == null || ids.Count == 0)
            {
                _view.ShowError("Select notifications first.");
                return;
            }

            if (!_view.Confirm($"Delete selected ({ids.Count}) notifications?")) return;

            int deleted = _service.DeleteMany(_view.UserId, ids);
            _all.RemoveAll(x => ids.Contains(x.NotificationId));

            RefreshUnread();
            ApplyFilter();
            _view.ShowInfo($"Deleted: {deleted}");
        }

        private void OnClearAll()
        {
            if (!_view.Confirm("Clear all notifications?")) return;

            int deleted = _service.ClearAll(_view.UserId);
            _all.Clear();

            RefreshUnread();
            ApplyFilter();
            _view.ShowInfo($"Cleared: {deleted}");
        }
        private void OnMarkSelectedRead()
        {
            var ids = _view.SelectedNotificationIds;
            if (ids.Count == 0) return;

            _service.MarkRead(_view.UserId, ids);
            OnLoad();
            _view.RequestBadgeRefresh();
        }

        private void OnMarkSelectedUnread()
        {
            var ids = _view.SelectedNotificationIds;
            if (ids.Count == 0) return;

            _service.MarkUnread(_view.UserId, ids);
            OnLoad();
            _view.RequestBadgeRefresh();
        }

    }
}
