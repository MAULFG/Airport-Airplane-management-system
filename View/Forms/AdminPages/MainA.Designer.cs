namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    partial class MainA
    {
        
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2Panel root;
        private Guna.UI2.WinForms.Guna2ShadowPanel header;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSub;

        private System.Windows.Forms.TableLayoutPanel kpiGrid;
        private System.Windows.Forms.TableLayoutPanel bottomGrid;

        private Guna.UI2.WinForms.Guna2ShadowPanel flightsCard;
        private Guna.UI2.WinForms.Guna2ShadowPanel alertsCard;

        // Flights
        private Guna.UI2.WinForms.Guna2ShadowPanel flightsHeader;
        private System.Windows.Forms.Label lblFlightsTitle;
        private System.Windows.Forms.LinkLabel lnkViewAllFlights;
        private System.Windows.Forms.FlowLayoutPanel flightsList;

        // Alerts
        private Guna.UI2.WinForms.Guna2ShadowPanel alertsHeader;
        private System.Windows.Forms.Label lblAlertsTitle;
        private System.Windows.Forms.Label lblAlertsRight;
        private System.Windows.Forms.FlowLayoutPanel alertsList;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            root = new Guna.UI2.WinForms.Guna2Panel();
            bottomGrid = new TableLayoutPanel();
            flightsCard = new Guna.UI2.WinForms.Guna2ShadowPanel();
            flightsList = new FlowLayoutPanel();
            flightsHeader = new Guna.UI2.WinForms.Guna2ShadowPanel();
            lblFlightsTitle = new Label();
            lnkViewAllFlights = new LinkLabel();
            alertsCard = new Guna.UI2.WinForms.Guna2ShadowPanel();
            alertsList = new FlowLayoutPanel();
            alertsHeader = new Guna.UI2.WinForms.Guna2ShadowPanel();
            lblAlertsTitle = new Label();
            lblAlertsRight = new Label();
            kpiGrid = new TableLayoutPanel();
            header = new Guna.UI2.WinForms.Guna2ShadowPanel();
            lblTitle = new Label();
            lblSub = new Label();
            root.SuspendLayout();
            bottomGrid.SuspendLayout();
            flightsCard.SuspendLayout();
            flightsHeader.SuspendLayout();
            alertsCard.SuspendLayout();
            alertsHeader.SuspendLayout();
            header.SuspendLayout();
            SuspendLayout(); 
            // root
            root.Controls.Add(bottomGrid);
            root.Controls.Add(kpiGrid);
            root.Controls.Add(header);
            root.CustomizableEdges = customizableEdges1;
            root.Dock = DockStyle.Fill;
            root.FillColor = Color.FromArgb(245, 246, 250);
            root.Location = new Point(0, 0);
            root.Margin = new Padding(3, 4, 3, 4);
            root.Name = "root";
            root.Padding = new Padding(32, 29, 32, 29);
            root.ShadowDecoration.CustomizableEdges = customizableEdges2;
            root.Size = new Size(1177, 960);
            root.TabIndex = 0; 
            // bottomGrid
            bottomGrid.BackColor = Color.Transparent;
            bottomGrid.ColumnCount = 2;
            bottomGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 53.4591179F));
            bottomGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46.5408821F));
            bottomGrid.Controls.Add(flightsCard, 0, 0);
            bottomGrid.Controls.Add(alertsCard, 1, 0);
            bottomGrid.Dock = DockStyle.Fill;
            bottomGrid.Location = new Point(32, 490);
            bottomGrid.Margin = new Padding(3, 4, 3, 4);
            bottomGrid.Name = "bottomGrid";
            bottomGrid.Padding = new Padding(0, 35, 0, 0);
            bottomGrid.RowCount = 1;
            bottomGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            bottomGrid.Size = new Size(1113, 441);
            bottomGrid.TabIndex = 0; 
            // flightsCard 
            flightsCard.BackColor = Color.Transparent;
            flightsCard.Controls.Add(flightsList);
            flightsCard.Controls.Add(flightsHeader);
            flightsCard.Dock = DockStyle.Fill;
            flightsCard.FillColor = Color.White;
            flightsCard.Location = new Point(0, 35);
            flightsCard.Margin = new Padding(0, 0, 18, 0);
            flightsCard.Name = "flightsCard";
            flightsCard.Padding = new Padding(21);
            flightsCard.Radius = 12;
            flightsCard.ShadowColor = Color.Black;
            flightsCard.ShadowDepth = 0;
            flightsCard.ShadowShift = 0;
            flightsCard.Size = new Size(577, 406);
            flightsCard.TabIndex = 0;
            // flightsList 
            flightsList.AutoScroll = true;
            flightsList.BackColor = Color.Transparent;
            flightsList.Dock = DockStyle.Fill;
            flightsList.FlowDirection = FlowDirection.TopDown;
            flightsList.Location = new Point(21, 62);
            flightsList.Margin = new Padding(0);
            flightsList.Name = "flightsList";
            flightsList.Padding = new Padding(0, 8, 0, 0);
            flightsList.Size = new Size(535, 323);
            flightsList.TabIndex = 0;
            flightsList.WrapContents = false; 
            // flightsHeader 
            flightsHeader.BackColor = Color.Transparent;
            flightsHeader.Controls.Add(lblFlightsTitle);
            flightsHeader.Controls.Add(lnkViewAllFlights);
            flightsHeader.Dock = DockStyle.Top;
            flightsHeader.FillColor = Color.Transparent;
            flightsHeader.Location = new Point(21, 21);
            flightsHeader.Margin = new Padding(3, 4, 3, 4);
            flightsHeader.Name = "flightsHeader";
            flightsHeader.ShadowColor = Color.Transparent;
            flightsHeader.ShadowDepth = 0;
            flightsHeader.ShadowShift = 0;
            flightsHeader.Size = new Size(535, 41);
            flightsHeader.TabIndex = 1; 
            // lblFlightsTitle 
            lblFlightsTitle.AutoSize = true;
            lblFlightsTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblFlightsTitle.ForeColor = Color.FromArgb(30, 30, 30);
            lblFlightsTitle.Location = new Point(5, 4);
            lblFlightsTitle.Name = "lblFlightsTitle";
            lblFlightsTitle.Size = new Size(236, 28);
            lblFlightsTitle.TabIndex = 0;
            lblFlightsTitle.Text = "Flights Status Overview";
            // lnkViewAllFlights 
            lnkViewAllFlights.ActiveLinkColor = Color.FromArgb(0, 150, 140);
            lnkViewAllFlights.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lnkViewAllFlights.AutoSize = true;
            lnkViewAllFlights.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lnkViewAllFlights.LinkColor = Color.FromArgb(0, 150, 140);
            lnkViewAllFlights.Location = new Point(383, 5);
            lnkViewAllFlights.Name = "lnkViewAllFlights";
            lnkViewAllFlights.Size = new Size(150, 23);
            lnkViewAllFlights.TabIndex = 1;
            lnkViewAllFlights.TabStop = true;
            lnkViewAllFlights.Text = "View all flights →";
            lnkViewAllFlights.VisitedLinkColor = Color.FromArgb(0, 150, 140); 
            // alertsCard 
            alertsCard.BackColor = Color.Transparent;
            alertsCard.Controls.Add(alertsList);
            alertsCard.Controls.Add(alertsHeader);
            alertsCard.Dock = DockStyle.Fill;
            alertsCard.FillColor = Color.White;
            alertsCard.Location = new Point(595, 35);
            alertsCard.Margin = new Padding(0);
            alertsCard.Name = "alertsCard";
            alertsCard.Padding = new Padding(21);
            alertsCard.Radius = 12;
            alertsCard.ShadowColor = Color.Black;
            alertsCard.ShadowDepth = 0;
            alertsCard.ShadowShift = 0;
            alertsCard.Size = new Size(518, 406);
            alertsCard.TabIndex = 1; 
            // alertsList 
            alertsList.AutoScroll = true;
            alertsList.BackColor = Color.Transparent;
            alertsList.Dock = DockStyle.Fill;
            alertsList.FlowDirection = FlowDirection.TopDown;
            alertsList.Location = new Point(21, 62);
            alertsList.Margin = new Padding(0);
            alertsList.Name = "alertsList";
            alertsList.Padding = new Padding(0, 13, 0, 0);
            alertsList.Size = new Size(476, 323);
            alertsList.TabIndex = 0;
            alertsList.WrapContents = false;
            // alertsHeader 
            alertsHeader.BackColor = Color.Transparent;
            alertsHeader.Controls.Add(lblAlertsTitle);
            alertsHeader.Controls.Add(lblAlertsRight);
            alertsHeader.Dock = DockStyle.Top;
            alertsHeader.FillColor = Color.White;
            alertsHeader.Location = new Point(21, 21);
            alertsHeader.Margin = new Padding(3, 4, 3, 4);
            alertsHeader.Name = "alertsHeader";
            alertsHeader.ShadowColor = Color.Transparent;
            alertsHeader.ShadowDepth = 0;
            alertsHeader.ShadowShift = 0;
            alertsHeader.Size = new Size(476, 41);
            alertsHeader.TabIndex = 1;
            // lblAlertsTitle 
            lblAlertsTitle.AutoSize = true;
            lblAlertsTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblAlertsTitle.ForeColor = Color.FromArgb(30, 30, 30);
            lblAlertsTitle.Location = new Point(5, 4);
            lblAlertsTitle.Name = "lblAlertsTitle";
            lblAlertsTitle.Size = new Size(186, 28);
            lblAlertsTitle.TabIndex = 0;
            lblAlertsTitle.Text = "Crew & Plane Alerts";
            // 
            // lblAlertsRight
            // 
            lblAlertsRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblAlertsRight.AutoSize = true;
            lblAlertsRight.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAlertsRight.ForeColor = Color.FromArgb(120, 120, 120);
            lblAlertsRight.Location = new Point(399, 5);
            lblAlertsRight.Name = "lblAlertsRight";
            lblAlertsRight.Size = new Size(75, 23);
            lblAlertsRight.TabIndex = 1;
            lblAlertsRight.Text = "0 Active";
            // kpiGrid
            kpiGrid.BackColor = Color.Transparent;
            kpiGrid.ColumnCount = 3;
            kpiGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            kpiGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            kpiGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            kpiGrid.Dock = DockStyle.Top;
            kpiGrid.Location = new Point(32, 116);
            kpiGrid.Margin = new Padding(3, 4, 3, 4);
            kpiGrid.Name = "kpiGrid";
            kpiGrid.Padding = new Padding(0, 11, 0, 0);
            kpiGrid.RowCount = 2;
            kpiGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            kpiGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            kpiGrid.Size = new Size(1113, 374);
            kpiGrid.TabIndex = 1;
            kpiGrid.Paint += kpiGrid_Paint;
            // header
            header.BackColor = Color.Transparent;
            header.Controls.Add(lblTitle);
            header.Controls.Add(lblSub);
            header.Dock = DockStyle.Top;
            header.FillColor = Color.Transparent;
            header.Location = new Point(32, 29);
            header.Margin = new Padding(3, 4, 3, 4);
            header.Name = "header";
            header.Radius = 10;
            header.ShadowColor = Color.Black;
            header.ShadowDepth = 0;
            header.ShadowShift = 0;
            header.Size = new Size(1113, 87);
            header.TabIndex = 2;
            // lblTitle 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(20, 20, 20);
            lblTitle.Location = new Point(10, 4);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(352, 46);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Dashboard Overview"; 
            // lblSub
            lblSub.AutoSize = true;
            lblSub.Font = new Font("Segoe UI", 11F);
            lblSub.ForeColor = Color.FromArgb(110, 110, 110);
            lblSub.Location = new Point(14, 49);
            lblSub.Name = "lblSub";
            lblSub.Size = new Size(406, 25);
            lblSub.TabIndex = 1;
            lblSub.Text = "Welcome back! Here's what's happening today.";
            // MainA 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 246, 250);
            Controls.Add(root);
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainA";
            Size = new Size(1177, 960);
            root.ResumeLayout(false);
            bottomGrid.ResumeLayout(false);
            flightsCard.ResumeLayout(false);
            flightsHeader.ResumeLayout(false);
            flightsHeader.PerformLayout();
            alertsCard.ResumeLayout(false);
            alertsHeader.ResumeLayout(false);
            alertsHeader.PerformLayout();
            header.ResumeLayout(false);
            header.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}
