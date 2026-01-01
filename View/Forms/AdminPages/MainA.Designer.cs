namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    partial class MainA
    {
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2Panel root;
        private Guna.UI2.WinForms.Guna2Panel header;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSub;

        private System.Windows.Forms.TableLayoutPanel kpiGrid;
        private System.Windows.Forms.TableLayoutPanel bottomGrid;

        private Guna.UI2.WinForms.Guna2ShadowPanel flightsCard;
        private Guna.UI2.WinForms.Guna2ShadowPanel alertsCard;

        // Flights
        private Guna.UI2.WinForms.Guna2Panel flightsHeader;
        private System.Windows.Forms.Label lblFlightsTitle;
        private System.Windows.Forms.LinkLabel lnkViewAllFlights;
        private System.Windows.Forms.FlowLayoutPanel flightsList;

        // Alerts
        private Guna.UI2.WinForms.Guna2Panel alertsHeader;
        private System.Windows.Forms.Label lblAlertsTitle;
        private System.Windows.Forms.Label lblAlertsRight;
        private System.Windows.Forms.FlowLayoutPanel alertsList;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            root = new Guna.UI2.WinForms.Guna2Panel();
            header = new Guna.UI2.WinForms.Guna2Panel();
            lblTitle = new System.Windows.Forms.Label();
            lblSub = new System.Windows.Forms.Label();

            kpiGrid = new System.Windows.Forms.TableLayoutPanel();
            bottomGrid = new System.Windows.Forms.TableLayoutPanel();

            flightsCard = new Guna.UI2.WinForms.Guna2ShadowPanel();
            alertsCard = new Guna.UI2.WinForms.Guna2ShadowPanel();

            flightsHeader = new Guna.UI2.WinForms.Guna2Panel();
            lblFlightsTitle = new System.Windows.Forms.Label();
            lnkViewAllFlights = new System.Windows.Forms.LinkLabel();
            flightsList = new System.Windows.Forms.FlowLayoutPanel();

            alertsHeader = new Guna.UI2.WinForms.Guna2Panel();
            lblAlertsTitle = new System.Windows.Forms.Label();
            lblAlertsRight = new System.Windows.Forms.Label();
            alertsList = new System.Windows.Forms.FlowLayoutPanel();

            SuspendLayout();

            // root
            root.Dock = System.Windows.Forms.DockStyle.Fill;
            root.FillColor = System.Drawing.Color.FromArgb(245, 246, 250);
            root.Padding = new System.Windows.Forms.Padding(28, 22, 28, 22);
            root.Controls.Add(bottomGrid);
            root.Controls.Add(kpiGrid);
            root.Controls.Add(header);

            // header
            header.Dock = System.Windows.Forms.DockStyle.Top;
            header.Height = 86;
            header.FillColor = System.Drawing.Color.Transparent;
            header.Controls.Add(lblTitle);
            header.Controls.Add(lblSub);

            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(20, 20, 20);
            lblTitle.Location = new System.Drawing.Point(0, 0);
            lblTitle.Text = "Dashboard Overview";

            lblSub.AutoSize = true;
            lblSub.Font = new System.Drawing.Font("Segoe UI", 11F);
            lblSub.ForeColor = System.Drawing.Color.FromArgb(110, 110, 110);
            lblSub.Location = new System.Drawing.Point(2, 48);
            lblSub.Text = "Welcome back! Here's what's happening today.";

            // kpiGrid (3x2)
            kpiGrid.Dock = System.Windows.Forms.DockStyle.Top;
            kpiGrid.Height = 380;
            kpiGrid.ColumnCount = 3;
            kpiGrid.RowCount = 2;
            kpiGrid.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            kpiGrid.BackColor = System.Drawing.Color.Transparent;
            kpiGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333f));
            kpiGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333f));
            kpiGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.333f));
            kpiGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
            kpiGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
            kpiGrid.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.None;

            // bottomGrid (Flights left, Alerts right)
            bottomGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            bottomGrid.ColumnCount = 2;
            bottomGrid.RowCount = 1;
            bottomGrid.Padding = new System.Windows.Forms.Padding(0, 26, 0, 0);
            bottomGrid.BackColor = System.Drawing.Color.Transparent;
            bottomGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65f));
            bottomGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35f));
            bottomGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));

            // -----------------------
            // Flights card (FIXED)
            // -----------------------
            flightsCard.BackColor = System.Drawing.Color.Transparent;
            flightsCard.FillColor = System.Drawing.Color.White;
            flightsCard.Radius = 12;
            flightsCard.Padding = new System.Windows.Forms.Padding(18, 16, 18, 16);
            flightsCard.Margin = new System.Windows.Forms.Padding(0, 0, 16, 0);
            flightsCard.Dock = System.Windows.Forms.DockStyle.Fill;

            // flights header
            flightsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            flightsHeader.Height = 44;
            flightsHeader.FillColor = System.Drawing.Color.Transparent;
            flightsHeader.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);

            lblFlightsTitle.AutoSize = true;
            lblFlightsTitle.Text = "Flights Status Overview";
            lblFlightsTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            lblFlightsTitle.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            lblFlightsTitle.Location = new System.Drawing.Point(0, 10);

            lnkViewAllFlights.AutoSize = true;
            lnkViewAllFlights.Text = "View all flights →";
            lnkViewAllFlights.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lnkViewAllFlights.LinkColor = System.Drawing.Color.FromArgb(0, 150, 140);
            lnkViewAllFlights.ActiveLinkColor = System.Drawing.Color.FromArgb(0, 150, 140);
            lnkViewAllFlights.VisitedLinkColor = System.Drawing.Color.FromArgb(0, 150, 140);
            lnkViewAllFlights.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lnkViewAllFlights.Location = new System.Drawing.Point(0, 12);

            flightsHeader.Controls.Add(lblFlightsTitle);
            flightsHeader.Controls.Add(lnkViewAllFlights);

            // flights list
            flightsList.Dock = System.Windows.Forms.DockStyle.Fill;
            flightsList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flightsList.WrapContents = false;
            flightsList.AutoScroll = true;
            flightsList.BackColor = System.Drawing.Color.Transparent;
            flightsList.Margin = new System.Windows.Forms.Padding(0);
            flightsList.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);

            flightsCard.Controls.Add(flightsList);
            flightsCard.Controls.Add(flightsHeader);

            // keep the link pinned to the right (without resizing the list manually)
            flightsHeader.SizeChanged += (s, e) =>
            {
                lnkViewAllFlights.Location = new System.Drawing.Point(
                    flightsHeader.Width - lnkViewAllFlights.Width,
                    lnkViewAllFlights.Location.Y
                );
            };

            // -----------------------
            // Alerts card (same fix)
            // -----------------------
            alertsCard.BackColor = System.Drawing.Color.Transparent;
            alertsCard.FillColor = System.Drawing.Color.White;
            alertsCard.Radius = 12;
            alertsCard.Padding = new System.Windows.Forms.Padding(18, 16, 18, 16);
            alertsCard.Margin = new System.Windows.Forms.Padding(0);
            alertsCard.Dock = System.Windows.Forms.DockStyle.Fill;

            alertsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            alertsHeader.Height = 44;
            alertsHeader.FillColor = System.Drawing.Color.Transparent;

            lblAlertsTitle.AutoSize = true;
            lblAlertsTitle.Text = "Crew & Plane Alerts";
            lblAlertsTitle.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Bold);
            lblAlertsTitle.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            lblAlertsTitle.Location = new System.Drawing.Point(0, 10);

            lblAlertsRight.AutoSize = true;
            lblAlertsRight.Text = "0 Active";
            lblAlertsRight.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblAlertsRight.ForeColor = System.Drawing.Color.FromArgb(120, 120, 120);
            lblAlertsRight.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            lblAlertsRight.Location = new System.Drawing.Point(0, 12);

            alertsHeader.Controls.Add(lblAlertsTitle);
            alertsHeader.Controls.Add(lblAlertsRight);

            alertsList.Dock = System.Windows.Forms.DockStyle.Fill;
            alertsList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            alertsList.WrapContents = false;
            alertsList.AutoScroll = true;
            alertsList.BackColor = System.Drawing.Color.Transparent;
            alertsList.Margin = new System.Windows.Forms.Padding(0);
            alertsList.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);

            alertsCard.Controls.Add(alertsList);
            alertsCard.Controls.Add(alertsHeader);

            alertsHeader.SizeChanged += (s, e) =>
            {
                lblAlertsRight.Location = new System.Drawing.Point(
                    alertsHeader.Width - lblAlertsRight.Width,
                    lblAlertsRight.Location.Y
                );
            };

            // Add bottom cards to grid
            bottomGrid.Controls.Add(flightsCard, 0, 0);
            bottomGrid.Controls.Add(alertsCard, 1, 0);

            // Add to control
            Controls.Add(root);
            BackColor = System.Drawing.Color.FromArgb(245, 246, 250);
            Name = "MainA";
            Size = new System.Drawing.Size(1319, 680);

            ResumeLayout(false);
        }
    }
}
