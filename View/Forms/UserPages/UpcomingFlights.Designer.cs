using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    partial class UpcomingFlights
    {


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
            flowFlights.Margin = new Padding(2);
            flowFlights.Name = "flowFlights";
            flowFlights.Padding = new Padding(9, 9, 9, 9);
            flowFlights.Size = new Size(963, 683);
            flowFlights.TabIndex = 0;
            flowFlights.WrapContents = false;
            // 
            // UpcomingFlights
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            Controls.Add(flowFlights);
            Margin = new Padding(2);
            Name = "UpcomingFlights";
            Size = new Size(963, 683);
            ResumeLayout(false);
        }
        private System.Windows.Forms.FlowLayoutPanel flowFlights;
    }
}
