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

        // Click chevron to expand (Presenter should load bookings)
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
                    // UI expand/collapse immediate
                    if (_expandedCard != null && _expandedCard != card)
                        _expandedCard.Collapse();

                    if (card.IsExpanded)
                    {
                        card.Collapse();
                        _expandedCard = null;
                        return;
                    }

                    // Expand skeleton now (shows section headers + loading)
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

            // Fill bookings
            card.SetBookings(upcoming, past);

            // keep only one expanded
            if (_expandedCard != null && _expandedCard != card)
                _expandedCard.Collapse();

            _expandedCard = card;
            card.Expand(); // finalize expand with correct height
        }

        public bool Confirm(string message, string title)
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowInfo(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // keep designer handlers safe
        private void lblUpcomingFlightsValue_Click(object sender, EventArgs e) { }
        private void lblUpcomingFlightsText_Click(object sender, EventArgs e) { }
        private void lblTotalPassengersValue_Click(object sender, EventArgs e) { }
        private void lblTotalPassengersText_Click(object sender, EventArgs e) { }
        private void lblSub_Click(object sender, EventArgs e) { }

        // =====================================================================
        // Passenger Card (inside same file, as you want)
        // =====================================================================
        private class PassengerCard : Guna2Panel
        {
            public PassengerSummaryRow Passenger { get; }
            public bool IsExpanded { get; private set; }

            public event Action<int> ToggleRequested;
            public event Action<int, int> CancelRequested;

            private readonly Guna2Button _btnChevron;
            private readonly Label _lblMember;

            private readonly Guna2Panel _expandArea;

            private readonly Label _lblUpcomingTitle;
            private readonly FlowLayoutPanel _upcomingList;
            private readonly Label _lblUpcomingEmpty;

            private readonly Label _lblPastTitle;
            private readonly FlowLayoutPanel _pastList;
            private readonly Label _lblPastEmpty;

            private readonly Label _lblLoading;

            public PassengerCard(PassengerSummaryRow p)
            {
                Passenger = p;

                Height = 92;
                Margin = new Padding(0, 0, 0, 10);

                BorderRadius = 14;
                BorderThickness = 1;
                BorderColor = Color.FromArgb(230, 230, 230);
                FillColor = Color.White;
                Padding = new Padding(18, 14, 18, 14);

                // Avatar
                var avatar = new Guna2CirclePictureBox
                {
                    Size = new Size(54, 54),
                    Location = new Point(18, 18),
                    Image = BuildAvatarCircle(),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    BackColor = Color.Transparent
                };
                Controls.Add(avatar);

                var avatarIcon = new Label
                {
                    AutoSize = true,
                    Text = "👤",
                    Font = new Font("Segoe UI Emoji", 18f),
                    Location = new Point(avatar.Left + 12, avatar.Top + 10)
                };
                Controls.Add(avatarIcon);

                // Name
                var lblName = new Label
                {
                    AutoSize = true,
                    Text = Passenger.FullName ?? "",
                    Font = new Font("Segoe UI", 12.5f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(25, 25, 25),
                    Location = new Point(84, 18)
                };
                Controls.Add(lblName);

                // Email
                var lblEmail = new Label
                {
                    AutoSize = true,
                    Text = "✉  " + (string.IsNullOrWhiteSpace(Passenger.Email) ? "—" : Passenger.Email),
                    Font = new Font("Segoe UI", 10f),
                    ForeColor = Color.FromArgb(110, 110, 110),
                    Location = new Point(84, 48)
                };
                Controls.Add(lblEmail);

                // Phone
                var lblPhone = new Label
                {
                    AutoSize = true,
                    Text = "📞  " + (string.IsNullOrWhiteSpace(Passenger.Phone) ? "—" : Passenger.Phone),
                    Font = new Font("Segoe UI", 10f),
                    ForeColor = Color.FromArgb(110, 110, 110),
                    Location = new Point(280, 48)
                };
                Controls.Add(lblPhone);

                // Member since (FIXED)
                _lblMember = new Label
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", 10f),
                    ForeColor = Color.FromArgb(110, 110, 110),
                    Location = new Point(460, 48)
                };
                Controls.Add(_lblMember);

                UpdateMemberLabel();
                Resize += (s, e) => UpdateMemberLabel();

                // counts right side
                var counts = BuildCounts(Passenger.UpcomingCount, Passenger.PastCount, Passenger.TotalCount);
                Controls.Add(counts);

                // Chevron (arrow)
                _btnChevron = new Guna2Button
                {
                    Size = new Size(38, 38),
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

                // layout on resize
                Resize += (s, e) =>
                {
                    counts.Location = new Point(Width - 290, 20);
                    _btnChevron.Location = new Point(Width - 52, 27);

                    _expandArea.Width = Width - 2 * Padding.Left;
                    _expandArea.Location = new Point(Padding.Left, 92);

                    foreach (Control c in _upcomingList.Controls) c.Width = _expandArea.Width;
                    foreach (Control c in _pastList.Controls) c.Width = _expandArea.Width;
                };

                counts.Location = new Point(Width - 290, 20);
                _btnChevron.Location = new Point(Width - 52, 27);
                _expandArea.Width = Width - 2 * Padding.Left;
                _expandArea.Location = new Point(Padding.Left, 92);

                Collapse();
            }

            // FIX: use MemberSince else CreatedAt else —
            private void UpdateMemberLabel()
            {
                bool compact = Width < 1050;


DateTime ? d = Passenger.CreatedAt;
                _lblMember.Text = "📅  Created at " + (d.HasValue ? d.Value.ToString(compact ? "yyyy" : "MMM dd, yyyy") : "—");


                string text = d.HasValue
                    ? d.Value.ToString(compact ? "yyyy" : "MMM dd, yyyy")
                    : "—";

                _lblMember.Text = "📅  Member since " + text;
            }

            public void ExpandLoading()
            {
                IsExpanded = true;
                _btnChevron.Text = "˄";
                _expandArea.Visible = true;

                _lblLoading.Visible = true;

                // minimal height while loading
                _expandArea.Height = 85;
                Height = 92 + _expandArea.Height + 14;

                // hide lists for now
                _upcomingList.Visible = false;
                _lblUpcomingEmpty.Visible = false;
                _lblPastTitle.Visible = false;
                _pastList.Visible = false;
                _lblPastEmpty.Visible = false;
            }

            public void SetBookings(List<PassengerBookingRow> upcoming, List<PassengerBookingRow> past)
            {
                _lblLoading.Visible = false;

                _upcomingList.SuspendLayout();
                _pastList.SuspendLayout();

                _upcomingList.Controls.Clear();
                _pastList.Controls.Clear();

                _lblUpcomingTitle.Text = $"Upcoming Flights ({upcoming.Count})";
                _lblPastTitle.Text = $"Past Flights ({past.Count})";

                foreach (var b in upcoming)
                {
                    var card = new BookingCard(b, true);
                    card.Width = _expandArea.Width;
                    card.CancelRequested += bookingId => CancelRequested?.Invoke(bookingId, Passenger.PassengerId);
                    _upcomingList.Controls.Add(card);
                }

                foreach (var b in past)
                {
                    var card = new BookingCard(b, false);
                    card.Width = _expandArea.Width;
                    _pastList.Controls.Add(card);
                }

                // layout
                int y = 10;
                _lblUpcomingTitle.Location = new Point(0, y);
                y = _lblUpcomingTitle.Bottom + 12;

                if (upcoming.Count == 0)
                {
                    _upcomingList.Visible = false;
                    _lblUpcomingEmpty.Visible = true;
                    _lblUpcomingEmpty.Location = new Point(0, y);
                    y = _lblUpcomingEmpty.Bottom + 16;
                }
                else
                {
                    _lblUpcomingEmpty.Visible = false;
                    _upcomingList.Visible = true;
                    _upcomingList.Location = new Point(0, y);
                    y = _upcomingList.Bottom + 16;
                }

                _lblPastTitle.Visible = true;
                _lblPastTitle.Location = new Point(0, y);
                y = _lblPastTitle.Bottom + 12;

                if (past.Count == 0)
                {
                    _pastList.Visible = false;
                    _lblPastEmpty.Visible = true;
                    _lblPastEmpty.Location = new Point(0, y);
                }
                else
                {
                    _lblPastEmpty.Visible = false;
                    _pastList.Visible = true;
                    _pastList.Location = new Point(0, y);
                }

                _upcomingList.ResumeLayout();
                _pastList.ResumeLayout();
            }

            public void Expand()
            {
                IsExpanded = true;
                _btnChevron.Text = "˄";
                _expandArea.Visible = true;

                // compute height based on content
                int h = 10;

                h += _lblUpcomingTitle.Height + 12;
                h += _upcomingList.Visible
                    ? _upcomingList.Controls.Cast<Control>().Sum(c => c.Height + c.Margin.Bottom) + 16
                    : _lblUpcomingEmpty.Height + 16;

                h += _lblPastTitle.Height + 12;
                h += _pastList.Visible
                    ? _pastList.Controls.Cast<Control>().Sum(c => c.Height + c.Margin.Bottom) + 8
                    : _lblPastEmpty.Height + 8;

                h += 10;

                _expandArea.Height = h;
                Height = 92 + h + 14;
            }

            public void Collapse()
            {
                IsExpanded = false;
                _btnChevron.Text = "˅";
                _expandArea.Visible = false;
                Height = 92;
            }

            private static FlowLayoutPanel BuildList() => new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = false,
                FlowDirection = FlowDirection.TopDown,
                BackColor = Color.Transparent
            };

            private static Label BuildEmpty(string text) => new Label
            {
                AutoSize = true,
                Text = text,
                Font = new Font("Segoe UI", 10f),
                ForeColor = Color.FromArgb(140, 140, 140)
            };

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
                    BackColor = Color.Transparent
                };

                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280));
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280));
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                grid.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200));

                grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 32));
                grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 52));
                grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));

                Controls.Add(grid);

                var topLeft = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = false,
                    BackColor = Color.Transparent
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
                    Text = "Flight ID",
                    Font = new Font("Segoe UI", 10f),
                    ForeColor = Color.FromArgb(120, 120, 120),
                    Margin = new Padding(0, 7, 8, 0)
                });

                topLeft.Controls.Add(new Label
                {
                    AutoSize = true,
                    Text = _b.FlightId.ToString(),
                    Font = new Font("Segoe UI", 11.5f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(25, 25, 25),
                    Margin = new Padding(0, 5, 12, 0)
                });

                var badge = new Label
                {
                    AutoSize = true,
                    Text = _isUpcoming ? "Upcoming" : "Past",
                    Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                    Padding = new Padding(10, 4, 10, 4),
                    Margin = new Padding(0, 4, 0, 0)
                };

                if (_isUpcoming)
                {
                    badge.ForeColor = Color.FromArgb(30, 140, 80);
                    badge.BackColor = Color.FromArgb(220, 245, 230);
                }
                else
                {
                    badge.ForeColor = Color.FromArgb(220, 60, 60);
                    badge.BackColor = Color.FromArgb(255, 230, 230);
                }

                topLeft.Controls.Add(badge);

                grid.Controls.Add(topLeft, 0, 0);
                grid.SetColumnSpan(topLeft, 2);

                var rightTop = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.RightToLeft,
                    WrapContents = false,
                    BackColor = Color.Transparent
                };

                if (_isUpcoming)
                {
                    var btnCancel = new Guna2Button
                    {
                        Text = "Cancel",
                        BorderRadius = 10,
                        FillColor = Color.FromArgb(255, 245, 245),
                        ForeColor = Color.FromArgb(220, 60, 60),
                        Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                        Size = new Size(120, 34),
                        Cursor = Cursors.Hand
                    };
                    btnCancel.Click += (s, e) => CancelRequested?.Invoke(_b.BookingId);
                    rightTop.Controls.Add(btnCancel);
                }

                grid.Controls.Add(rightTop, 3, 0);

                grid.Controls.Add(new Label
                {
                    Text = "From\n" + _b.FromCity,
                    Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(25, 25, 25),
                    Dock = DockStyle.Fill
                }, 0, 1);

                grid.Controls.Add(new Label
                {
                    Text = "To\n" + _b.ToCity,
                    Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(25, 25, 25),
                    Dock = DockStyle.Fill
                }, 1, 1);

                grid.Controls.Add(new Label
                {
                    Text = $"Depart: {_b.Departure:MMM dd, yyyy HH:mm}\nArrive:  {_b.Arrival:MMM dd, yyyy HH:mm}",
                    Font = new Font("Segoe UI", 10f),
                    ForeColor = Color.FromArgb(110, 110, 110),
                    Dock = DockStyle.Fill
                }, 2, 1);

                var bottom = new Label
                {
                    Text = $"Seat: {_b.SeatNumber}   |   {_b.Category}   |   {_b.Status}",
                    Font = new Font("Segoe UI", 10f),
                    ForeColor = Color.FromArgb(110, 110, 110),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleRight
                };
                grid.Controls.Add(bottom, 0, 2);
                grid.SetColumnSpan(bottom, 4);
            }
        }
    }
}
