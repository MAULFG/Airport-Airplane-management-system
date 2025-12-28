using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    public partial class PassengerMangement : UserControl, IPassengerManagementView
    {
        // ===== MVP Events (View -> Presenter) =====
        public event Action ViewLoaded;
        public event Action<string> SearchChanged;

        // Click card (or chevron) to expand (Presenter should load bookings)
        public event Action<int> PassengerToggleRequested; // passengerId

        // Cancel booking from upcoming list
        public event Action<int, int> CancelBookingRequested; // bookingId, passengerId

        private PassengerCard _expandedCard;

        public PassengerMangement()
        {
            InitializeComponent();

            // txtSearch/listPanel exist in Designer
            txtSearch.TextChanged += (s, e) => SearchChanged?.Invoke(txtSearch.Text);

            listPanel.Resize += (s, e) =>
            {
                foreach (Control c in listPanel.Controls)
                    c.Width = listPanel.ClientSize.Width - 25;
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
                ViewLoaded?.Invoke();
        }

        // ===== Presenter -> View =====

        public void SetHeaderCounts(int totalPassengers, int upcomingFlightsNotFullyBooked)
        {
            lblTotalPassengersValue.Text = totalPassengers.ToString();
            lblUpcomingFlightsValue.Text = upcomingFlightsNotFullyBooked.ToString();
        }

        public void RenderPassengers(List<PassengerSummaryRow> passengers)
        {
            passengers ??= new List<PassengerSummaryRow>();

            listPanel.SuspendLayout();
            listPanel.Controls.Clear();
            _expandedCard = null;

            foreach (var p in passengers)
            {
                var card = new PassengerCard(p);
                card.Width = listPanel.ClientSize.Width - 25;

                card.ToggleRequested += passengerId =>
                {
                    // collapse previous expanded
                    if (_expandedCard != null && _expandedCard != card)
                        _expandedCard.Collapse();

                    // toggle current
                    if (card.IsExpanded)
                    {
                        card.Collapse();
                        _expandedCard = null;
                        return;
                    }

                    // Expand skeleton now (shows headers + loading)
                    card.ExpandLoading();
                    _expandedCard = card;

                    // Ask presenter to load bookings for this passenger
                    PassengerToggleRequested?.Invoke(passengerId);
                };

                card.CancelRequested += (bookingId, passengerId) =>
                {
                    CancelBookingRequested?.Invoke(bookingId, passengerId);
                };

                listPanel.Controls.Add(card);
            }

            listPanel.ResumeLayout();
        }

        // Presenter calls this after it loads bookings from DB
        public void ShowPassengerBookings(int passengerId, List<PassengerBookingRow> upcoming, List<PassengerBookingRow> past)
        {
            upcoming ??= new List<PassengerBookingRow>();
            past ??= new List<PassengerBookingRow>();

            var card = listPanel.Controls.OfType<PassengerCard>()
                .FirstOrDefault(c => c.Passenger.PassengerId == passengerId);

            if (card == null) return;

            card.SetBookings(upcoming, past);
            card.Expand(); // ensure expanded after data loaded
        }

        // ========================= Passenger Card =========================
        private class PassengerCard : Guna2Panel
        {
            public PassengerSummaryRow Passenger { get; }
            public bool IsExpanded { get; private set; }

            public event Action<int> ToggleRequested;               // passengerId
            public event Action<int, int> CancelRequested;          // bookingId, passengerId

            private readonly Label _lblName;
            private readonly Label _lblEmail;
            private readonly Label _lblPhone;
            private readonly Label _lblMember;
            private readonly Guna2Button _btnChevron;

            private readonly Guna2Panel _expandArea;

            private readonly Label _lblUpcomingTitle;
            private readonly Label _lblPastTitle;
            private readonly Label _lblLoading;

            private readonly FlowLayoutPanel _upcomingList;
            private readonly FlowLayoutPanel _pastList;

            private readonly Label _lblUpcomingEmpty;
            private readonly Label _lblPastEmpty;

            public PassengerCard(PassengerSummaryRow p)
            {
                Passenger = p;

                BorderRadius = 16;
                BorderThickness = 1;
                BorderColor = Color.FromArgb(235, 235, 235);
                FillColor = Color.White;

                Height = 92;
                Margin = new Padding(0, 0, 0, 14);
                Padding = new Padding(18, 16, 18, 16);

                // Avatar circle
                // Avatar PictureBox
                var picAvatar = new PictureBox
                {
                    Size = new Size(48, 48),
                    Location = new Point(18, 18),
                    SizeMode = PictureBoxSizeMode.Zoom,
                    BackColor = Color.FromArgb(245, 245, 245),
                    Image = Properties.Resources.user_48px, // <-- your PNG resource
                };

                // Make avatar circular
                picAvatar.Paint += (s, e) =>
                {
                    using var gp = new System.Drawing.Drawing2D.GraphicsPath();
                    gp.AddEllipse(0, 0, picAvatar.Width - 1, picAvatar.Height - 1);
                    picAvatar.Region = new Region(gp);

                    e.Graphics.SmoothingMode =
                        System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    using var pen = new Pen(Color.FromArgb(220, 220, 220), 1);
                    e.Graphics.DrawEllipse(
                        pen, 0, 0, picAvatar.Width - 1, picAvatar.Height - 1);
                };

                // ADD TO THE CARD (this)
                Controls.Add(picAvatar);

                // Name
                _lblName = new Label
                {
                    AutoSize = true,
                    Text = Passenger.FullName ?? "—",
                    Font = new Font("Segoe UI", 12.5f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(30, 30, 30),
                    Location = new Point(90, 18)
                };
                Controls.Add(_lblName);

                // Email
                _lblEmail = new Label
                {
                    AutoSize = true,
                    Text = string.IsNullOrWhiteSpace(Passenger.Email) ? "—" : Passenger.Email,
                    Font = new Font("Segoe UI", 10f),
                    ForeColor = Color.FromArgb(110, 110, 110),
                    Location = new Point(90, 46)
                };
                Controls.Add(_lblEmail);

                // Phone (icon + value)
                _lblPhone = new Label
                {
                    AutoSize = true,
                    Text = "📞  " + (string.IsNullOrWhiteSpace(Passenger.Phone) ? "—" : Passenger.Phone),
                    Font = new Font("Segoe UI", 10f),
                    ForeColor = Color.FromArgb(90, 90, 90),
                    Location = new Point(340, 46)
                };
                Controls.Add(_lblPhone);

                // Member since
                _lblMember = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10f),
                    ForeColor = Color.FromArgb(90, 90, 90),
                    Location = new Point(520, 46)
                };
                Controls.Add(_lblMember);
                UpdateMemberLabel();

                // Counts table (Upcoming/Past/Total)
                var counts = BuildCounts(Passenger.UpcomingCount, Passenger.PastCount, Passenger.TotalCount);
                counts.Location = new Point(Width - 290, 20);
                counts.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                Controls.Add(counts);

                // Chevron
                _btnChevron = new Guna2Button
                {
                    Size = new Size(34, 34),
                    BorderRadius = 10,
                    FillColor = Color.Transparent,
                    ForeColor = Color.FromArgb(80, 80, 80),
                    Text = "˅",
                    Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    Cursor = Cursors.Hand
                };
                _btnChevron.Click += (s, e) => ToggleRequested?.Invoke(Passenger.PassengerId);
                Controls.Add(_btnChevron);

                // Expand area
                _expandArea = new Guna2Panel
                {
                    BorderRadius = 12,
                    FillColor = Color.White,
                    Visible = false
                };
                Controls.Add(_expandArea);

                _lblUpcomingTitle = new Label
                {
                    AutoSize = true,
                    Text = "Upcoming Flights (0)",
                    Font = new Font("Segoe UI", 11.5f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(40, 40, 40),
                    Location = new Point(0, 10)
                };
                _expandArea.Controls.Add(_lblUpcomingTitle);

                _lblLoading = new Label
                {
                    AutoSize = true,
                    Text = "Loading bookings...",
                    Font = new Font("Segoe UI", 10f),
                    ForeColor = Color.FromArgb(140, 140, 140),
                    Location = new Point(0, 45),
                    Visible = false
                };
                _expandArea.Controls.Add(_lblLoading);

                _upcomingList = BuildList();
                _expandArea.Controls.Add(_upcomingList);

                _lblUpcomingEmpty = BuildEmpty("No upcoming flights.");
                _expandArea.Controls.Add(_lblUpcomingEmpty);

                _lblPastTitle = new Label
                {
                    AutoSize = true,
                    Text = "Past Flights (0)",
                    Font = new Font("Segoe UI", 11.5f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(40, 40, 40),
                    Visible = false
                };
                _expandArea.Controls.Add(_lblPastTitle);

                _pastList = BuildList();
                _pastList.Visible = false;
                _expandArea.Controls.Add(_pastList);

                _lblPastEmpty = BuildEmpty("No past flights.");
                _lblPastEmpty.Visible = false;
                _expandArea.Controls.Add(_lblPastEmpty);

                // ===== Make the whole card clickable to toggle =====
                Action toggle = () => ToggleRequested?.Invoke(Passenger.PassengerId);

                // Click anywhere on the card (except inside expand area controls)
                MakeClickable(this, toggle);
                MakeNonToggle(_expandArea);

                // Chevron remains clickable
                _btnChevron.Cursor = Cursors.Hand;

                // layout on resize
                Resize += (s, e) =>
                {
                    counts.Location = new Point(Width - 290, 20);
                    _btnChevron.Location = new Point(Width - 52, 27);

                    _expandArea.Width = Width - 2 * Padding.Left;
                    _expandArea.Location = new Point(Padding.Left, 92);

                    foreach (Control c in _upcomingList.Controls) c.Width = _expandArea.Width;
                    foreach (Control c in _pastList.Controls) c.Width = _expandArea.Width;

                    UpdateMemberLabel();
                    if (IsExpanded) ReLayoutExpanded();
                };

                counts.Location = new Point(Width - 290, 20);
                _btnChevron.Location = new Point(Width - 52, 27);
                _expandArea.Width = Width - 2 * Padding.Left;
                _expandArea.Location = new Point(Padding.Left, 92);

                Collapse();
            }

            // ================== CLICK HELPERS (FIX CS0120) ==================
            private static void MakeClickable(Control root, Action onClick)
            {
                if (root == null) return;

                root.Cursor = Cursors.Hand;
                root.Click += (s, e) => onClick?.Invoke();

                foreach (Control child in root.Controls)
                {
                    // keep buttons textbox etc working (but still allow click)
                    MakeClickable(child, onClick);
                }
            }

            // Prevent clicks inside expand area from collapsing the card
            private static void MakeNonToggle(Control root)
            {
                if (root == null) return;

                root.Cursor = Cursors.Default;
                root.Click += (s, e) =>
                {
                    // stop bubbling (WinForms doesn't truly bubble, but prevents our added handlers on children)
                };

                foreach (Control child in root.Controls)
                {
                    child.Cursor = Cursors.Default;
                    // remove hand feel in expand area
                    MakeNonToggle(child);
                }
            }

            // ================== MEMBER SINCE ==================
            private void UpdateMemberLabel()
            {
                bool compact = Width < 1050;

                DateTime? d = Passenger.CreatedAt; // <-- ONLY MemberSince (no CreatedAt)
                string text = d.HasValue
                    ? d.Value.ToString(compact ? "yyyy" : "MMM dd, yyyy")
                    : "—";

                _lblMember.Text = "🗓  Member since " + text;
            }

            // ================== EXPAND/COLLAPSE ==================
            public void ExpandLoading()
            {
                IsExpanded = true;
                _btnChevron.Text = "˄";
                _expandArea.Visible = true;

                _lblLoading.Visible = true;

                // show placeholders while waiting
                _lblUpcomingTitle.Text = "Upcoming Flights (0)";
                _lblPastTitle.Text = "Past Flights (0)";

                _upcomingList.Controls.Clear();
                _pastList.Controls.Clear();

                _upcomingList.Visible = false;
                _pastList.Visible = false;

                _lblUpcomingEmpty.Visible = false;
                _lblPastEmpty.Visible = false;

                ReLayoutExpanded();
            }

            public void SetBookings(List<PassengerBookingRow> upcoming, List<PassengerBookingRow> past)
            {
                upcoming ??= new List<PassengerBookingRow>();
                past ??= new List<PassengerBookingRow>();

                _lblLoading.Visible = false;

                _lblUpcomingTitle.Text = $"Upcoming Flights ({upcoming.Count})";
                _lblPastTitle.Text = $"Past Flights ({past.Count})";

                _upcomingList.SuspendLayout();
                _pastList.SuspendLayout();

                _upcomingList.Controls.Clear();
                _pastList.Controls.Clear();

                // Upcoming cards (with cancel)
                foreach (var b in upcoming)
                {
                    var card = new BookingCard(b, true);
                    card.Width = _expandArea.Width;
                    card.CancelRequested += bookingId => CancelRequested?.Invoke(bookingId, Passenger.PassengerId);
                    _upcomingList.Controls.Add(card);
                }

                // Past cards
                foreach (var b in past)
                {
                    var card = new BookingCard(b, false);
                    card.Width = _expandArea.Width;
                    _pastList.Controls.Add(card);
                }

                // Visibility logic
                if (upcoming.Count == 0)
                {
                    _upcomingList.Visible = false;
                    _lblUpcomingEmpty.Visible = true;
                }
                else
                {
                    _lblUpcomingEmpty.Visible = false;
                    _upcomingList.Visible = true;
                }

                _lblPastTitle.Visible = true;

                if (past.Count == 0)
                {
                    _pastList.Visible = false;
                    _lblPastEmpty.Visible = true;
                }
                else
                {
                    _lblPastEmpty.Visible = false;
                    _pastList.Visible = true;
                }

                _upcomingList.ResumeLayout();
                _pastList.ResumeLayout();

                ReLayoutExpanded();
            }

            public void Expand()
            {
                if (IsExpanded) { ReLayoutExpanded(); return; }

                IsExpanded = true;
                _btnChevron.Text = "˄";
                _expandArea.Visible = true;
                ReLayoutExpanded();
            }

            public void Collapse()
            {
                IsExpanded = false;
                _btnChevron.Text = "˅";
                _expandArea.Visible = false;
                Height = 92;
            }

            private void ReLayoutExpanded()
            {
                int y = 10;

                _lblUpcomingTitle.Location = new Point(0, y);
                y = _lblUpcomingTitle.Bottom + 12;

                if (_lblLoading.Visible)
                {
                    _lblLoading.Location = new Point(0, y);
                    y = _lblLoading.Bottom + 10;

                    _upcomingList.Visible = false;
                    _pastList.Visible = false;
                    _lblUpcomingEmpty.Visible = false;
                    _lblPastEmpty.Visible = false;
                    _lblPastTitle.Visible = false;
                }
                else
                {
                    // Upcoming
                    if (_upcomingList.Visible)
                    {
                        _upcomingList.Location = new Point(0, y);
                        y = _upcomingList.Bottom + 16;
                    }
                    else if (_lblUpcomingEmpty.Visible)
                    {
                        _lblUpcomingEmpty.Location = new Point(0, y);
                        y = _lblUpcomingEmpty.Bottom + 16;
                    }

                    // Past title
                    _lblPastTitle.Location = new Point(0, y);
                    y = _lblPastTitle.Bottom + 12;

                    if (_pastList.Visible)
                    {
                        _pastList.Location = new Point(0, y);
                        y = _pastList.Bottom + 6;
                    }
                    else if (_lblPastEmpty.Visible)
                    {
                        _lblPastEmpty.Location = new Point(0, y);
                        y = _lblPastEmpty.Bottom + 6;
                    }
                }

                int h = y + 10;
                _expandArea.Height = h;

                Height = 92 + h + 14;
            }

            // ================== UI HELPERS ==================
            private static FlowLayoutPanel BuildList()
            {
                return new FlowLayoutPanel
                {
                    AutoSize = true,
                    WrapContents = false,
                    FlowDirection = FlowDirection.TopDown,
                    BackColor = Color.Transparent,
                    Location = new Point(0, 45),
                    Margin = new Padding(0),
                    Padding = new Padding(0)
                };
            }

            private static Label BuildEmpty(string text)
            {
                return new Label
                {
                    AutoSize = true,
                    Text = text,
                    Font = new Font("Segoe UI", 10f),
                    ForeColor = Color.FromArgb(140, 140, 140)
                };
            }

            private static TableLayoutPanel BuildCounts(int upcoming, int past, int total)
            {
                var t = new TableLayoutPanel
                {
                    ColumnCount = 3,
                    RowCount = 2,
                    Size = new Size(210, 52),
                    BackColor = Color.Transparent
                };
                t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
                t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
                t.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
                t.RowStyles.Add(new RowStyle(SizeType.Absolute, 18));
                t.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

                Label H(string s) => new Label
                {
                    Text = s,
                    Font = new Font("Segoe UI", 8.5f),
                    ForeColor = Color.FromArgb(140, 140, 140),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label V(string s, Color c) => new Label
                {
                    Text = s,
                    Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                    ForeColor = c,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                t.Controls.Add(H("Upcoming"), 0, 0);
                t.Controls.Add(H("Past"), 1, 0);
                t.Controls.Add(H("Total"), 2, 0);

                t.Controls.Add(V(upcoming.ToString(), Color.FromArgb(30, 110, 255)), 0, 1);
                t.Controls.Add(V(past.ToString(), Color.FromArgb(220, 60, 60)), 1, 1);
                t.Controls.Add(V(total.ToString(), Color.FromArgb(35, 35, 35)), 2, 1);

                return t;
            }

            private static Image BuildAvatarCircle()
            {
                Bitmap bmp = new Bitmap(54, 54);
                using (var g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.Clear(Color.Transparent);
                    using var br = new SolidBrush(Color.FromArgb(245, 245, 245));
                    g.FillEllipse(br, 0, 0, 54, 54);
                    using var pen = new Pen(Color.FromArgb(220, 220, 220), 2);
                    g.DrawEllipse(pen, 1, 1, 52, 52);
                }
                return bmp;
            }
        }

        // ========================= Booking Card =========================
        private class BookingCard : Guna2Panel
        {
            private readonly PassengerBookingRow _b;
            private readonly bool _isUpcoming;

            public event Action<int> CancelRequested;

            public BookingCard(PassengerBookingRow b, bool isUpcoming)
            {
                _b = b;
                _isUpcoming = isUpcoming;

                BorderRadius = 12;
                BorderThickness = 1;
                BorderColor = Color.FromArgb(235, 235, 235);
                FillColor = Color.White;

                Height = 135;
                Margin = new Padding(0, 0, 0, 12);
                Padding = new Padding(14, 12, 14, 12);

                Build();
            }

            private void Build()
            {
                var grid = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 4,
                    RowCount = 3,
                    BackColor = Color.Transparent,
                    Margin = new Padding(0),
                    Padding = new Padding(0)
                };

                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280));
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280));
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));

                grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
                grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
                grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));

                Controls.Add(grid);

                // Top left: Flight ID + badge
                var topLeft = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = false,
                    BackColor = Color.Transparent,
                    Margin = new Padding(0),
                    Padding = new Padding(0)
                };

                topLeft.Controls.Add(new Label
                {
                    AutoSize = true,
                    Text = "✈",
                    Font = new Font("Segoe UI Symbol", 16f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(30, 110, 255),
                    Margin = new Padding(0, 0, 8, 0)
                });

                topLeft.Controls.Add(new Label
                {
                    AutoSize = true,
                    Text = "Flight ID ",
                    Font = new Font("Segoe UI", 10f),
                    ForeColor = Color.FromArgb(140, 140, 140),
                    Margin = new Padding(0, 6, 4, 0)
                });

                topLeft.Controls.Add(new Label
                {
                    AutoSize = true,
                    Text = _b.FlightId.ToString(),
                    Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(35, 35, 35),
                    Margin = new Padding(0, 4, 10, 0)
                });

                var badge = new Label
                {
                    AutoSize = true,
                    Text = _isUpcoming ? "UPCOMING" : "PAST",
                    Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                    ForeColor = _isUpcoming ? Color.FromArgb(30, 110, 255) : Color.FromArgb(220, 60, 60),
                    BackColor = Color.FromArgb(245, 245, 245),
                    Padding = new Padding(8, 4, 8, 4),
                    Margin = new Padding(0, 4, 0, 0)
                };
                topLeft.Controls.Add(badge);

                grid.Controls.Add(topLeft, 0, 0);
                grid.SetColumnSpan(topLeft, 2);

                // Cancel button (only upcoming)
                if (_isUpcoming)
                {
                    var btnCancel = new Guna2Button
                    {
                        Text = "Cancel Booking",
                        BorderRadius = 10,
                        FillColor = Color.FromArgb(255, 240, 240),
                        ForeColor = Color.FromArgb(220, 60, 60),
                        Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                        Dock = DockStyle.Fill,
                        Cursor = Cursors.Hand
                    };
                    btnCancel.Click += (s, e) => CancelRequested?.Invoke(_b.BookingId);
                    grid.Controls.Add(btnCancel, 3, 0);
                    grid.SetRowSpan(btnCancel, 1);
                }

                // From/To
                grid.Controls.Add(MakeBlock("From", _b.FromCity), 0, 1);
                grid.Controls.Add(MakeBlock("To", _b.ToCity), 1, 1);

                // Date/Time
                grid.Controls.Add(
    MakeTwoBlock(
        "Departure", _b.Departure.ToString("MMM dd, yyyy  HH:mm"),
        "Arrival", _b.Arrival.ToString("MMM dd, yyyy  HH:mm")
    ),
    2, 1
);
                grid.SetColumnSpan(grid.GetControlFromPosition(2, 1), 2);


                // Bottom row
                grid.Controls.Add(MakeMini("💺", $"Seat {(_b.SeatNumber ?? "—")}"), 0, 2);
                grid.Controls.Add(MakeMini("🎟", $"{(_b.Category ?? "—")}"), 1, 2);
                grid.Controls.Add(MakeMini("📌", $"{(_b.Status ?? "—")}"), 2, 2);
            }

            private Control MakeBlock(string title, string value)
            {
                var p = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };

                var t = new Label
                {
                    AutoSize = true,
                    Text = title,
                    Font = new Font("Segoe UI", 9.5f),
                    ForeColor = Color.FromArgb(120, 120, 120),
                    Location = new Point(0, 0)
                };

                var v = new Label
                {
                    AutoSize = true,
                    Text = string.IsNullOrWhiteSpace(value) ? "—" : value,
                    Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(35, 35, 35),
                    Location = new Point(0, 18)
                };

                p.Controls.Add(t);
                p.Controls.Add(v);
                return p;
            }
            private Control MakeTwoBlock(string title1, string value1, string title2, string value2)
            {
                var wrap = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 1,
                    BackColor = Color.Transparent,
                    Margin = new Padding(0),
                    Padding = new Padding(0)
                };

                wrap.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
                wrap.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

                wrap.Controls.Add(MakeBlock(title1, value1), 0, 0);
                wrap.Controls.Add(MakeBlock(title2, value2), 1, 0);

                return wrap;
            }

            private Control MakeMini(string icon, string text)
            {
                return new Label
                {
                    Dock = DockStyle.Fill,
                    Text = icon + "  " + text,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Segoe UI", 10f),
                    ForeColor = Color.FromArgb(90, 90, 90),
                    AutoSize = false
                };
            }
        }

        // (keep designer click handlers if they exist)
        private void lblTotalPassengersText_Click(object sender, EventArgs e) { }
        private void lblTotalPassengersValue_Click(object sender, EventArgs e) { }
        private void lblUpcomingFlightsText_Click(object sender, EventArgs e) { }
        private void lblSub_Click(object sender, EventArgs e) { }
        private void lblUpcomingFlightsValue_Click(object sender, EventArgs e) { }
        public bool Confirm(string title, string message)
        {
            return MessageBox.Show(
                message,
                title,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            ) == DialogResult.Yes;
        }

        public void ShowError(string message)
        {
            MessageBox.Show(
                message,
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error
            );
        }

        public void ShowInfo(string message)
        {
            MessageBox.Show(
                message,
                "Info",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

    }
}
