using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Exceptions;
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

        public MyTicketsBookingHistory()
        {
            InitializeComponent();
            flowTickets.SizeChanged += (_, __) => FixCardsWidth();
            Dock = DockStyle.Fill;
          
            btnRefresh.Click += (_, __) => RefreshClicked?.Invoke();
            btnClear.Click += (_, __) => ClearSearchAndFilter();

            cmbFilter.SelectedIndexChanged += (_, __) => FilterChanged?.Invoke();
            txtSearch.TextChanged += (_, __) => SearchChanged?.Invoke();

            VisibleChanged += (_, __) =>
            {
                if (Visible && _initialized)
                    Activate();
            };
            this.DoubleBuffered = true;
        }

        public void Initialize(INavigationService navigation, IAppSession session)
        {
            if (_initialized) return;

            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));

            var connStr = "server=localhost;port=3306;database=user;user=root;password=2006";
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _repo = new MySqlMyTicketsRepository(connStr);
            _service = new MyTicketsService(_repo);
            _presenter = new MyTicketsPresenter(this, _service);

            _initialized = true;
        }

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

        // ===================== IMyTicketsView =====================
        public int UserId => _session.CurrentUser?.UserID ?? 0; // 0 if no user logged in
        public string Filter => cmbFilter.SelectedItem?.ToString() ?? "All";
        public string SearchText => txtSearch.Text ?? "";
        public int? SelectedBookingId => _selectedBookingId;

        public event Action ViewLoaded;
        public event Action RefreshClicked;
        public event Action CancelClicked;
        public event Action FilterChanged;
        public event Action SearchChanged;

        public void BindTickets(List<MyTicketRow> rows)
        {
            flowTickets.SuspendLayout();
            flowTickets.Visible = false; // ✅ prevents redraw flicker while rebuilding

            try
            {
                flowTickets.Controls.Clear();
                _selectedBookingId = null;

                lblCount.Text = $"Tickets ({rows.Count})";
                pnlEmpty.Visible = rows.Count == 0;

                if (rows.Count == 0)
                {
                    flowTickets.Controls.Add(pnlEmpty);
                    return;
                }

                foreach (var r in rows)
                    flowTickets.Controls.Add(CreateTicketCard(r));

                FixCardsWidth();
                TryFocusBookingCard();
            }
            finally
            {
                flowTickets.Visible = true;
                flowTickets.ResumeLayout(true);

                FixCardsWidth();
                // ForceFlowScrollRecalc();   // ✅ ADD THIS

                flowTickets.Invalidate();
                flowTickets.Update();
            }

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

        // ===================== CARD UI =====================

        private Control CreateTicketCard(MyTicketRow t)
        {
            int cardWidth = flowTickets.ClientSize.Width
               - flowTickets.Padding.Horizontal
               - 35; // scrollbar safety

            if (cardWidth < 900) cardWidth = 900;

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

            card.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

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

            // DETAILS PANEL (collapsed by default)
            var details = BuildDetailsPanel(t, card.Width - 28);
            details.Name = "detailsPanel";
            details.Location = new Point(14, 90);
            details.Visible = false;
            card.Controls.Add(details);

            card.Name = "ticket_" + t.BookingId;

            // CLICK selects + toggles
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

                details.Visible = !expanded;
                card.Height = !expanded ? 250 : 90;

                // ✅ allow shrinking back
                card.MinimumSize = new Size(card.Width, 0);

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
                Size = new Size(details.Width - 40, details.Height - 70), // ✅ less width => more breathing room
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
                // Dock = DockStyle.Fill,
                AutoSize = true,
                Padding = new Padding(0, 0, 0, 3)
            };
        }


        private void PositionRightAligned(Control parent, Control child, int rightPadding, int top)
        {
            child.Location = new Point(
                parent.Width - child.Width - rightPadding,
                top);
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
            foreach (Control c in flowTickets.Controls)
            {
                if (c == pnlEmpty) continue;

                if (c is Guna2ShadowPanel sp)
                    sp.FillColor = Color.White;
            }

            if (selected is Guna2ShadowPanel selectedSp)
                selectedSp.FillColor = Color.FromArgb(248, 249, 251);
        }
        private void FixCardsWidth()
        {
            int w = flowTickets.ClientSize.Width
                    - flowTickets.Padding.Horizontal
                    - 35; // scrollbar safety

            if (w < 900) w = 900;

            foreach (Control c in flowTickets.Controls)
            {
                if (c == pnlEmpty) continue;

                c.Width = w;

                // ✅ DO NOT lock the height
                c.MinimumSize = new Size(w, 0); // or just remove MinimumSize entirely

                var details = c.Controls.Find("detailsPanel", true).FirstOrDefault();
                if (details != null)
                    details.Width = w - 28;
            }
        }

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


    }
}