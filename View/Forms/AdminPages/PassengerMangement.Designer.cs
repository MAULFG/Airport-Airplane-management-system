using System;
using System.Windows.Forms;
namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    partial class PassengerMangement
    {
        private System.ComponentModel.IContainer components = null;

        private Guna.UI2.WinForms.Guna2Panel root;
        private Guna.UI2.WinForms.Guna2Panel header;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSub;

        private Guna.UI2.WinForms.Guna2Panel statsPanel;
        private System.Windows.Forms.Label lblTotalPassengersValue;
        private System.Windows.Forms.Label lblTotalPassengersText;
        private System.Windows.Forms.Label lblUpcomingFlightsValue;
        private System.Windows.Forms.Label lblUpcomingFlightsText;

        private Guna.UI2.WinForms.Guna2TextBox txtSearch;
        private System.Windows.Forms.FlowLayoutPanel listPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.root = new Guna.UI2.WinForms.Guna2Panel();
            this.listPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.header = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSub = new System.Windows.Forms.Label();
            this.statsPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTotalPassengersValue = new System.Windows.Forms.Label();
            this.lblTotalPassengersText = new System.Windows.Forms.Label();
            this.lblUpcomingFlightsValue = new System.Windows.Forms.Label();
            this.lblUpcomingFlightsText = new System.Windows.Forms.Label();
            this.root.SuspendLayout();
            this.header.SuspendLayout();
            this.statsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // root
            // 
            this.root.Controls.Add(this.listPanel);
            this.root.Controls.Add(this.txtSearch);
            this.root.Controls.Add(this.header);
            this.root.Dock = System.Windows.Forms.DockStyle.Fill;
            this.root.FillColor = System.Drawing.Color.WhiteSmoke;
            this.root.Location = new System.Drawing.Point(0, 0);
            this.root.Name = "root";
            this.root.Padding = new System.Windows.Forms.Padding(28, 20, 28, 20);
            this.root.Size = new System.Drawing.Size(1319, 680);
            this.root.TabIndex = 0;
            // 
            // listPanel
            // 
            this.listPanel.AutoScroll = true;
            this.listPanel.BackColor = System.Drawing.Color.Transparent;
            this.listPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.listPanel.Location = new System.Drawing.Point(28, 167);
            this.listPanel.Name = "listPanel";
            this.listPanel.Padding = new System.Windows.Forms.Padding(0, 14, 0, 0);
            this.listPanel.Size = new System.Drawing.Size(1263, 493);
            this.listPanel.TabIndex = 0;
            this.listPanel.WrapContents = false;
            // 
            // txtSearch
            // 
            this.txtSearch.AutoRoundedCorners = true;
            this.txtSearch.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.txtSearch.BorderRadius = 27;
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.DefaultText = "";
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtSearch.Location = new System.Drawing.Point(28, 110);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.txtSearch.PlaceholderText = "Search by name, email, or phone...";
            this.txtSearch.SelectedText = "";
            this.txtSearch.Size = new System.Drawing.Size(1263, 57);
            this.txtSearch.TabIndex = 1;
            // 
            // header
            // 
            this.header.Controls.Add(this.lblTitle);
            this.header.Controls.Add(this.lblSub);
            this.header.Controls.Add(this.statsPanel);
            this.header.Dock = System.Windows.Forms.DockStyle.Top;
            this.header.FillColor = System.Drawing.Color.Transparent;
            this.header.Location = new System.Drawing.Point(28, 20);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(1263, 90);
            this.header.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.lblTitle.Location = new System.Drawing.Point(-1, -9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(401, 46);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Passenger Management";
            // 
            // lblSub
            // 
            this.lblSub.AutoSize = true;
            this.lblSub.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.lblSub.Location = new System.Drawing.Point(2, 48);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(410, 25);
            this.lblSub.TabIndex = 1;
            this.lblSub.Text = "View and manage all passenger flight bookings";
            this.lblSub.Click += new System.EventHandler(this.lblSub_Click);
            // 
            // statsPanel
            // 
            this.statsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.statsPanel.Controls.Add(this.lblTotalPassengersValue);
            this.statsPanel.Controls.Add(this.lblTotalPassengersText);
            this.statsPanel.Controls.Add(this.lblUpcomingFlightsValue);
            this.statsPanel.Controls.Add(this.lblUpcomingFlightsText);
            this.statsPanel.FillColor = System.Drawing.Color.Transparent;
            this.statsPanel.Location = new System.Drawing.Point(940, 6);
            this.statsPanel.Name = "statsPanel";
            this.statsPanel.Size = new System.Drawing.Size(320, 70);
            this.statsPanel.TabIndex = 2;
            // 
            // lblTotalPassengersValue
            // 
            this.lblTotalPassengersValue.AutoSize = true;
            this.lblTotalPassengersValue.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotalPassengersValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.lblTotalPassengersValue.Location = new System.Drawing.Point(64, 10);
            this.lblTotalPassengersValue.Name = "lblTotalPassengersValue";
            this.lblTotalPassengersValue.Size = new System.Drawing.Size(28, 32);
            this.lblTotalPassengersValue.TabIndex = 0;
            this.lblTotalPassengersValue.Text = "0";
            this.lblTotalPassengersValue.Click += new System.EventHandler(this.lblTotalPassengersValue_Click);
            // 
            // lblTotalPassengersText
            // 
            this.lblTotalPassengersText.AutoSize = true;
            this.lblTotalPassengersText.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalPassengersText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.lblTotalPassengersText.Location = new System.Drawing.Point(19, 38);
            this.lblTotalPassengersText.Name = "lblTotalPassengersText";
            this.lblTotalPassengersText.Size = new System.Drawing.Size(134, 23);
            this.lblTotalPassengersText.TabIndex = 1;
            this.lblTotalPassengersText.Text = "Total Passengers";
            this.lblTotalPassengersText.Click += new System.EventHandler(this.lblTotalPassengersText_Click);
            // 
            // lblUpcomingFlightsValue
            // 
            this.lblUpcomingFlightsValue.AutoSize = true;
            this.lblUpcomingFlightsValue.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblUpcomingFlightsValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.lblUpcomingFlightsValue.Location = new System.Drawing.Point(238, 10);
            this.lblUpcomingFlightsValue.Name = "lblUpcomingFlightsValue";
            this.lblUpcomingFlightsValue.Size = new System.Drawing.Size(28, 32);
            this.lblUpcomingFlightsValue.TabIndex = 2;
            this.lblUpcomingFlightsValue.Text = "0";
            this.lblUpcomingFlightsValue.Click += new System.EventHandler(this.lblUpcomingFlightsValue_Click);
            // 
            // lblUpcomingFlightsText
            // 
            this.lblUpcomingFlightsText.AutoSize = true;
            this.lblUpcomingFlightsText.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUpcomingFlightsText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.lblUpcomingFlightsText.Location = new System.Drawing.Point(174, 38);
            this.lblUpcomingFlightsText.Name = "lblUpcomingFlightsText";
            this.lblUpcomingFlightsText.Size = new System.Drawing.Size(143, 23);
            this.lblUpcomingFlightsText.TabIndex = 3;
            this.lblUpcomingFlightsText.Text = "Upcoming Flights";
            this.lblUpcomingFlightsText.Click += new System.EventHandler(this.lblUpcomingFlightsText_Click);
            // 
            // PassengerManagementForm
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.root);
            this.Name = "PassengerManagementForm";
            this.Size = new System.Drawing.Size(1319, 680);
            this.root.ResumeLayout(false);
            this.header.ResumeLayout(false);
            this.header.PerformLayout();
            this.statsPanel.ResumeLayout(false);
            this.statsPanel.PerformLayout();
            this.ResumeLayout(false);
        }
    }
}
