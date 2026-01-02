using System;
using System.Drawing;
using System.Windows.Forms;

namespace Ticket_Booking_System_OOP.WinFormsApp.Forms.UserPages
{
    public partial class UpcomingFlights : UserControl
    {
        private FlowLayoutPanel flowFlights;

        // Event used by the presenter
        public event EventHandler LoadFlightsRequested;

        public UpcomingFlights()
        {
            InitializeComponent();
        }

        private Guna.UI2.WinForms.Guna2Panel headerPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblHeader;

        private void InitializeComponent()
        {
            flowFlights = new FlowLayoutPanel();
            headerPanel = new Guna.UI2.WinForms.Guna2Panel();
            lblHeader = new Guna.UI2.WinForms.Guna2HtmlLabel();

            SuspendLayout();

            // ================= HEADER PANEL =================
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Height = 80;
            headerPanel.FillColor = Color.DarkCyan;
            headerPanel.BorderRadius = 20;
            headerPanel.Padding = new Padding(15);
            headerPanel.Margin = new Padding(10);

            // ================= HEADER LABEL =================
            lblHeader.Text = "Upcoming Flights";
            lblHeader.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblHeader.ForeColor = Color.White;
            lblHeader.AutoSize = true;
            lblHeader.Location = new Point(20, 20);

            headerPanel.Controls.Add(lblHeader);

            // ================= FLOW PANEL =================
            flowFlights.AutoScroll = true;
            flowFlights.Dock = DockStyle.Fill;
            flowFlights.FlowDirection = FlowDirection.TopDown;
            flowFlights.WrapContents = false;
            flowFlights.Padding = new Padding(10);
            flowFlights.BackColor = Color.Transparent;


            // ================= USER CONTROL =================
            BackColor = Color.White; // dark theme
            Controls.Add(flowFlights);
            Controls.Add(headerPanel); // add header on top
            Name = "UpcomingFlights";
            Size = new Size(1280, 720);

            ResumeLayout(false);
        }

    }
}
