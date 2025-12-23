using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Controls
{
    partial class UserFlightCard
    {
        private Panel pnlHeader;
        private Panel pnlDetails;
        private Label lblRoute;
        private Label lblFlightId;
        private Label lblDate;
        private Label lblPlane;
        private Label lblSeats;
        private Button btnGo;

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblRoute = new Label();
            lblFlightId = new Label();
            pnlDetails = new Panel();
            lblDate = new Label();
            lblPlane = new Label();
            lblSeats = new Label();
            btnGo = new Button();
            pnlHeader.SuspendLayout();
            pnlDetails.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(lblRoute);
            pnlHeader.Controls.Add(lblFlightId);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(900, 70);
            pnlHeader.TabIndex = 1;
        

            // 
            // lblRoute
            // 
            lblRoute.AutoSize = true;
            lblRoute.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblRoute.Location = new Point(0, 0);
            lblRoute.Name = "lblRoute";
            lblRoute.Size = new Size(0, 21);
            lblRoute.TabIndex = 0;
            // 
            // lblFlightId
            // 
            lblFlightId.AutoSize = true;
            lblFlightId.Location = new Point(0, 30);
            lblFlightId.Name = "lblFlightId";
            lblFlightId.Size = new Size(0, 15);
            lblFlightId.TabIndex = 1;
            // 
            // pnlDetails
            // 
            pnlDetails.Controls.Add(lblDate);
            pnlDetails.Controls.Add(lblPlane);
            pnlDetails.Controls.Add(lblSeats);
            pnlDetails.Controls.Add(btnGo);
            pnlDetails.Dock = DockStyle.Fill;
            pnlDetails.Location = new Point(0, 70);
            pnlDetails.Name = "pnlDetails";
            pnlDetails.Size = new Size(900, 0);
            pnlDetails.TabIndex = 0;
            // 
            // lblDate
            // 
            lblDate.Location = new Point(0, 10);
            lblDate.Name = "lblDate";
            lblDate.Size = new Size(100, 23);
            lblDate.TabIndex = 0;
            // 
            // lblPlane
            // 
            lblPlane.Location = new Point(0, 35);
            lblPlane.Name = "lblPlane";
            lblPlane.Size = new Size(100, 23);
            lblPlane.TabIndex = 1;
            // 
            // lblSeats
            // 
            lblSeats.Location = new Point(0, 60);
            lblSeats.Name = "lblSeats";
            lblSeats.Size = new Size(100, 23);
            lblSeats.TabIndex = 2;
            // 
            // btnGo
            // 
            btnGo.Location = new Point(0, 90);
            btnGo.Name = "btnGo";
            btnGo.Size = new Size(80, 23);
            btnGo.TabIndex = 3;
            btnGo.Text = "Go";
            // 
            // UserFlightCard
            // 
            Controls.Add(pnlDetails);
            Controls.Add(pnlHeader);
            Name = "UserFlightCard";
            Size = new Size(900, 70);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlDetails.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
