using Guna.UI2.WinForms;
using System.ComponentModel;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    partial class MyTicketsBookingHistory
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        

        private Guna2Panel root;
        private Guna2ComboBox cmbFilter;
        private Guna2TextBox txtSearch;
        private Guna2Button btnRefresh;
        private Guna2Button btnClear;

        private Guna2ShadowPanel cardList;
        private Guna2HtmlLabel lblCount;
        private Guna2Separator sepTop;

        // private FlowLayoutPanel flowTickets;
        private FlowLayoutPanel flowTickets;

        private Guna2Panel pnlEmpty;
        private Guna2HtmlLabel lblEmpty;
        private Panel pnlListBody;

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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            root = new Guna2Panel();
            cardList = new Guna2ShadowPanel();
            lblCount = new Guna2HtmlLabel();
            sepTop = new Guna2Separator();
            pnlListBody = new Panel();
            flowTickets = new FlowLayoutPanel();
            panelSearch = new Guna2Panel();
            flowFilters = new FlowLayoutPanel();
            lblPassengers = new Label();
            cmbFilter = new Guna2ComboBox();
            label1 = new Label();
            txtSearch = new Guna2TextBox();
            btnRefresh = new Guna2Button();
            btnClear = new Guna2Button();
            root.SuspendLayout();
            cardList.SuspendLayout();
            pnlListBody.SuspendLayout();
            panelSearch.SuspendLayout();
            flowFilters.SuspendLayout();
            SuspendLayout();
            // 
            // root
            // 
            root.BackColor = Color.FromArgb(245, 246, 250);
            root.Controls.Add(cardList);
            root.Controls.Add(panelSearch);
            root.CustomizableEdges = customizableEdges11;
            root.Dock = DockStyle.Fill;
            root.Location = new Point(0, 0);
            root.Name = "root";
            root.Padding = new Padding(15);
            root.ShadowDecoration.CustomizableEdges = customizableEdges12;
            root.Size = new Size(1030, 720);
            root.TabIndex = 0;
            // 
            // cardList
            // 
            cardList.BackColor = Color.Transparent;
            cardList.Controls.Add(lblCount);
            cardList.Controls.Add(sepTop);
            cardList.Controls.Add(pnlListBody);
            cardList.Dock = DockStyle.Fill;
            cardList.FillColor = Color.White;
            cardList.Location = new Point(15, 95);
            cardList.Name = "cardList";
            cardList.Padding = new Padding(16);
            cardList.Radius = 14;
            cardList.ShadowColor = Color.Black;
            cardList.ShadowDepth = 18;
            cardList.Size = new Size(1000, 610);
            cardList.TabIndex = 0;
            // 
            // lblCount
            // 
            lblCount.BackColor = Color.Transparent;
            lblCount.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblCount.Location = new Point(27, 21);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(86, 25);
            lblCount.TabIndex = 0;
            lblCount.Text = "Tickets (0)";
            // 
            // sepTop
            // 
            sepTop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            sepTop.Location = new Point(18, 50);
            sepTop.Name = "sepTop";
            sepTop.Size = new Size(1000, 2);
            sepTop.TabIndex = 1;
            // 
            // pnlListBody
            // 
            pnlListBody.Controls.Add(flowTickets);
            pnlListBody.Dock = DockStyle.Fill;
            pnlListBody.Location = new Point(16, 16);
            pnlListBody.Name = "pnlListBody";
            pnlListBody.Size = new Size(968, 578);
            pnlListBody.TabIndex = 2;
            // 
            // flowTickets
            // 
            flowTickets.AutoScroll = true;
            flowTickets.Dock = DockStyle.Fill;
            flowTickets.FlowDirection = FlowDirection.TopDown;
            flowTickets.Location = new Point(0, 0);
            flowTickets.Name = "flowTickets";
            flowTickets.Padding = new Padding(6, 6, 24, 20);
            flowTickets.Size = new Size(968, 578);
            flowTickets.TabIndex = 0;
            flowTickets.WrapContents = false;
            // 
            // panelSearch
            // 
            panelSearch.BorderRadius = 20;
            panelSearch.Controls.Add(flowFilters);
            panelSearch.CustomizableEdges = customizableEdges9;
            panelSearch.Dock = DockStyle.Top;
            panelSearch.FillColor = Color.White;
            panelSearch.Location = new Point(15, 15);
            panelSearch.Name = "panelSearch";
            panelSearch.Padding = new Padding(15);
            panelSearch.ShadowDecoration.CustomizableEdges = customizableEdges10;
            panelSearch.Size = new Size(1000, 80);
            panelSearch.TabIndex = 1;
            // 
            // flowFilters
            // 
            flowFilters.Controls.Add(lblPassengers);
            flowFilters.Controls.Add(cmbFilter);
            flowFilters.Controls.Add(label1);
            flowFilters.Controls.Add(txtSearch);
            flowFilters.Controls.Add(btnRefresh);
            flowFilters.Controls.Add(btnClear);
            flowFilters.Dock = DockStyle.Fill;
            flowFilters.Location = new Point(15, 15);
            flowFilters.Name = "flowFilters";
            flowFilters.Padding = new Padding(5);
            flowFilters.Size = new Size(970, 50);
            flowFilters.TabIndex = 0;
            // 
            // lblPassengers
            // 
            lblPassengers.AutoSize = true;
            lblPassengers.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPassengers.Location = new Point(10, 15);
            lblPassengers.Margin = new Padding(5, 10, 0, 0);
            lblPassengers.Name = "lblPassengers";
            lblPassengers.Size = new Size(36, 15);
            lblPassengers.TabIndex = 0;
            lblPassengers.Text = "Filter";
            // 
            // cmbFilter
            // 
            cmbFilter.BackColor = Color.Transparent;
            cmbFilter.BorderRadius = 10;
            cmbFilter.CustomizableEdges = customizableEdges1;
            cmbFilter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilter.FocusedColor = Color.Empty;
            cmbFilter.Font = new Font("Segoe UI", 10F);
            cmbFilter.ForeColor = Color.FromArgb(68, 88, 112);
            cmbFilter.ItemHeight = 30;
            cmbFilter.Items.AddRange(new object[] { "All", "Confirmed", "Pending", "Cancelled", "Upcoming", "Past" });
            cmbFilter.Location = new Point(49, 8);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.ShadowDecoration.CustomizableEdges = customizableEdges2;
            cmbFilter.Size = new Size(180, 36);
            cmbFilter.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(242, 15);
            label1.Margin = new Padding(10, 10, 0, 0);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 2;
            label1.Text = "Search";
            // 
            // txtSearch
            // 
            txtSearch.BorderRadius = 10;
            txtSearch.CustomizableEdges = customizableEdges3;
            txtSearch.DefaultText = "";
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.Location = new Point(290, 8);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Passenger, city, flight id...";
            txtSearch.SelectedText = "";
            txtSearch.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtSearch.Size = new Size(180, 36);
            txtSearch.TabIndex = 3;
            // 
            // btnRefresh
            // 
            btnRefresh.BorderRadius = 10;
            btnRefresh.CustomizableEdges = customizableEdges5;
            btnRefresh.FillColor = Color.DarkCyan;
            btnRefresh.Font = new Font("Segoe UI", 9F);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(476, 8);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnRefresh.Size = new Size(120, 36);
            btnRefresh.TabIndex = 4;
            btnRefresh.Text = "Refresh";
            // 
            // btnClear
            // 
            btnClear.BorderRadius = 10;
            btnClear.CustomizableEdges = customizableEdges7;
            btnClear.FillColor = Color.FromArgb(200, 205, 210);
            btnClear.Font = new Font("Segoe UI", 9F);
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(602, 8);
            btnClear.Name = "btnClear";
            btnClear.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnClear.Size = new Size(120, 36);
            btnClear.TabIndex = 5;
            btnClear.Text = "Clear";
            // 
            // MyTicketsBookingHistory
            // 
            Controls.Add(root);
            Name = "MyTicketsBookingHistory";
            Size = new Size(1030, 720);
            root.ResumeLayout(false);
            cardList.ResumeLayout(false);
            cardList.PerformLayout();
            pnlListBody.ResumeLayout(false);
            panelSearch.ResumeLayout(false);
            flowFilters.ResumeLayout(false);
            flowFilters.PerformLayout();
            ResumeLayout(false);
        }


        #endregion
        private Guna2Panel panelSearch;
        private FlowLayoutPanel flowFilters;
        private Label lblPassengers;
        private Label label1;
    }
}
