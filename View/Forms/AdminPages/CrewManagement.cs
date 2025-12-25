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


    public partial class CrewManagement : UserControl, ICrewManagementView


    {
        // ========================= EVENTS (MVP) =========================
        public event Action ViewLoaded;
        public event Action AddOrUpdateClicked;
        public event Action CancelEditClicked;
        public event Action<Crew> EditRequested;
        public event Action<Crew> DeleteRequested;
        public event Action FilterChanged;

        // ========================= UI FIELDS =========================

        private Guna2Panel root;
        private Guna2ShadowPanel leftCard;
        private Guna2ShadowPanel rightCard;

        // Left inputs
        private Guna2TextBox txtFullName;
        private Guna2ComboBox cmbRole;
        private Guna2TextBox txtEmail;
        private Guna2TextBox txtPhone;
        private Guna2ComboBox cmbStatus;
        private Guna2ComboBox cmbFlight;
        private Guna2Button btnAddOrUpdate;
        private Guna2Button btnCancelEdit;

        // Right
        private Guna2HtmlLabel lblCount;
        private Guna2ComboBox cmbFilter;
        private FlowLayoutPanel flow;

        // Data cached in the view for rendering only
        private List<Crew> allCrew = new List<Crew>();

        // Presenter tells the view when edit mode changes
        private bool _uiEditMode = false;

        // Dropdown item
        private sealed class FlightItem
        {
            public int? FlightId { get; }
            public string Text { get; }

            public FlightItem(int? flightId, string text)
            {
                FlightId = flightId;
                Text = text;
            }

            public override string ToString() => Text;
        }

        // ========================= ICrewManagementView (INPUTS) =========================

        public string FullName => txtFullName?.Text?.Trim() ?? "";
        public string Email => txtEmail?.Text?.Trim() ?? "";
        public string Phone => txtPhone?.Text?.Trim() ?? "";
        public string Role => cmbRole?.SelectedItem?.ToString() ?? "";
        public string Status => cmbStatus?.SelectedItem?.ToString() ?? "Active";

        public int? SelectedFlightId
        {
            get
            {
                if (cmbFlight?.SelectedItem is FlightItem fi)
                    return fi.FlightId;
                return null;
            }
        }

        // ========================= CTOR =========================

        public CrewManagement()
        {
            InitializeComponent();
            BuildUI();
            this.Load += (_, __) => ViewLoaded?.Invoke();
            this.Load += (_, __) => MessageBox.Show("Crew View Loaded");

        }


        // ========================= ICrewManagementView (OUTPUTS) =========================

        public void RenderFlights(List<Flight> flights)
        {
            cmbFlight.Items.Clear();
            cmbFlight.Items.Add(new FlightItem(null, "Unassigned"));

            if (flights != null)
            {
                foreach (var f in flights.OrderByDescending(ff => ff.FlightID))
                {
                    string text = $"#{f.FlightID}  |  {f.From} → {f.To}  |  {f.Departure:dd MMM HH:mm}";
                    cmbFlight.Items.Add(new FlightItem(f.FlightID, text));
                }
            }

            cmbFlight.SelectedIndex = 0;

            RefreshFilterItems();
            ApplyStatusRulesToFlightUI();
        }

        public void RenderCrew(List<Crew> crew)
        {
            allCrew = crew ?? new List<Crew>();
            RenderCards();
        }

        public void SetEditMode(bool editing)
        {
            _uiEditMode = editing;

            if (!editing)
            {
                // reset inputs (Presenter owns which crew was edited; view just clears UI)
                txtFullName.Clear();
                txtEmail.Clear();
                txtPhone.Clear();
                cmbRole.SelectedIndex = 0;
                cmbStatus.SelectedIndex = 0;
                if (cmbFlight.Items.Count > 0) cmbFlight.SelectedIndex = 0;

                btnAddOrUpdate.Text = "Add Crew Member";
                btnCancelEdit.Visible = false;
            }
            else
            {
                btnAddOrUpdate.Text = "Update Crew Member";
                btnCancelEdit.Visible = true;
            }

            ApplyStatusRulesToFlightUI();
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowInfo(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // OPTIONAL helper for Presenter:
        // Call this from Presenter after EnterEditMode(Crew c)
        // if you want the view to fill fields.
        public void FillFormFromCrew(Crew c)
        {
            if (c == null) return;

            txtFullName.Text = c.FullName;
            txtEmail.Text = c.Email;
            txtPhone.Text = c.Phone;

            int roleIndex = cmbRole.Items.Cast<object>().ToList()
                .FindIndex(o => o.ToString().Equals(c.Role, StringComparison.OrdinalIgnoreCase));
            cmbRole.SelectedIndex = roleIndex >= 0 ? roleIndex : 0;

            cmbStatus.SelectedIndex =
                string.Equals(c.Status, "inactive", StringComparison.OrdinalIgnoreCase) ? 1 : 0;

            SelectFlightInDropdown(c.FlightId);

            ApplyStatusRulesToFlightUI();
        }

        // ========================= UI BUILD =========================

        private void BuildUI()
        {
            BackColor = Color.FromArgb(245, 246, 250);
            Dock = DockStyle.Fill;

            Controls.Clear();

            root = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(26),
                BackColor = Color.Transparent
            };
            Controls.Add(root);

            leftCard = MakeCard();
            leftCard.Size = new Size(430, 650);
            leftCard.Location = new Point(26, 26);
            leftCard.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom;
            root.Controls.Add(leftCard);

            rightCard = MakeCard();
            rightCard.Location = new Point(leftCard.Right + 26, 26);
            rightCard.Size = new Size(900, 650);
            rightCard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            root.Controls.Add(rightCard);

            BuildLeftCard();
            BuildRightCard();

            Resize += (_, __) =>
            {
                rightCard.Location = new Point(leftCard.Right + 26, 26);
                rightCard.Size = new Size(Width - rightCard.Left - 26, leftCard.Height);

                flow.Size = new Size(rightCard.Width - 36, rightCard.Height - 86);
                RenderCards();
            };
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
            var title = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Text = "Add / Edit Crew Member",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 30),
                Location = new Point(20, 18),
                AutoSize = true
            };
            leftCard.Controls.Add(title);

            int x = 26;
            int w = leftCard.Width - 52;
            int y = 70;

            leftCard.Controls.Add(MakeLabel("Full Name *", x, y)); y += 22;
            txtFullName = MakeTextBox("", x, y, w);
            leftCard.Controls.Add(txtFullName); y += 58;

            leftCard.Controls.Add(MakeLabel("Role *", x, y)); y += 22;
            cmbRole = MakeComboBox(x, y, w);
            cmbRole.Items.AddRange(new object[] { "Captain", "First Officer", "Flight Attendant", "Purser", "Flight Engineer" });
            cmbRole.SelectedIndex = 0;
            leftCard.Controls.Add(cmbRole); y += 58;

            leftCard.Controls.Add(MakeLabel("Email *", x, y)); y += 22;
            txtEmail = MakeTextBox("email@airline.com", x, y, w);
            leftCard.Controls.Add(txtEmail); y += 58;

            leftCard.Controls.Add(MakeLabel("Phone *", x, y)); y += 22;
            txtPhone = MakeTextBox("+1 555-0000", x, y, w);
            leftCard.Controls.Add(txtPhone); y += 58;

            leftCard.Controls.Add(MakeLabel("Status *", x, y)); y += 22;
            cmbStatus = MakeComboBox(x, y, w);
            cmbStatus.Items.AddRange(new object[] { "Active", "Inactive" });
            cmbStatus.SelectedIndex = 0;
            cmbStatus.SelectedIndexChanged += (_, __) => ApplyStatusRulesToFlightUI();
            leftCard.Controls.Add(cmbStatus); y += 58;

            leftCard.Controls.Add(MakeLabel("Flight (optional)", x, y)); y += 22;
            cmbFlight = MakeComboBox(x, y, w);
            leftCard.Controls.Add(cmbFlight); y += 76;

            btnAddOrUpdate = new Guna2Button
            {
                Text = "Add Crew Member",
                BorderRadius = 12,
                FillColor = Color.FromArgb(35, 93, 220),
                Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                ForeColor = Color.White,
                Size = new Size(w, 48),
                Location = new Point(x, y)
            };
            btnAddOrUpdate.Click += (_, __) => AddOrUpdateClicked?.Invoke();
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
            btnCancelEdit.Click += (_, __) => CancelEditClicked?.Invoke();
            leftCard.Controls.Add(btnCancelEdit);
        }

        private void BuildRightCard()
        {
            

            lblCount = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Text = "Crew Members (0)",
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 30),
                Location = new Point(18, 18),
                AutoSize = true
            };
            rightCard.Controls.Add(lblCount);

            cmbFilter = MakeComboBox(rightCard.Width - 18 - 170, 12, 170);
            cmbFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbFilter.Items.Add("All Flights");
            cmbFilter.SelectedIndex = 0;
            cmbFilter.SelectedIndexChanged += (_, __) => FilterChanged?.Invoke();
            rightCard.Controls.Add(cmbFilter);

            flow = new FlowLayoutPanel
            {
                Location = new Point(18, 64),
                Size = new Size(rightCard.Width - 36, rightCard.Height - 86),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoScroll = true,
                WrapContents = false,
                FlowDirection = FlowDirection.TopDown,
                BackColor = Color.Transparent
            };

            // Enable double buffering AFTER initializing
            typeof(Panel).InvokeMember(
                "DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, flow, new object[] { true });

            rightCard.Controls.Add(flow);

        }

        private Label MakeLabel(string text, int x, int y)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(70, 70, 70),
                Location = new Point(x, y)
            };
        }

        private Guna2TextBox MakeTextBox(string placeholder, int x, int y, int w)
        {
            return new Guna2TextBox
            {
                BorderRadius = 10,
                Font = new Font("Segoe UI", 10F),
                Location = new Point(x, y),
                Size = new Size(w, 42),
                PlaceholderText = placeholder,
                FocusedState = { BorderColor = Color.FromArgb(35, 93, 220) },
                HoverState = { BorderColor = Color.FromArgb(35, 93, 220) }
            };
        }

        private Guna2ComboBox MakeComboBox(int x, int y, int w)
        {
            return new Guna2ComboBox
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
        }

        // ====================== STATUS RULE UI ONLY ======================

        private void ApplyStatusRulesToFlightUI()
        {
            bool inactive = (cmbStatus.SelectedItem?.ToString() ?? "")
                .Equals("Inactive", StringComparison.OrdinalIgnoreCase);

            if (inactive)
            {
                if (cmbFlight.Items.Count > 0) cmbFlight.SelectedIndex = 0; // Unassigned
                cmbFlight.Enabled = false;
            }
            else
            {
                cmbFlight.Enabled = true;
            }
        }

        // ====================== FILTER + RENDER ======================

        private void RefreshFilterItems()
        {
            var current = cmbFilter.SelectedItem?.ToString() ?? "All Flights";

            cmbFilter.Items.Clear();
            cmbFilter.Items.Add("All Flights");
            cmbFilter.Items.Add("Unassigned");

            for (int i = 0; i < cmbFlight.Items.Count; i++)
            {
                if (cmbFlight.Items[i] is FlightItem fi && fi.FlightId.HasValue)
                {
                    string item = $"Flight #{fi.FlightId.Value}";
                    if (!cmbFilter.Items.Contains(item))
                        cmbFilter.Items.Add(item);
                }
            }

            if (cmbFilter.Items.Cast<object>().Any(i => i.ToString() == current))
                cmbFilter.SelectedItem = current;
            else
                cmbFilter.SelectedIndex = 0;
        }

        private void RenderCards()
        {
            if (flow == null) return;

            flow.SuspendLayout();

            string filter = cmbFilter.SelectedItem?.ToString() ?? "All Flights";

            List<Crew> view;
            if (filter == "All Flights")
                view = allCrew;
            else if (filter == "Unassigned")
                view = allCrew.FindAll(c => !c.FlightId.HasValue);
            else if (filter.StartsWith("Flight #") && int.TryParse(filter.Replace("Flight #", ""), out int flightId))
                view = allCrew.FindAll(c => c.FlightId == flightId);
            else
                view = allCrew;

            lblCount.Text = $"Crew Members ({view.Count})";

            // Use dictionary to track existing controls
            var existing = new Dictionary<string, Control>();
            foreach (Control ctrl in flow.Controls)
            {
                if (ctrl.Tag is string empId) existing[empId] = ctrl;
            }

            flow.Controls.Clear();

            foreach (var c in view)
            {
                if (existing.TryGetValue(c.EmployeeId, out var ctrl))
                {
                    // Reuse existing control
                    flow.Controls.Add(ctrl);
                }
                else
                {
                    // Create new control
                    var card = CreateCrewCard(c);
                    card.Tag = c.EmployeeId; // Tag for reuse
                    flow.Controls.Add(card);
                }
            }

            flow.ResumeLayout();
        }

        private Control CreateCrewCard(Crew c)
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
            card.Width = Math.Max(100, flow.ClientSize.Width - 25);

            var name = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Text = c.FullName,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 30),
                AutoSize = true,
                Location = new Point(16, 14)
            };
            card.Controls.Add(name);

            var roleBadge = Badge(c.Role, Color.FromArgb(222, 235, 255), Color.FromArgb(35, 93, 220));
            roleBadge.Location = new Point(name.Right + 14, 12);
            card.Controls.Add(roleBadge);

            bool active = string.Equals(c.Status, "active", StringComparison.OrdinalIgnoreCase);
            var stBack = active ? Color.FromArgb(220, 245, 228) : Color.FromArgb(235, 235, 235);
            var stFore = active ? Color.FromArgb(28, 140, 60) : Color.FromArgb(90, 90, 90);
            var stBadge = Badge(c.Status, stBack, stFore);
            stBadge.Location = new Point(roleBadge.Right + 10, 12);
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
                Location = new Point(card.Width - 96, 10)
            };
            btnEdit.Click += (_, __) => EditRequested?.Invoke(c);
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
                Location = new Point(card.Width - 48, 10)
            };
            btnDel.Click += (_, __) => DeleteRequested?.Invoke(c);
            card.Controls.Add(btnDel);

            card.Controls.Add(InfoLine("Employee ID:", c.EmployeeId, 16, 56));
            card.Controls.Add(InfoLine("Email:", c.Email, 16, 80));
            card.Controls.Add(InfoLine("Phone:", c.Phone, card.Width / 2 + 10, 56));

            string flightText = c.FlightId.HasValue ? c.FlightId.Value.ToString() : "Unassigned";
            card.Controls.Add(InfoLine("Flight:", flightText, card.Width / 2 + 10, 80));

            card.SizeChanged += (_, __) =>
            {
                btnEdit.Location = new Point(card.Width - 96, 10);
                btnDel.Location = new Point(card.Width - 48, 10);
            };

            return card;
        }

        private Control InfoLine(string label, string value, int x, int y)
        {
            var p = new Panel { BackColor = Color.Transparent, Location = new Point(x, y), Size = new Size(360, 22) };

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

            b.HandleCreated += (_, __) => b.Region = RoundRegion(new Rectangle(0, 0, b.Width, b.Height), 10);
            b.SizeChanged += (_, __) => b.Region = RoundRegion(new Rectangle(0, 0, b.Width, b.Height), 10);

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

        private void SelectFlightInDropdown(int? flightId)
        {
            if (flightId == null)
            {
                cmbFlight.SelectedIndex = 0;
                return;
            }

            for (int i = 0; i < cmbFlight.Items.Count; i++)
            {
                if (cmbFlight.Items[i] is FlightItem fi && fi.FlightId == flightId)
                {
                    cmbFlight.SelectedIndex = i;
                    return;
                }
            }

            cmbFlight.SelectedIndex = 0;
        }

        private void CrewManagement_Load(object sender, EventArgs e)
        {

        }
    }
}