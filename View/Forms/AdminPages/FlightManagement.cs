using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.View.Interfaces;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    public partial class FlightManagement : UserControl, IFlightManagementView
    {
        // MVP events
        public event EventHandler ViewLoaded;
        public event EventHandler AddClicked;
        public event EventHandler UpdateClicked;
        public event EventHandler CancelEditClicked;
        public event Action<int> EditRequested;
        public event Action<int> DeleteRequested;
        public event Action<int>? ViewCrewRequested;
        private readonly ToolTip _tip = new ToolTip();


        public event EventHandler FilterChanged;

        // Root/layout
        private Guna2Panel root;
        private TableLayoutPanel layout;
        private Guna2ShadowPanel leftCard;
        private Guna2ShadowPanel rightCard;

        // Left inputs
        private Guna2TextBox txtFrom;
        private Guna2TextBox txtTo;
        private DateTimePicker dtDeparture;
        private DateTimePicker dtArrival;
        private Guna2ComboBox cmbPlane;
        private Guna2Button btnAddOrUpdate;
        private Guna2Button btnCancelEdit;

        // Right
        private Guna2HtmlLabel lblCount;
        private Guna2ComboBox cmbFilter;
        private FlowLayoutPanel flow;

        private List<Plane> _planes = new();
        private List<Flight> _flights = new();

        // Edit state
        public bool IsEditMode { get; set; }
        public int? EditingFlightId { get; set; }

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
            BuildUI();

            // Important: ensure presenter can load data
            Load += (_, __) => ViewLoaded?.Invoke(this, EventArgs.Empty);
        }

        // ============ IFlightManagementView (inputs) ============
        public string FromCity => txtFrom.Text;
        public string ToCity => txtTo.Text;
        public DateTime Departure => dtDeparture.Value;
        public DateTime Arrival => dtArrival.Value;

        public int? SelectedPlaneId =>
            cmbPlane.SelectedItem is PlaneItem pi ? pi.PlaneId : (int?)null;

        public string CurrentFilter => cmbFilter.SelectedItem?.ToString() ?? "All Flights";

        // ============ IFlightManagementView (outputs) ============
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
            if (cmbPlane.Items.Count > 0) cmbPlane.SelectedIndex = 0;

            RefreshFilterItems();
        }

        public void SetFlights(List<Flight> flights)
        {
            _flights = flights ?? new List<Flight>();
            lblCount.Text = $"Flights ({_flights.Count})";
            RenderCards();
            flow?.PerformLayout();
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
            if (cmbPlane.Items.Count > 0) cmbPlane.SelectedIndex = 0;
        }

        public void EnterEditMode(Flight f)
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
        }

        public void ExitEditMode()
        {
            // buttons
            btnAddOrUpdate.Text = "Add Flight";
            btnCancelEdit.Visible = false;   // or Enabled = false
            btnCancelEdit.Enabled = false;

            // clear inputs safely
            txtFrom.Text = "";
            txtTo.Text = "";

            // dates: choose sane defaults
            dtDeparture.Value = DateTime.Now;
            dtArrival.Value = DateTime.Now.AddHours(1);

            // plane combo: don't crash if empty
            if (cmbPlane.Items.Count > 0)
                cmbPlane.SelectedIndex = 0;
            else
                cmbPlane.SelectedIndex = -1;
        }


        // ========================= DOCKING FIX: TABLE LAYOUT =========================
        private void BuildUI()
        {
            BackColor = Color.FromArgb(245, 246, 250);
            Dock = DockStyle.Fill;

            Controls.Clear();

            root = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(26),
                BackColor = BackColor   // important: match page background
            };

            Controls.Add(root);

            layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.Transparent
            };

            // left fixed, right fill
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 430));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            root.Controls.Add(layout);

            leftCard = MakeCard();
            leftCard.Dock = DockStyle.Fill;
            leftCard.Margin = new Padding(0, 0, 26, 0);

            rightCard = MakeCard();
            rightCard.Dock = DockStyle.Fill;
            rightCard.Margin = new Padding(0);

            layout.Controls.Add(leftCard, 0, 0);
            layout.Controls.Add(rightCard, 1, 0);

            BuildLeftCard();
            BuildRightCard();
        }

        private Guna2ShadowPanel MakeCard()
        {
            return new Guna2ShadowPanel
            {
                BackColor = Color.Transparent,
                FillColor = Color.White,
                Radius = 14,
                ShadowColor = Color.Black,
                ShadowDepth = 18,
                ShadowShift = 2,
                Padding = new Padding(20)
            };
        }

        private void BuildLeftCard()
        {
            leftCard.Controls.Clear();

            var title = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Text = "Add / Edit Flight",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 30),
                Location = new Point(20, 18),
                AutoSize = true
            };
            leftCard.Controls.Add(title);

            int x = 26;
            int w = 360; // ✅ stable width inside left card
            int y = 70;

            leftCard.Controls.Add(MakeLabel("From *", x, y)); y += 22;
            txtFrom = MakeTextBox("Beirut", x, y, w);
            leftCard.Controls.Add(txtFrom); y += 58;

            leftCard.Controls.Add(MakeLabel("To *", x, y)); y += 22;
            txtTo = MakeTextBox("Paris", x, y, w);
            leftCard.Controls.Add(txtTo); y += 58;

            leftCard.Controls.Add(MakeLabel("Departure *", x, y)); y += 22;
            dtDeparture = MakeDateTimePicker(x, y, w);
            leftCard.Controls.Add(dtDeparture); y += 58;

            leftCard.Controls.Add(MakeLabel("Arrival *", x, y)); y += 22;
            dtArrival = MakeDateTimePicker(x, y, w);
            leftCard.Controls.Add(dtArrival); y += 58;

            leftCard.Controls.Add(MakeLabel("Plane *", x, y)); y += 22;
            cmbPlane = MakeComboBox(x, y, w);
            leftCard.Controls.Add(cmbPlane); y += 76;

            btnAddOrUpdate = new Guna2Button
            {
                Text = "Add Flight",
                BorderRadius = 12,
                FillColor = Color.FromArgb(35, 93, 220),
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(w, 48),
                Location = new Point(x, y)
            };
            btnAddOrUpdate.Click += (_, __) =>
            {
                if (!IsEditMode) AddClicked?.Invoke(this, EventArgs.Empty);
                else UpdateClicked?.Invoke(this, EventArgs.Empty);
            };
            leftCard.Controls.Add(btnAddOrUpdate);

            btnCancelEdit = new Guna2Button
            {
                Text = "Cancel Edit",
                BorderRadius = 12,
                FillColor = Color.FromArgb(235, 235, 235),
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60),
                Size = new Size(w, 44),
                Location = new Point(x, y + 56),
                Visible = false
            };
            btnCancelEdit.Click += (_, __) => ExitEditMode();
            leftCard.Controls.Add(btnCancelEdit);

            dtDeparture.ValueChanged += (_, __) =>
            {
                if (dtArrival.Value <= dtDeparture.Value)
                    dtArrival.Value = dtDeparture.Value.AddHours(2);
            };
        }

        private void BuildRightCard()
        {
            rightCard.Controls.Clear();

            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 52,
                BackColor = Color.Transparent
            };
            rightCard.Controls.Add(header);

            lblCount = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Text = "Flights (0)",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 30),
                AutoSize = true,
                Location = new Point(18, 16)
            };
            header.Controls.Add(lblCount);

            cmbFilter = MakeComboBox(0, 0, 190);
            cmbFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbFilter.Location = new Point(header.Width - 18 - cmbFilter.Width, 10);
            cmbFilter.SelectedIndexChanged += (_, __) => FilterChanged?.Invoke(this, EventArgs.Empty);
            header.Controls.Add(cmbFilter);

            header.SizeChanged += (_, __) =>
            {
                cmbFilter.Location = new Point(header.Width - 18 - cmbFilter.Width, 10);
            };

            flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = false,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(18, 60, 18, 18),
                BackColor = Color.Transparent
            };
            rightCard.Controls.Add(flow);

            flow.SizeChanged += (_, __) => RefreshCardsWidth();
        }

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

        // ========================= render =========================
        private void RenderCards()
        {
            if (flow == null) return;

            flow.SuspendLayout();
            flow.Controls.Clear();

            foreach (var f in _flights)
                flow.Controls.Add(CreateFlightCard(f));

            // ✅ Bottom spacer so the last card (buttons/shadow) isn’t clipped
            flow.Controls.Add(new Panel
            {
                Height = 52,                 // try 18–24 if needed
                Width = 1,
                Margin = new Padding(0),
                BackColor = Color.Transparent
            });

            flow.ResumeLayout();
            RefreshCardsWidth();
        }

        private void RefreshCardsWidth()
        {
            if (flow == null) return;

            int w = Math.Max(200, flow.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 8);
            foreach (Control c in flow.Controls)
            {
                c.Width = w;
            }
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
                Height = 140
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

            _tip.SetToolTip(btnCrew, "See crew");   // ✅ tooltip here

            btnCrew.Click += (_, __) => ViewCrewRequested?.Invoke(f.FlightID);
            card.Controls.Add(btnCrew);

            // info lines
            card.Controls.Add(InfoLine("Departure:", f.Departure.ToString("dd MMM yyyy  HH:mm"), 16, 56));
            card.Controls.Add(InfoLine("Arrival:", f.Arrival.ToString("dd MMM yyyy  HH:mm"), 16, 80));
            card.Controls.Add(InfoLine("Plane:", $"#{f.PlaneIDFromDb}", 334, 56));
            card.Controls.Add(InfoLine("Duration:", GetDurationText(f), 334, 80));

            // Keep badges and buttons positioned correctly when size changes
            card.SizeChanged += (_, __) =>
            {
                // Right-aligned icons: Crew, Edit, Delete
                btnDel.Location = new Point(card.Width - 48, 10);
                btnEdit.Location = new Point(card.Width - 96, 10);
                btnCrew.Location = new Point(card.Width - 144, 10);

                routeBadge.Location = new Point(title.Right + 14, 12);
                stBadge.Location = new Point(routeBadge.Right + 10, 12);
            };

            // initial placement
            btnDel.Location = new Point(card.Width - 48, 10);
            btnEdit.Location = new Point(card.Width - 96, 10);
            btnCrew.Location = new Point(card.Width - 144, 10);


            return card;
        }

        private Control InfoLine(string label, string value, int x, int y)
        {
            var p = new Panel
            {
                BackColor = Color.Transparent,
                Location = new Point(x, y),
                Size = new Size(320, 22)
            };

            var l1 = new Label
            {
                Text = label,
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(120, 120, 120),
                Location = new Point(0, 2)
            };

            var l2 = new Label
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

        private Label MakeLabel(string text, int x, int y) => new Label
        {
            Text = text,
            AutoSize = true,
            Font = new Font("Segoe UI", 9.5F),
            ForeColor = Color.FromArgb(70, 70, 70),
            Location = new Point(x, y)
        };

        private Guna2TextBox MakeTextBox(string placeholder, int x, int y, int w) => new Guna2TextBox
        {
            BorderRadius = 10,
            Font = new Font("Segoe UI", 10F),
            Location = new Point(x, y),
            Size = new Size(w, 42),
            PlaceholderText = placeholder,
            FocusedState = { BorderColor = Color.FromArgb(35, 93, 220) },
            HoverState = { BorderColor = Color.FromArgb(35, 93, 220) }
        };

        private Guna2ComboBox MakeComboBox(int x, int y, int w) => new Guna2ComboBox
        {
            BackColor = Color.Transparent,
            BorderRadius = 10,
            DrawMode = DrawMode.OwnerDrawFixed,
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = new Font("Segoe UI", 10F),
            ForeColor = Color.FromArgb(60, 60, 60),
            ItemHeight = 36,
            Location = new Point(x, y),
            Size = new Size(w, 42),
            FocusedState = { BorderColor = Color.FromArgb(35, 93, 220) }
        };

        private DateTimePicker MakeDateTimePicker(int x, int y, int w) => new DateTimePicker
        {
            Location = new Point(x, y),
            Size = new Size(w, 42),
            Font = new Font("Segoe UI", 10F),
            Format = DateTimePickerFormat.Custom,
            CustomFormat = "dd MMM yyyy  HH:mm",
            ShowUpDown = true
        };

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
                // +2 avoids “cropped” corners/edges on some DPI/font renders
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
            if (cmbPlane.Items.Count > 0) cmbPlane.SelectedIndex = 0;
        }
    }
}
