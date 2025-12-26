using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    public partial class CrewManagement : UserControl, ICrewManagementView
    {
        private FlowLayoutPanel flowCrew;

        private bool _uiEditMode;
        private bool _suppressFilterEvent;
        private bool _suppressFormSync;
        private int? _flightIdFilter;

        public CrewManagement()
        {
            InitializeComponent();
            BuildFlow();
            WireEvents();
        }
        private void BuildFlow()
        {
            flowCrew = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,  
                WrapContents = false,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(12, 50, 12, 12)
            };

            typeof(FlowLayoutPanel).InvokeMember(
                "DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetProperty,
                null,
                flowCrew,
                new object[] { true });

            rightCard.Controls.Add(flowCrew);

            flowCrew.SizeChanged += (_, __) =>
            {
                foreach (Control c in flowCrew.Controls)
                    c.Width = flowCrew.ClientSize.Width - flowCrew.Padding.Horizontal;
            };
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
        public event Action AddOrUpdateClicked;
        public event Action CancelEditClicked;
        public event Action<Crew> EditRequested;
        public event Action<Crew> DeleteRequested;
        public event Action FilterChanged;
        public event EventHandler LoadCrewRequested;

        public string FullName => txtFullName.Text.Trim();
        public string Email => txtEmail.Text.Trim();
        public string Phone => txtPhone.Text.Trim();
        public string Role => cmbRole.SelectedItem?.ToString() ?? "";
        public string Status => cmbStatus.SelectedItem?.ToString() ?? "Active";
        public int? SelectedFlightId => (cmbFlight.SelectedItem as FlightItem)?.FlightId;
        public bool IsInEditMode => _uiEditMode;

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
            cmbFilter.Items.Clear();
            cmbFilter.Items.Add(new FlightItem(null, "All Flights"));
            cmbFilter.Items.Add(new FlightItem(null, "Unassigned")); // <-- add this

            if (flights != null)
            {
                foreach (var f in flights)
                    cmbFilter.Items.Add(new FlightItem(f.FlightID, $"Flight #{f.FlightID}"));
            }

            cmbFilter.SelectedIndex = 0;
        }



        public void RenderCrew(IEnumerable<Crew> crew)
        {
            if (crew == null) return;

            // Apply flight filter
            IEnumerable<Crew> filteredCrew;
            if (_flightIdFilter == null)
                filteredCrew = crew; // all flights
            else if (_flightIdFilter == -1)
                filteredCrew = crew.Where(c => !c.FlightId.HasValue); // only unassigned
            else
                filteredCrew = crew.Where(c => c.FlightId == _flightIdFilter); // specific flight

            flowCrew.SuspendLayout();
            flowCrew.Controls.Clear();

            foreach (var c in filteredCrew)
            {
                var card = CreateCrewCard(c);
                card.Width = flowCrew.ClientSize.Width - flowCrew.Padding.Horizontal;
                flowCrew.Controls.Add(card);
            }

            flowCrew.ResumeLayout(true);
            lblCount.Text = $"Crew Members ({filteredCrew.Count()})";
        }


        public void SetEditMode(bool editing)
        {
            _uiEditMode = editing;

            if (!editing)
            {
                txtFullName.Clear();
                txtEmail.Clear();
                txtPhone.Clear();
                cmbRole.SelectedIndex = 0;
                cmbStatus.SelectedIndex = 0;
                cmbFlight.SelectedIndex = 0;

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

        public void FillForm(
    string fullName,
    string role,
    string status,
    string email,
    string phone,
    int? flightId)
        {
            txtFullName.Text = fullName;
            txtEmail.Text = email;
            txtPhone.Text = phone;

            cmbRole.SelectedItem = role;
            cmbStatus.SelectedItem = status;

            SetFormFlight(flightId);
            ApplyStatusRulesToFlightUI();
        }

        public int? GetFlightFilter()
        {
            return (cmbFilter.SelectedItem as FlightItem)?.FlightId;
        }


        private void ParseFilter()
        {
            var selected = cmbFilter.SelectedItem as FlightItem;

            if (selected == null)
            {
                _flightIdFilter = null;
                return;
            }

            if (selected.Text == "All Flights")
                _flightIdFilter = null; // show all
            else if (selected.Text == "Unassigned")
                _flightIdFilter = -1;   // sentinel for unassigned
            else
                _flightIdFilter = selected.FlightId; // specific flight
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
            if (flightId == null)
            {
                cmbFlight.SelectedIndex = 0;
                return;
            }

            foreach (var item in cmbFlight.Items.OfType<FlightItem>())
                if (item.FlightId == flightId)
                    cmbFlight.SelectedItem = item;
        }


        private void ApplyStatusRulesToFlightUI()
        {
            bool inactive = Status.Equals("Inactive", StringComparison.OrdinalIgnoreCase);

            cmbFlight.Enabled = !inactive;
            if (inactive)
                cmbFlight.SelectedIndex = 0;
        }

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

        private Control CreateCrewCard(Crew c)
        {
            var card = new Guna2ShadowPanel
            {
                BackColor = Color.Transparent,
                FillColor = Color.White,
                Radius = 12,
                Padding = new Padding(16, 14, 16, 14),
                Margin = new Padding(0, 0, 0, 12),
                Height = 140,
                ShadowDepth = 10,
                ShadowColor = Color.FromArgb(150, 0, 0, 0)
                
            };
            card.Width = Math.Max(100, flowCrew.ClientSize.Width - 30);

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
            card.Controls.Add(InfoLine("Phone:", c.Phone, 270, 56));

            string flightText = c.FlightId.HasValue ? c.FlightId.Value.ToString() : "Unassigned";
            card.Controls.Add(InfoLine("Flight:", flightText, 270, 80));

            card.SizeChanged += (_, __) =>
            {
                btnEdit.Location = new Point(card.Width - 96, 10);
                btnDel.Location = new Point(card.Width - 48, 10);
            };

            return card;
        }

        private Control InfoLine(string label, string value, int x, int y)
        {
            var p = new Guna2Panel { Location = new Point(x, y), Size = new Size(230, 22), BackColor = Color.Transparent ,FillColor=Color.Transparent};
            var l1 = new Label { Text = label, AutoSize = true, Location = new Point(0, 2), Font = new Font("Segoe UI", 9F),BackColor=Color.Transparent,ForeColor = Color.FromArgb(120, 120, 120) };
            var l2 = new Label { Text = value ?? "", AutoSize = true, Location = new Point(l1.Right - 28, 2), Font = new Font("Segoe UI", 9F), BackColor = Color.Transparent, ForeColor = Color.FromArgb(60, 60, 60) };
            p.Controls.Add(l1); p.Controls.Add(l2);
            p.SizeChanged += (_, __) => l2.Location = new Point(l1.Right + 6, 2);
            return p;
        }
            
        private Guna2HtmlLabel Badge(string text, Color back, Color fore)
        {
            return new Guna2HtmlLabel { AutoSize = true, Text = $"  {text}  ", BackColor = back, ForeColor = fore, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
        }


        private void flow_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
