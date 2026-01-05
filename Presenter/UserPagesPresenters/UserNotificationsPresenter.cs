using System;
using System.Collections.Generic;
using System.Linq;
using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Repositories;

namespace Airport_Airplane_management_system.Presenter.UserPagesPresenters
{
    public class UserNotificationsPresenter
    {
        private readonly IUserNotificationsView _view;
        private readonly IUserNotificationsRepository _usernRepo;
        private readonly UserNotificationsService _service;

        private List<UserNotificationRow> _all = new();

        public UserNotificationsPresenter(IUserNotificationsView view)
        {
            _view = view;
            _usernRepo =new MySqlUserNotificationsRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            _service = new UserNotificationsService(_usernRepo);

            // Events
            _view.ViewLoaded += () => RefreshData();
            _view.RefreshClicked += () => RefreshData();

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

            _view.SelectAllClicked += OnSelectAll;
        }

        /// <summary>
        /// ✅ Public method to refresh all notifications from the service.
        /// Call this whenever the Notifications panel is opened.
        /// </summary>
        public void RefreshData()
        {
            try
            {
                _all = _service.Load(_view.UserId); // load all notifications
                ApplyFilter();                       // apply current filter/search
                RefreshUnread();                     // update unread count/badge
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

            if (!string.Equals(filter, "All", StringComparison.OrdinalIgnoreCase))
            {
                if (string.Equals(filter, "Unread", StringComparison.OrdinalIgnoreCase))
                    rows = rows.Where(n => !n.IsRead).ToList();
                else if (string.Equals(filter, "Read", StringComparison.OrdinalIgnoreCase))
                    rows = rows.Where(n => n.IsRead).ToList();
                else
                    rows = rows.Where(n => string.Equals(n.Type, filter, StringComparison.OrdinalIgnoreCase)).ToList();
            }

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
            RefreshData();
            _view.RequestBadgeRefresh();
        }

        private void OnMarkSelectedUnread()
        {
            var ids = _view.SelectedNotificationIds;
            if (ids.Count == 0) return;

            _service.MarkUnread(_view.UserId, ids);
            RefreshData();
            _view.RequestBadgeRefresh();
        }
        private void OnSelectAll()
        {
            // if already everything selected -> clear selection (toggle behavior)
            var visibleIds = _all
                .Where(n => ShouldBeVisibleInCurrentFilter(n))
                .Select(n => n.NotificationId)
                .ToList();

            var currentlySelected = _view.SelectedNotificationIds ?? new List<int>();

            bool allSelected = visibleIds.Count > 0 && visibleIds.All(id => currentlySelected.Contains(id));

            if (allSelected)
            {
                _view.ClearSelectionPublic();
                return;
            }

            // Select ALL visible cards (after filter/search)
            _view.SelectAllUI();
        }


        private bool ShouldBeVisibleInCurrentFilter(UserNotificationRow n)
        {
            string filter = (_view.Filter ?? "All").Trim();
            string search = (_view.SearchText ?? "").Trim();

            // filter logic (must match ApplyFilter)
            bool ok = true;

            if (!string.Equals(filter, "All", StringComparison.OrdinalIgnoreCase))
            {
                if (string.Equals(filter, "Unread", StringComparison.OrdinalIgnoreCase))
                    ok = !n.IsRead;
                else if (string.Equals(filter, "Read", StringComparison.OrdinalIgnoreCase))
                    ok = n.IsRead;
                else
                    ok = string.Equals(n.Type, filter, StringComparison.OrdinalIgnoreCase);
            }

            if (!ok) return false;

            if (!string.IsNullOrWhiteSpace(search))
            {
                return
                    (n.Title ?? "").Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (n.Message ?? "").Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (n.Type ?? "").Contains(search, StringComparison.OrdinalIgnoreCase) ||
                    (n.BookingId?.ToString() ?? "").Contains(search);
            }

            return true;
        }

    }
}