using Airport_Airplane_management_system.Model.Core.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.UserControls
{
    public partial class PlaneScheduleControl : UserControl
    {
        // ---- Events for outside (Presenter/Form) ----
        public event EventHandler? CloseClicked;

        // Click on empty "Available" slot => (selectedDate, hour)
        public event EventHandler<(DateTime date, int hour)>? SlotSelected;

        // Click on a flight card
        public event EventHandler<FlightBlock>? FlightClicked;

        private const int HOURS = 24;
        private const int ROW_HEIGHT = 70;

        private DateTime _selectedDate = DateTime.Today;

        private int _planeId;
        private readonly List<FlightBlock> _blocks = new();

        public bool AllowBooking { get; private set; } = true;

        public PlaneScheduleControl()
        {
            InitializeComponent();

            Dock = DockStyle.Fill;
            DoubleBuffered = true;
            LayoutHeaderButtons();

            var host = pnlHeader.Parent ?? pnlHeader;
            host.SizeChanged += (s, e) => LayoutHeaderButtons();
            pnlHeader.LocationChanged += (s, e) => LayoutHeaderButtons();

            // buttons
            btnClose.Click += (s, e) => CloseClicked?.Invoke(this, EventArgs.Empty);
            btnClose.SizeChanged += (s, e) => MakeButtonRounded(btnClose, 10);

            // horizontal scroll (date cards)
            flowDates.AutoScroll = true;
            flowDates.WrapContents = false;
            flowDates.FlowDirection = FlowDirection.LeftToRight;
            flowDates.HorizontalScroll.Visible = true;
            flowDates.VerticalScroll.Visible = false;

            // mouse wheel => horizontal scroll
            flowDates.MouseWheel += (s, e) =>
            {
                int newX = flowDates.HorizontalScroll.Value - e.Delta;
                if (newX < flowDates.HorizontalScroll.Minimum) newX = flowDates.HorizontalScroll.Minimum;
                if (newX > flowDates.HorizontalScroll.Maximum) newX = flowDates.HorizontalScroll.Maximum;
                flowDates.AutoScrollPosition = new Point(newX, 0);
            };

            // timeline size fitting
            scrollTimeline.SizeChanged += (s, e) => FitTimelineSize();
            SizeChanged += (s, e) => FitTimelineSize();

            BuildDefaultTimeline();

            // default title
            lblTitle.Text = "Schedule";
            SetSelectedDate(DateTime.Today);
        }

        // Public API
        public void SetAircraftTitle(string title)
        {
            lblTitle.Text = title;
        }

        public void SetMode(bool allowBooking)
        {
            AllowBooking = allowBooking;
        }
        public void BindPlaneSchedule(int planeId, IEnumerable<Flight> planeFlights, DateTime startDate, int days = 360)
        {
            _planeId = planeId;
            _blocks.Clear();

            foreach (var f in planeFlights.Where(x => x.PlaneIDFromDb == planeId))
            {
                var start = f.Departure;
                var end = f.Arrival;
                if (end <= start) end = start.AddHours(1);

                _blocks.Add(new FlightBlock
                {
                    PlaneId = f.PlaneIDFromDb,
                    FlightId = f.FlightID,
                    From = f.From ?? "",
                    To = f.To ?? "",
                    Start = start,
                    End = end
                });
            }

            BuildDateCards(startDate, days);
            SetSelectedDate(startDate.Date);
        }

        // Dates row (cards)
        private void BuildDateCards(DateTime startDate, int days)
        {
            flowDates.SuspendLayout();
            flowDates.Controls.Clear();

            for (int i = 0; i < days; i++)
            {
                var date = startDate.Date.AddDays(i);

                int count = _blocks.Count(b => b.PlaneId == _planeId && ShouldShowOnDay(b, date));

                var card = new DateCard(date, count);
                card.Margin = new Padding(0, 0, 10, 0);
                card.Clicked += (s, e) => SetSelectedDate(date);

                flowDates.Controls.Add(card);
            }

            flowDates.ResumeLayout();
            HighlightDateCard(_selectedDate);
        }

        private void HighlightDateCard(DateTime date)
        {
            foreach (Control c in flowDates.Controls)
            {
                if (c is DateCard dc)
                    dc.SetSelected(dc.Date == date.Date);
            }
        }

        private void ScrollToSelectedDate()
        {
            foreach (Control c in flowDates.Controls)
            {
                if (c is DateCard dc && dc.Date == _selectedDate)
                {
                    flowDates.ScrollControlIntoView(dc);
                    break;
                }
            }
        }
        // Selected Date + Timeline Rendering
        public void SetSelectedDate(DateTime date)
        {
            _selectedDate = date.Date;
            lblTimeline.Text = $"Timeline for {_selectedDate:yyyy-MM-dd}";

            HighlightDateCard(_selectedDate);
            ScrollToSelectedDate();
            RenderTimelineForSelectedDay();
        }

        private void RenderTimelineForSelectedDay()
        {
            DateTime dayStart = _selectedDate.Date;
            DateTime dayEnd = dayStart.AddDays(1);

            // 1) Reset all slots to AVAILABLE
            for (int r = 0; r < HOURS; r++)
            {
                var slot = GetSlotPanelAtRow(r);
                if (slot == null) continue;

                slot.Visible = true;
                slot.Enabled = true;
                slot.Controls.Clear();
                slot.Tag = null;

                slot.BorderDash = true;
                slot.BorderWidth = 1.2f;
                slot.Padding = new Padding(14, 4, 14, 4);

                int hour = r;
                slot.Controls.Add(CreateAvailableButton(hour, slot));
            }

            // Take flights for this plane that overlap this day
            var todays = _blocks
                .Where(b => b.PlaneId == _planeId && ShouldShowOnDay(b, dayStart))
                .Select(b =>
                {
                    var (segStart, segEnd) = GetSegmentForDay(b, dayStart);
                    return new { Block = b, SegStart = segStart, SegEnd = segEnd };
                })
                .OrderBy(x => x.SegStart)
                .ToList();

            foreach (var item in todays)
            {
                var b = item.Block;
                var segStart = item.SegStart;
                var segEnd = item.SegEnd;

                int startHour = segStart.Hour;
                int endHour;

                if (segEnd == dayEnd)
                {
                    endHour = 24;
                }
                else
                {
                    endHour = segEnd.Hour + ((segEnd.Minute > 0 || segEnd.Second > 0) ? 1 : 0);
                    endHour = Math.Min(24, endHour);
                }

                if (endHour <= startHour)
                    endHour = Math.Min(24, startHour + 1);

                // mark every overlapped hour as BUSY (no Available)
                for (int h = startHour; h < endHour; h++)
                {
                    var slot = GetSlotPanelAtRow(h);
                    if (slot == null) continue;

                    slot.Visible = true;
                    slot.Enabled = true;
                    slot.Controls.Clear();
                    slot.Tag = b;

                    slot.BorderDash = false;
                    slot.BorderWidth = 0;
                    slot.Padding = Padding.Empty;

                    var card = new FlightCard(b, segStart, segEnd)
                    {
                        Dock = DockStyle.Fill,
                        Margin = Padding.Empty
                    };
                    card.Clicked += (s, e) => FlightClicked?.Invoke(this, b);

                    slot.Controls.Add(card);
                }
            }
        }

        // ---------------- helpers ----------------

        private static bool ShouldShowOnDay(FlightBlock b, DateTime day)
        {
            DateTime dayStart = day.Date;
            DateTime dayEnd = dayStart.AddDays(1);
            return b.Start < dayEnd && b.End > dayStart;
        }

        private static (DateTime segStart, DateTime segEnd) GetSegmentForDay(FlightBlock b, DateTime day)
        {
            DateTime dayStart = day.Date;
            DateTime dayEnd = dayStart.AddDays(1);

            DateTime s = b.Start < dayStart ? dayStart : b.Start;
            DateTime e = b.End > dayEnd ? dayEnd : b.End;

            if (e <= s) e = s.AddHours(1);
            return (s, e);
        }

        // Timeline table build
        private void BuildDefaultTimeline()
        {
            tblTimeline.SuspendLayout();

            tblTimeline.Controls.Clear();
            tblTimeline.RowStyles.Clear();
            tblTimeline.ColumnStyles.Clear();

            tblTimeline.ColumnCount = 2;
            tblTimeline.RowCount = HOURS;

            tblTimeline.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tblTimeline.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            for (int i = 0; i < HOURS; i++)
            {
                tblTimeline.RowStyles.Add(new RowStyle(SizeType.Absolute, ROW_HEIGHT));

                var lblTime = new Label
                {
                    Text = $"{i:00}:00",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    ForeColor = Color.FromArgb(90, 90, 90),
                    Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                };

                int capturedHour = i;

                var slot = new DashedRoundPanel
                {
                    Dock = DockStyle.Fill,
                    Margin = new Padding(10, 3, 10, 3),
                    Padding = new Padding(14, 4, 14, 4),
                    Radius = 14,
                    BorderColor = Color.FromArgb(215, 215, 215),
                    BorderWidth = 1.2f,
                    BorderDash = true,
                    BackColor = Color.White,
                    Cursor = Cursors.Hand
                };

                slot.Click += (s, e) =>
                {
                    if (!AllowBooking) return;
                    if (slot.Tag is FlightBlock) return;
                    SlotSelected?.Invoke(this, (_selectedDate, capturedHour));
                };

                slot.Controls.Add(CreateAvailableButton(capturedHour, slot));

                tblTimeline.Controls.Add(lblTime, 0, i);
                tblTimeline.Controls.Add(slot, 1, i);
            }

            tblTimeline.ResumeLayout(true);
            FitTimelineSize();
        }

        private DashedRoundPanel? GetSlotPanelAtRow(int row)
        {
            return tblTimeline.GetControlFromPosition(1, row) as DashedRoundPanel;
        }

        private Control CreateAvailableButton(int hour, DashedRoundPanel slot)
        {
            var btn = new Button
            {
                Text = "Available",
                Dock = DockStyle.Fill,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(155, 155, 155),
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                Cursor = Cursors.Hand,
                TabStop = false
            };

            if (!AllowBooking)
            {
                btn.Enabled = false;
                btn.Cursor = Cursors.Default;
                btn.ForeColor = Color.FromArgb(190, 190, 190);
            }

            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn.FlatAppearance.MouseDownBackColor = Color.Transparent;

            btn.Click += (s, e) =>
            {
                if (!AllowBooking) return;
                if (slot.Tag is FlightBlock) return;
                SlotSelected?.Invoke(this, (_selectedDate, hour));
            };

            return btn;
        }

        private void FitTimelineSize()
        {
            if (tblTimeline == null || scrollTimeline == null) return;

            tblTimeline.Width = scrollTimeline.ClientSize.Width;
            tblTimeline.Height = HOURS * ROW_HEIGHT;
        }

        // Data DTO exposed publicly

        public class FlightBlock
        {
            public int PlaneId { get; set; }
            public int FlightId { get; set; }
            public string From { get; set; } = "";
            public string To { get; set; } = "";
            public DateTime Start { get; set; }
            public DateTime End { get; set; }

            public string Code => $"#{FlightId}";
            public string Route => $"{From} → {To}";
        }
        // UI Components
        private sealed class DateCard : UserControl
        {
            public event EventHandler? Clicked;
            public DateTime Date { get; }

            private readonly RoundedBorderPanel _box;
            private readonly Label _badge;

            public DateCard(DateTime date, int flightsCount)
            {
                Date = date.Date;
                Size = new Size(120, 78);
                BackColor = Color.Transparent;

                _box = new RoundedBorderPanel
                {
                    Dock = DockStyle.Fill,
                    Radius = 12,
                    BorderColor = Color.FromArgb(220, 220, 220),
                    BorderWidth = 1.2f,
                    BackColor = Color.White,
                    Padding = new Padding(10, 10, 10, 8),
                };

                var lblDow = new Label
                {
                    Text = Date.ToString("ddd"),
                    AutoSize = false,
                    Height = 16,
                    Dock = DockStyle.Top,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(120, 120, 120),
                    Font = new Font("Segoe UI", 8F, FontStyle.Regular),
                };

                var lblDay = new Label
                {
                    Text = Date.ToString("dd"),
                    AutoSize = false,
                    Height = 28,
                    Dock = DockStyle.Top,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Black,
                    Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                };

                var lblMon = new Label
                {
                    Text = Date.ToString("MMM"),
                    AutoSize = false,
                    Height = 16,
                    Dock = DockStyle.Top,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(120, 120, 120),
                    Font = new Font("Segoe UI", 8F, FontStyle.Regular),
                };

                _badge = new Label
                {
                    Visible = flightsCount > 0,
                    Text = $"{flightsCount} flights",
                    AutoSize = false,
                    Height = 18,
                    Dock = DockStyle.Bottom,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.Black,
                    Font = new Font("Segoe UI", 7.5F, FontStyle.Regular),
                    BackColor = Color.FromArgb(240, 240, 240),
                    Margin = new Padding(12, 6, 12, 0),
                };

                _box.Controls.Add(_badge);
                _box.Controls.Add(lblMon);
                _box.Controls.Add(lblDay);
                _box.Controls.Add(lblDow);

                Controls.Add(_box);
                WireClicks(this);
            }

            private void WireClicks(Control root)
            {
                foreach (Control c in root.Controls)
                    WireClicks(c);

                root.Click += (s, e) => Clicked?.Invoke(this, EventArgs.Empty);
            }

            public void SetSelected(bool selected)
            {
                if (selected)
                {
                    _box.BorderColor = Color.FromArgb(30, 120, 255);
                    _box.BorderWidth = 1.8f;
                }
                else
                {
                    _box.BorderColor = Color.FromArgb(220, 220, 220);
                    _box.BorderWidth = 1.2f;
                }
                _box.Invalidate();
            }
        }

        private sealed class FlightCard : UserControl
        {
            public event EventHandler? Clicked;

            public FlightCard(FlightBlock b, DateTime displayStart, DateTime displayEnd)
            {
                BackColor = Color.Transparent;

                var bg = new RoundedBorderPanel
                {
                    Dock = DockStyle.Fill,
                    Radius = 14,
                    BorderColor = Color.Transparent,
                    BorderWidth = 0,
                    BackColor = Color.FromArgb(33, 99, 255),
                    Padding = new Padding(14, 10, 14, 10),
                    Cursor = Cursors.Hand
                };

                var table = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 2,
                    RowCount = 2,
                    BackColor = Color.Transparent
                };
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 78f));
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22f));
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 52f));
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 48f));

                var lblCode = new Label
                {
                    Dock = DockStyle.Fill,
                    AutoSize = true,
                    Text = b.Code,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9.75f, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleLeft
                };

                var lblRoute = new Label
                {
                    Dock = DockStyle.Fill,
                    AutoSize = true,
                    Text = b.Route,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9f, FontStyle.Regular),
                    TextAlign = ContentAlignment.MiddleLeft
                };

                string rightText;
                if (displayEnd == displayStart.Date.AddDays(1))
                    rightText = $"{displayStart:HH:mm} - 24:00";
                else
                    rightText = $"{displayStart:HH:mm} - {displayEnd:HH:mm}";

                var lblTime = new Label
                {
                    Dock = DockStyle.Fill,
                    AutoSize = true,
                    Text = rightText,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 9f, FontStyle.Regular),
                    TextAlign = ContentAlignment.MiddleRight
                };
                table.SetRowSpan(lblTime, 2);

                table.Controls.Add(lblCode, 0, 0);
                table.Controls.Add(lblRoute, 0, 1);
                table.Controls.Add(lblTime, 1, 0);

                bg.Controls.Add(table);
                Controls.Add(bg);

                WireClicks(this);
            }

            private void WireClicks(Control root)
            {
                foreach (Control c in root.Controls)
                    WireClicks(c);

                root.Click += (s, e) => Clicked?.Invoke(this, EventArgs.Empty);
            }
        }

        // Custom panels
        private class RoundedBorderPanel : Panel
        {
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public int Radius { get; set; } = 12;

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public Color BorderColor { get; set; } = Color.FromArgb(220, 220, 220);

            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public float BorderWidth { get; set; } = 1.2f;

            public RoundedBorderPanel()
            {
                SetStyle(ControlStyles.AllPaintingInWmPaint |
                         ControlStyles.UserPaint |
                         ControlStyles.OptimizedDoubleBuffer |
                         ControlStyles.ResizeRedraw, true);
            }

            protected override void OnSizeChanged(EventArgs e)
            {
                base.OnSizeChanged(e);

                if (Width <= 0 || Height <= 0) return;

                var rect = new Rectangle(0, 0, Width, Height);
                using var path = GetRoundRectPath(Rectangle.Inflate(rect, -1, -1), Radius);
                Region = new Region(path);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                var rect = Rectangle.Inflate(ClientRectangle, -1, -1);
                rect.Width -= 1;
                rect.Height -= 1;

                using var path = GetRoundRectPath(rect, Radius);
                using var brush = new SolidBrush(BackColor);
                e.Graphics.FillPath(brush, path);

                if (BorderWidth > 0.01f && BorderColor.A > 0)
                {
                    using var pen = new Pen(BorderColor, BorderWidth);
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private sealed class DashedRoundPanel : RoundedBorderPanel
        {
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
            public bool BorderDash { get; set; } = true;

            protected override void OnPaint(PaintEventArgs e)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                var rect = Rectangle.Inflate(ClientRectangle, -1, -1);
                rect.Width -= 1;
                rect.Height -= 1;

                using var path = GetRoundRectPath(rect, Radius);
                using var brush = new SolidBrush(BackColor);
                e.Graphics.FillPath(brush, path);

                if (BorderWidth > 0.01f)
                {
                    using var pen = new Pen(BorderColor, BorderWidth);
                    if (BorderDash) pen.DashStyle = DashStyle.Dash;
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        private static GraphicsPath GetRoundRectPath(Rectangle rect, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();

            return path;
        }

        private void LayoutHeaderButtons()
        {
            Control host = pnlHeader.Parent ?? pnlHeader;

            int rightPad = 18;
            int topPad = 12;

            int y = pnlHeader.Top + topPad;
            int rightEdge = host.ClientSize.Width - rightPad;

            // Close at far right
            btnClose.Location = new Point(rightEdge - btnClose.Width, y);
            btnClose.BringToFront();
        }

        private void MakeButtonRounded(Button btn, int radius = 14)
        {
            btn.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, btn.Width, btn.Height, radius, radius));
        }

        [DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect, int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);

        private void lblSelectDate_Click(object sender, EventArgs e) { }
        private void flowDates_Paint(object sender, PaintEventArgs e) { }
    }
}
