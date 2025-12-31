using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Airport_Airplane_management_system.View.Controls;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    partial class MyTicketsBookingHistory
    {
        private IContainer components = null;

        private Guna2Panel root;
        private Guna2HtmlLabel lblTitle;
        private Guna2HtmlLabel lblSubtitle;

        private Guna2ShadowPanel cardFilters;
        private Guna2ComboBox cmbFilter;
        private Guna2TextBox txtSearch;
        private Guna2Button btnRefresh;
        private Guna2Button btnClear;
        private Guna2HtmlLabel lblFilterCaption;
        private Guna2HtmlLabel lblSearchCaption;

        private Guna2ShadowPanel cardList;
        private Guna2HtmlLabel lblCount;
        private Guna2Separator sepTop;

        // private FlowLayoutPanel flowTickets;
        private DoubleBufferedFlowLayoutPanel flowTickets;

        private Guna2Panel pnlEmpty;
        private Guna2HtmlLabel lblEmpty;
        private Panel pnlListBody;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pnlListBody = new Panel();
            flowTickets = new DoubleBufferedFlowLayoutPanel();
            pnlEmpty = new Guna2Panel();
            lblEmpty = new Guna2HtmlLabel();
            cardList = new Guna2ShadowPanel();
            lblCount = new Guna2HtmlLabel();
            sepTop = new Guna2Separator();
            root = new Guna2Panel();
            lblTitle = new Guna2HtmlLabel();
            lblSubtitle = new Guna2HtmlLabel();
            cardFilters = new Guna2ShadowPanel();
            lblFilterCaption = new Guna2HtmlLabel();
            cmbFilter = new Guna2ComboBox();
            lblSearchCaption = new Guna2HtmlLabel();
            txtSearch = new Guna2TextBox();
            btnRefresh = new Guna2Button();
            btnClear = new Guna2Button();
            pnlListBody.SuspendLayout();
            flowTickets.SuspendLayout();
            pnlEmpty.SuspendLayout();
            cardList.SuspendLayout();
            root.SuspendLayout();
            cardFilters.SuspendLayout();
            SuspendLayout();
            // 
            // pnlListBody
            // 
            pnlListBody.Dock = DockStyle.Fill;
            pnlListBody.Padding = new Padding(0);
            pnlListBody.Margin = new Padding(0);
            pnlListBody.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlListBody.BackColor = Color.Transparent;
            pnlListBody.Controls.Add(flowTickets);
            pnlListBody.Location = new Point(16, 70);
            pnlListBody.Name = "pnlListBody";
            pnlListBody.Size = new Size(1268, 860);
            pnlListBody.TabIndex = 2;
            // 
            // flowTickets
            // 
            flowTickets.Dock = DockStyle.Fill;
            flowTickets.AutoScroll = true;
            flowTickets.WrapContents = false;
            flowTickets.FlowDirection = FlowDirection.TopDown;
            flowTickets.Margin = new Padding(0); // add new
            flowTickets.AutoSize = false;          // VERY IMPORTANT
            flowTickets.Margin = new Padding(0);
            flowTickets.Padding = new Padding(6, 6, 24, 60); // it was only one 6
            flowTickets.BackColor = Color.Transparent;

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
            lblEmpty.Size = new Size(189, 33);
            lblEmpty.TabIndex = 0;
            lblEmpty.Text = "No tickets found.";
            // 
            // cardList
            // 
            cardList.BackColor = Color.Transparent;
            cardList.Controls.Add(lblCount);
            cardList.Controls.Add(sepTop);
            cardList.Controls.Add(pnlListBody);
            cardList.FillColor = Color.White;
            cardList.Location = new Point(395, 115);
            cardList.Name = "cardList";
            cardList.Padding = new Padding(16);
            cardList.Radius = 14;
            cardList.ShadowColor = Color.Black;
            cardList.ShadowDepth = 18;
            cardList.Size = new Size(1268, 860);
            cardList.TabIndex = 3;
            // 
            // lblCount
            // 
            lblCount.BackColor = Color.Transparent;
            lblCount.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblCount.ForeColor = Color.FromArgb(25, 33, 45);
            lblCount.Location = new Point(18, 16);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(105, 32);
            lblCount.TabIndex = 0;
            lblCount.Text = "Tickets (0)";
            // 
            // sepTop
            // 
            sepTop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            sepTop.Location = new Point(18, 52);
            sepTop.Name = "sepTop";
            sepTop.Size = new Size(1268, 10);
            sepTop.TabIndex = 1;
            // 
            // root
            // 
            root.BackColor = Color.FromArgb(245, 246, 250);
            root.Controls.Add(lblTitle);
            root.Controls.Add(lblSubtitle);
            root.Controls.Add(cardFilters);
            root.Controls.Add(cardList);
            root.CustomizableEdges = customizableEdges11;
            root.Dock = DockStyle.Fill;
            root.Location = new Point(0, 0);
            root.Name = "root";
            root.Padding = new Padding(20);
            root.ShadowDecoration.CustomizableEdges = customizableEdges12;
            root.Size = new Size(1750, 1094);
            root.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.Font = new Font("Segoe UI", 28.2F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(25, 33, 45);
            lblTitle.Location = new Point(22, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(240, 64);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "My Tickets";
            // 
            // lblSubtitle
            // 
            lblSubtitle.BackColor = Color.Transparent;
            lblSubtitle.Font = new Font("Segoe UI", 12F);
            lblSubtitle.ForeColor = Color.FromArgb(90, 105, 120);
            lblSubtitle.Location = new Point(26, 68);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(354, 30);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "View your bookings and manage tickets.";
            // 
            // cardFilters
            // 
            cardFilters.BackColor = Color.Transparent;
            cardFilters.Controls.Add(lblFilterCaption);
            cardFilters.Controls.Add(cmbFilter);
            cardFilters.Controls.Add(lblSearchCaption);
            cardFilters.Controls.Add(txtSearch);
            cardFilters.Controls.Add(btnRefresh);
            cardFilters.Controls.Add(btnClear);
            cardFilters.FillColor = Color.White;
            cardFilters.Location = new Point(20, 115);
            cardFilters.Name = "cardFilters";
            cardFilters.Padding = new Padding(16);
            cardFilters.Radius = 14;
            cardFilters.ShadowColor = Color.Black;
            cardFilters.ShadowDepth = 18;
            cardFilters.Size = new Size(360, 860);
            cardFilters.TabIndex = 2;
            // 
            // lblFilterCaption
            // 
            lblFilterCaption.BackColor = Color.Transparent;
            lblFilterCaption.Font = new Font("Segoe UI", 11F);
            lblFilterCaption.ForeColor = Color.FromArgb(60, 70, 85);
            lblFilterCaption.Location = new Point(18, 20);
            lblFilterCaption.Name = "lblFilterCaption";
            lblFilterCaption.Size = new Size(41, 27);
            lblFilterCaption.TabIndex = 0;
            lblFilterCaption.Text = "Filter";
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
            cmbFilter.Location = new Point(18, 48);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cmbFilter.Size = new Size(320, 36);
            cmbFilter.TabIndex = 1;
            // 
            // lblSearchCaption
            // 
            lblSearchCaption.BackColor = Color.Transparent;
            lblSearchCaption.Font = new Font("Segoe UI", 11F);
            lblSearchCaption.ForeColor = Color.FromArgb(60, 70, 85);
            lblSearchCaption.Location = new Point(18, 105);
            lblSearchCaption.Name = "lblSearchCaption";
            lblSearchCaption.Size = new Size(55, 27);
            lblSearchCaption.TabIndex = 2;
            lblSearchCaption.Text = "Search";
            // 
            // txtSearch
            // 
            txtSearch.BorderColor = Color.FromArgb(220, 225, 230);
            txtSearch.BorderRadius = 10;
            txtSearch.CustomizableEdges = customizableEdges5;
            txtSearch.DefaultText = "";
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.Location = new Point(18, 133);
            txtSearch.Margin = new Padding(3, 4, 3, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Passenger, city, flight id...";
            txtSearch.SelectedText = "";
            txtSearch.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtSearch.Size = new Size(320, 44);
            txtSearch.TabIndex = 3;
            // 
            // btnRefresh
            // 
            btnRefresh.BorderRadius = 12;
            btnRefresh.CustomizableEdges = customizableEdges7;
            btnRefresh.FillColor = Color.DodgerBlue;
            btnRefresh.Font = new Font("Segoe UI", 10F);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(18, 198);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnRefresh.Size = new Size(320, 46);
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
            btnClear.Location = new Point(18, 252);
            btnClear.Name = "btnClear";
            btnClear.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnClear.Size = new Size(320, 46);
            btnClear.TabIndex = 5;
            btnClear.Text = "Clear";
            // 
            // MyTicketsBookingHistory
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.FromArgb(245, 246, 250);
            Controls.Add(root);
            Name = "MyTicketsBookingHistory";
            Size = new Size(1750, 1094);
            pnlListBody.ResumeLayout(false);
            flowTickets.ResumeLayout(false);
            pnlEmpty.ResumeLayout(false);
            pnlEmpty.PerformLayout();
            cardList.ResumeLayout(false);
            cardList.PerformLayout();
            root.ResumeLayout(false);
            root.PerformLayout();
            cardFilters.ResumeLayout(false);
            cardFilters.PerformLayout();
            ResumeLayout(false);
            /*
            pnlListBody.Dock = DockStyle.Fill;
            pnlListBody.Location = new Point(16, 70);   // you can remove Location/Size if Dock=Fill
            pnlListBody.Size = new Size(cardList.Width - 32, cardList.Height - 86);
            pnlListBody.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pnlListBody.AutoScroll = true;
            pnlListBody.Padding = new Padding(6); */

        }
    }
}