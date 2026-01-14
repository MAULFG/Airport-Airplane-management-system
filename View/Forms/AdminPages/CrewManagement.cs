using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    public partial class CrewManagement : UserControl, ICrewManagementView
    {
        private FlowLayoutPanel flowCrew;
        private Panel _listHost;                

        private bool _uiEditMode;
        private bool _suppressFilterEvent;
        private bool _suppressFormSync;
        private int? _flightIdFilter;

        public CrewManagement()
        {
            InitializeComponent();
            BuildFlow();
            WireEvents();
            this.Load += (_, __) => PositionFilterToRight();
            rightCard.SizeChanged += (_, __) => PositionFilterToRight();
        }

        private void BuildFlow()
        {
            _listHost = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent
            };
            rightCard.Controls.Add(_listHost);
            _listHost.SendToBack();

            flowCrew = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = false,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(12, 12, 12, 24) // bottom padding helps last card not feel cut
            };

            typeof(FlowLayoutPanel).InvokeMember(
                "DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetProperty,
                null,
                flowCrew,
                new object[] { true });
            typeof(FlowLayoutPanel).InvokeMember(
               "DoubleBuffered",
               BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetProperty,
               null,
               _listHost,
               new object[] { true });

            _listHost.Controls.Add(flowCrew);

            //  recompute reserved header space when layout changes
            rightCard.SizeChanged += (_, __) => UpdateHeaderReserve();
            this.SizeChanged += (_, __) => UpdateHeaderReserve();

            // initial reserve
            UpdateHeaderReserve();

            // widths
            flowCrew.SizeChanged += (_, __) => RefreshCardsWidth();
        }

        private void UpdateHeaderReserve()
        {
            int headerBottom = 0;

            foreach (Control c in rightCard.Controls)
            {
                if (c == _listHost) continue;
                if (!c.Visible) continue;
                headerBottom = Math.Max(headerBottom, c.Bottom);
            }

            // fallback if designer controls are inside another header panel etc.
            headerBottom = Math.Max(headerBottom, 52);

            // add a small gap
            _listHost.Padding = new Padding(0, headerBottom + 5, 0, 12);
            RefreshCardsWidth();
        }

        private void RefreshCardsWidth()
        {
            if (flowCrew == null) return;

            int w = flowCrew.ClientSize.Width
                    - SystemInformation.VerticalScrollBarWidth
                    - flowCrew.Padding.Horizontal
                    - 4;

            foreach (Control c in flowCrew.Controls)
                c.Width = Math.Max(100, w);
        }

        private void WireEvents()
        {
            btnAddOrUpdate.Click += (_, __) => AddOrUpdateClicked?.Invoke();
            btnCancelEdit.Click += (_, __) => CancelEditClicked?.Invoke();

            cmbFilter.SelectedIndexChanged += (_, __) =>
            {
                if (_suppressFilterEvent) return;
                ParseFilter();
                FilterChanged?.Invoke();
            };

            cmbStatus.SelectedIndexChanged += (_, __) =>
            {
                if (_suppressFormSync) return;
                ApplyStatusRulesToFlightUI();
            };

            VisibleChanged += (_, __) =>
            {
                if (Visible)
                    LoadCrewRequested?.Invoke(this, EventArgs.Empty);
            };
        }

        // ======= ICrewManagementView events =======
        public event Action AddOrUpdateClicked;
        public event Action CancelEditClicked;
        public event Action<Crew> EditRequested;
        public event Action<Crew> DeleteRequested;
        public event Action FilterChanged;
        public event EventHandler LoadCrewRequested;

        // ======= Inputs =======
        public string FullName => txtFullName.Text.Trim();
        public string Email => txtEmail.Text.Trim();
        public string Phone => txtPhone.Text.Trim();
        public string Role => cmbRole.SelectedItem?.ToString() ?? "";
        public string Status => cmbStatus.SelectedItem?.ToString() ?? "Active";
        public int? SelectedFlightId => (cmbFlight.SelectedItem as FlightItem)?.FlightId;

        public bool IsInEditMode => _uiEditMode;

        // ======= Rendering flights =======
        public void RenderFlights(List<Flight> flights)
        {
            cmbFlight.Items.Clear();
            cmbFlight.Items.Add(new FlightItem(null, "Unassigned"));

            if (flights != null)
            {
                foreach (var f in flights)
                    cmbFlight.Items.Add(new FlightItem(f.FlightID, $"Flight #{f.FlightID}"));
            }

            cmbFlight.SelectedIndex = 0;
            ApplyStatusRulesToFlightUI();
        }

        public void RenderFilterFlights(List<Flight> flights)
        {
            // ✅ preserve current selection so it doesn't "reset itself"
            int? keepId = _flightIdFilter; // null / -1 / flightId

            _suppressFilterEvent = true;

            cmbFilter.Items.Clear();
            cmbFilter.Items.Add(new FlightItem(null, "All Flights"));
            cmbFilter.Items.Add(new FlightItem(-1, "Unassigned"));

            if (flights != null)
            {
                foreach (var f in flights)
                    cmbFilter.Items.Add(new FlightItem(f.FlightID, $"Flight #{f.FlightID}"));
            }

            // restore selection
            if (!keepId.HasValue)
            {
                cmbFilter.SelectedIndex = 0;
            }
            else
            {
                var match = cmbFilter.Items.OfType<FlightItem>()
                    .FirstOrDefault(i => i.FlightId.HasValue && i.FlightId.Value == keepId.Value);

                cmbFilter.SelectedItem = match ?? cmbFilter.Items[0];
            }

            ParseFilter();
            _suppressFilterEvent = false;
        }

        // ======= Rendering crew =======
        public void RenderCrew(IEnumerable<Crew> crew)
        {
            if (crew == null) return;

            IEnumerable<Crew> filteredCrew;
            if (_flightIdFilter == null)
                filteredCrew = crew;
            else if (_flightIdFilter == -1)
                filteredCrew = crew.Where(c => !c.FlightId.HasValue);
            else
                filteredCrew = crew.Where(c => c.FlightId.HasValue && c.FlightId.Value == _flightIdFilter.Value);

            flowCrew.SuspendLayout();
            flowCrew.Controls.Clear();

            foreach (var c in filteredCrew)
            {
                var card = CreateCrewCard(c);
                flowCrew.Controls.Add(card);
            }

            //  extra bottom space so last card never feels “cut”
            flowCrew.Controls.Add(new Panel { Height = 20, Width = 1, BackColor = Color.Transparent });

            flowCrew.ResumeLayout(true);

            lblCount.Text = $"Crew Members ({filteredCrew.Count()})";
            RefreshCardsWidth();
        }

        // ======= Edit mode =======
        public void SetEditMode(bool editing)
        {
            _uiEditMode = editing;

            if (!editing)
            {
                txtFullName.Clear();
                txtEmail.Clear();
                txtPhone.Clear();

                if (cmbRole.Items.Count > 0) cmbRole.SelectedIndex = 0;
                if (cmbStatus.Items.Count > 0) cmbStatus.SelectedIndex = 0;
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

        public void FillForm(string fullName, string role, string status, string email, string phone, int? flightId)
        {
            txtFullName.Text = fullName;
            txtEmail.Text = email;
            txtPhone.Text = phone;

            cmbRole.SelectedItem = role;
            cmbStatus.SelectedItem = status;

            SetFormFlight(flightId);
            ApplyStatusRulesToFlightUI();
        }

        // ======= Filter API =======
        public void SetFlightFilter(int? flightId)
        {
            _suppressFilterEvent = true;

            _flightIdFilter = flightId;

            if (cmbFilter.Items.Count > 0)
            {
                if (!flightId.HasValue)
                {
                    cmbFilter.SelectedIndex = 0;
                }
                else
                {
                    var match = cmbFilter.Items.OfType<FlightItem>()
                        .FirstOrDefault(i => i.FlightId.HasValue && i.FlightId.Value == flightId.Value);

                    cmbFilter.SelectedItem = match ?? cmbFilter.Items[0];
                }
            }

            _suppressFilterEvent = false;
            FilterChanged?.Invoke();
        }

        public int? GetFlightFilter() => _flightIdFilter;

        private void ParseFilter()
        {
            var selected = cmbFilter.SelectedItem as FlightItem;
            if (selected == null) { _flightIdFilter = null; return; }

            if (selected.Text == "All Flights")
                _flightIdFilter = null;
            else
                _flightIdFilter = selected.FlightId; // -1 or real id
        }

        public void SyncFormFlightWithFilter(int? flightId)
        {
            _suppressFormSync = true;

            SetFormFlight(flightId);
            cmbFlight.Enabled = !flightId.HasValue;

            _suppressFormSync = false;
        }

        private void SetFormFlight(int? flightId)
        {
            if (cmbFlight.Items.Count == 0) return;

            if (flightId == null)
            {
                cmbFlight.SelectedIndex = 0;
                return;
            }

            foreach (var item in cmbFlight.Items.OfType<FlightItem>())
            {
                if (item.FlightId == flightId)
                {
                    cmbFlight.SelectedItem = item;
                    return;
                }
            }

            cmbFlight.SelectedIndex = 0;
        }

        private void ApplyStatusRulesToFlightUI()
        {
            bool inactive = Status.Equals("Inactive", StringComparison.OrdinalIgnoreCase);

            cmbFlight.Enabled = !inactive;
            if (inactive && cmbFlight.Items.Count > 0)
                cmbFlight.SelectedIndex = 0;
        }

        // ======= Feedback =======
        public void ShowError(string msg) =>
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void ShowInfo(string msg) =>
            MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private class FlightItem
        {
            public int? FlightId { get; }
            public string Text { get; }

            public FlightItem(int? id, string text)
            {
                FlightId = id;
                Text = text;
            }

            public override string ToString() => Text;
        }

        // ======= Card =======
        private Control CreateCrewCard(Crew c)
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
            card.Controls.Add(roleBadge);

            bool active = string.Equals(c.Status, "active", StringComparison.OrdinalIgnoreCase);
            var stBack = active ? Color.FromArgb(220, 245, 228) : Color.FromArgb(235, 235, 235);
            var stFore = active ? Color.FromArgb(28, 140, 60) : Color.FromArgb(90, 90, 90);
            var stBadge = Badge(c.Status, stBack, stFore);
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
                TabStop = false
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
                TabStop = false
            };
            btnDel.Click += (_, __) => DeleteRequested?.Invoke(c);
            card.Controls.Add(btnDel);

            int d = 340;

            card.Controls.Add(InfoLine("ID:", c.EmployeeId, 16, 56));
            card.Controls.Add(InfoLine("Email:", c.Email, 16, 80));
            card.Controls.Add(InfoLine("Phone:", c.Phone, d, 56));

            string flightText = c.FlightId.HasValue ? c.FlightId.Value.ToString() : "Unassigned";
            card.Controls.Add(InfoLine("Flight:", flightText, d, 80));

            // ✅ one layout method to position everything safely
            void LayoutHeader()
            {
                roleBadge.Location = new Point(name.Right + 14, 12);
                stBadge.Location = new Point(roleBadge.Right + 10, 12);

                int right = card.ClientSize.Width;

                // ✅ smaller margin => more to the right
                int rMargin = 4;

                btnDel.Location = new Point(right - btnDel.Width - rMargin -15, 10);
                btnEdit.Location = new Point(btnDel.Left - btnEdit.Width - 4, 10);

            }

            card.SizeChanged += (_, __) => LayoutHeader();
            card.Layout += (_, __) => LayoutHeader();

            return card;
        }

        private Control InfoLine(string label, string value, int x, int y)
        {
            var p = new Guna2Panel
            {
                Location = new Point(x, y),
                Size = new Size(320, 22),
                BackColor = Color.Transparent
            };

            var l1 = new Label
            {
                Text = label,
                AutoSize = true,
                Location = new Point(0, 2),
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(120, 120, 120)
            };

            var l2 = new Label
            {
                Text = value ?? "",
                AutoSize = true,
                Location = new Point(l1.Right + 6, 2),
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            p.Controls.Add(l1);
            p.Controls.Add(l2);

            p.SizeChanged += (_, __) => l2.Location = new Point(l1.Right + 6, 2);

            return p;
        }

        private Guna2HtmlLabel Badge(string text, Color back, Color fore)
        {
            return new Guna2HtmlLabel
            {
                AutoSize = true,
                Text = $"  {text}  ",
                BackColor = back,
                ForeColor = fore,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
        }

        // ======= Clear =======
        public void Clear()
        {
            _uiEditMode = false;
            _suppressFilterEvent = true;
            _suppressFormSync = true;

            _flightIdFilter = null;

            txtFullName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();

            if (cmbRole.Items.Count > 0) cmbRole.SelectedIndex = 0;
            if (cmbStatus.Items.Count > 0) cmbStatus.SelectedIndex = 0;
            if (cmbFlight.Items.Count > 0) cmbFlight.SelectedIndex = 0;

            btnAddOrUpdate.Text = "Add Crew Member";
            btnCancelEdit.Visible = false;

            cmbFilter.Items.Clear();
            cmbFilter.Text = string.Empty;

            flowCrew?.Controls.Clear();
            lblCount.Text = "Crew Members (0)";

            _suppressFilterEvent = false;
            _suppressFormSync = false;
        }

        // these Designer is wired to them
        private void rightCard_Paint(object sender, PaintEventArgs e) { }
        private void root_Paint(object sender, PaintEventArgs e) { }
        private void PositionFilterToRight()
        {
            if (cmbFilter == null || rightCard == null || lblCount == null) return;

            // make it stick to the right side when resizing
            cmbFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            int rightMargin = 26;
            int x = rightCard.ClientSize.Width - cmbFilter.Width - rightMargin;

            int minX = lblCount.Right + 16;
            if (x < minX) x = minX;

            cmbFilter.Left = x;
            cmbFilter.Top = lblCount.Top - 2;
        }

    }
}
