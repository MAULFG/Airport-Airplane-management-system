//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Windows.Forms;

//namespace Airport_Airplane_management_system.View.UserControls
//{
//    public partial class PlaneScheduleControl : UserControl
//    {
//        public event EventHandler? CloseClicked;
//        public event EventHandler? AddFlightClicked;
//        public event EventHandler<DateTime>? DateSelected;
//        public event EventHandler<FlightBlock>? FlightClicked;

//        private DateTime _selectedDate = DateTime.Today;

//        public PlaneScheduleControl()
//        {
//            InitializeComponent();

//            this.Dock = DockStyle.Fill;
//            this.DoubleBuffered = true;

//            btnClose.Click += (s, e) => CloseClicked?.Invoke(this, EventArgs.Empty);
//            btnAddFlight.Click += (s, e) => AddFlightClicked?.Invoke(this, EventArgs.Empty);

//            // Make button rounded like the design
//            btnAddFlight.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnAddFlight.Width, btnAddFlight.Height, 14, 14));
//            btnAddFlight.SizeChanged += (s, e) =>
//            {
//                btnAddFlight.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btnAddFlight.Width, btnAddFlight.Height, 14, 14));
//            };

//            BuildDefaultTimeline();     // 00:00 -> 23:00 rows
//            BuildSampleDates();         // you can remove this and call SetDates(...) from presenter
//            SetSelectedDate(DateTime.Today);
//        }

//        // ----------------------------
//        // Public API (call from Presenter)
//        // ----------------------------
//        public void SetAircraftTitle(string aircraftName)
//        {
//            lblTitle.Text = $"{aircraftName} Schedule";
//        }

//        public void SetSelectedDate(DateTime date)
//        {
//            _selectedDate = date.Date;
//            lblTimeline.Text = $"Timeline for {_selectedDate:yyyy-MM-dd}";
//            HighlightDateCard(_selectedDate);

//            DateSelected?.Invoke(this, _selectedDate);
//        }

//        public void SetDates(IEnumerable<DateBadge> dates)
//        {
//            flowDates.SuspendLayout();
//            flowDates.Controls.Clear();

//            foreach (var d in dates)
//            {
//                var card = new DateCard(d.Date, d.FlightsCount);
//                card.Margin = new Padding(0, 0, 10, 0);
//                card.Clicked += (s, e) => SetSelectedDate(d.Date);
//                flowDates.Controls.Add(card);
//            }

//            flowDates.ResumeLayout();
//        }

//        public void ClearFlights()
//        {
//            // Reset all slots to "Available"
//            for (int r = 0; r < tblTimeline.RowCount; r++)
//            {
//                var slot = GetSlotPanelAtRow(r);
//                if (slot == null) continue;

//                slot.Controls.Clear();
//                slot.Controls.Add(CreateAvailableLabel());
//                slot.Tag = null;
//            }
//        }

//        public void SetFlights(IEnumerable<FlightBlock> flights)
//        {
//            ClearFlights();

//            foreach (var f in flights)
//            {
//                AddFlightBlock(f);
//            }
//        }

//        public void AddFlightBlock(FlightBlock f)
//        {
//            // This implementation places the flight card into the row of its start hour.
//            // If you want minute-accurate vertical positioning later, we can upgrade it.
//            int hourRow = Math.Max(0, Math.Min(23, f.Start.Hour));
//            var slot = GetSlotPanelAtRow(hourRow);
//            if (slot == null) return;

//            slot.Controls.Clear();

//            var card = new FlightCard(f);
//            card.Dock = DockStyle.Fill;
//            card.Margin = new Padding(0);
//            card.Clicked += (s, e) => FlightClicked?.Invoke(this, f);

//            slot.Controls.Add(card);
//            slot.Tag = f;
//        }

//        // ----------------------------
//        // Timeline build
//        // ----------------------------
//        private void BuildDefaultTimeline()
//        {
//            tblTimeline.SuspendLayout();
//            tblTimeline.Controls.Clear();
//            tblTimeline.RowStyles.Clear();

//            tblTimeline.RowCount = 24;

//            for (int i = 0; i < 24; i++)
//            {
//                tblTimeline.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));

//                var lblTime = new Label
//                {
//                    Text = $"{i:00}:00",
//                    Dock = DockStyle.Fill,
//                    TextAlign = ContentAlignment.MiddleLeft,
//                    ForeColor = Color.FromArgb(90, 90, 90),
//                    Font = new Font("Segoe UI", 9F, FontStyle.Regular),
//                    Padding = new Padding(0, 0, 0, 0),
//                };

//                var slot = new DashedRoundPanel
//                {
//                    Dock = DockStyle.Fill,
//                    Margin = new Padding(0, 6, 0, 6),
//                    Padding = new Padding(14, 0, 14, 0),
//                    Radius = 14,
//                    BorderColor = Color.FromArgb(215, 215, 215),
//                    BorderDash = true,
//                    BorderWidth = 1.2f,
//                    BackColor = Color.White,
//                };

//                slot.Controls.Add(CreateAvailableLabel());

//                tblTimeline.Controls.Add(lblTime, 0, i);
//                tblTimeline.Controls.Add(slot, 1, i);
//            }

//            tblTimeline.ResumeLayout();
//        }

//        private Label CreateAvailableLabel()
//        {
//            return new Label
//            {
//                Text = "Available",
//                Dock = DockStyle.Fill,
//                TextAlign = ContentAlignment.MiddleCenter,
//                ForeColor = Color.FromArgb(155, 155, 155),
//                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
//            };
//        }

//        private DashedRoundPanel? GetSlotPanelAtRow(int row)
//        {
//            // column 1 is slot panel
//            var ctrl = tblTimeline.GetControlFromPosition(1, row);
//            return ctrl as DashedRoundPanel;
//        }

//        // ----------------------------
//        // Dates (sample builder) - replace with presenter data
//        // ----------------------------
//        private void BuildSampleDates()
//        {
//            var list = new List<DateBadge>();
//            var start = DateTime.Today.AddDays(-2);

//            for (int i = 0; i < 7; i++)
//            {
//                var d = start.AddDays(i);
//                list.Add(new DateBadge(d, flightsCount: (i == 3 ? 2 : 0)));
//            }

//            SetDates(list);
//        }

//        private void HighlightDateCard(DateTime date)
//        {
//            foreach (Control c in flowDates.Controls)
//            {
//                if (c is DateCard dc)
//                    dc.SetSelected(dc.Date == date.Date);
//            }
//        }

//        // ----------------------------
//        // Helpers for rounded region
//        // ----------------------------
//        [System.Runtime.InteropServices.DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
//        private static extern IntPtr CreateRoundRectRgn(
//            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

//        // ----------------------------
//        // Models
//        // ----------------------------
//        public record DateBadge(DateTime Date, int FlightsCount);

//        public class FlightBlock
//        {
//            public string Code { get; set; } = "AA101";
//            public string Route { get; set; } = "New York (JFK) → Los Angeles (LAX)";
//            public DateTime Start { get; set; } = DateTime.Today.AddHours(8);
//            public DateTime End { get; set; } = DateTime.Today.AddHours(11.5);
//        }

//        // ----------------------------
//        // UI: Date card control
//        // ----------------------------
//        private sealed class DateCard : UserControl
//        {
//            public event EventHandler? Clicked;

//            public DateTime Date { get; }
//            private readonly Label _lblDow;
//            private readonly Label _lblDay;
//            private readonly Label _lblMon;
//            private readonly Label _badge;
//            private readonly RoundedBorderPanel _box;

//            private bool _selected;

//            public DateCard(DateTime date, int flightsCount)
//            {
//                Date = date.Date;
//                this.Size = new Size(92, 78);
//                this.BackColor = Color.Transparent;

//                _box = new RoundedBorderPanel
//                {
//                    Dock = DockStyle.Fill,
//                    Radius = 12,
//                    BorderColor = Color.FromArgb(220, 220, 220),
//                    BorderWidth = 1.2f,
//                    BackColor = Color.White,
//                    Padding = new Padding(10, 10, 10, 8),
//                };

//                _lblDow = new Label
//                {
//                    Text = Date.ToString("ddd"),
//                    AutoSize = false,
//                    Height = 16,
//                    Dock = DockStyle.Top,
//                    TextAlign = ContentAlignment.MiddleCenter,
//                    ForeColor = Color.FromArgb(120, 120, 120),
//                    Font = new Font("Segoe UI", 8F, FontStyle.Regular),
//                };

//                _lblDay = new Label
//                {
//                    Text = Date.ToString("dd"),
//                    AutoSize = false,
//                    Height = 28,
//                    Dock = DockStyle.Top,
//                    TextAlign = ContentAlignment.MiddleCenter,
//                    ForeColor = Color.Black,
//                    Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
//                };

//                _lblMon = new Label
//                {
//                    Text = Date.ToString("MMM"),
//                    AutoSize = false,
//                    Height = 16,
//                    Dock = DockStyle.Top,
//                    TextAlign = ContentAlignment.MiddleCenter,
//                    ForeColor = Color.FromArgb(120, 120, 120),
//                    Font = new Font("Segoe UI", 8F, FontStyle.Regular),
//                };

//                _badge = new Label
//                {
//                    Visible = flightsCount > 0,
//                    Text = $"{flightsCount} flights",
//                    AutoSize = false,
//                    Height = 18,
//                    Dock = DockStyle.Bottom,
//                    TextAlign = ContentAlignment.MiddleCenter,
//                    ForeColor = Color.Black,
//                    Font = new Font("Segoe UI", 7.5F, FontStyle.Regular),
//                    BackColor = Color.FromArgb(240, 240, 240),
//                    Margin = new Padding(12, 6, 12, 0),
//                };

//                _box.Controls.Add(_badge);
//                _box.Controls.Add(_lblMon);
//                _box.Controls.Add(_lblDay);
//                _box.Controls.Add(_lblDow);

//                this.Controls.Add(_box);

//                WireClicks(this);
//            }

//            private void WireClicks(Control root)
//            {
//                foreach (Control c in root.Controls)
//                    WireClicks(c);

//                root.Click += (s, e) => Clicked?.Invoke(this, EventArgs.Empty);
//            }

//            public void SetSelected(bool selected)
//            {
//                _selected = selected;
//                if (_selected)
//                {
//                    _box.BorderColor = Color.FromArgb(30, 120, 255);
//                    _box.BorderWidth = 1.8f;
//                }
//                else
//                {
//                    _box.BorderColor = Color.FromArgb(220, 220, 220);
//                    _box.BorderWidth = 1.2f;
//                }
//                _box.Invalidate();
//            }
//        }

//        // ----------------------------
//        // UI: Flight card (blue)
//        // ----------------------------
//        private sealed class FlightCard : UserControl
//        {
//            public event EventHandler? Clicked;

//            private readonly RoundedBorderPanel _bg;
//            private readonly Label _code;
//            private readonly Label _route;
//            private readonly Label _time;

//            public FlightCard(FlightBlock f)
//            {
//                this.BackColor = Color.Transparent;

//                _bg = new RoundedBorderPanel
//                {
//                    Dock = DockStyle.Fill,
//                    Radius = 12,
//                    BorderColor = Color.Transparent,
//                    BorderWidth = 0,
//                    BackColor = Color.FromArgb(26, 92, 255),
//                    Padding = new Padding(14, 10, 14, 10),
//                };

//                _code = new Label
//                {
//                    Text = f.Code,
//                    Dock = DockStyle.Top,
//                    Height = 18,
//                    ForeColor = Color.White,
//                    Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
//                };

//                _route = new Label
//                {
//                    Text = f.Route,
//                    Dock = DockStyle.Top,
//                    Height = 18,
//                    ForeColor = Color.White,
//                    Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
//                };

//                _time = new Label
//                {
//                    Text = $"{f.Start:HH:mm} - {f.End:HH:mm}",
//                    AutoSize = false,
//                    Dock = DockStyle.Right,
//                    Width = 92,
//                    TextAlign = ContentAlignment.MiddleRight,
//                    ForeColor = Color.White,
//                    Font = new Font("Segoe UI Semibold", 8.5F, FontStyle.Bold),
//                };

//                var left = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
//                left.Controls.Add(_route);
//                left.Controls.Add(_code);

//                _bg.Controls.Add(_time);
//                _bg.Controls.Add(left);

//                this.Controls.Add(_bg);

//                WireClicks(this);
//            }

//            private void WireClicks(Control root)
//            {
//                foreach (Control c in root.Controls)
//                    WireClicks(c);

//                root.Click += (s, e) => Clicked?.Invoke(this, EventArgs.Empty);
//            }
//        }

//        // ----------------------------
//        // Painting panels
//        // ----------------------------
//        private class RoundedBorderPanel : Panel
//        {
//            public int Radius { get; set; } = 12;
//            public Color BorderColor { get; set; } = Color.FromArgb(220, 220, 220);
//            public float BorderWidth { get; set; } = 1.2f;

//            public RoundedBorderPanel()
//            {
//                this.SetStyle(ControlStyles.AllPaintingInWmPaint |
//                              ControlStyles.UserPaint |
//                              ControlStyles.OptimizedDoubleBuffer |
//                              ControlStyles.ResizeRedraw, true);
//            }

//            protected override void OnPaint(PaintEventArgs e)
//            {
//                base.OnPaint(e);

//                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

//                var rect = this.ClientRectangle;
//                rect.Width -= 1;
//                rect.Height -= 1;

//                using var path = GetRoundRectPath(rect, Radius);
//                using var brush = new SolidBrush(this.BackColor);
//                e.Graphics.FillPath(brush, path);

//                if (BorderWidth > 0.01f && BorderColor.A > 0)
//                {
//                    using var pen = new Pen(BorderColor, BorderWidth);
//                    e.Graphics.DrawPath(pen, path);
//                }
//            }
//        }

//        private sealed class DashedRoundPanel : RoundedBorderPanel
//        {
//            public bool BorderDash { get; set; } = true;

//            protected override void OnPaint(PaintEventArgs e)
//            {
//                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

//                var rect = this.ClientRectangle;
//                rect.Width -= 1;
//                rect.Height -= 1;

//                using var path = GetRoundRectPath(rect, Radius);
//                using var brush = new SolidBrush(this.BackColor);
//                e.Graphics.FillPath(brush, path);

//                if (BorderWidth > 0.01f)
//                {
//                    using var pen = new Pen(BorderColor, BorderWidth);
//                    if (BorderDash) pen.DashStyle = DashStyle.Dash;
//                    e.Graphics.DrawPath(pen, path);
//                }

//                base.OnPaint(e);
//            }
//        }

//        private static GraphicsPath GetRoundRectPath(Rectangle rect, int radius)
//        {
//            int d = radius * 2;
//            var path = new GraphicsPath();

//            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
//            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
//            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
//            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
//            path.CloseFigure();

//            return path;
//        }
//    }
//}
