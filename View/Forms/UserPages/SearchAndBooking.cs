using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Core.Classes;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    public partial class SearchAndBooking : UserControl, ISearchAndBookingView
    {
        public SearchAndBooking()
        {
            InitializeComponent();
            flowFlights.Resize += (s, e) =>
            {
                foreach (Control c in flowFlights.Controls)
                {
                    c.Width = flowFlights.ClientSize.Width - 50; // adjust width dynamically
                }
            };

            // Bind button click to event
            btnSearch.Click += (s, e) => SearchClicked?.Invoke(this, EventArgs.Empty);
        }

        public bool IsDateSelected => dtDeparture.Checked;

        public DateTime? DepartureDate => dtDeparture.Checked ? dtDeparture.Value : (DateTime?)null;


        public string From => cbFrom.SelectedIndex > 0 ? cbFrom.SelectedItem.ToString() : null;
        public string To => cbTo.SelectedIndex > 0 ? cbTo.SelectedItem.ToString() : null;
       
        public int Passengers => (int)numPassengers.Value;
        public string Class => cbClass.SelectedIndex > 0 ? cbClass.SelectedItem.ToString() : null;

        public event EventHandler SearchClicked;

        public void DisplayFlights(List<Flight> flights)
        {
            flowFlights.Controls.Clear();

            foreach (var flight in flights)
            {
                flowFlights.Controls.Add(CreateFlightCard(flight));
            }
        }

        private Guna2Panel CreateFlightCard(Flight f)
        {
            int cardMargin = 20;
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
                BorderThickness = 1,
                BorderColor = Color.Black,
                Margin = new Padding(10),
                Tag = false // collapsed
            };

            // ===== ICON =====
            var planePic = new Guna2PictureBox
            {
                Size = new Size(iconSize, iconSize),
                Location = new Point(padding, padding),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Properties.Resources.icon1
            };
            card.Controls.Add(planePic);

            // ===== ROUTE LABEL =====
            var lblRoute = new Guna2HtmlLabel
            {
                Text = $"{f.From} → {f.To}",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(planePic.Right + spacing, padding),
                AutoSize = true
            };
            card.Controls.Add(lblRoute);

            // ===== FLIGHT ID LABEL =====
            var lblFlightID = new Guna2HtmlLabel
            {
                Text = $"Flight #{f.FlightID}",
                Font = new Font("Arial", 10),
                Location = new Point(lblRoute.Left, lblRoute.Bottom + 5),
                AutoSize = true
            };
            card.Controls.Add(lblFlightID);

            // ===== DEPARTURE DATE LABEL (right aligned) =====
            var lblDate = new Guna2HtmlLabel
            {
                Text = f.Departure.ToString("yyyy-MM-dd"),
                Font = new Font("Arial", 10),
                AutoSize = true,
                Location= new Point(-500, padding)

            };
            card.Controls.Add(lblDate);

            // ===== DETAILS PANEL (collapsed by default) =====
            var details = new Guna2Panel
            {
                Width = card.Width - padding * 2,
                Height = 160,
                Location = new Point(padding, card.Height),
                FillColor = Color.White,
                Visible = false
            };
            card.Controls.Add(details);

            // Plane info
            var lblPlaneID = new Guna2HtmlLabel
            {
                Text = f.Plane != null ? $"Plane ID: {f.Plane.PlaneID}" : "Plane ID: N/A",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(0, 0),
                AutoSize = true
            };
            details.Controls.Add(lblPlaneID);

            var lblModel = new Guna2HtmlLabel
            {
                Text = f.Plane != null ? $"Model: {f.Plane.Model}" : "Model: N/A",
                Font = new Font("Arial", 10),
                Location = new Point(220, 0),
                AutoSize = true
            };
            details.Controls.Add(lblModel);

            // Times
            details.Controls.Add(CreateRow("Departure:", f.Departure, 30, details.Width));
            details.Controls.Add(CreateRow("Arrival:", f.Arrival, 55, details.Width));

            // Seats
            int firstTotal = 0, businessTotal = 0, economyTotal = 0;
            int firstAvailable = 0, businessAvailable = 0, economyAvailable = 0;

            foreach (var s in f.FlightSeats ?? Enumerable.Empty<FlightSeats>())
            {
                if (s?.ClassType == null) continue;
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

            int y = 110;
            int x = 0;
            int seatSpacing = 220;

            if (firstTotal > 0) { details.Controls.Add(CreateSeatLabel($"First Class: {firstAvailable}/{firstTotal}", x, y)); x += seatSpacing; }
            if (businessTotal > 0) { details.Controls.Add(CreateSeatLabel($"Business Class: {businessAvailable}/{businessTotal}", x, y)); x += seatSpacing; }
            if (economyTotal > 0) { details.Controls.Add(CreateSeatLabel($"Economy Class: {economyAvailable}/{economyTotal}", x, y)); }

            // ===== BOOK BUTTON =====
            var btnBook = new Guna2Button
            {
                Text = "Book",
                Width = 160,
                Height = 40,
                BorderRadius = 10,
                FillColor = Color.DarkCyan,
                ForeColor = Color.White,
                Font = new Font("Arial", 12, FontStyle.Bold),
                Anchor = AnchorStyles.Right | AnchorStyles.Bottom
            };
            
            
            details.Controls.Add(btnBook);
            btnBook.Top = details.Height - btnBook.Height - 10;
            btnBook.Left = details.Width - btnBook.Width - 10;
            btnBook.Click += (s, e) => {
                int flightId = f.FlightID;
                BookFlightRequested?.Invoke(flightId);
            };

            // ===== TOGGLE EXPAND/COLLAPSE =====
            void Toggle()
            {
                bool expanded = (bool)card.Tag;
                card.Tag = !expanded;
                details.Visible = !expanded;
                card.Height = !expanded ? 240 : 70;
            }

            card.Click += (s, e) => Toggle();
            foreach (Control c in card.Controls)
                c.Click += (s, e) => Toggle();

            // ===== UPDATE RIGHT-ALIGNED LABELS ON RESIZE =====
            card.Resize += (s, e) =>
            {
                lblDate.Left = card.Width - lblDate.Width - padding;
                details.Width = card.Width - padding * 2;
                btnBook.Left = details.Width - btnBook.Width - 10;
            };

            // Initial right-alignment
            lblDate.Left = card.Width - lblDate.Width - padding;

            return card;
        }


        private Guna2Panel CreateRow(string title, DateTime date, int y, int panelWidth)
        {
            int padding = 10;

            var panel = new Guna2Panel
            {
                BackColor = Color.LightBlue,
                BorderRadius = 10,
                Location = new Point(0, y),
                Size = new Size(flowFlights.ClientSize.Width , 20)
            };

            // Title Label (left aligned)
            var lblTitle = new Guna2HtmlLabel
            {
                Text = title,
                Font = new Font("Arial", 9, FontStyle.Bold),
                Location = new Point(padding, 0),
                AutoSize = true
            };
            panel.Controls.Add(lblTitle);

            // Date Label (center)
            var lblDate = new Guna2HtmlLabel
            {
                Text = date.ToString("yyyy-MM-dd"),
                Font = new Font("Arial", 9),
                AutoSize = true
            };
            panel.Controls.Add(lblDate);

            // Time Label (right)
            var lblTime = new Guna2HtmlLabel
            {
                Text = date.ToString("HH:mm"),
                Font = new Font("Arial", 9),
                AutoSize = true
            };
            panel.Controls.Add(lblTime);

            // ===== POSITIONING =====
            void UpdatePositions()
            {
                lblDate.Left = (panel.Width - lblDate.Width) / 2;
                lblTime.Left = panel.Width - lblTime.Width - padding;
            }

            panel.Resize += (s, e) => UpdatePositions();
            UpdatePositions(); // initial positioning

            return panel;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
        private Guna2HtmlLabel CreateSeatLabel(string text, int x, int y) =>
            new Guna2HtmlLabel { Text = text, Font = new Font("Arial", 9), Location = new Point(x, y), AutoSize = true };
        public event Action<int> BookFlightRequested;
        private void flowFlights_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
