using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    public partial class FlightManagement : UserControl, IFlightManagementView
    {
        private FlowLayoutPanel flowFlights;
        private bool _uiEditMode;
        private bool _suppressFilterEvent;
        private int? _selectedFlightId;
        public FlightManagement()
        {
            InitializeComponent();
            BuildFlow();
            WireEvents();
        }
        private void BuildFlow()
        {
            flowFlights = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = false,
                FlowDirection = FlowDirection.TopDown,
                Padding = new Padding(12, 50, 12, 12)
            };

            // Double-buffering to prevent flicker
            typeof(FlowLayoutPanel).InvokeMember(
                "DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetProperty,
                null,
                flowFlights,
                new object[] { true });

            rightCard.Controls.Add(flowFlights);

            flowFlights.SizeChanged += (_, __) =>
            {
                foreach (Control c in flowFlights.Controls)
                    c.Width = flowFlights.ClientSize.Width - flowFlights.Padding.Horizontal;
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

            VisibleChanged += (_, __) =>
            {
                if (Visible)
                    LoadFlightsRequested?.Invoke(this, EventArgs.Empty);
            };
        }

        // EVENTS
        public event Action AddOrUpdateClicked;
        public event Action CancelEditClicked;
        public event Action<Flight> EditRequested;
        public event Action<Flight> DeleteRequested;
        public event Action FilterChanged;
        public event EventHandler LoadFlightsRequested;

        // FORM PROPERTIES
        public string FlightNumber => txtFlightNumber.Text.Trim();
        public string Origin => txtOrigin.Text.Trim();
        public string Destination => txtDestination.Text.Trim();
        
        public string Date => txtDate.Text.Trim();
        public string Time => txtTime.Text.Trim();
        public string Status => cmbStatus.SelectedItem?.ToString() ?? "Scheduled";
        public bool IsInEditMode => _uiEditMode;

        public int? SelectedFlightId => _selectedFlightId;

        // RENDERING FLIGHTS AND FILTERS
        public void RenderFlights(IEnumerable<Flight> flights)
        {
            flowFlights.SuspendLayout();
            flowFlights.Controls.Clear();

            if (flights != null)
            {
                foreach (var f in flights)
                {
                    var card = CreateFlightCard(f);
                    card.Width = flowFlights.ClientSize.Width - flowFlights.Padding.Horizontal;
                    flowFlights.Controls.Add(card);
                }
            }

            flowFlights.ResumeLayout(true);
            lblCount.Text = $"Flights ({flights?.Count() ?? 0})";
        }

        public void RenderFilterFlights(List<Flight> flights)
        {
            cmbFilter.Items.Clear();
            cmbFilter.Items.Add("All Flights");

            if (flights != null)
            {
                foreach (var f in flights)
                    cmbFilter.Items.Add($"Flight #{f.FlightID}");
            }

            cmbFilter.SelectedIndex = 0;
        }

        public void SetEditMode(bool editing)
        {
            _uiEditMode = editing;

            if (!editing)
            {
                txtFlightNumber.Clear();
                txtOrigin.Clear();
                txtDestination.Clear();
                txtDate.Clear();
                txtTime.Clear();
                cmbStatus.SelectedIndex = 0;

                btnAddOrUpdate.Text = "Add Flight";
                btnCancelEdit.Visible = false;
            }
            else
            {
                btnAddOrUpdate.Text = "Update Flight";
                btnCancelEdit.Visible = true;
            }
        }

        public void FillForm(Flight f)
        {
            if (f == null) return;

            txtFlightNumber.Text = f.FlightID.ToString();          
            txtOrigin.Text = f.From;                               
            txtDestination.Text = f.To;                            
            txtDate.Text = f.Departure.ToString("yyyy-MM-dd");     
            txtTime.Text = f.Departure.ToString("HH:mm");          
            cmbStatus.SelectedItem = f.Plane != null ? "Assigned" : "Unassigned"; 

            SetEditMode(true);
            _selectedFlightId = f.FlightID;
        }


        public int? GetFilterFlightId()
        {
            var selected = cmbFilter.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selected) || selected == "All Flights") return null;
            if (selected.StartsWith("Flight #"))
                return int.Parse(selected.Replace("Flight #", ""));
            return null;
        }

        private void ParseFilter()
        {
            _selectedFlightId = GetFilterFlightId();
        }

        public void ShowError(string msg) =>
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public void ShowInfo(string msg) =>
            MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        // CREATE DYNAMIC FLIGHT CARD
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

            card.Width = Math.Max(100, flowFlights.ClientSize.Width - 25);

            // Flight number label
            var lblNumber = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Text =  f.FlightID.ToString(),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 30),
                AutoSize = true,
                Location = new Point(16, 14)
            };
            card.Controls.Add(lblNumber);

            // Status badge
            // var statusColor = f.Status switch
            //   {
            //  "Scheduled" => (Back: Color.FromArgb(220, 245, 228), Fore: Color.FromArgb(28, 140, 60)),
            //  "Delayed" => (Back: Color.FromArgb(255, 240, 220), Fore: Color.FromArgb(220, 110, 0)),
            //  "Cancelled" => (Back: Color.FromArgb(245, 228, 228), Fore: Color.FromArgb(200, 0, 0)),
            //   _ => (Back: Color.LightGray, Fore: Color.Black)
            //  };
            //   var statusBadge = Badge(f.Status, statusColor.Back, statusColor.Fore);
            // statusBadge.Location = new Point(lblNumber.Right + 14, 12);
            // card.Controls.Add(statusBadge);

            // Edit button
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
            btnEdit.Click += (_, __) => EditRequested?.Invoke(f);
            card.Controls.Add(btnEdit);

            // Delete button
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
            btnDel.Click += (_, __) => DeleteRequested?.Invoke(f);
            card.Controls.Add(btnDel);

            int leftColX = 16;
            int rightColX = card.Width / 2;

            card.Controls.Add(InfoLine("Origin:", f.From, leftColX, 56));
            card.Controls.Add(InfoLine("Destination:", f.To, leftColX, 80));
            card.Controls.Add(InfoLine("Date:", f.Departure.ToString("yyyy-MM-dd"), rightColX, 56));
            card.Controls.Add(InfoLine("Time:", f.Departure.ToString("HH:mm"), rightColX, 80));

            card.SizeChanged += (_, __) =>
            {
                btnEdit.Location = new Point(card.Width - 96, 10);
                btnDel.Location = new Point(card.Width - 48, 10);
            };

            return card;
        }

        private Control InfoLine(string label, string value, int x, int y)
        {
            var p = new Guna2Panel
            {
                Location = new Point(x, y),
                Size = new Size(230, 22),
                BackColor = Color.Transparent,
                FillColor = Color.Transparent
            };

            var l1 = new Label
            {
                Text = label,
                AutoSize = true,
                Location = new Point(10, 2),
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(120, 120, 120)
            };

            var l2 = new Label
            {
                Text = value ?? "",
                AutoSize = true,
                Location = new Point(l1.Right - 50, 2),
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(60, 60, 60)
            };

            p.Controls.Add(l1);
            p.Controls.Add(l2);

            // Ensure right alignment on resize
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
        private void FlightManagement_Load(object sender, EventArgs e)
        {

        }
    }
}
