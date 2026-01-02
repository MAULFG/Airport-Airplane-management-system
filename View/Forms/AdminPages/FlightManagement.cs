using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    public partial class FlightManagement : UserControl, IFlightManagementView
    {
        // MVP events
        public event Action<int>? PlaneScheduleRequested;
        public event EventHandler ViewLoaded;
        public event EventHandler AddClicked;
        public event EventHandler UpdateClicked;
        public event EventHandler CancelEditClicked;
        public event Action<int> EditRequested;
        public event Action<int> DeleteRequested;
        public event Action<int>? ViewCrewRequested;
        public event EventHandler FilterChanged;
        public event Action<int> PlaneChanged;

        // Internal
        private Guna2Panel panelScheduleHost;
        private readonly ToolTip _tip = new ToolTip();
        private List<Plane> _planes = new();
        private List<Flight> _flights = new();

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEditMode { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int? EditingFlightId { get; set; }

        // Prices
        public decimal EconomyPrice => ParsePrice(txtEconomyPrice?.Text);
        public decimal BusinessPrice => ParsePrice(txtBusinessPrice?.Text);
        public decimal FirstPrice => ParsePrice(txtFirstPrice?.Text);

        private decimal ParsePrice(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return 0m;
            return decimal.TryParse(s.Trim(), out var v) ? v : 0m;
        }

        private sealed class PlaneItem
        {
            public int PlaneId { get; }
            public string Text { get; }
            public PlaneItem(int planeId, string text) { PlaneId = planeId; Text = text; }
            public override string ToString() => Text;
        }

        public FlightManagement()
        {
            InitializeComponent();

            // MVP load
            Load += (_, __) => ViewLoaded?.Invoke(this, EventArgs.Empty);

            // Flicker-free panel for schedule
            panelScheduleHost = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Visible = false
            };
            Controls.Add(panelScheduleHost);

            // Flicker-free FlowLayoutPanel
            typeof(FlowLayoutPanel).InvokeMember(
                "DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetProperty,
                null,
                flow,
                new object[] { true });

            // Add/Update
            btnAddOrUpdate.Click += (_, __) =>
            {
                if (!IsEditMode) AddClicked?.Invoke(this, EventArgs.Empty);
                else UpdateClicked?.Invoke(this, EventArgs.Empty);
            };

            // Cancel
            btnCancelEdit.Click += (_, __) => CancelEditClicked?.Invoke(this, EventArgs.Empty);

            // Filter
            cmbFilter.SelectedIndexChanged += (_, __) => FilterChanged?.Invoke(this, EventArgs.Empty);

            rightHeader.SizeChanged += (_, __) =>
            {
                cmbFilter.Location = new Point(rightHeader.Width - 18 - cmbFilter.Width, 10);
            };
            cmbFilter.Location = new Point(rightHeader.Width - 18 - cmbFilter.Width, 10);

            // Date logic
            dtDeparture.ValueChanged += (_, __) =>
            {
                if (dtArrival.Value <= dtDeparture.Value)
                    dtArrival.Value = dtDeparture.Value.AddHours(2);
            };

            // Resize cards
            flow.SizeChanged += (_, __) => RefreshCardsWidth();

            // Plane selection
            cmbPlane.SelectionChangeCommitted += (_, __) =>
            {
                if (cmbPlane.SelectedItem is PlaneItem pi)
                {
                    PlaneScheduleRequested?.Invoke(pi.PlaneId);
                    PlaneChanged?.Invoke(pi.PlaneId);
                }
            };
        }

        // ======== IFlightManagementView ========
        public string FromCity => txtFrom.Text;
        public string ToCity => txtTo.Text;
        public DateTime Departure => dtDeparture.Value;
        public DateTime Arrival => dtArrival.Value;
        public int? SelectedPlaneId =>
            cmbPlane.SelectedItem is PlaneItem pi ? pi.PlaneId : (int?)null;
        public string CurrentFilter => cmbFilter.SelectedItem?.ToString() ?? "All Flights";

        public void SetPlanes(List<Plane> planes)
        {
            _planes = planes ?? new List<Plane>();

            cmbPlane.Items.Clear();
            foreach (var p in _planes.OrderBy(x => x.PlaneID))
            {
                string type = p.GetType().Name;
                string text = $"#{p.PlaneID}  |  {type}  |  {p.Status}";
                cmbPlane.Items.Add(new PlaneItem(p.PlaneID, text));
            }

            if (cmbPlane.Items.Count > 0)
                cmbPlane.SelectedIndex = 0;

            RefreshFilterItems();

            if (cmbPlane.SelectedItem is PlaneItem pi0)
                PlaneChanged?.Invoke(pi0.PlaneId);
        }

        public void SetFlights(List<Flight> flights)
        {
            _flights = flights ?? new List<Flight>();
            lblCount.Text = $"Flights ({_flights.Count})";
            RenderCards();
        }

        public void ShowInfo(string message) =>
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public void ShowError(string message) =>
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public bool Confirm(string message) =>
            MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;

        public void ClearForm()
        {
            txtFrom.Clear();
            txtTo.Clear();
            dtDeparture.Value = DateTime.Now.AddHours(2);
            dtArrival.Value = DateTime.Now.AddHours(4);

            txtEconomyPrice.Text = "";
            txtBusinessPrice.Text = "";
            txtFirstPrice.Text = "";

            if (cmbPlane.Items.Count > 0) cmbPlane.SelectedIndex = 0;
        }

        public void EnterEditMode(Flight f, Dictionary<string, decimal> seatPrices)
        {
            IsEditMode = true;
            EditingFlightId = f.FlightID;

            txtFrom.Text = f.From;
            txtTo.Text = f.To;

            dtDeparture.Value = f.Departure;


            dtArrival.Value = f.Arrival;

            SelectPlaneInDropdown(f.PlaneIDFromDb);

            btnAddOrUpdate.Text = "Update Flight";
            btnCancelEdit.Visible = true;
            btnCancelEdit.Enabled = true;

            txtEconomyPrice.Text = seatPrices.TryGetValue("economy", out var eco) ? eco.ToString("0.00") : "";
            txtBusinessPrice.Text = seatPrices.TryGetValue("business", out var bus) ? bus.ToString("0.00") : "";

            if (seatPrices.TryGetValue("vip", out var vip))
                txtFirstPrice.Text = vip.ToString("0.00");
            else if (seatPrices.TryGetValue("first", out var first))
                txtFirstPrice.Text = first.ToString("0.00");
            else
                txtFirstPrice.Text = "";
        }

        public void ExitEditMode()
        {
            IsEditMode = false;
            EditingFlightId = null;

            btnAddOrUpdate.Text = "Add Flight";
            btnCancelEdit.Visible = false;
            btnCancelEdit.Enabled = false;

            txtFrom.Text = "";
            txtTo.Text = "";
            dtDeparture.Value = DateTime.Now;
            dtArrival.Value = DateTime.Now.AddHours(1);

            txtEconomyPrice.Text = "";
            txtBusinessPrice.Text = "";
            txtFirstPrice.Text = "";

            if (cmbPlane.Items.Count > 0)
                cmbPlane.SelectedIndex = 0;
            else
                cmbPlane.SelectedIndex = -1;
        }

        // ======== Filter ========
        private void RefreshFilterItems()
        {
            string current = cmbFilter.SelectedItem?.ToString() ?? "All Flights";

            cmbFilter.Items.Clear();
            cmbFilter.Items.Add("All Flights");
            cmbFilter.Items.Add("Upcoming");
            cmbFilter.Items.Add("Past");

            foreach (var p in _planes.OrderBy(pp => pp.PlaneID))
                cmbFilter.Items.Add($"Plane #{p.PlaneID}");

            if (cmbFilter.Items.Cast<object>().Any(i => i.ToString() == current))
                cmbFilter.SelectedItem = current;
            else
                cmbFilter.SelectedIndex = 0;
        }

        // ======== Cards ========
        private void RenderCards()
        {
            if (flow == null) return;

            flow.SuspendLayout();
            flow.Controls.Clear();

            foreach (var f in _flights)
            {
                var card = CreateFlightCard(f);
                card.Width = flow.ClientSize.Width - flow.Padding.Horizontal;
                flow.Controls.Add(card);
            }

            // Add invisible spacer to give room at bottom
            var spacer = new Panel
            {
                Height = 100, // adjust the blank space you want
                Width = 1,
                BackColor = Color.Transparent
            };
            flow.Controls.Add(spacer);

            flow.ResumeLayout(true);
        }


        private void RefreshCardsWidth()
        {
            if (flow == null) return;

            int w = flow.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 10; // adjust for scrollbar
            foreach (Control c in flow.Controls)
                c.Width = w;
        }

        private Control CreateFlightCard(Flight f)
        {
            var card = new Guna2ShadowPanel
            {
                BackColor = Color.Transparent,
                FillColor = Color.White,
                Radius = 12,
                Padding = new Padding(16, 14, 16, 14),
                Margin = new Padding(0, 0, 0, 12),
                Height = 120,
                ShadowDepth = 100,
                ShadowShift = 5,
                ShadowColor = Color.Black
            };

            var title = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Text = $"Flight #{f.FlightID}",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 30),
                AutoSize = true,
                Location = new Point(16, 14)
            };
            card.Controls.Add(title);

            var routeBadge = Badge($"{f.From} → {f.To}", Color.FromArgb(222, 235, 255), Color.FromArgb(35, 93, 220));
            routeBadge.Location = new Point(title.Right + 14, 12);
            card.Controls.Add(routeBadge);

            var statusText = GetFlightStatusText(f);
            var (stBack, stFore) = GetStatusColors(statusText);
            var stBadge = Badge(statusText, stBack, stFore);
            stBadge.Location = new Point(routeBadge.Right + 10, 12);
            card.Controls.Add(stBadge);

            var btnEdit = new Guna2Button
            {
                Text = "✎",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 93, 220),
                FillColor = Color.Transparent,
                BorderRadius = 8,
                Size = new Size(38, 34),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
            };
            btnEdit.Click += (_, __) => EditRequested?.Invoke(f.FlightID);
            card.Controls.Add(btnEdit);

            var btnDel = new Guna2Button
            {
                Text = "🗑",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(220, 45, 45),
                FillColor = Color.Transparent,
                BorderRadius = 8,
                Size = new Size(38, 34),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
            };
            btnDel.Click += (_, __) => DeleteRequested?.Invoke(f.FlightID);
            card.Controls.Add(btnDel);

            var btnCrew = new Guna2Button
            {
                Text = "👥",
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(20, 130, 80),
                FillColor = Color.Transparent,
                BorderRadius = 8,
                Size = new Size(38, 34),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
            };
            _tip.SetToolTip(btnCrew, "See crew");
            btnCrew.Click += (_, __) => ViewCrewRequested?.Invoke(f.FlightID);
            card.Controls.Add(btnCrew);

            card.Controls.Add(InfoLine("Departure:", f.Departure.ToString("dd MMM yyyy  HH:mm"), 16, 56));
            card.Controls.Add(InfoLine("Arrival:", f.Arrival.ToString("dd MMM yyyy  HH:mm"), 16, 80));
            card.Controls.Add(InfoLine("Plane:", $"#{f.PlaneIDFromDb}", 334, 56));
            card.Controls.Add(InfoLine("Duration:", GetDurationText(f), 334, 80));

            card.SizeChanged += (_, __) =>
            {
                btnDel.Location = new Point(card.Width - 48, 10);
                btnEdit.Location = new Point(card.Width - 96, 10);
                btnCrew.Location = new Point(card.Width - 144, 10);

                routeBadge.Location = new Point(title.Right + 14, 12);
                stBadge.Location = new Point(routeBadge.Right + 10, 12);
            };

            btnDel.Location = new Point(card.Width - 48, 10);
            btnEdit.Location = new Point(card.Width - 96, 10);
            btnCrew.Location = new Point(card.Width - 144, 10);

            return card;
        }

        private Control InfoLine(string label, string value, int x, int y)
        {
            var p = new Guna2Panel
            {
                BackColor = Color.Transparent,
                Location = new Point(x, y),
                Size = new Size(320, 22)
            };

            var l1 = new Guna2HtmlLabel
            {
                Text = label,
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(120, 120, 120),
                Location = new Point(0, 2)
            };

            var l2 = new Guna2HtmlLabel
            {
                Text = value ?? "",
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(l1.Right + 6, 2)
            };

            p.Controls.Add(l1);
            p.Controls.Add(l2);

            p.SizeChanged += (_, __) => l2.Location = new Point(l1.Right + 6, 2);

            return p;
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

        private Guna2HtmlLabel Badge(string text, Color back, Color fore)
        {
            if (string.IsNullOrWhiteSpace(text)) text = "-";

            var b = new Guna2HtmlLabel
            {
                AutoSize = true,
                BackColor = back,
                ForeColor = fore,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
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

        private static string GetFlightStatusText(Flight f)
        {
            var now = DateTime.Now;
            if (now < f.Departure) return "Upcoming";
            if (now >= f.Departure && now <= f.Arrival) return "In Air";
            return "Past";
        }

        private static (Color back, Color fore) GetStatusColors(string status)
        {
            switch (status)
            {
                case "Upcoming":
                    return (Color.FromArgb(222, 235, 255), Color.FromArgb(35, 93, 220));
                case "In Air":
                    return (Color.FromArgb(220, 245, 228), Color.FromArgb(28, 140, 60));
                default:
                    return (Color.FromArgb(235, 235, 235), Color.FromArgb(90, 90, 90));
            }
        }

        private static string GetDurationText(Flight f)
        {
            var d = f.Arrival - f.Departure;
            if (d.TotalMinutes < 0) return "-";
            return $"{(int)d.TotalHours}h {d.Minutes}m";
        }

        private void SelectPlaneInDropdown(int planeId)
        {
            for (int i = 0; i < cmbPlane.Items.Count; i++)
            {
                if (cmbPlane.Items[i] is PlaneItem pi && pi.PlaneId == planeId)
                {
                    cmbPlane.SelectedIndex = i;
                    return;
                }
            }

            if (cmbPlane.Items.Count > 0)
                cmbPlane.SelectedIndex = 0;
        }

        public void SetTimes(DateTime dep, DateTime arr)
        {
            dtDeparture.Value = dep;
            dtArrival.Value = arr;
        }

        public void ShowDockedSchedule(Control schedule)
        {
            panelScheduleHost.Controls.Clear();
            schedule.Dock = DockStyle.Fill;
            panelScheduleHost.Controls.Add(schedule);
            panelScheduleHost.Visible = true;
        }

        public void HideDockedSchedule()
        {
            panelScheduleHost.Controls.Clear();
            panelScheduleHost.Visible = false;
        }

        public void PrepareNewFlight(int planeId, DateTime dep, DateTime arr)
        {
            SelectPlaneInDropdown(planeId);
            SetTimes(dep, arr);
        }

        public void SetSeatClassAvailability(HashSet<string> classesLower)
        {
            classesLower ??= new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            bool hasEco = classesLower.Contains("economy");
            bool hasBus = classesLower.Contains("business");
            bool hasVip = classesLower.Contains("vip");
            bool hasFirst = classesLower.Contains("first") || classesLower.Contains("first class");

            rowEconomy.Visible = hasEco;
            rowBusiness.Visible = hasBus;
            rowFirst.Visible = hasVip || hasFirst;
        }


        public void SetFirstLabel(string text)
        {
            // lblFirst must exist in the designer (the label that currently shows "First")
            lblFirst.Text = text;
        }




        private void FlightManagement_Load(object sender, EventArgs e)
        {

        }

        private void flow_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblCount_Click(object sender, EventArgs e)
        {

        }

        private void txtTo_TextChanged(object sender, EventArgs e)
        {




        }

        private void cmbPlane_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rowFirst_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddOrUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
