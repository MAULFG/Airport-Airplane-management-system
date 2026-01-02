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

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
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
            // 
            // root
            // 
            root.Controls.Add(bottomGrid);
            root.Controls.Add(kpiGrid);
            root.Controls.Add(header);
            root.CustomizableEdges = customizableEdges1;
            root.Dock = DockStyle.Fill;
            root.FillColor = Color.FromArgb(245, 246, 250);
            root.Location = new Point(0, 0);
            root.Name = "root";
            root.Padding = new Padding(28, 22, 28, 22);
            root.ShadowDecoration.CustomizableEdges = customizableEdges2;
            root.Size = new Size(1280, 720);
            root.TabIndex = 0;
            // 
            // bottomGrid
            // 
            bottomGrid.BackColor = Color.Transparent;
            bottomGrid.ColumnCount = 2;
            bottomGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 61.5196075F));
            bottomGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 38.4803925F));
            bottomGrid.Controls.Add(flightsCard, 0, 0);
            bottomGrid.Controls.Add(alertsCard, 1, 0);
            bottomGrid.Dock = DockStyle.Fill;
            bottomGrid.Location = new Point(28, 478);
            bottomGrid.Name = "bottomGrid";
            bottomGrid.Padding = new Padding(0, 26, 0, 0);
            bottomGrid.RowCount = 1;
            bottomGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            bottomGrid.Size = new Size(1224, 220);
            bottomGrid.TabIndex = 0;
            // 
            // flightsCard
            // 
            flightsCard.BackColor = Color.Transparent;
            flightsCard.Controls.Add(flightsList);
            flightsCard.Controls.Add(flightsHeader);
            flightsCard.Dock = DockStyle.Fill;
            flightsCard.FillColor = Color.White;
            flightsCard.Location = new Point(0, 26);
            flightsCard.Margin = new Padding(0, 0, 16, 0);
            flightsCard.Name = "flightsCard";
            flightsCard.Padding = new Padding(18, 16, 18, 16);
            flightsCard.Radius = 12;
            flightsCard.ShadowColor = Color.Black;
            flightsCard.Size = new Size(737, 194);
            flightsCard.TabIndex = 0;
            // 
            // flightsList
            // 
            flightsList.AutoScroll = true;
            flightsList.BackColor = Color.Transparent;
            flightsList.Dock = DockStyle.Fill;
            flightsList.FlowDirection = FlowDirection.TopDown;
            flightsList.Location = new Point(18, 60);
            flightsList.Margin = new Padding(0);
            flightsList.Name = "flightsList";
            flightsList.Padding = new Padding(0, 6, 0, 0);
            flightsList.Size = new Size(701, 118);
            flightsList.TabIndex = 0;
            flightsList.WrapContents = false;
            // 
            // flightsHeader
            // 
            flightsHeader.BackColor = Color.Transparent;
            flightsHeader.Controls.Add(lblFlightsTitle);
            flightsHeader.Controls.Add(lnkViewAllFlights);
            flightsHeader.Dock = DockStyle.Top;
            flightsHeader.FillColor = Color.Transparent;
            flightsHeader.Location = new Point(18, 16);
            flightsHeader.Name = "flightsHeader";
            flightsHeader.ShadowColor = Color.Transparent;
            flightsHeader.ShadowDepth = 0;
            flightsHeader.ShadowShift = 0;
            flightsHeader.Size = new Size(701, 44);
            flightsHeader.TabIndex = 1;
            // 
            // lblFlightsTitle
            // 
            lblFlightsTitle.AutoSize = true;
            lblFlightsTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblFlightsTitle.ForeColor = Color.FromArgb(30, 30, 30);
            lblFlightsTitle.Location = new Point(4, 10);
            lblFlightsTitle.Name = "lblFlightsTitle";
            lblFlightsTitle.Size = new Size(189, 21);
            lblFlightsTitle.TabIndex = 0;
            lblFlightsTitle.Text = "Flights Status Overview";
            // 
            // lnkViewAllFlights
            // 
            lnkViewAllFlights.ActiveLinkColor = Color.FromArgb(0, 150, 140);
            lnkViewAllFlights.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lnkViewAllFlights.AutoSize = true;
            lnkViewAllFlights.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lnkViewAllFlights.LinkColor = Color.FromArgb(0, 150, 140);
            lnkViewAllFlights.Location = new Point(568, 12);
            lnkViewAllFlights.Name = "lnkViewAllFlights";
            lnkViewAllFlights.Size = new Size(122, 19);
            lnkViewAllFlights.TabIndex = 1;
            lnkViewAllFlights.TabStop = true;
            lnkViewAllFlights.Text = "View all flights →";
            lnkViewAllFlights.VisitedLinkColor = Color.FromArgb(0, 150, 140);
            // 
            // alertsCard
            // 
            alertsCard.BackColor = Color.Transparent;
            alertsCard.Controls.Add(alertsList);
            alertsCard.Controls.Add(alertsHeader);
            alertsCard.Dock = DockStyle.Fill;
            alertsCard.FillColor = Color.White;
            alertsCard.Location = new Point(753, 26);
            alertsCard.Margin = new Padding(0);
            alertsCard.Name = "alertsCard";
            alertsCard.Padding = new Padding(18, 16, 18, 16);
            alertsCard.Radius = 12;
            alertsCard.ShadowColor = Color.Black;
            alertsCard.Size = new Size(471, 194);
            alertsCard.TabIndex = 1;
            // 
            // alertsList
            // 
            alertsList.AutoScroll = true;
            alertsList.BackColor = Color.Transparent;
            alertsList.Dock = DockStyle.Fill;
            alertsList.FlowDirection = FlowDirection.TopDown;
            alertsList.Location = new Point(18, 60);
            alertsList.Margin = new Padding(0);
            alertsList.Name = "alertsList";
            alertsList.Padding = new Padding(0, 10, 0, 0);
            alertsList.Size = new Size(435, 118);
            alertsList.TabIndex = 0;
            alertsList.WrapContents = false;
            // 
            // alertsHeader
            // 
            alertsHeader.BackColor = Color.Transparent;
            alertsHeader.Controls.Add(lblAlertsTitle);
            alertsHeader.Controls.Add(lblAlertsRight);
            alertsHeader.Dock = DockStyle.Top;
            alertsHeader.FillColor = Color.White;
            alertsHeader.Location = new Point(18, 16);
            alertsHeader.Name = "alertsHeader";
            alertsHeader.ShadowColor = Color.Transparent;
            alertsHeader.ShadowDepth = 0;
            alertsHeader.ShadowShift = 0;
            alertsHeader.Size = new Size(435, 44);
            alertsHeader.TabIndex = 1;
            // 
            // lblAlertsTitle
            // 
            lblAlertsTitle.AutoSize = true;
            lblAlertsTitle.Font = new Font("Segoe UI", 11.5F, FontStyle.Bold);
            lblAlertsTitle.ForeColor = Color.FromArgb(30, 30, 30);
            lblAlertsTitle.Location = new Point(4, 10);
            lblAlertsTitle.Name = "lblAlertsTitle";
            lblAlertsTitle.Size = new Size(147, 21);
            lblAlertsTitle.TabIndex = 0;
            lblAlertsTitle.Text = "Crew & Plane Alerts";
            // 
            // lblAlertsRight
            // 
            lblAlertsRight.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblAlertsRight.AutoSize = true;
            lblAlertsRight.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAlertsRight.ForeColor = Color.FromArgb(120, 120, 120);
            lblAlertsRight.Location = new Point(235, 12);
            lblAlertsRight.Name = "lblAlertsRight";
            lblAlertsRight.Size = new Size(63, 19);
            lblAlertsRight.TabIndex = 1;
            lblAlertsRight.Text = "0 Active";
            // 
            // kpiGrid
            // 
            kpiGrid.BackColor = Color.Transparent;
            kpiGrid.ColumnCount = 3;
            kpiGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            kpiGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            kpiGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333F));
            kpiGrid.Dock = DockStyle.Top;
            kpiGrid.Location = new Point(28, 108);
            kpiGrid.Name = "kpiGrid";
            kpiGrid.Padding = new Padding(0, 8, 0, 0);
            kpiGrid.RowCount = 2;
            kpiGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            kpiGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            kpiGrid.Size = new Size(1224, 370);
            kpiGrid.TabIndex = 1;
            // 
            // header
            // 
            header.BackColor = Color.Transparent;
            header.Controls.Add(lblTitle);
            header.Controls.Add(lblSub);
            header.Dock = DockStyle.Top;
            header.FillColor = Color.White;
            header.Location = new Point(28, 22);
            header.Name = "header";
            header.Radius = 10;
            header.ShadowColor = Color.Black;
            header.Size = new Size(1224, 86);
            header.TabIndex = 2;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(20, 20, 20);
            lblTitle.Location = new Point(9, 11);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(286, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Dashboard Overview";
            // 
            // lblSub
            // 
            lblSub.AutoSize = true;
            lblSub.Font = new Font("Segoe UI", 11F);
            lblSub.ForeColor = Color.FromArgb(110, 110, 110);
            lblSub.Location = new Point(9, 48);
            lblSub.Name = "lblSub";
            lblSub.Size = new Size(320, 20);
            lblSub.TabIndex = 1;
            lblSub.Text = "Welcome back! Here's what's happening today.";
            // 
            // MainA
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 246, 250);
            Controls.Add(root);
            Name = "MainA";
            Size = new Size(1030, 720);
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
