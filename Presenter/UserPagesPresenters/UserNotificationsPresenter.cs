using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Core.Classes.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airport_Airplane_management_system.Presenter.UserPagesPresenters
{
    public class UserNotificationsPresenter
    {
        private readonly IUserNotificationsView _view;
        private readonly IUserNotificationsRepository _usernRepo;
        private readonly UserNotificationsService _service;
        private readonly IAppSession _session;

        private int? userid => _session?.CurrentUser?.UserID;

        private List<UserNotificationRow> _all = new();

        public UserNotificationsPresenter(IUserNotificationsView view, IAppSession session)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _session = session ?? throw new ArgumentNullException(nameof(session));

            _usernRepo = new MySqlUserNotificationsRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            _service = new UserNotificationsService(_usernRepo);

            // Event subscriptions
            _view.ViewLoaded += RefreshData;
            _view.RefreshClicked += RefreshData;

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

            _session.UserLoggedIn += RefreshData;
        }
        public void RefreshData()
        {
            if (userid == null)
            {
                _view.BindNotifications(new List<UserNotificationRow>());
                _view.SetUnreadCount(0);
                return;
            }

            try
            {
                _all = _service.Load(userid.Value);
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
            if (userid == null) return;
            int unread = _service.GetUnreadCount(userid.Value);
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
                rows = filter.ToLower() switch
                {
                    "unread" => rows.Where(n => !n.IsRead).ToList(),
                    "read" => rows.Where(n => n.IsRead).ToList(),
                    _ => rows.Where(n => string.Equals(n.Type, filter, StringComparison.OrdinalIgnoreCase)).ToList()
                };
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
            if (_view.FocusedNotificationId == null || userid == null) return;

            var n = _all.FirstOrDefault(x => x.NotificationId == _view.FocusedNotificationId.Value);
            if (n == null || n.IsRead) return;

            try
            {
                if (_service.MarkRead(userid.Value, n.NotificationId))
                {
                    n.IsRead = true;
                    RefreshUnread();
                    ApplyFilter();
                }
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        private void OnMenuMarkRead() => ToggleMarkReadUnread(true);
        private void OnMenuMarkUnread() => ToggleMarkReadUnread(false);

        private void ToggleMarkReadUnread(bool markRead)
        {
            if (_view.FocusedNotificationId == null || userid == null) return;

            var n = _all.FirstOrDefault(x => x.NotificationId == _view.FocusedNotificationId.Value);
            if (n == null) return;

            try
            {
                if (markRead && !n.IsRead)
                {
                    _service.MarkRead(userid.Value, n.NotificationId);
                    n.IsRead = true;
                }
                else if (!markRead && n.IsRead)
                {
                    _service.MarkUnread(userid.Value, n.NotificationId);
                    n.IsRead = false;
                }

                RefreshUnread();
                ApplyFilter();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        private void OnMenuDelete()
        {
            if (_view.FocusedNotificationId == null || userid == null) return;

            if (!_view.Confirm("Delete this notification?")) return;

            int id = _view.FocusedNotificationId.Value;
            try
            {
                if (_service.DeleteOne(userid.Value, id))
                {
                    _all.RemoveAll(x => x.NotificationId == id);
                    RefreshUnread();
                    ApplyFilter();
                }
                else
                {
                    _view.ShowError("Delete failed.");
                }
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        private void OnDeleteSelected()
        {
            if (userid == null) return;

            var ids = _view.SelectedNotificationIds;
            if (ids == null || ids.Count == 0)
            {
                _view.ShowError("Select notifications first.");
                return;
            }

            if (!_view.Confirm($"Delete selected ({ids.Count}) notifications?")) return;

            try
            {
                int deleted = _service.DeleteMany(userid.Value, ids);
                _all.RemoveAll(x => ids.Contains(x.NotificationId));
                RefreshUnread();
                ApplyFilter();
                _view.ShowInfo($"Deleted: {deleted}");
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        private void OnClearAll()
        {
            if (userid == null) return;

            if (!_view.Confirm("Clear all notifications?")) return;

            try
            {
                int deleted = _service.ClearAll(userid.Value);
                _all.Clear();
                RefreshUnread();
                ApplyFilter();
                _view.ShowInfo($"Cleared: {deleted}");
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        private void OnMarkSelectedRead() => MarkSelected(true);
        private void OnMarkSelectedUnread() => MarkSelected(false);

        private void MarkSelected(bool markRead)
        {
            if (userid == null) return;

            var ids = _view.SelectedNotificationIds;
            if (ids == null || ids.Count == 0) return;

            try
            {
                if (markRead)
                    _service.MarkRead(userid.Value, ids);
                else
                    _service.MarkUnread(userid.Value, ids);

                RefreshData();
                _view.RequestBadgeRefresh();
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        private void OnSelectAll()
        {
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

            _view.SelectAllUI();
        }

        private bool ShouldBeVisibleInCurrentFilter(UserNotificationRow n)
        {
            string filter = (_view.Filter ?? "All").Trim();
            string search = (_view.SearchText ?? "").Trim();

            bool ok = filter.ToLower() switch
            {
                "unread" => !n.IsRead,
                "read" => n.IsRead,
                _ => string.Equals(n.Type, filter, StringComparison.OrdinalIgnoreCase)
            };

            if (!ok) return false;

            if (!string.IsNullOrWhiteSpace(search))
            {
                return (n.Title ?? "").Contains(search, StringComparison.OrdinalIgnoreCase) ||
                       (n.Message ?? "").Contains(search, StringComparison.OrdinalIgnoreCase) ||
                       (n.Type ?? "").Contains(search, StringComparison.OrdinalIgnoreCase) ||
                       (n.BookingId?.ToString() ?? "").Contains(search);
            }

            return true;
        }
    }
}
