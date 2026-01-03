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

        private void InitializeComponent()
        {
            flowFlights = new FlowLayoutPanel();

            SuspendLayout();

            // ---------------- FLOW PANEL ----------------
            flowFlights.Dock = DockStyle.Fill;
            flowFlights.AutoScroll = true;                 // ✅ scrolling
            flowFlights.WrapContents = false;              // ✅ vertical list
            flowFlights.FlowDirection = FlowDirection.TopDown;
            flowFlights.Padding = new Padding(10);
            flowFlights.BackColor = Color.Transparent;

            // ---------------- USER CONTROL ----------------
            BackColor = SystemColors.ControlDark;
            Dock = DockStyle.Fill;                          // ✅ IMPORTANT
            Controls.Add(flowFlights);
            Name = "UpcomingFlights";
            Size = new Size(1030, 720);



            ResumeLayout(false);
        }
    }
}
