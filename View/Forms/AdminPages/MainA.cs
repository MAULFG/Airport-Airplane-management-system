using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    public partial class MainA : UserControl, IMainAView
    {
        // KPI value labels (Presenter updates)
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

            // Ensure list layout behaves like Figma
            flightsList.WrapContents = false;
            flightsList.FlowDirection = FlowDirection.TopDown;
            flightsList.AutoScroll = true;
            flightsList.HorizontalScroll.Enabled = false;
            flightsList.HorizontalScroll.Visible = false;

            alertsList.WrapContents = false;
            alertsList.FlowDirection = FlowDirection.TopDown;
            alertsList.AutoScroll = true;
            alertsList.HorizontalScroll.Enabled = false;
            alertsList.HorizontalScroll.Visible = false;

            flightsList.SizeChanged += (_, __) => ForceRowsFullWidth(flightsList);
            alertsList.SizeChanged += (_, __) => ForceRowsFullWidth(alertsList);

            // Build UI once (View responsibility)
            if (!_built)
            {
                BuildFigmaUI();
                _built = true;
            }

            // ✅ Make the TOP (yellow) "View all flights →" clickable
            WireTopViewAllFlightsLink();
        }

        // =========================
        //  IMainAView methods (Presenter calls these)
        // =========================
        public void ShowKpis(MainAKpiDto dto)
        {
            if (!_built)
            {
                BuildFigmaUI();
                _built = true;
            }

            _kpiFlightsBig.Text = dto.TotalFlights.ToString();
            _kpiFlightsInAir.Text = dto.FlightsInAir.ToString();
            _kpiFlightsUpcoming.Text = dto.FlightsUpcoming.ToString();
            _kpiFlightsPast.Text = dto.FlightsPast.ToString();

            _kpiPlanesBig.Text = dto.TotalPlanes.ToString();
            _kpiPlanesActive.Text = dto.ActivePlanes.ToString();
            _kpiPlanesInactive.Text = dto.InactivePlanes.ToString();

            _kpiCrewBig.Text = dto.TotalCrew.ToString();
            _kpiCrewAssigned.Text = dto.CrewAssigned.ToString();
            _kpiCrewUnassigned.Text = dto.CrewUnassigned.ToString();

            _kpiPassengersBig.Text = dto.TotalPassengers.ToString();
            _kpiPassengersUpcoming.Text = dto.PassengersUpcoming.ToString();
            _kpiPassengersPast.Text = dto.PassengersPast.ToString();

            _kpiAlertsBig.Text = dto.ActiveAlerts.ToString();
            _kpiOpsBig.Text = dto.OpsText ?? "0.0%";
        }

        public void ShowFlights(MainAFlightsDto dto)
        {
            if (dto == null) return;

            flightsList.SuspendLayout();
            flightsList.Controls.Clear();

            // ✅ NO showViewAll here => removes the duplicate red-circle link
            AddSectionHeader(
                flightsList,
                $"In Air ({dto.InAir?.Count ?? 0})",
                Color.FromArgb(28, 140, 60)
            );

            foreach (var f in dto.InAir ?? new List<Flight>())
                flightsList.Controls.Add(MakeFlightRow(f, "In Air", Color.FromArgb(220, 245, 228), Color.FromArgb(28, 140, 60)));

            AddSectionHeader(flightsList, $"Upcoming (Next 48h) ({dto.Upcoming48h?.Count ?? 0})", Color.FromArgb(35, 93, 220));
            foreach (var f in dto.Upcoming48h ?? new List<Flight>())
                flightsList.Controls.Add(MakeFlightRow(f, "Upcoming", Color.FromArgb(222, 235, 255), Color.FromArgb(35, 93, 220)));

            AddSectionHeader(flightsList, $"Past ({dto.Past?.Count ?? 0})", Color.FromArgb(160, 160, 160));
            foreach (var f in dto.Past ?? new List<Flight>())
                flightsList.Controls.Add(MakeFlightRow(f, "Completed", Color.FromArgb(235, 235, 235), Color.FromArgb(90, 90, 90)));

            flightsList.ResumeLayout(true);
            ForceRowsFullWidth(flightsList);
        }

        public void ShowAlerts(MainAAlertsDto dto)
        {
            if (dto == null) return;

            alertsList.SuspendLayout();
            alertsList.Controls.Clear();

            int active = 0;

            if (dto.UnassignedCrew > 0)
            {
                alertsList.Controls.Add(MakeAlertRow(
                    $"{dto.UnassignedCrew} Crew members unassigned",
                    "Some crew members need flight assignments",
                    back: Color.FromArgb(255, 240, 240),
                    border: Color.FromArgb(255, 210, 210),
                    fore: Color.FromArgb(220, 60, 60),
                    icon: "👥"
                ));
                active++;
            }

            if (dto.InactivePlanes > 0)
            {
                alertsList.Controls.Add(MakeAlertRow(
                    $"{dto.InactivePlanes} Planes inactive",
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

            // hard-disable horizontal scroll
            alertsList.HorizontalScroll.Enabled = false;
            alertsList.HorizontalScroll.Visible = false;
            alertsList.AutoScrollMinSize = new Size(0, 0);
        }

        // =========================
        //  UI BUILD (FIGMA)
        // =========================
        private void BuildFigmaUI()
        {
            kpiGrid.SuspendLayout();
            kpiGrid.Controls.Clear();

            const int CARD_HEIGHT = 165;

            EnsureKpiGridLayout(rowHeight: CARD_HEIGHT + 18);

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
                onClick: () => GoToFlightsRequested?.Invoke()
            ), 2, 1);

            kpiGrid.ResumeLayout(true);
        }

        private void EnsureKpiGridLayout(int rowHeight)
        {
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
            const int PAD = 18;

            var card = new Guna2ShadowPanel
            {
                BackColor = Color.Transparent,
                FillColor = Color.White,
                Radius = 12,
                ShadowColor = Color.FromArgb(220, 220, 220),
                ShadowDepth = 18,
                ShadowShift = 2,
                Margin = new Padding(10),
                Padding = new Padding(PAD, 16, PAD, 16),
                Dock = DockStyle.Fill,
                Cursor = Cursors.Hand,
                MinimumSize = new Size(0, cardHeight)
            };

            // Bottom area where breakdown goes (ABOVE divider)
            int bottomHeight = breakdown != null && breakdown.Length > 0
                ? (breakdown.Length * 16 + 6)
                : 0;

            var bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = bottomHeight,
                BackColor = Color.Transparent
            };

            var divider = new Panel
            {
                Height = 1,
                BackColor = Color.FromArgb(238, 238, 238),
                Dock = DockStyle.Bottom
            };

            // (Add divider first, then bottomPanel => divider sits ABOVE bottomPanel)
            card.Controls.Add(bottomPanel);
            card.Controls.Add(divider);

            var iconBox = new Guna2Panel
            {
                Size = new Size(40, 40),
                BorderRadius = 12,
                FillColor = iconBack,
                Location = new Point(PAD, 16)
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
                Location = new Point(PAD + 48, 16),
                BackColor = Color.Transparent
            };

            bigOut = new Label
            {
                AutoSize = true,
                Text = "0",
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 30),
                Location = new Point(PAD + 48, 36),
                BackColor = Color.Transparent
            };

            var lblSub = new Label
            {
                AutoSize = true,
                Text = subtitleSmall ?? "",
                Font = new Font("Segoe UI", 8.7f),
                ForeColor = Color.FromArgb(140, 140, 140),
                BackColor = Color.Transparent
            };

            card.Controls.Add(iconBox);
            card.Controls.Add(lblTitle);
            card.Controls.Add(bigOut);
            card.Controls.Add(lblSub);

            // -------- Breakdown (now ABOVE the divider) --------
            if (breakdown != null && breakdown.Length > 0)
            {
                for (int line = 0; line < breakdown.Length; line++)
                {
                    var (label, value, valueColor) = breakdown[line];
                    value.ForeColor = valueColor;
                    value.BackColor = Color.Transparent;

                    var left = new Label
                    {
                        AutoSize = true,
                        Text = label,
                        Font = new Font("Segoe UI", 8.7f),
                        ForeColor = Color.FromArgb(120, 120, 120),
                        BackColor = Color.Transparent,
                        Location = new Point(PAD, 3 + line * 16)
                    };

                    value.Location = new Point(bottomPanel.Width - value.Width - PAD, 3 + line * 16);

                    bottomPanel.Controls.Add(left);
                    bottomPanel.Controls.Add(value);

                    bottomPanel.SizeChanged += (_, __) =>
                    {
                        value.Left = bottomPanel.Width - value.Width - PAD;
                    };
                }

                // keep subtitle where it was for breakdown cards (usually empty anyway)
                lblSub.Location = new Point(PAD + 48, 66);
            }
            else
            {
                // -------- Cards without breakdown: subtitle should sit JUST ABOVE divider --------
                lblSub.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;

                void PositionSub()
                {
                    // place directly above divider with a small gap
                    int y = divider.Top - lblSub.Height - 10;
                    if (y < 70) y = 70; // safety
                    lblSub.Location = new Point(PAD + 48, y);
                }

                card.SizeChanged += (_, __) => PositionSub();
                card.HandleCreated += (_, __) => PositionSub();
            }

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
        //  LIST RENDER HELPERS
        // =========================
        private void ForceRowsFullWidth(FlowLayoutPanel host)
        {
            int w = host.ClientSize.Width - host.Padding.Horizontal;

            if (host.VerticalScroll.Visible)
                w -= SystemInformation.VerticalScrollBarWidth;

            // extra safety to kill horizontal scroll
            w -= 18;
            if (w < 50) return;

            foreach (Control c in host.Controls)
            {
                c.Width = w;
            }

            host.HorizontalScroll.Enabled = false;
            host.HorizontalScroll.Visible = false;
            host.AutoScrollMinSize = new Size(0, 0);
        }

        private void AddSectionHeader(
            FlowLayoutPanel host,
            string text,
            Color dotColor
        )
        {
            var row = new Panel
            {
                Height = 28,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 8, 0, 6),
                Width = host.ClientSize.Width
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

            string from = f?.From ?? "";
            string to = f?.To ?? "";

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
                Text = (f != null && f.PlaneIDFromDb > 0) ? $"#{f.PlaneIDFromDb}" : "",
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
                Margin = new Padding(0, 0, 0, 10),
                Padding = new Padding(14, 12, 14, 12),

                // allow growth to fit wrapped text
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                MinimumSize = new Size(0, 78)
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 2,
                RowCount = 2,
                BackColor = Color.Transparent
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 30));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            var ico = new Label
            {
                AutoSize = true,
                Text = icon,
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = fore,
                Margin = new Padding(0, 2, 0, 0)
            };

            var t = new Label
            {
                AutoSize = true,
                Text = title,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                ForeColor = fore,
                Margin = new Padding(0, 0, 0, 2),
                MaximumSize = new Size(1000, 0)
            };

            var s = new Label
            {
                AutoSize = true,
                Text = sub,
                Font = new Font("Segoe UI", 9.3f),
                ForeColor = Color.FromArgb(90, 90, 90),
                Margin = new Padding(0, 0, 0, 0),
                MaximumSize = new Size(1000, 0)
            };

            layout.Controls.Add(ico, 0, 0);
            layout.SetRowSpan(ico, 2);

            layout.Controls.Add(t, 1, 0);
            layout.Controls.Add(s, 1, 1);

            card.Controls.Add(layout);

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

        // =========================
        //  TOP "View all flights →" (yellow) click hook
        // =========================
        private void WireTopViewAllFlightsLink()
        {
            // We don't assume the designer name.
            // We find the first Label/LinkLabel that matches the header text.
            var target = FindByText(this, "View all flights →");

            if (target != null)
            {
                target.Cursor = Cursors.Hand;
                target.Click -= TopViewAll_Click; // avoid double hook if constructor runs again
                target.Click += TopViewAll_Click;
            }
        }

        private void TopViewAll_Click(object sender, EventArgs e)
        {
            GoToFlightsRequested?.Invoke();
        }

        private static Control FindByText(Control root, string text)
        {
            foreach (Control child in root.Controls)
            {
                if (child is Label || child is LinkLabel)
                {
                    var t = (child.Text ?? "").Trim();
                    if (t == text.Trim()) return child;
                }

                var found = FindByText(child, text);
                if (found != null) return found;
            }
            return null;
        }
    }
}
