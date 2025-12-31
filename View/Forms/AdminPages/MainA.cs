using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    public partial class MainA : UserControl
    {
        private IBookingRepository _bookingRepo;
        private IFlightRepository _flightRepo;
        private IPlaneRepository _planeRepo;
        private ICrewRepository _crewRepo;
        private IPassengerRepository _passengerRepo;

        // KPI value labels (real data updates)
        private Label _kpiFlightsBig, _kpiPlanesBig, _kpiCrewBig, _kpiPassengersBig, _kpiAlertsBig, _kpiOpsBig;
        private Label _kpiFlightsInAir, _kpiFlightsUpcoming, _kpiFlightsPast;
        private Label _kpiPlanesActive, _kpiPlanesInactive;
        private Label _kpiCrewAssigned, _kpiCrewUnassigned;
        private Label _kpiPassengersUpcoming, _kpiPassengersPast;

        private bool _built;

        // ✅ Navigation events (AdminDashboard subscribes to these)
        public event Action GoToFlightsRequested;
        public event Action GoToPlanesRequested;
        public event Action GoToCrewRequested;
        public event Action GoToPassengersRequested;
        public event Action GoToNotificationsRequested;

        public MainA()
        {
            InitializeComponent();

            // These MUST exist in Designer:
            // kpiGrid (TableLayoutPanel), flightsList (FlowLayoutPanel), alertsList (FlowLayoutPanel), lblAlertsRight (Label)

            flightsList.WrapContents = false;
            flightsList.FlowDirection = FlowDirection.TopDown;
            flightsList.AutoScroll = true;

            alertsList.WrapContents = false;
            alertsList.FlowDirection = FlowDirection.TopDown;
            alertsList.AutoScroll = true;

            flightsList.SizeChanged += (_, __) => ForceRowsFullWidth(flightsList);
            alertsList.SizeChanged += (_, __) => ForceRowsFullWidth(alertsList);
        }

        /// <summary>Call this from AdminDashboard after repos are created.</summary>
        public void BindRepositories(
            IFlightRepository flightRepo,
            IPlaneRepository planeRepo,
            ICrewRepository crewRepo,
            IPassengerRepository passengerRepo,
            IBookingRepository bookingRepo
        )
        {
            _flightRepo = flightRepo;
            _planeRepo = planeRepo;
            _crewRepo = crewRepo;
            _passengerRepo = passengerRepo;
            _bookingRepo = bookingRepo;

            if (!_built)
            {
                BuildFigmaUI();
                _built = true;
            }

            LoadRealData();
        }

        // =========================
        //  UI BUILD (FIGMA)
        // =========================
        private void BuildFigmaUI()
        {
            kpiGrid.SuspendLayout();
            kpiGrid.Controls.Clear();

            // 🔥 CHANGE THIS to increase card height
            const int CARD_HEIGHT = 165;

            // IMPORTANT: In TableLayoutPanel, RowStyle controls card height (Dock=Fill)
            // Make rows absolute so the cards become taller like Figma.
            EnsureKpiGridLayout(rowHeight: CARD_HEIGHT + 18);

            // 6 KPI cards (3x2)
            kpiGrid.Controls.Add(MakeKpiCard(
                iconText: "✈",
                iconBack: Color.FromArgb(232, 248, 245),
                iconFore: Color.FromArgb(0, 160, 150),
                title: "Total Flights",
                bigOut: out _kpiFlightsBig,
                subtitleSmall: "",
                cardHeight: CARD_HEIGHT,
                breakdown: new (string, Label, Color)[]
                {
                    ("In Air",   _kpiFlightsInAir    = NewValueLabel(), Color.FromArgb(28,140,60)),
                    ("Upcoming", _kpiFlightsUpcoming = NewValueLabel(), Color.FromArgb(35,93,220)),
                    ("Past",     _kpiFlightsPast     = NewValueLabel(), Color.FromArgb(120,120,120)),
                },
                onClick: () => GoToFlightsRequested?.Invoke()
            ), 0, 0);

            kpiGrid.Controls.Add(MakeKpiCard(
                iconText: "✈",
                iconBack: Color.FromArgb(235, 242, 255),
                iconFore: Color.FromArgb(35, 93, 220),
                title: "Active Planes",
                bigOut: out _kpiPlanesBig,
                subtitleSmall: "",
                cardHeight: CARD_HEIGHT,
                breakdown: new (string, Label, Color)[]
                {
                    ("Active",   _kpiPlanesActive   = NewValueLabel(), Color.FromArgb(28,140,60)),
                    ("Inactive", _kpiPlanesInactive = NewValueLabel(), Color.FromArgb(120,120,120)),
                },
                onClick: () => GoToPlanesRequested?.Invoke()
            ), 1, 0);

            kpiGrid.Controls.Add(MakeKpiCard(
                iconText: "👥",
                iconBack: Color.FromArgb(242, 238, 255),
                iconFore: Color.FromArgb(120, 80, 220),
                title: "Crew Members",
                bigOut: out _kpiCrewBig,
                subtitleSmall: "",
                cardHeight: CARD_HEIGHT,
                breakdown: new (string, Label, Color)[]
                {
                    ("Assigned",   _kpiCrewAssigned   = NewValueLabel(), Color.FromArgb(28,140,60)),
                    ("Unassigned", _kpiCrewUnassigned = NewValueLabel(), Color.FromArgb(220,60,60)),
                },
                onClick: () => GoToCrewRequested?.Invoke()
            ), 2, 0);

            kpiGrid.Controls.Add(MakeKpiCard(
                iconText: "👤",
                iconBack: Color.FromArgb(236, 245, 255),
                iconFore: Color.FromArgb(35, 93, 220),
                title: "Passengers",
                bigOut: out _kpiPassengersBig,
                subtitleSmall: "",
                cardHeight: CARD_HEIGHT,
                breakdown: new (string, Label, Color)[]
                {
                    ("Upcoming", _kpiPassengersUpcoming = NewValueLabel(), Color.FromArgb(35,93,220)),
                    ("Past",     _kpiPassengersPast     = NewValueLabel(), Color.FromArgb(120,120,120)),
                },
                onClick: () => GoToPassengersRequested?.Invoke()
            ), 0, 1);

            kpiGrid.Controls.Add(MakeKpiCard(
                iconText: "⚠",
                iconBack: Color.FromArgb(255, 239, 239),
                iconFore: Color.FromArgb(220, 60, 60),
                title: "Active Alerts",
                bigOut: out _kpiAlertsBig,
                subtitleSmall: "Requires attention",
                cardHeight: CARD_HEIGHT,
                breakdown: Array.Empty<(string, Label, Color)>(),
                onClick: () => GoToNotificationsRequested?.Invoke()
            ), 1, 1);

            kpiGrid.Controls.Add(MakeKpiCard(
                iconText: "✔",
                iconBack: Color.FromArgb(234, 248, 238),
                iconFore: Color.FromArgb(28, 140, 60),
                title: "Flight Operations",
                bigOut: out _kpiOpsBig,
                subtitleSmall: "On-time performance",
                cardHeight: CARD_HEIGHT,
                breakdown: Array.Empty<(string, Label, Color)>(),
                onClick: () => GoToFlightsRequested?.Invoke() // choose where to go
            ), 2, 1);

            kpiGrid.ResumeLayout(true);
        }

        private void EnsureKpiGridLayout(int rowHeight)
        {
            // Keep whatever columns you already have, but force 2 rows to Absolute height.
            kpiGrid.RowCount = 2;
            if (kpiGrid.RowStyles.Count < 2)
            {
                kpiGrid.RowStyles.Clear();
                kpiGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
                kpiGrid.RowStyles.Add(new RowStyle(SizeType.Absolute, rowHeight));
            }
            else
            {
                kpiGrid.RowStyles[0].SizeType = SizeType.Absolute;
                kpiGrid.RowStyles[0].Height = rowHeight;

                kpiGrid.RowStyles[1].SizeType = SizeType.Absolute;
                kpiGrid.RowStyles[1].Height = rowHeight;
            }

            // Prevent auto-shrinking
            kpiGrid.AutoSize = false;
            kpiGrid.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
        }

        private Label NewValueLabel()
        {
            return new Label
            {
                AutoSize = true,
                Text = "-",
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 40, 40),
            };
        }

        private Control MakeKpiCard(
            string iconText,
            Color iconBack,
            Color iconFore,
            string title,
            out Label bigOut,
            string subtitleSmall,
            int cardHeight,
            (string label, Label value, Color valueColor)[] breakdown,
            Action onClick
        )
        {
            var card = new Guna2ShadowPanel
            {
                BackColor = Color.Transparent,
                FillColor = Color.White,
                Radius = 12,
                ShadowColor = Color.FromArgb(220, 220, 220),
                ShadowDepth = 18,
                ShadowShift = 2,
                Margin = new Padding(10),
                Padding = new Padding(18, 16, 18, 16),
                Dock = DockStyle.Fill,
                Cursor = Cursors.Hand,

                // Height is controlled by TableLayoutPanel row height, but keep a minimum.
                MinimumSize = new Size(0, cardHeight)
            };

            // icon box
            var iconBox = new Guna2Panel
            {
                Size = new Size(40, 40),
                BorderRadius = 12,
                FillColor = iconBack,
                Location = new Point(18, 16)
            };
            var iconLbl = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Text = iconText,
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = iconFore,
                BackColor = Color.Transparent
            };
            iconBox.Controls.Add(iconLbl);

            var lblTitle = new Label
            {
                AutoSize = true,
                Text = title,
                Font = new Font("Segoe UI", 9.5f),
                ForeColor = Color.FromArgb(110, 110, 110),
                Location = new Point(66, 16),
                BackColor = Color.Transparent
            };

            bigOut = new Label
            {
                AutoSize = true,
                Text = "0",
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 30),
                Location = new Point(66, 36),
                BackColor = Color.Transparent
            };

            var lblSub = new Label
            {
                AutoSize = true,
                Text = subtitleSmall ?? "",
                Font = new Font("Segoe UI", 8.7f),
                ForeColor = Color.FromArgb(140, 140, 140),
                Location = new Point(66, 66),
                BackColor = Color.Transparent
            };

            // Divider
            var divider = new Panel
            {
                Height = 1,
                BackColor = Color.FromArgb(238, 238, 238),
                Dock = DockStyle.Bottom
            };

            card.Controls.Add(iconBox);
            card.Controls.Add(lblTitle);
            card.Controls.Add(bigOut);
            card.Controls.Add(lblSub);
            card.Controls.Add(divider);

            // Breakdown lines (bottom)
            int y0 = cardHeight - 52;
            int line = 0;

            foreach (var (label, value, valueColor) in breakdown)
            {
                value.ForeColor = valueColor;

                var left = new Label
                {
                    AutoSize = true,
                    Text = label,
                    Font = new Font("Segoe UI", 8.7f),
                    ForeColor = Color.FromArgb(120, 120, 120),
                    BackColor = Color.Transparent,
                    Location = new Point(18, y0 + line * 16)
                };

                value.BackColor = Color.Transparent;
                value.Location = new Point(card.Width - 60, y0 + line * 16);

                card.Controls.Add(left);
                card.Controls.Add(value);

                card.SizeChanged += (_, __) =>
                {
                    value.Left = card.Width - value.Width - 18;
                };

                line++;
            }

            // ✅ Make EVERYTHING clickable (root + children)
            WireCardClick(card, onClick);

            return card;
        }

        private void WireCardClick(Control root, Action click)
        {
            if (root == null) return;

            root.Cursor = Cursors.Hand;
            root.Click += (_, __) => click?.Invoke();

            foreach (Control c in root.Controls)
                WireCardClick(c, click);
        }

        // =========================
        //  REAL DATA
        // =========================
        private void LoadRealData()
        {
            if (_flightRepo == null || _planeRepo == null || _crewRepo == null || _passengerRepo == null)
                return;

            var flights = InvokeList<Flight>(_flightRepo, "GetAllFlights", "GetFlights", "GetAll") ?? new List<Flight>();
            var planes = InvokeList<Plane>(_planeRepo, "GetAllPlanes", "GetPlanes", "GetAll") ?? new List<Plane>();
            var crew = InvokeList<Crew>(_crewRepo, "GetAllCrew", "GetAllCrewMembers", "GetCrew", "GetAll") ?? new List<Crew>();

            var passengerSummary = _passengerRepo.GetPassengersSummary();
            int totalPassengers = passengerSummary?.Count ?? 0;

            var now = DateTime.Now;
            int inAir = flights.Count(f => f.Departure <= now && now < f.Arrival);
            int upcoming = flights.Count(f => f.Departure > now);
            int past = flights.Count(f => f.Arrival <= now);

            _kpiFlightsBig.Text = flights.Count.ToString();
            _kpiFlightsInAir.Text = inAir.ToString();
            _kpiFlightsUpcoming.Text = upcoming.ToString();
            _kpiFlightsPast.Text = past.ToString();

            int activePlanes = planes.Count(p => ReadBool(p, "IsActive", "Active") == true);
            int inactivePlanes = planes.Count - activePlanes;

            _kpiPlanesBig.Text = activePlanes.ToString();
            _kpiPlanesActive.Text = activePlanes.ToString();
            _kpiPlanesInactive.Text = inactivePlanes.ToString();

            int assigned = crew.Count(c => c.FlightId.HasValue);
            int unassigned = crew.Count - assigned;

            _kpiCrewBig.Text = crew.Count.ToString();
            _kpiCrewAssigned.Text = assigned.ToString();
            _kpiCrewUnassigned.Text = unassigned.ToString();

            _kpiPassengersBig.Text = totalPassengers.ToString();

            _kpiPassengersUpcoming.Text = passengerSummary?.Sum(p => p.UpcomingCount).ToString() ?? "0";
            _kpiPassengersPast.Text = passengerSummary?.Sum(p => p.PastCount).ToString() ?? "0";

            int alertsCount = 0;
            if (unassigned > 0) alertsCount++;
            if (inactivePlanes > 0) alertsCount++;

            _kpiAlertsBig.Text = alertsCount.ToString();

            double ops = flights.Count == 0 ? 0 : 100.0;
            _kpiOpsBig.Text = $"{ops:0.0}%";

            RenderFlightsList(flights, now);
            RenderAlertsList(unassigned, inactivePlanes);
        }

        // =========================
        //  REPO HELPERS
        // =========================
        private static List<T> InvokeList<T>(object repo, params string[] methods)
        {
            if (repo == null) return null;

            foreach (var m in methods)
            {
                var mi = repo.GetType().GetMethod(m, BindingFlags.Public | BindingFlags.Instance);
                if (mi == null) continue;

                var res = mi.Invoke(repo, null);
                if (res is List<T> list) return list;
                if (res is IEnumerable<T> en) return en.ToList();
            }
            return null;
        }

        private static bool? ReadBool(object obj, params string[] props)
        {
            if (obj == null) return null;

            foreach (var p in props)
            {
                var pi = obj.GetType().GetProperty(p, BindingFlags.Public | BindingFlags.Instance);
                if (pi == null) continue;

                var v = pi.GetValue(obj);
                if (v is bool b) return b;

                if (v is string s)
                {
                    var t = s.Trim().ToLowerInvariant();
                    if (t == "active" || t == "available" || t == "true") return true;
                    if (t == "inactive" || t == "unavailable" || t == "false") return false;
                }
            }
            return null;
        }

        // =========================
        //  LIST RENDER
        // =========================
        private void RenderFlightsList(List<Flight> flights, DateTime now)
        {
            flightsList.SuspendLayout();
            flightsList.Controls.Clear();

            var inAir = flights.Where(f => f.Departure <= now && now < f.Arrival)
                               .OrderBy(f => f.Departure).Take(3).ToList();

            var upcoming = flights.Where(f => f.Departure > now && f.Departure <= now.AddHours(48))
                                  .OrderBy(f => f.Departure).Take(8).ToList();

            var past = flights.Where(f => f.Arrival <= now)
                              .OrderByDescending(f => f.Arrival).Take(5).ToList();

            AddSectionHeader(flightsList, $"In Air ({inAir.Count})", Color.FromArgb(28, 140, 60));
            foreach (var f in inAir)
                flightsList.Controls.Add(MakeFlightRow(f, "In Air", Color.FromArgb(220, 245, 228), Color.FromArgb(28, 140, 60)));

            AddSectionHeader(flightsList, $"Upcoming (Next 48h) ({upcoming.Count})", Color.FromArgb(35, 93, 220));
            foreach (var f in upcoming)
                flightsList.Controls.Add(MakeFlightRow(f, "Upcoming", Color.FromArgb(222, 235, 255), Color.FromArgb(35, 93, 220)));

            AddSectionHeader(flightsList, $"Past ({past.Count})", Color.FromArgb(160, 160, 160));
            foreach (var f in past)
                flightsList.Controls.Add(MakeFlightRow(f, "Completed", Color.FromArgb(235, 235, 235), Color.FromArgb(90, 90, 90)));

            flightsList.ResumeLayout(true);
            ForceRowsFullWidth(flightsList);
        }

        private void RenderAlertsList(int unassignedCrew, int inactivePlanes)
        {
            alertsList.SuspendLayout();
            alertsList.Controls.Clear();

            int active = 0;

            if (unassignedCrew > 0)
            {
                alertsList.Controls.Add(MakeAlertRow(
                    $"{unassignedCrew} Crew members unassigned",
                    "Some crew members need flight assignments",
                    back: Color.FromArgb(255, 240, 240),
                    border: Color.FromArgb(255, 210, 210),
                    fore: Color.FromArgb(220, 60, 60),
                    icon: "👥"
                ));
                active++;
            }

            if (inactivePlanes > 0)
            {
                alertsList.Controls.Add(MakeAlertRow(
                    $"{inactivePlanes} Planes inactive",
                    "Some planes are inactive and may need maintenance/status update",
                    back: Color.FromArgb(255, 248, 232),
                    border: Color.FromArgb(255, 230, 170),
                    fore: Color.FromArgb(190, 120, 0),
                    icon: "✈"
                ));
                active++;
            }

            lblAlertsRight.Text = $"{active} Active";

            alertsList.ResumeLayout(true);
            ForceRowsFullWidth(alertsList);
        }

        private void ForceRowsFullWidth(FlowLayoutPanel host)
        {
            int w = host.ClientSize.Width - host.Padding.Horizontal - SystemInformation.VerticalScrollBarWidth - 4;
            if (w < 50) return;

            foreach (Control c in host.Controls)
                c.Width = w;
        }

        private void AddSectionHeader(FlowLayoutPanel host, string text, Color dotColor)
        {
            var row = new Panel
            {
                Height = 28,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 8, 0, 6)
            };

            var dot = new Panel { Size = new Size(8, 8), Location = new Point(2, 10) };
            dot.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using var br = new SolidBrush(dotColor);
                e.Graphics.FillEllipse(br, 0, 0, 8, 8);
            };
            row.Controls.Add(dot);

            var lbl = new Label
            {
                AutoSize = true,
                Text = text,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 40, 40),
                Location = new Point(16, 6)
            };
            row.Controls.Add(lbl);

            host.Controls.Add(row);
        }

        private Control MakeFlightRow(Flight f, string badgeText, Color badgeBack, Color badgeFore)
        {
            var row = new Panel
            {
                Height = 54,
                Margin = new Padding(0, 0, 0, 6),
                BackColor = Color.Transparent
            };

            string from = f.From ?? "";
            string to = f.To ?? "";

            var lblRoute = new Label
            {
                AutoSize = true,
                Text = $"{from}  →  {to}",
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 35, 35),
                Location = new Point(0, 6)
            };

            var lblTime = new Label
            {
                AutoSize = true,
                Text = $"{f.Departure:dd MMM HH:mm} - {f.Arrival:dd MMM HH:mm}",
                Font = new Font("Segoe UI", 9.5f),
                ForeColor = Color.FromArgb(120, 120, 120),
                Location = new Point(0, 30)
            };

            var badge = MakePill(badgeText, badgeBack, badgeFore);
            row.Controls.Add(lblRoute);
            row.Controls.Add(lblTime);
            row.Controls.Add(badge);

            var planeCode = new Label
            {
                AutoSize = true,
                Text = f.PlaneIDFromDb > 0 ? $"#{f.PlaneIDFromDb}" : "",
                Font = new Font("Segoe UI", 9.5f),
                ForeColor = Color.FromArgb(140, 140, 140)
            };
            row.Controls.Add(planeCode);

            row.SizeChanged += (_, __) =>
            {
                badge.Location = new Point(row.Width - badge.Width - 70, 14);
                planeCode.Location = new Point(row.Width - planeCode.Width - 10, 18);
            };

            row.Controls.Add(new Panel
            {
                BackColor = Color.FromArgb(240, 240, 240),
                Height = 1,
                Dock = DockStyle.Bottom
            });

            return row;
        }

        private Control MakeAlertRow(string title, string sub, Color back, Color border, Color fore, string icon)
        {
            var card = new Guna2Panel
            {
                BorderRadius = 12,
                BorderThickness = 1,
                BorderColor = border,
                FillColor = back,
                Height = 76,
                Margin = new Padding(0, 0, 0, 10),
                Padding = new Padding(14, 12, 14, 12)
            };

            var ico = new Label
            {
                AutoSize = true,
                Text = icon,
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = fore,
                Location = new Point(14, 16)
            };

            var t = new Label
            {
                AutoSize = true,
                Text = title,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                ForeColor = fore,
                Location = new Point(44, 12)
            };

            var s = new Label
            {
                AutoSize = true,
                Text = sub,
                Font = new Font("Segoe UI", 9.3f),
                ForeColor = Color.FromArgb(90, 90, 90),
                Location = new Point(44, 36)
            };

            card.Controls.Add(ico);
            card.Controls.Add(t);
            card.Controls.Add(s);
            return card;
        }

        private Guna2HtmlLabel MakePill(string text, Color back, Color fore)
        {
            var b = new Guna2HtmlLabel
            {
                AutoSize = true,
                BackColor = back,
                ForeColor = fore,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Text = $"  {text}  "
            };

            void ApplyRound()
            {
                var rect = new Rectangle(0, 0, b.Width + 2, b.Height + 2);
                b.Region = RoundRegion(rect, 10);
            }

            b.HandleCreated += (_, __) => ApplyRound();
            b.SizeChanged += (_, __) => ApplyRound();

            return b;
        }

        private Region RoundRegion(Rectangle r, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return new Region(path);
        }
    }
}
