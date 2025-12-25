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
        }

        private void InitializeComponent()
        {
            flowFlights = new FlowLayoutPanel();

            SuspendLayout();

            Dock = DockStyle.Fill;
            BackColor = SystemColors.ControlDark;

            flowFlights.Dock = DockStyle.Fill;
            flowFlights.AutoScroll = true;
            flowFlights.WrapContents = false;
            flowFlights.FlowDirection = FlowDirection.TopDown;
            flowFlights.Padding = new Padding(10);

            Controls.Add(flowFlights);

            // 🔑 RELOAD WHEN PAGE IS SHOWN
            VisibleChanged += (s, e) =>
            {
                if (Visible)
                    LoadFlightsRequested?.Invoke(this, EventArgs.Empty);
            };

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
            var card = new Guna2Panel
            {
                Width = 950,
                Height = 70,
                FillColor = Color.White,
                BorderRadius = 10,
                Margin = new Padding(5),
                Tag = false // collapsed
            };

            // ===== ICON =====
            var planePic = new Guna2PictureBox
            {
                Size = new Size(50, 50),
                Image = Properties.Resources.icon,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Location = new Point(10, 10)
            };
            card.Controls.Add(planePic);

            // ===== ROUTE =====
            card.Controls.Add(new Guna2HtmlLabel
            {
                Text = $"{f.From} → {f.To}",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(80, 10),
                AutoSize = true
            });

            // ===== FLIGHT ID =====
            card.Controls.Add(new Guna2HtmlLabel
            {
                Text = $"Flight #{f.FlightID}",
                Font = new Font("Arial", 10),
                Location = new Point(80, 35),
                AutoSize = true
            });

            // ===== DATE =====
            card.Controls.Add(new Guna2HtmlLabel
            {
                Text = f.Departure.ToString("yyyy-MM-dd"),
                Font = new Font("Arial", 10),
                AutoSize = true,
                Location = new Point(card.Width - 190, 25)
            });

            // ================= DETAILS PANEL =================
            var details = new Guna2Panel
            {
                Width = card.Width - 20,
                Height = 170,
                Location = new Point(10, 70),
                Visible = false,
                FillColor = Color.White
            };
            card.Controls.Add(details);

            // ===== PLANE INFO =====
            details.Controls.Add(new Guna2HtmlLabel
            {
                Text = f.Plane != null ? $"Plane ID: {f.Plane.PlaneID}" : "Plane ID: N/A",
                Font = new Font("Arial", 10, FontStyle.Bold),
                Location = new Point(0, 0),
                AutoSize = true
            });

            details.Controls.Add(new Guna2HtmlLabel
            {
                Text = f.Plane != null ? $"Model: {f.Plane.Model}" : "Model: N/A",
                Font = new Font("Arial", 10),
                Location = new Point(220, 0),
                AutoSize = true
            });

            // ===== TIMES =====
            details.Controls.Add(CreateRow("Departure:", f.Departure, 30));
            details.Controls.Add(CreateRow("Arrival:", f.Arrival, 55));

            // ===== SEAT CALCULATION =====
            int fT = 0, bT = 0, eT = 0;
            int fA = 0, bA = 0, eA = 0;

            foreach (var s in f.FlightSeats)
            {
                if (s.ClassType == "First") { fT++; if (!s.IsBooked) fA++; }
                if (s.ClassType == "Business") { bT++; if (!s.IsBooked) bA++; }
                if (s.ClassType == "Economy") { eT++; if (!s.IsBooked) eA++; }
            }

            int y = 110;
            int x = 0;
            int gap = 220;

            if (fT > 0)
            {
                details.Controls.Add(CreateSeatLabel($"First Class: {fA}/{fT}", x, y));
                x += gap;
            }
            if (bT > 0)
            {
                details.Controls.Add(CreateSeatLabel($"Business Class: {bA}/{bT}", x, y));
                x += gap;
            }
            if (eT > 0)
            {
                details.Controls.Add(CreateSeatLabel($"Economy Class: {eA}/{eT}", x, y));
            }

            // ===== GO BUTTON =====
            var btnGo = new Guna2Button
            {
                Text = "Go",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Size = new Size(160, 40),
                Location = new Point(details.Width - 180, 115),
                FillColor = Color.FromArgb(30, 144, 255),
                ForeColor = Color.White,
                BorderRadius = 10
            };
            details.Controls.Add(btnGo);

            // ===== TOGGLE =====
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

            return card;
        }

        private Control CreateRow(string title, DateTime date, int y)
        {
            var panel = new Guna2Panel
            {
                BackColor = Color.LightBlue,
                BorderRadius = 10,
                Location = new Point(0, y),
                Size = new Size(600, 20)
            };

            panel.Controls.Add(new Guna2HtmlLabel
            {
                Text = title,
                Font = new Font("Arial", 9, FontStyle.Bold),
                Location = new Point(0, 0)
            });

            panel.Controls.Add(new Guna2HtmlLabel
            {
                Text = date.ToString("yyyy-MM-dd HH:mm"),
                Location = new Point(120, 0)
            });

            return panel;
        }

        private Guna2HtmlLabel CreateSeatLabel(string text, int x, int y)
        {
            return new Guna2HtmlLabel
            {
                Text = text,
                Font = new Font("Arial", 9),
                Location = new Point(x, y),
                AutoSize = true
            };
        }
    }
}
