using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Core.Classes.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Presenter.UserPagesPresenters;
using Airport_Airplane_management_system.View.Interfaces;
using Guna.UI2.WinForms;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    public partial class MyTicketsBookingHistory : UserControl, IMyTicketsView
    {
        private bool _initialized;
        private INavigationService _navigation;
        private IAppSession _session;
        private IMyTicketsRepository _repo;
        private MyTicketsService _service;
        private MyTicketsPresenter _presenter;

        private int? _selectedBookingId;
        private int? _focusBookingId;

        private readonly Dictionary<int, Guna2ShadowPanel> _ticketCards = new Dictionary<int, Guna2ShadowPanel>();

        public event Action BadgeRefreshRequested;

        // Initializes the booking history user control
        public MyTicketsBookingHistory()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;

            typeof(FlowLayoutPanel)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(flowTickets, true, null);

            flowTickets.SizeChanged += (_, __) => FixCardsWidth();

            btnRefresh.Click += (_, __) => RefreshClicked?.Invoke();
            btnClear.Click += (_, __) => ClearSearchAndFilter();

            cmbFilter.SelectedIndexChanged += (_, __) => FilterChanged?.Invoke();
            txtSearch.TextChanged += (_, __) => SearchChanged?.Invoke();

            VisibleChanged += (_, __) =>
            {
                if (Visible && _initialized)
                    Activate();
            };
        }

        // Initializes repositories, services, and presenter
        public void Initialize(INavigationService navigation, IAppSession session)
        {
            if (_initialized) return;

            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
            _session = session ?? throw new ArgumentNullException(nameof(session));

            var connStr = "server=localhost;port=3306;database=user;user=root;password=2006";
            _repo = new MySqlMyTicketsRepository(connStr);
            _service = new MyTicketsService(_repo);

            var notifRepo = new MySqlNotificationWriterRepository(connStr);
            var notifWriter = new NotificationWriterService(notifRepo);

            _presenter = new MyTicketsPresenter(this, session);

            _initialized = true;
        }

        // Activates the view by raising the ViewLoaded event
        public void Activate()
        {
            if (!_initialized) return;
            ViewLoaded?.Invoke();
        }

        private void ClearSearchAndFilter()
        {
            if (cmbFilter.Items.Count > 0)
                cmbFilter.SelectedIndex = 0;

            txtSearch.Text = "";
        }

        private IAppSession Session => _session ?? throw new InvalidOperationException("Session is not initialized");
        public int UserId => Session.CurrentUser?.UserID ?? 0;
        public string Filter => cmbFilter.SelectedItem?.ToString() ?? "All";
        public string SearchText => txtSearch.Text ?? "";
        public int? SelectedBookingId => _selectedBookingId;

        public event Action ViewLoaded;
        public event Action RefreshClicked;
        public event Action CancelClicked;
        public event Action FilterChanged;
        public event Action SearchChanged;

        // Binds ticket data to the flow layout panel
        public void BindTickets(List<MyTicketRow> rows)
        {
            rows ??= new List<MyTicketRow>();
            flowTickets.SuspendLayout();

            foreach (var card in _ticketCards.Values)
                card.Visible = false;

            foreach (var t in rows)
            {
                Guna2ShadowPanel card;
                if (_ticketCards.ContainsKey(t.BookingId))
                {
                    card = _ticketCards[t.BookingId];
                    card.Visible = true;
                    UpdateCard(card, t);
                }
                else
                {
                    card = CreateTicketCard(t);
                    _ticketCards[t.BookingId] = card;
                    flowTickets.Controls.Add(card);
                }
            }

            FixCardsWidth();
            TryFocusBookingCard();
            flowTickets.ResumeLayout(true);

            lblCount.Text = $"Tickets ({rows.Count})";
        }

        private void UpdateCard(Guna2ShadowPanel card, MyTicketRow t)
        {
            foreach (var lbl in card.Controls.OfType<Guna2HtmlLabel>())
            {
                if (lbl.Text.Contains("→")) lbl.Text = $"{t.FromCity} → {t.ToCity}";
                else if (lbl.Text.StartsWith("Passenger:")) lbl.Text = $"Passenger: {t.PassengerName}";
                else if (lbl.Text.Contains("Booking ID:")) lbl.Text = $"Booking ID: {t.BookingId}";
            }

            var details = card.Controls.Find("detailsPanel", true).FirstOrDefault();
            if (details != null)
            {
                var btnCancel = details.Controls.OfType<Guna2Button>().FirstOrDefault();
                if (btnCancel != null)
                    btnCancel.Enabled = !string.Equals(t.Status, "Cancelled", StringComparison.OrdinalIgnoreCase);
            }

            var lblStatus = card.Controls.OfType<Guna2HtmlLabel>().FirstOrDefault(l => l.Text == t.Status || l.Text.Contains("Confirmed") || l.Text.Contains("Cancelled") || l.Text.Contains("Pending"));
            if (lblStatus != null)
                ApplyStatusStyle(lblStatus, t.Status);
        }

        public bool Confirm(string message)
        {
            return MessageBox.Show(
                message,
                "Confirm",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;
        }

        public void ShowInfo(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Creates a ticket card for a single ticket row
        private Guna2ShadowPanel CreateTicketCard(MyTicketRow t)
        {
            int cardWidth = Math.Max(flowTickets.ClientSize.Width - flowTickets.Padding.Horizontal - 35, 900);

            var card = new Guna2ShadowPanel
            {
                BackColor = Color.Transparent,
                FillColor = Color.White,
                Radius = 14,
                ShadowColor = Color.Black,
                ShadowDepth = 12,
                Width = cardWidth,
                Height = 90,
                Margin = new Padding(8),
                Padding = new Padding(14),
                Tag = false
            };

            var lblRoute = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 33, 45),
                Text = $"{t.FromCity} → {t.ToCity}",
                Location = new Point(16, 12),
                AutoSize = true
            };

            var lblPassenger = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(60, 70, 85),
                Text = $"Passenger: {t.PassengerName}",
                Location = new Point(16, 45),
                AutoSize = true
            };

            var lblMetaRight = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 70, 85),
                Text = $"{t.Departure:yyyy-MM-dd HH:mm}",
                AutoSize = true
            };

            var lblStatus = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Text = t.Status,
                AutoSize = true
            };

            var lblPrice = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 33, 45),
                Text = $"{t.Price:N2}",
                AutoSize = true
            };

            card.Controls.Add(lblRoute);
            card.Controls.Add(lblPassenger);
            card.Controls.Add(lblMetaRight);
            card.Controls.Add(lblStatus);
            card.Controls.Add(lblPrice);

            PositionRightAligned(card, lblMetaRight, 16, 12);
            PositionRightAligned(card, lblStatus, 16, 42);
            PositionRightAligned(card, lblPrice, 16, 64);

            ApplyStatusStyle(lblStatus, t.Status);

            card.Name = "ticket_" + t.BookingId;

            void SelectCard()
            {
                _selectedBookingId = t.BookingId;
                HighlightSelectedCard(card);
            }

            void Toggle()
            {
                bool expanded = (bool)card.Tag;
                card.Tag = !expanded;

                flowTickets.SuspendLayout();

                var details = card.Controls.Find("detailsPanel", true).FirstOrDefault();
                if (details == null)
                {
                    details = BuildDetailsPanel(t, card.Width - 28);
                    details.Name = "detailsPanel";
                    details.Location = new Point(14, 90);
                    card.Controls.Add(details);
                }

                details.Visible = !expanded;
                card.Height = !expanded ? 250 : 90;

                flowTickets.ResumeLayout(true);
                FixCardsWidth();
                flowTickets.ScrollControlIntoView(details);
            }

            void ClickAny(object s, EventArgs e)
            {
                SelectCard();
                Toggle();
            }

            card.Click += ClickAny;
            foreach (Control c in card.Controls)
                c.Click += ClickAny;

            return card;
        }

        private Guna2Panel BuildDetailsPanel(MyTicketRow t, int width)
        {
            var details = new Guna2Panel
            {
                Width = width,
                Height = 150,
                FillColor = Color.FromArgb(248, 249, 251),
                BorderRadius = 12
            };

            var grid = new TableLayoutPanel
            {
                Location = new Point(8, 12),
                Size = new Size(details.Width - 40, details.Height - 70),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                ColumnCount = 2,
                RowCount = 3,
                BackColor = Color.Transparent
            };

            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));
            grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));

            grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
            grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
            grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));

            grid.Controls.Add(MakeDetailLabel($"Booking ID: {t.BookingId}", 0, 0), 0, 0);
            grid.Controls.Add(MakeDetailLabel($"Flight ID: {t.FlightId}", 0, 0), 1, 0);

            grid.Controls.Add(MakeDetailLabel($"Seat: {t.SeatNumber}", 0, 0), 0, 1);
            grid.Controls.Add(MakeDetailLabel($"Category: {t.Category}", 0, 0), 1, 1);

            grid.Controls.Add(MakeDetailLabel($"Departure: {t.Departure:yyyy-MM-dd HH:mm}", 0, 0), 0, 2);
            grid.Controls.Add(MakeDetailLabel($"Arrival: {t.Arrival:yyyy-MM-dd HH:mm}", 0, 0), 1, 2);

            details.Controls.Add(grid);

            var btnCancel = new Guna2Button
            {
                Text = "Cancel Ticket",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.White,
                FillColor = Color.FromArgb(30, 59, 89),
                BorderRadius = 10,
                Size = new Size(160, 40),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(details.Width - 175, details.Height - 52),
                Enabled = !string.Equals(t.Status, "Cancelled", StringComparison.OrdinalIgnoreCase)
            };

            btnCancel.Click += (_, __) =>
            {
                _selectedBookingId = t.BookingId;
                CancelClicked?.Invoke();
            };

            details.Controls.Add(btnCancel);

            details.SizeChanged += (_, __) =>
            {
                grid.Width = details.Width - 28;
                btnCancel.Location = new Point(details.Width - 190, details.Height - 52);
            };

            return details;
        }

        private Guna2HtmlLabel MakeDetailLabel(string text, int x, int y)
        {
            return new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(60, 70, 85),
                Text = text,
                AutoSize = true,
                Padding = new Padding(0, 0, 0, 3)
            };
        }

        private void PositionRightAligned(Control parent, Control child, int rightPadding, int top)
        {
            void Reposition()
            {
                child.Location = new Point(parent.Width - child.Width - rightPadding, top);
            }

            Reposition();
            parent.SizeChanged += (_, __) => Reposition();
        }

        private void ApplyStatusStyle(Guna2HtmlLabel lbl, string status)
        {
            if (string.Equals(status, "Cancelled", StringComparison.OrdinalIgnoreCase))
                lbl.ForeColor = Color.Gray;
            else if (string.Equals(status, "Confirmed", StringComparison.OrdinalIgnoreCase))
                lbl.ForeColor = Color.SeaGreen;
            else if (string.Equals(status, "Pending", StringComparison.OrdinalIgnoreCase))
                lbl.ForeColor = Color.DarkOrange;
            else
                lbl.ForeColor = Color.FromArgb(60, 70, 85);
        }

        private void HighlightSelectedCard(Control selected)
        {
            foreach (var c in flowTickets.Controls.OfType<Guna2ShadowPanel>())
                c.FillColor = Color.White;

            if (selected is Guna2ShadowPanel selectedSp)
                selectedSp.FillColor = Color.FromArgb(248, 249, 251);
        }

        private void FixCardsWidth()
        {
            int w = Math.Max(flowTickets.ClientSize.Width - flowTickets.Padding.Horizontal - 35, 900);

            foreach (var c in flowTickets.Controls.OfType<Guna2ShadowPanel>().Where(x => x.Visible))
            {
                c.Width = w;

                var details = c.Controls.Find("detailsPanel", true).FirstOrDefault();
                if (details != null)
                    details.Width = w - 28;

            }
        }

        // Focuses a ticket by booking ID
        public void FocusBooking(int bookingId)
        {
            _focusBookingId = bookingId;
            TryFocusBookingCard();
        }

        private void TryFocusBookingCard()
        {
            if (_focusBookingId == null) return;

            string targetName = "ticket_" + _focusBookingId.Value;

            var target = flowTickets.Controls
                .OfType<Control>()
                .FirstOrDefault(c => c.Name == targetName);

            if (target != null)
            {
                flowTickets.ScrollControlIntoView(target);
                HighlightSelectedCard(target);
            }

            _focusBookingId = null;
        }

        // Requests badge refresh via event
        public void RequestBadgeRefresh()
        {
            BadgeRefreshRequested?.Invoke();
        }

        private void flowTickets_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
