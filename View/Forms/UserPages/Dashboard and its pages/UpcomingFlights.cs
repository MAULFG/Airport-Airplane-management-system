using Airport_Airplane_management_system.Model.Core.Classes;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    public partial class UpcomingFlights : UserControl
    {
        private FlowLayoutPanel flowFlights;

        public event EventHandler LoadFlightsRequested;

        public UpcomingFlights()
        {
            InitializeComponent();
            VisibleChanged += (s, e) =>
            {
                if (Visible)
                    LoadFlightsRequested?.Invoke(this, EventArgs.Empty);
            };
            flowFlights.Resize += (s, e) =>
            {
                foreach (Control c in flowFlights.Controls)
                {
                    c.Width = flowFlights.ClientSize.Width - 50;
                }
            };
        }
        private Guna2Panel headerPanel;
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            flowFlights = new FlowLayoutPanel();
            headerPanel = new Guna2Panel();
            lblHeader = new Label();
            headerPanel.SuspendLayout();
            SuspendLayout();
            // 
            // flowFlights
            // 
            flowFlights.AutoScroll = true;
            flowFlights.Dock = DockStyle.Fill;
            flowFlights.FlowDirection = FlowDirection.TopDown;
            flowFlights.Location = new Point(20, 100);
            flowFlights.Name = "flowFlights";
            flowFlights.Padding = new Padding(10, 10, 10, 200);
            flowFlights.Size = new Size(1240, 600);
            flowFlights.TabIndex = 0;
            flowFlights.WrapContents = false;
            flowFlights.Paint += flowFlights_Paint_2;
            // 
            // headerPanel
            // 
            headerPanel.BorderRadius = 20;
            headerPanel.Controls.Add(lblHeader);
            headerPanel.CustomizableEdges = customizableEdges1;
            headerPanel.Dock = DockStyle.Top;
            headerPanel.FillColor = Color.DarkCyan;
            headerPanel.Location = new Point(20, 20);
            headerPanel.Name = "headerPanel";
            headerPanel.Padding = new Padding(20);
            headerPanel.ShadowDecoration.CustomizableEdges = customizableEdges2;
            headerPanel.Size = new Size(1240, 80);
            headerPanel.TabIndex = 1;
            // 
            // lblHeader
            // 
            lblHeader.Text = "Upcoming Flights in Our Airline";
            lblHeader.Font = new Font("Segoe UI", 18, FontStyle.Bold);
            lblHeader.ForeColor = Color.White;
            lblHeader.BackColor = Color.Transparent;
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.TextAlign = ContentAlignment.MiddleLeft;
            lblHeader.Location = new Point(0, 0);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(100, 23);
            lblHeader.TabIndex = 0;
            // 
            // UpcomingFlights
            // 
            BackColor = Color.White;
            Controls.Add(flowFlights);
            Controls.Add(headerPanel);
            Name = "UpcomingFlights";
            Padding = new Padding(20);
            Size = new Size(1280, 720);
            headerPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        public void LoadFlights(IEnumerable<Flight> flights)
        {
            flowFlights.Controls.Clear();

            foreach (var f in flights)
                if (f.Departure > DateTime.Now.AddDays(1))
                {
                    flowFlights.Controls.Add(CreateCard(f));

                }

        }

        private Guna2Panel CreateCard(Flight f)
        {
            int padding = 10;
            int iconSize = 50;
            int spacing = 10;

            var card = new Guna2Panel
            {
                Width = flowFlights.ClientSize.Width - 50,
                Height = 70,
                FillColor = Color.White,
                BorderRadius = 10,
                Margin = new Padding(5),
                Tag = false
            };

            var planePic = new Guna2PictureBox
            {
                Size = new Size(iconSize, iconSize),
                Image = Properties.Resources.icon1,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(padding, padding),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            card.Controls.Add(planePic);

            var lblRoute = new Guna2HtmlLabel
            {
                Text = $"{f.From} → {f.To}",
                Font = new Font("Arial", 12, FontStyle.Bold),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            card.Controls.Add(lblRoute);

            var lblFlightID = new Guna2HtmlLabel
            {
                Text = $"Flight #{f.FlightID}",
                Font = new Font("Arial", 10),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            card.Controls.Add(lblFlightID);

            var lblDate = new Guna2HtmlLabel
            {
                Text = f.Departure.ToString("yyyy-MM-dd"),
                Font = new Font("Arial", 10),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            card.Controls.Add(lblDate);

            var details = new Guna2Panel
            {
                Width = card.Width - padding * 2,
                Height = 200,
                Location = new Point(padding, card.Height),
                FillColor = Color.White,
                Visible = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            card.Controls.Add(details);

            var btnBook = new Guna2Button
            {
                Text = "Book",
                Width = 160,
                Height = 40,
                BorderRadius = 10,
                BackColor = Color.Transparent,
                FillColor = Color.DarkCyan,
                ForeColor = Color.White,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            details.Controls.Add(btnBook);
            btnBook.Click += (s, e) =>
            {
                int flightId = f.FlightID;
                BookFlightRequested?.Invoke(flightId);
            };

            var lblPlaneID = new Guna2HtmlLabel
            {
                Text = f.Plane != null ? $"Plane ID: {f.Plane.PlaneID}" : "Plane ID: N/A",
                Font = new Font("Arial", 10, FontStyle.Bold),
                AutoSize = true
            };
            details.Controls.Add(lblPlaneID);

            var lblPlaneModel = new Guna2HtmlLabel
            {
                Text = f.Plane != null ? $"Model: {f.Plane.Model}" : "Model: N/A",
                Font = new Font("Arial", 10),
                AutoSize = true
            };
            details.Controls.Add(lblPlaneModel);

            var rowDeparture = CreateRow("Departure:", f.Departure, details.Width);
            var rowArrival = CreateRow("Arrival:", f.Arrival, details.Width);
            details.Controls.Add(rowDeparture);
            details.Controls.Add(rowArrival);

            int firstTotal = 0, businessTotal = 0, economyTotal = 0, vipTotal = 0;
            int firstAvailable = 0, businessAvailable = 0, economyAvailable = 0, vipAvailable = 0;

            if (f?.FlightSeats != null)
            {
                foreach (var s in f.FlightSeats)
                {
                    if (s == null || string.IsNullOrWhiteSpace(s.ClassType)) continue;
                    string type = s.ClassType.Trim().ToLower();

                    switch (type)
                    {
                        case "vip":
                            vipTotal++;
                            if (!s.IsBooked) vipAvailable++;
                            break;
                        case "first":
                            firstTotal++;
                            if (!s.IsBooked) firstAvailable++;
                            break;
                        case "business":
                            businessTotal++;
                            if (!s.IsBooked) businessAvailable++;
                            break;
                        case "economy":
                            economyTotal++;
                            if (!s.IsBooked) economyAvailable++;
                            break;

                    }
                }
            }

            var lblTotalSeats = new Guna2HtmlLabel
            {
                Text = $"Total Seats: {firstTotal + businessTotal + economyTotal + vipTotal}",
                Font = new Font("Arial", 9, FontStyle.Bold),
                AutoSize = true
            };
            details.Controls.Add(lblTotalSeats);

            var lblAvailableSeats = new Guna2HtmlLabel
            {
                Text = $"Available Seats: {firstAvailable + businessAvailable + economyAvailable + vipAvailable}",
                Font = new Font("Arial", 9, FontStyle.Bold),
                AutoSize = true
            };
            details.Controls.Add(lblAvailableSeats);

            if (vipTotal > 0) details.Controls.Add(CreateSeatLabel($"VIP Class: {vipAvailable}/{vipTotal}", 0, 0));
            if (firstTotal > 0) details.Controls.Add(CreateSeatLabel($"First Class: {firstAvailable}/{firstTotal}", 0, 0));
            if (businessTotal > 0) details.Controls.Add(CreateSeatLabel($"Business Class: {businessAvailable}/{businessTotal}", 0, 0));
            if (economyTotal > 0) details.Controls.Add(CreateSeatLabel($"Economy Class: {economyAvailable}/{economyTotal}", 0, 0));

            void Toggle()
            {
                bool expanded = (bool)card.Tag;
                card.Tag = !expanded;
                details.Visible = !expanded;
                card.Height = !expanded ? 240 : 70;
                PositionElements();
            }

            card.Click += (s, e) => Toggle();
            foreach (Control c in card.Controls)
            {
                if (c != btnBook)
                    c.Click += (s, e) => Toggle();
            }

            void PositionElements()
            {
                lblRoute.Location = new Point(planePic.Right + spacing, padding);
                lblFlightID.Location = new Point(planePic.Right + spacing, lblRoute.Bottom + 5);
                lblDate.Left = card.Width - lblDate.Width - padding - 35;
                lblDate.Top = padding;

                details.Width = card.Width - padding * 2;

                lblPlaneID.Location = new Point(0, 0);
                lblPlaneModel.Location = new Point(lblPlaneID.Right + 100, 0);

                rowDeparture.Top = 30;
                rowDeparture.Width = details.Width - 20;
                rowArrival.Top = rowDeparture.Bottom + 5;
                rowArrival.Width = details.Width - 20;

                int ySeatsHeader = rowArrival.Bottom + 10;
                lblTotalSeats.Location = new Point(0, ySeatsHeader);
                lblAvailableSeats.Location = new Point(lblTotalSeats.Right + spacing, ySeatsHeader);

                int ySeat = ySeatsHeader + lblTotalSeats.Height + 5;
                int xSeat = 0;
                foreach (Control seatLabel in details.Controls.OfType<Guna2HtmlLabel>().Where(l => l.Text.Contains("Class:")))
                {
                    seatLabel.Location = new Point(xSeat, ySeat);
                    xSeat += 220;
                }

                btnBook.Left = details.Width - btnBook.Width - 10;
                btnBook.Top = details.Height - btnBook.Height - 45;
            }

            PositionElements();
            card.Resize += (s, e) => PositionElements();

            return card;
        }
        public event Action<int> BookFlightRequested;

        private Guna2Panel CreateRow(string title, DateTime date, int panelWidth)
        {
            int padding = 10;

            var panel = new Guna2Panel
            {
                Size = new Size(panelWidth, 15),
                BackColor = Color.LightBlue,
                BorderRadius = 10
            };

            var lblTitle = new Guna2HtmlLabel
            {
                Text = title,
                Font = new Font("Arial", 9, FontStyle.Bold),
                AutoSize = true
            };
            panel.Controls.Add(lblTitle);

            var lblDate = new Guna2HtmlLabel
            {
                Text = date.ToString("yyyy-MM-dd"),
                Font = new Font("Arial", 9),
                AutoSize = true
            };
            panel.Controls.Add(lblDate);

            var lblTime = new Guna2HtmlLabel
            {
                Text = date.ToString("HH:mm"),
                Font = new Font("Arial", 9),
                AutoSize = true
            };
            panel.Controls.Add(lblTime);

            void UpdatePositions()
            {
                lblDate.Left = (panel.Width - lblDate.Width) / 2 - 200;
                lblTime.Left = panel.Width - lblTime.Width - padding - 250;
                lblTitle.Left = padding;
            }

            panel.Resize += (s, e) => UpdatePositions();
            UpdatePositions();

            return panel;
        }

        private Guna2HtmlLabel CreateSeatLabel(string text, int x, int y) =>
             new Guna2HtmlLabel { Text = text, Font = new Font("Arial", 9), Location = new Point(x, y), AutoSize = true };

        private void flowFlights_Paint(object sender, PaintEventArgs e)
        {

        }

        private Label lblHeader;

        private void flowFlights_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void flowFlights_Paint_2(object sender, PaintEventArgs e)
        {

        }
    }
}
