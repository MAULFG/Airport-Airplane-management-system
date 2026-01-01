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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pnlListBody = new Panel();
            flowTickets = new FlowLayoutPanel();
            pnlEmpty = new Guna2Panel();
            lblEmpty = new Guna2HtmlLabel();
            cardList = new Guna2ShadowPanel();
            lblCount = new Guna2HtmlLabel();
            sepTop = new Guna2Separator();
            root = new Guna2Panel();
            cmbFilter = new Guna2ComboBox();
            txtSearch = new Guna2TextBox();
            btnRefresh = new Guna2Button();
            btnClear = new Guna2Button();
            panelSearch = new Guna2Panel();
            flowFilters = new FlowLayoutPanel();
            lblPassengers = new Label();
            label1 = new Label();
            pnlListBody.SuspendLayout();
            pnlEmpty.SuspendLayout();
            cardList.SuspendLayout();
            root.SuspendLayout();
            panelSearch.SuspendLayout();
            flowFilters.SuspendLayout();
            SuspendLayout();
            // 
            // pnlListBody
            // 
            pnlListBody.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlListBody.BackColor = Color.Transparent;
            pnlListBody.Controls.Add(flowTickets);
            pnlListBody.Location = new Point(16, 70);
            pnlListBody.Margin = new Padding(0);
            pnlListBody.Name = "pnlListBody";
            pnlListBody.Size = new Size(186, 200);
            pnlListBody.TabIndex = 2;
            // 
            // flowTickets
            // 
            flowTickets.AutoScroll = true;
            flowTickets.BackColor = Color.Transparent;
            flowTickets.Dock = DockStyle.Fill;
            flowTickets.FlowDirection = FlowDirection.TopDown;
            flowTickets.Location = new Point(0, 0);
            flowTickets.Margin = new Padding(0);
            flowTickets.Name = "flowTickets";
            flowTickets.Padding = new Padding(6, 6, 24, 20);
            flowTickets.Size = new Size(186, 200);
            flowTickets.TabIndex = 0;
            flowTickets.WrapContents = false;
            // 
            // pnlEmpty
            // 
            pnlEmpty.Anchor = AnchorStyles.Top;
            pnlEmpty.Controls.Add(lblEmpty);
            pnlEmpty.CustomizableEdges = customizableEdges1;
            pnlEmpty.FillColor = Color.Transparent;
            pnlEmpty.Location = new Point(9, 9);
            pnlEmpty.Name = "pnlEmpty";
            pnlEmpty.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pnlEmpty.Size = new Size(600, 120);
            pnlEmpty.TabIndex = 0;
            // 
            // lblEmpty
            // 
            lblEmpty.BackColor = Color.Transparent;
            lblEmpty.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblEmpty.ForeColor = Color.FromArgb(90, 105, 120);
            lblEmpty.Location = new Point(0, 20);
            lblEmpty.Name = "lblEmpty";
            lblEmpty.Size = new Size(157, 27);
            lblEmpty.TabIndex = 0;
            lblEmpty.Text = "No tickets found.";
            // 
            // cardList
            // 
            cardList.BackColor = Color.Transparent;
            cardList.Controls.Add(lblCount);
            cardList.Controls.Add(sepTop);
            cardList.Controls.Add(pnlListBody);
            cardList.Dock = DockStyle.Bottom;
            cardList.FillColor = Color.White;
            cardList.Location = new Point(10, 76);
            cardList.Name = "cardList";
            cardList.Padding = new Padding(16);
            cardList.Radius = 14;
            cardList.ShadowColor = Color.Black;
            cardList.ShadowDepth = 18;
            cardList.Size = new Size(943, 597);
            cardList.TabIndex = 3;
            // 
            // lblCount
            // 
            lblCount.BackColor = Color.Transparent;
            lblCount.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblCount.ForeColor = Color.FromArgb(25, 33, 45);
            lblCount.Location = new Point(18, 16);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(86, 25);
            lblCount.TabIndex = 0;
            lblCount.Text = "Tickets (0)";
            // 
            // sepTop
            // 
            sepTop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            sepTop.Location = new Point(18, 52);
            sepTop.Name = "sepTop";
            sepTop.Size = new Size(943, 10);
            sepTop.TabIndex = 1;
            // 
            // root
            // 
            root.BackColor = Color.FromArgb(245, 246, 250);
            root.Controls.Add(panelSearch);
            root.Controls.Add(cardList);
            root.CustomizableEdges = customizableEdges13;
            root.Dock = DockStyle.Fill;
            root.Location = new Point(0, 0);
            root.Name = "root";
            root.Padding = new Padding(10);
            root.ShadowDecoration.CustomizableEdges = customizableEdges14;
            root.Size = new Size(963, 683);
            root.TabIndex = 0;
            // 
            // cmbFilter
            // 
            cmbFilter.BackColor = Color.Transparent;
            cmbFilter.BorderRadius = 10;
            cmbFilter.CustomizableEdges = customizableEdges3;
            cmbFilter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilter.FocusedColor = Color.Empty;
            cmbFilter.Font = new Font("Segoe UI", 10F);
            cmbFilter.ForeColor = Color.FromArgb(68, 88, 112);
            cmbFilter.ItemHeight = 30;
            cmbFilter.Items.AddRange(new object[] { "All", "Confirmed", "Pending", "Cancelled", "Upcoming", "Past" });
            cmbFilter.Location = new Point(49, 8);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cmbFilter.Size = new Size(187, 36);
            cmbFilter.TabIndex = 1;
            // 
            // txtSearch
            // 
            txtSearch.BorderColor = Color.FromArgb(220, 225, 230);
            txtSearch.BorderRadius = 10;
            txtSearch.CustomizableEdges = customizableEdges5;
            txtSearch.DefaultText = "";
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.Location = new Point(292, 9);
            txtSearch.Margin = new Padding(3, 4, 3, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Passenger, city, flight id...";
            txtSearch.SelectedText = "";
            txtSearch.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtSearch.Size = new Size(187, 36);
            txtSearch.TabIndex = 3;
            // 
            // btnRefresh
            // 
            btnRefresh.BorderRadius = 12;
            btnRefresh.CustomizableEdges = customizableEdges7;
            btnRefresh.FillColor = Color.DarkCyan;
            btnRefresh.Font = new Font("Segoe UI", 10F);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(485, 8);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnRefresh.Size = new Size(124, 36);
            btnRefresh.TabIndex = 4;
            btnRefresh.Text = "Refresh";
            // 
            // btnClear
            // 
            btnClear.BorderRadius = 12;
            btnClear.CustomizableEdges = customizableEdges9;
            btnClear.FillColor = Color.FromArgb(200, 205, 210);
            btnClear.Font = new Font("Segoe UI", 10F);
            btnClear.ForeColor = Color.FromArgb(25, 33, 45);
            btnClear.Location = new Point(615, 8);
            btnClear.Name = "btnClear";
            btnClear.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnClear.Size = new Size(124, 36);
            btnClear.TabIndex = 5;
            btnClear.Text = "Clear";
            // 
            // panelSearch
            // 
            panelSearch.BackColor = Color.White;
            panelSearch.BorderRadius = 20;
            panelSearch.Controls.Add(flowFilters);
            panelSearch.CustomizableEdges = customizableEdges11;
            panelSearch.Dock = DockStyle.Top;
            panelSearch.FillColor = Color.White;
            panelSearch.Location = new Point(10, 10);
            panelSearch.Name = "panelSearch";
            panelSearch.Padding = new Padding(15);
            panelSearch.ShadowDecoration.CustomizableEdges = customizableEdges12;
            panelSearch.Size = new Size(943, 80);
            panelSearch.TabIndex = 5;
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
            flowFilters.Size = new Size(913, 50);
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
            lblPassengers.TabIndex = 8;
            lblPassengers.Text = "Filter";
            lblPassengers.Click += lblPassengers_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.Location = new Point(244, 15);
            label1.Margin = new Padding(5, 10, 0, 0);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 9;
            label1.Text = "Search";
            // 
            // MyTicketsBookingHistory
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 246, 250);
            Controls.Add(root);
            Name = "MyTicketsBookingHistory";
            Size = new Size(963, 683);
            pnlListBody.ResumeLayout(false);
            pnlEmpty.ResumeLayout(false);
            pnlEmpty.PerformLayout();
            cardList.ResumeLayout(false);
            cardList.PerformLayout();
            root.ResumeLayout(false);
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
