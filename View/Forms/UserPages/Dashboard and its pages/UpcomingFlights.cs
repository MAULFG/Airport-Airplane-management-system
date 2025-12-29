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

        // MVP trigger
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
                    c.Width = flowFlights.ClientSize.Width - 50; // adjust width dynamically
                }
            };
        }

        private void InitializeComponent()
        {
            flowFlights = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // flowFlights
            // 
            flowFlights.AutoScroll = true;
            flowFlights.Dock = DockStyle.Fill;
            flowFlights.FlowDirection = FlowDirection.TopDown;
            flowFlights.Location = new Point(0, 0);
            flowFlights.Name = "flowFlights";
            flowFlights.Padding = new Padding(10);
            flowFlights.Size = new Size(963, 592);
            flowFlights.TabIndex = 0;
            flowFlights.WrapContents = false;
            flowFlights.Paint += flowFlights_Paint;
            // 
            // UpcomingFlights
            // 
            BackColor = SystemColors.ControlDark;
            Controls.Add(flowFlights);
            Name = "UpcomingFlights";
            Size = new Size(1819, 761);
            ResumeLayout(false);
        }

        // ================= PRESENTER CALLS THIS =================
        public void LoadFlights(IEnumerable<Flight> flights)
        {
            flowFlights.Controls.Clear();

            foreach (var f in flights)
                flowFlights.Controls.Add(CreateCard(f));
        }

        // ================= ORIGINAL CARD DESIGN =================
        private Guna2Panel CreateCard(Flight f)
        {
            int padding = 10;
            int iconSize = 50;
            int spacing = 10;

            // ===== CARD =====
            var card = new Guna2Panel
            {
                Width = flowFlights.ClientSize.Width - 50,
                Height = 70,
                FillColor = Color.White,
                BorderRadius = 10,
                Margin = new Padding(5),
                Tag = false // collapsed
            };

            // ===== PLANE ICON =====
            var planePic = new Guna2PictureBox
            {
                Size = new Size(iconSize, iconSize),
                Image = Properties.Resources.icon1,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(padding, padding),
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            card.Controls.Add(planePic);

            // ===== ROUTE LABEL =====
            var lblRoute = new Guna2HtmlLabel
            {
                Text = $"{f.From} → {f.To}",
                Font = new Font("Arial", 12, FontStyle.Bold),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            card.Controls.Add(lblRoute);

            // ===== FLIGHT ID LABEL =====
            var lblFlightID = new Guna2HtmlLabel
            {
                Text = $"Flight #{f.FlightID}",
                Font = new Font("Arial", 10),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Left
            };
            card.Controls.Add(lblFlightID);

            // ===== DATE LABEL =====
            var lblDate = new Guna2HtmlLabel
            {
                Text = f.Departure.ToString("yyyy-MM-dd"),
                Font = new Font("Arial", 10),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            card.Controls.Add(lblDate);

            // ===== DETAILS PANEL =====
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

            // ===== BOOK BUTTON =====
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
            btnBook.Click += (s, e) => {
                int flightId = f.FlightID;
                BookFlightRequested?.Invoke(flightId);
            };

            // ===== PLANE INFO =====
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

            // ===== ROWS =====
            var rowDeparture = CreateRow("Departure:", f.Departure, details.Width);
            var rowArrival = CreateRow("Arrival:", f.Arrival, details.Width);
            details.Controls.Add(rowDeparture);
            details.Controls.Add(rowArrival);

            // ===== SEAT COUNTS =====
            int firstTotal = 0, businessTotal = 0, economyTotal = 0;
            int firstAvailable = 0, businessAvailable = 0, economyAvailable = 0;

            if (f?.FlightSeats != null)
            {
                foreach (var s in f.FlightSeats)
                {
                    if (s == null || string.IsNullOrWhiteSpace(s.ClassType)) continue;
                    string type = s.ClassType.Trim().ToLower();

                    switch (type)
                    {
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

            // ===== TOTAL / AVAILABLE SEATS =====
            var lblTotalSeats = new Guna2HtmlLabel
            {
                Text = $"Total Seats: {firstTotal + businessTotal + economyTotal}",
                Font = new Font("Arial", 9, FontStyle.Bold),
                AutoSize = true
            };
            details.Controls.Add(lblTotalSeats);

            var lblAvailableSeats = new Guna2HtmlLabel
            {
                Text = $"Available Seats: {firstAvailable + businessAvailable + economyAvailable}",
                Font = new Font("Arial", 9, FontStyle.Bold),
                AutoSize = true
            };
            details.Controls.Add(lblAvailableSeats);

            // ===== CLASS LABELS =====
            if (firstTotal > 0) details.Controls.Add(CreateSeatLabel($"First Class: {firstAvailable}/{firstTotal}", 0, 0));
            if (businessTotal > 0) details.Controls.Add(CreateSeatLabel($"Business Class: {businessAvailable}/{businessTotal}", 0, 0));
            if (economyTotal > 0) details.Controls.Add(CreateSeatLabel($"Economy Class: {economyAvailable}/{economyTotal}", 0, 0));

            // ===== TOGGLE =====
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
                if (c != btnBook) // <-- exclude the button
                    c.Click += (s, e) => Toggle();
            }

            // ===== POSITION ELEMENTS =====
            void PositionElements()
            {
                lblRoute.Location = new Point(planePic.Right + spacing, padding);
                lblFlightID.Location = new Point(planePic.Right + spacing, lblRoute.Bottom + 5);
                lblDate.Left = card.Width - lblDate.Width - padding-35;
                lblDate.Top = padding;

                details.Width = card.Width - padding * 2;

                lblPlaneID.Location = new Point(0, 0);
                lblPlaneModel.Location = new Point(lblPlaneID.Right + 100, 0);

                rowDeparture.Top = 30;
                rowDeparture.Width = details.Width - 20;
                rowArrival.Top = rowDeparture.Bottom + 5;
                rowArrival.Width = details.Width - 20;

                // Total/Available seats
                int ySeatsHeader = rowArrival.Bottom + 10;
                lblTotalSeats.Location = new Point(0, ySeatsHeader);
                lblAvailableSeats.Location = new Point(lblTotalSeats.Right + spacing, ySeatsHeader);

                // Class labels
                int ySeat = ySeatsHeader + lblTotalSeats.Height + 5;
                int xSeat = 0;
                foreach (Control seatLabel in details.Controls.OfType<Guna2HtmlLabel>().Where(l => l.Text.Contains("Class:")))
                {
                    seatLabel.Location = new Point(xSeat, ySeat);
                    xSeat += 220;
                }

                // Book button
                btnBook.Left = details.Width - btnBook.Width - 10;
                btnBook.Top = details.Height - btnBook.Height - 45;
            }

            PositionElements();
            card.Resize += (s, e) => PositionElements();

            return card;
        }
        public event Action<int> BookFlightRequested;

        // ===== CREATE ROW =====
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
                lblDate.Left = (panel.Width - lblDate.Width) / 2-200;
                lblTime.Left = panel.Width - lblTime.Width - padding-250;
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
    }
}
