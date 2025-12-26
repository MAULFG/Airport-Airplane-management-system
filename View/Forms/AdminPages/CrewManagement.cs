using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using System.Linq.Expressions;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    public partial class CrewManagement : UserControl, ICrewManagementView
    {
        private FlowLayoutPanel flowCrew;
        private List<Crew> allCrew = new List<Crew>();
        private bool _uiEditMode = false;

        public CrewManagement()
        {
            InitializeComponent();

            // Create FlowLayoutPanel
            flowCrew = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = false,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(10, 50, 10, 10)
            };

            // Enable double buffering to prevent flicker
            typeof(FlowLayoutPanel).InvokeMember(
                "DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, flowCrew, new object[] { true });

            rightCard.Controls.Add(flowCrew);

            // Ensure child cards resize with panel
            flowCrew.SizeChanged += (s, e) =>
            {
                foreach (Control card in flowCrew.Controls)
                    card.Width = flowCrew.ClientSize.Width - flowCrew.Padding.Left - flowCrew.Padding.Right;
            };

            // Load crew when control becomes visible
            VisibleChanged += (s, e) =>
            {
                if (Visible)
                    LoadCrewRequested?.Invoke(this, EventArgs.Empty);
            };

            // MVP event wiring
            btnAddOrUpdate.Click += (_, __) => AddOrUpdateClicked?.Invoke();
            btnCancelEdit.Click += (_, __) => CancelEditClicked?.Invoke();
            cmbFilter.SelectedIndexChanged += (_, __) => FilterChanged?.Invoke();
        }
        // ===== Events (MVP) =====
        public event Action ViewLoaded;
        public event Action AddOrUpdateClicked;
        public event Action CancelEditClicked;
        public event Action<Crew> EditRequested;
        public event Action<Crew> DeleteRequested;
        public event Action FilterChanged;
        public event EventHandler LoadCrewRequested;
        // ===== ICrewManagementView Inputs =====
        public string FullName => txtFullName.Text.Trim();
        public string Email => txtEmail.Text.Trim();
        public string Phone => txtPhone.Text.Trim();
        public string Role => cmbRole.SelectedItem?.ToString() ?? "";
        public string Status => cmbStatus.SelectedItem?.ToString() ?? "Active";
        public int? SelectedFlightId => (cmbFlight.SelectedItem as FlightItem)?.FlightId;
        public void RenderCrew(IEnumerable<Crew> crew)
        {
            if (crew == null) return;

            flowCrew.SuspendLayout();
            flowCrew.Controls.Clear();

            foreach (var c in crew)
            {
                var card = CreateCrewCard(c);
                card.Width = flowCrew.ClientSize.Width - flowCrew.Padding.Left - flowCrew.Padding.Right;
                flowCrew.Controls.Add(card);
            }

            flowCrew.ResumeLayout(false);
        }
        public void FillForm(string fullName, string role, string status, string email, string phone, int? flightId)
        {
            txtFullName.Text = fullName;
            cmbRole.SelectedItem = role;
            cmbStatus.SelectedItem = status;
            txtEmail.Text = email;
            txtPhone.Text = phone;

            SetFormFlight(flightId); // this selects "Flight #x" or "Unassigned"
        }
        public void SetFormFlight(int? flightId)
        {
            if (cmbFlight == null) return;

            if (flightId.HasValue)
            {
                string target = $"Flight #{flightId.Value}";
                for (int i = 0; i < cmbFlight.Items.Count; i++)
                {
                    if (cmbFlight.Items[i]?.ToString() == target)
                    {
                        cmbFlight.SelectedIndex = i;
                        return;
                    }
                }
            }
        }
        public void SyncFormFlightWithFilter(int? flightId)
        {
            if (cmbFlight == null) return;

            _suppressFormSync = true;
            try
            {
                if (flightId.HasValue)
                {
                    // Filter = Flight #X → force form to that flight
                    for (int i = 0; i < cmbFlight.Items.Count; i++)
                    {
                        if (cmbFlight.Items[i]?.ToString() == $"Flight #{flightId.Value}")
                        {
                            cmbFlight.SelectedIndex = i;
                            cmbFlight.Enabled = false;   // optional UX lock
                            return;
                        }
                    }
                }
                else
                {
                    // Filter = All Flights → force Unassigned
                    for (int i = 0; i < cmbFlight.Items.Count; i++)
                    {
                        if (cmbFlight.Items[i]?.ToString() == "Unassigned")
                        {
                            cmbFlight.SelectedIndex = i;
                            cmbFlight.Enabled = true;
                            return;
                        }
                    }
                }
            }
            finally
            {
                _suppressFormSync = false;
            }
        }
        public int? GetFlightFilter() => _flightIdFilter;
        private bool _suppressFormSync;
        private int? _flightIdFilter;
        public void SetFlightFilter(int? flightId)
        {
            if (flightId.HasValue)
            {
                // assumes your filter combobox is the top-right one (example name: cmbFilter)
                // select "Flight #X"
                var target = $"Flight #{flightId.Value}";
                for (int i = 0; i < cmbFilter.Items.Count; i++)
                {
                    if (cmbFilter.Items[i]?.ToString() == target)
                    {
                        cmbFilter.SelectedIndex = i;
                        return;
                    }
                }
            }

            // if null OR not found -> All Flights
            if (cmbFilter.Items.Count > 0)
                cmbFilter.SelectedIndex = 0;
        }


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

            cmbStatus.SelectedIndexChanged += (_, __) => ApplyStatusRulesToFlightUI();
            cmbFlight.SelectedIndex = 0;

            RefreshFilterItems();
            ApplyStatusRulesToFlightUI();
        }

        // ===== Edit Mode =====
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
        // ===== Utilities =====
        public void ShowError(string message) => MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        public void ShowInfo(string message) => MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        public void FillFormFromCrew(Crew c)
        {
            if (c == null) return;

            txtFullName.Text = c.FullName;
            txtEmail.Text = c.Email;
            txtPhone.Text = c.Phone;

            int roleIndex = cmbRole.Items.Cast<object>()
                .ToList()
                .FindIndex(o => o.ToString().Equals(c.Role, StringComparison.OrdinalIgnoreCase));
            cmbRole.SelectedIndex = roleIndex >= 0 ? roleIndex : 0;

            cmbStatus.SelectedIndex = string.Equals(c.Status, "Inactive", StringComparison.OrdinalIgnoreCase) ? 1 : 0;
            SelectFlightInDropdown(c.FlightId);

            ApplyStatusRulesToFlightUI();
        }

        private void ApplyStatusRulesToFlightUI()
        {
            bool inactive = (cmbStatus.SelectedItem?.ToString() ?? "").Equals("Inactive", StringComparison.OrdinalIgnoreCase);
            cmbFlight.Enabled = !inactive;

            if (inactive && cmbFlight.Items.Count > 0)
                cmbFlight.SelectedIndex = 0;
        }

        // ===== Helpers =====
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

        private class FlightItem
        {
            public int? FlightId { get; }
            public string Text { get; }
            public FlightItem(int? flightId, string text) { FlightId = flightId; Text = text; }
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
            card.Controls.Add(InfoLine("Phone:", c.Phone,card.Width/2 -100, 56));

            string flightText = c.FlightId.HasValue ? c.FlightId.Value.ToString() : "Unassigned";
            card.Controls.Add(InfoLine("Flight:", flightText, card.Width / 2 - 100, 80));

            card.SizeChanged += (_, __) =>
            {
                btnEdit.Location = new Point(card.Width - 96, 10);
                btnDel.Location = new Point(card.Width - 48, 10);
            };

            return card;
        }

        private Control InfoLine(string label, string value, int x, int y)
        {
            var p = new Guna2Panel { Location = new Point(x, y), Size = new Size(200, 22), BackColor = Color.Transparent ,FillColor=Color.Transparent};
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
