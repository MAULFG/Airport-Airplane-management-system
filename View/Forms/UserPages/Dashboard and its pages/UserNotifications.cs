using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Presenter.UserPagesPresenters;
using Airport_Airplane_management_system.View.Controls;
using Airport_Airplane_management_system.View.Interfaces;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    public partial class UserNotifications : UserControl, IUserNotificationsView
    {

        private bool _initialized;
        private INavigationService _navigation;

        private IUserNotificationsRepository _repo;
        private UserNotificationsService _service;
        private UserNotificationsPresenter _presenter;

        // UI state
        private readonly HashSet<int> _selectedIds = new();
        private int? _focusedId;

        // Exposed event to dashboard
        public event Action BadgeRefreshRequested;
        public event Action<int> SeeTicketRequested;
        public event Action MarkSelectedReadClicked;
        public event Action MarkSelectedUnreadClicked;

        public UserNotifications()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint, true);
            this.UpdateStyles();

            Dock = DockStyle.Fill;


            // events
            btnRefresh.Click += (_, __) => RefreshClicked?.Invoke();
            btnClearAll.Click += (_, __) => ClearAllClicked?.Invoke();
            btnDeleteSelected.Click += (_, __) => DeleteSelectedClicked?.Invoke();
            btnSelectAll.Click += (_, __) => SelectAllClicked?.Invoke();

            cmbFilter.SelectedIndexChanged += (_, __) => FilterChanged?.Invoke();
            txtSearch.TextChanged += (_, __) => SearchChanged?.Invoke();

            VisibleChanged += (_, __) =>
            {
                if (Visible && _initialized)
                    Activate();
            };
            btnMarkReadSelected.Click += (_, __) => MarkSelectedReadClicked?.Invoke();
            btnMarkUnreadSelected.Click += (_, __) => MarkSelectedUnreadClicked?.Invoke();
            flow.SizeChanged += (_, __) => FixCardsWidth();

        }

        // CALL THIS like MyTickets / UserSettings
        public void Initialize(INavigationService navigation)
        {
            if (_initialized) return;

            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));

            var connStr = "server=localhost;port=3306;database=user;user=root;password=2006";

            _repo = new MySqlUserNotificationsRepository(connStr);
            _service = new UserNotificationsService(_repo);
            _presenter = new UserNotificationsPresenter(this);

            _initialized = true;
        }

        public void Activate()
        {
            if (!_initialized) return;
            ViewLoaded?.Invoke();
        }

        // ================== IUserNotificationsView ==================
        public int UserId => _navigation?.GetCurrentUserId() ?? 0;

        public string Filter => cmbFilter.SelectedItem?.ToString() ?? "All";
        public string SearchText => txtSearch.Text ?? "";

        public List<int> SelectedNotificationIds => _selectedIds.ToList();
        public int? FocusedNotificationId => _focusedId;

        public event Action ViewLoaded;
        public event Action RefreshClicked;
        public event Action ClearAllClicked;
        public event Action DeleteSelectedClicked;
        public event Action SelectAllClicked;

        public event Action FilterChanged;
        public event Action SearchChanged;

        public event Action NotificationClicked;
        public event Action MenuMarkReadClicked;
        public event Action MenuMarkUnreadClicked;
        public event Action MenuDeleteClicked;
        public event Action MenuSeeTicketClicked;

        public void BindNotifications(List<UserNotificationRow> rows)
        {
            flow.SuspendLayout();
            try
            {
                flow.Controls.Clear();

                _selectedIds.Clear();
                _focusedId = null;
                UpdateSelectionBar();

                lblCount.Text = $"Notifications ({rows.Count})";

                pnlEmpty.Visible = rows.Count == 0;
                if (rows.Count == 0)
                {
                    flow.Controls.Add(pnlEmpty);
                    return;
                }

                foreach (var n in rows)
                    flow.Controls.Add(CreateCard(n));

                // ✅ IMPORTANT: set padding once here (do not multiply widths)
                flow.Padding = new Padding(6, 6, 26, 6); // right padding gives scrollbar space
            }
            finally
            {
                flow.ResumeLayout(true);
            }

            // ✅ force proper width now
            FixCardsWidth();
        }



        public void SetUnreadCount(int count)
        {
            lblUnread.Text = count <= 0 ? "" : $"Unread: {count}";
        }

        public void ShowInfo(string message)
            => MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public void ShowError(string message)
            => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public bool Confirm(string message)
            => MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;

        public void RequestBadgeRefresh()
        {
            BadgeRefreshRequested?.Invoke();
        }

        // ================== UI HELPERS ==================
        private Control CreateCard(UserNotificationRow n)
        {
            int cardWidth = Math.Max(520, flow.DisplayRectangle.Width - flow.Padding.Horizontal - 16);

            var card = new Guna2ShadowPanel
            {
                BackColor = Color.Transparent,
                FillColor = n.IsRead ? Color.White : Color.FromArgb(248, 249, 251),
                Radius = 14,
                ShadowDepth = 10,
                ShadowColor = Color.Black,

                Width = cardWidth,
                Height = 95,
                Margin = new Padding(8),
                Padding = new Padding(14, 14, 70, 14),
                Tag = n
            };

            var lblTitle = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 33, 45),
                Text = n.Title,
                Location = new Point(16, 10),
                AutoSize = true
            };

            var lblMsg = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(60, 70, 85),
                //   Text = Shorten(n.Message, 100),
                Text = n.Message,
                Location = new Point(16, 38),
                AutoSize = true
            };

            var lblMeta = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(120, 130, 140),
                Text = $"{n.Type} • {n.CreatedAt:yyyy-MM-dd HH:mm}",
                Location = new Point(16, 66),
                AutoSize = true
            };

            var btnMenu = new Guna2Button
            {
                Text = "⋮",
                Font = new Font("Segoe UI Symbol", 14F, FontStyle.Bold),
                FillColor = Color.Transparent,
                ForeColor = Color.FromArgb(60, 70, 85),
                Size = new Size(40, 34),
                BorderRadius = 8
            };

            void PositionMenu()
            {
                int x = card.Width - btnMenu.Width - 10;
                btnMenu.Location = new Point(x, 6);
                btnMenu.BringToFront();
            }

            PositionMenu();
            card.SizeChanged += (_, __) => PositionMenu();

            btnMenu.Click += (_, __) =>
            {
                _focusedId = n.NotificationId;
                ShowCardMenu(btnMenu, n);
            };

            void CardClick(object s, EventArgs e)
            {
                _focusedId = n.NotificationId;

                bool ctrl = (ModifierKeys & Keys.Control) == Keys.Control;

                if (ctrl)
                {
                    ToggleSelect(n.NotificationId, card);   // ✅ multi-select
                    return;
                }

                // normal click: clear selection + open notification
                ClearSelectionUI();
                NotificationClicked?.Invoke();
            }

            // click handlers
            card.Click += CardClick;
            lblTitle.Click += CardClick;
            lblMsg.Click += CardClick;
            lblMeta.Click += CardClick;

            card.Controls.Add(lblTitle);
            card.Controls.Add(lblMsg);
            card.Controls.Add(lblMeta);
            card.Controls.Add(btnMenu);

            // ✅ show selection highlight if already selected
            ApplySelectedVisual(card, _selectedIds.Contains(n.NotificationId));

            return card;
        }







        private void ShowCardMenu(Control anchor, UserNotificationRow n)
        {
            var menu = new ContextMenuStrip();

            // Click = read, so menu only shows Mark as Read when unread
            if (!n.IsRead)
                menu.Items.Add("Mark as read").Click += (_, __) => MenuMarkReadClicked?.Invoke();
            else
                menu.Items.Add("Mark as unread").Click += (_, __) => MenuMarkUnreadClicked?.Invoke();

            menu.Items.Add("Delete").Click += (_, __) => MenuDeleteClicked?.Invoke();

            // "See ticket" only if BookingId exists
            if (n.BookingId.HasValue)
            {
                menu.Items.Add("See ticket").Click += (_, __) =>
                {

                    _focusedId = n.NotificationId;
                    SeeTicketRequested?.Invoke(n.BookingId.Value);
                };
            }

            menu.Show(anchor, new Point(0, anchor.Height));
        }

        // selection bar
        private void ToggleSelect(int notificationId, Control card)
        {
            if (_selectedIds.Contains(notificationId))
                _selectedIds.Remove(notificationId);
            else
                _selectedIds.Add(notificationId);

            ApplySelectedVisual(card, _selectedIds.Contains(notificationId));
            UpdateSelectionBar();
        }

        private void ClearSelectionUI()
        {
            _selectedIds.Clear();
            UpdateSelectionBar();

            foreach (Control c in flow.Controls)
                ApplySelectedVisual(c, false);
        }

        private void ApplySelectedVisual(Control card, bool selected)
        {
            if (card is Guna2ShadowPanel sp && sp.Tag is UserNotificationRow row)
            {
                sp.FillColor =
                    selected ? Color.FromArgb(230, 245, 255)
                    : row.IsRead ? Color.White
                    : Color.FromArgb(248, 249, 251);
            }
        }



        private void UpdateSelectionBar()
        {
            pnlSelection.Visible = _selectedIds.Count > 0;
            lblSelected.Text = _selectedIds.Count > 0 ? $"Selected: {_selectedIds.Count}" : "";
        }

        private static string Shorten(string s, int max)
        {
            s ??= "";
            if (s.Length <= max) return s;
            return s.Substring(0, max - 3) + "...";
        }


        private int GetCardWidth()
        {
            const int CUT_RIGHT = 220;
            const int CUT_LEFT = 10;

            int w = flow.ClientSize.Width - flow.Padding.Horizontal - CUT_LEFT - CUT_RIGHT;

            // keep the “wider cards” rule here ONLY (not in two places)
            w = (int)(w * 1.25);

            if (w < 520) w = 520;
            return w;
        }

        private void FixCardsWidth()
        {
            // DisplayRectangle.Width is better when AutoScroll is ON
            int w = flow.DisplayRectangle.Width - flow.Padding.Horizontal - 16;
            if (w < 520) w = 520;

            foreach (Control c in flow.Controls)
            {
                if (c is Guna2ShadowPanel sp && sp.Tag is UserNotificationRow)
                    sp.Width = w;
            }
        }
        public void SelectAllUI()
        {
            _selectedIds.Clear();

            foreach (Control c in flow.Controls)
            {
                if (c is Guna2ShadowPanel sp && sp.Tag is UserNotificationRow row)
                {
                    _selectedIds.Add(row.NotificationId);
                    ApplySelectedVisual(sp, true);
                }
            }

            UpdateSelectionBar();
        }

        public void ClearSelectionPublic()
        {
            ClearSelectionUI();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblCount_Click(object sender, EventArgs e)
        {

        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }

        private void panelSearch_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}