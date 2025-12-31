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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            root = new Guna.UI2.WinForms.Guna2Panel();
            listPanel = new FlowLayoutPanel();
            txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            header = new Guna.UI2.WinForms.Guna2Panel();
            lblTitle = new Label();
            lblSub = new Label();
            statsPanel = new Guna.UI2.WinForms.Guna2Panel();
            lblTotalPassengersValue = new Label();
            lblTotalPassengersText = new Label();
            root.SuspendLayout();
            header.SuspendLayout();
            statsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // root
            // 
            root.Controls.Add(listPanel);
            root.Controls.Add(txtSearch);
            root.Controls.Add(header);
            root.CustomizableEdges = customizableEdges7;
            root.Dock = DockStyle.Fill;
            root.FillColor = Color.WhiteSmoke;
            root.Location = new Point(0, 0);
            root.Name = "root";
            root.Padding = new Padding(28, 20, 28, 20);
            root.ShadowDecoration.CustomizableEdges = customizableEdges8;
            root.Size = new Size(1319, 680);
            root.TabIndex = 0;
            // 
            // listPanel
            // 
            listPanel.AutoScroll = true;
            listPanel.BackColor = Color.Transparent;
            listPanel.Dock = DockStyle.Fill;
            listPanel.FlowDirection = FlowDirection.TopDown;
            listPanel.Location = new Point(28, 167);
            listPanel.Name = "listPanel";
            listPanel.Padding = new Padding(0, 14, 0, 0);
            listPanel.Size = new Size(1263, 493);
            listPanel.TabIndex = 0;
            listPanel.WrapContents = false;
            // 
            // txtSearch
            // 
            txtSearch.AutoRoundedCorners = true;
            txtSearch.BorderColor = Color.FromArgb(220, 220, 220);
            txtSearch.BorderRadius = 27;
            txtSearch.Cursor = Cursors.IBeam;
            txtSearch.CustomizableEdges = customizableEdges1;
            txtSearch.DefaultText = "";
            txtSearch.Dock = DockStyle.Top;
            txtSearch.Font = new Font("Segoe UI", 10.5F);
            txtSearch.Location = new Point(28, 110);
            txtSearch.Margin = new Padding(0);
            txtSearch.Name = "txtSearch";
            txtSearch.Padding = new Padding(12, 0, 12, 0);
            txtSearch.PlaceholderText = "Search by name, email, or phone...";
            txtSearch.SelectedText = "";
            txtSearch.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtSearch.Size = new Size(1263, 57);
            txtSearch.TabIndex = 1;
            // 
            // header
            // 
            header.Controls.Add(lblTitle);
            header.Controls.Add(lblSub);
            header.Controls.Add(statsPanel);
            header.CustomizableEdges = customizableEdges5;
            header.Dock = DockStyle.Top;
            header.FillColor = Color.Transparent;
            header.Location = new Point(28, 20);
            header.Name = "header";
            header.ShadowDecoration.CustomizableEdges = customizableEdges6;
            header.Size = new Size(1263, 90);
            header.TabIndex = 2;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(20, 20, 20);
            lblTitle.Location = new Point(-1, -9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(401, 46);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Passenger Management";
            // 
            // lblSub
            // 
            lblSub.AutoSize = true;
            lblSub.Font = new Font("Segoe UI", 11F);
            lblSub.ForeColor = Color.FromArgb(110, 110, 110);
            lblSub.Location = new Point(2, 48);
            lblSub.Name = "lblSub";
            lblSub.Size = new Size(410, 25);
            lblSub.TabIndex = 1;
            lblSub.Text = "View and manage all passenger flight bookings";
            lblSub.Click += lblSub_Click;
            // 
            // statsPanel
            // 
            statsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            statsPanel.Controls.Add(lblTotalPassengersValue);
            statsPanel.Controls.Add(lblTotalPassengersText);
            statsPanel.CustomizableEdges = customizableEdges3;
            statsPanel.FillColor = Color.Transparent;
            statsPanel.Location = new Point(940, 6);
            statsPanel.Name = "statsPanel";
            statsPanel.ShadowDecoration.CustomizableEdges = customizableEdges4;
            statsPanel.Size = new Size(320, 70);
            statsPanel.TabIndex = 2;
            // 
            // lblTotalPassengersValue
            // 
            lblTotalPassengersValue.AutoSize = true;
            lblTotalPassengersValue.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotalPassengersValue.ForeColor = Color.FromArgb(20, 20, 20);
            lblTotalPassengersValue.Location = new Point(220, 10);
            lblTotalPassengersValue.Name = "lblTotalPassengersValue";
            lblTotalPassengersValue.Size = new Size(28, 32);
            lblTotalPassengersValue.TabIndex = 0;
            lblTotalPassengersValue.Text = "0";
            lblTotalPassengersValue.Click += lblTotalPassengersValue_Click;
            // 
            // lblTotalPassengersText
            // 
            lblTotalPassengersText.AutoSize = true;
            lblTotalPassengersText.Font = new Font("Segoe UI", 10F);
            lblTotalPassengersText.ForeColor = Color.FromArgb(110, 110, 110);
            lblTotalPassengersText.Location = new Point(166, 42);
            lblTotalPassengersText.Name = "lblTotalPassengersText";
            lblTotalPassengersText.Size = new Size(134, 23);
            lblTotalPassengersText.TabIndex = 1;
            lblTotalPassengersText.Text = "Total Passengers";
            lblTotalPassengersText.Click += lblTotalPassengersText_Click;
            // 
            // PassengerMangement
            // 
            BackColor = Color.WhiteSmoke;
            Controls.Add(root);
            Name = "PassengerMangement";
            Size = new Size(1319, 680);
            root.ResumeLayout(false);
            header.ResumeLayout(false);
            header.PerformLayout();
            statsPanel.ResumeLayout(false);
            statsPanel.PerformLayout();
            ResumeLayout(false);
        }
    }
}
