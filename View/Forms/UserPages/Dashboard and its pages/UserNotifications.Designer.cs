using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Airport_Airplane_management_system.View.Controls;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    partial class UserNotifications
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblUnread;

        private DoubleBufferedFlowLayoutPanel flow;
        private Panel pnlEmpty;
        private Label lblEmpty;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblUnread = new Label();
            flow = new DoubleBufferedFlowLayoutPanel();
            pnlEmpty = new Panel();
            lblEmpty = new Label();
            lblTitle = new Label();
            tableLayoutpnlSelection = new TableLayoutPanel();
            flowLayoutpnlSelection = new FlowLayoutPanel();
            lblCount = new Label();
            pnlSelection = new Panel();
            btnMarkReadSelected = new Guna2Button();
            btnMarkUnreadSelected = new Guna2Button();
            lblSelected = new Label();
            btnDeleteSelected = new Guna2Button();
            flowFilters = new FlowLayoutPanel();
            cmbFilter = new Guna2ComboBox();
            txtSearch = new Guna2TextBox();
            btnSelectAll = new Guna2Button();
            btnClearAll = new Guna2Button();
            btnRefresh = new Guna2Button();
            pnlEmpty.SuspendLayout();
            tableLayoutpnlSelection.SuspendLayout();
            flowLayoutpnlSelection.SuspendLayout();
            pnlSelection.SuspendLayout();
            flowFilters.SuspendLayout();
            SuspendLayout();
            // 
            // lblUnread
            // 
            lblUnread.AutoSize = true;
            lblUnread.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUnread.ForeColor = Color.FromArgb(120, 130, 140);
            lblUnread.Location = new Point(230, 74);
            lblUnread.Name = "lblUnread";
            lblUnread.Size = new Size(0, 19);
            lblUnread.TabIndex = 2;
            // 
            // flow
            // 
            flow.AutoScroll = true;
            flow.BackColor = Color.White;
            flow.Dock = DockStyle.Fill;
            flow.FlowDirection = FlowDirection.TopDown;
            flow.Location = new Point(10, 226);
            flow.Name = "flow";
            flow.Padding = new Padding(6);
            flow.Size = new Size(1010, 484);
            flow.TabIndex = 8;
            flow.WrapContents = false;
            // 
            // pnlEmpty
            // 
            pnlEmpty.BackColor = Color.White;
            pnlEmpty.Controls.Add(lblEmpty);
            pnlEmpty.Location = new Point(0, 0);
            pnlEmpty.Name = "pnlEmpty";
            pnlEmpty.Size = new Size(860, 200);
            pnlEmpty.TabIndex = 0;
            // 
            // lblEmpty
            // 
            lblEmpty.AutoSize = true;
            lblEmpty.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblEmpty.ForeColor = Color.FromArgb(120, 130, 140);
            lblEmpty.Location = new Point(20, 20);
            lblEmpty.Name = "lblEmpty";
            lblEmpty.Size = new Size(160, 25);
            lblEmpty.TabIndex = 0;
            lblEmpty.Text = "No notifications.";
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.Location = new Point(3, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1004, 33);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Notifications                                      ";
            lblTitle.Click += lblTitle_Click;
            // 
            // tableLayoutpnlSelection
            // 
            tableLayoutpnlSelection.ColumnCount = 1;
            tableLayoutpnlSelection.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 47.5F));
            tableLayoutpnlSelection.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 52.5F));
            tableLayoutpnlSelection.Controls.Add(flowFilters, 0, 1);
            tableLayoutpnlSelection.Controls.Add(flowLayoutpnlSelection, 0, 2);
            tableLayoutpnlSelection.Controls.Add(lblTitle, 0, 0);
            tableLayoutpnlSelection.Dock = DockStyle.Top;
            tableLayoutpnlSelection.Location = new Point(10, 10);
            tableLayoutpnlSelection.Name = "tableLayoutpnlSelection";
            tableLayoutpnlSelection.RowCount = 3;
            tableLayoutpnlSelection.RowStyles.Add(new RowStyle(SizeType.Percent, 33.3333321F));
            tableLayoutpnlSelection.RowStyles.Add(new RowStyle(SizeType.Percent, 66.6666641F));
            tableLayoutpnlSelection.RowStyles.Add(new RowStyle(SizeType.Absolute, 116F));
            tableLayoutpnlSelection.Size = new Size(1010, 216);
            tableLayoutpnlSelection.TabIndex = 9;
            // 
            // flowLayoutpnlSelection
            // 
            flowLayoutpnlSelection.Controls.Add(lblCount);
            flowLayoutpnlSelection.Controls.Add(pnlSelection);
            flowLayoutpnlSelection.Dock = DockStyle.Fill;
            flowLayoutpnlSelection.Location = new Point(3, 102);
            flowLayoutpnlSelection.Name = "flowLayoutpnlSelection";
            flowLayoutpnlSelection.Padding = new Padding(5);
            flowLayoutpnlSelection.Size = new Size(1004, 111);
            flowLayoutpnlSelection.TabIndex = 3;

            // 
            // lblCount
            // 
            lblCount.AutoSize = true;
            lblCount.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCount.ForeColor = Color.FromArgb(60, 70, 85);
            lblCount.Location = new Point(8, 5);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(125, 20);
            lblCount.TabIndex = 1;
            lblCount.Text = "Notifications (0)";
            // 
            // pnlSelection
            // 
            pnlSelection.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlSelection.BackColor = Color.FromArgb(240, 248, 255);
            pnlSelection.Controls.Add(btnMarkReadSelected);
            pnlSelection.Controls.Add(btnMarkUnreadSelected);
            pnlSelection.Controls.Add(lblSelected);
            pnlSelection.Controls.Add(btnDeleteSelected);
            pnlSelection.Location = new Point(8, 28);
            pnlSelection.Name = "pnlSelection";
            pnlSelection.Size = new Size(888, 40);
            pnlSelection.TabIndex = 7;
            pnlSelection.Visible = false;

            // 
            // btnMarkReadSelected
            // 
            btnMarkReadSelected.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMarkReadSelected.BorderRadius = 10;
            btnMarkReadSelected.CustomizableEdges = customizableEdges11;
            btnMarkReadSelected.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnMarkReadSelected.ForeColor = Color.White;
            btnMarkReadSelected.Location = new Point(2264, 5);
            btnMarkReadSelected.Name = "btnMarkReadSelected";
            btnMarkReadSelected.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnMarkReadSelected.Size = new Size(140, 30);
            btnMarkReadSelected.TabIndex = 0;
            btnMarkReadSelected.Text = "Mark read";
            // 
            // btnMarkUnreadSelected
            // 
            btnMarkUnreadSelected.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMarkUnreadSelected.BorderRadius = 10;
            btnMarkUnreadSelected.CustomizableEdges = customizableEdges13;
            btnMarkUnreadSelected.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnMarkUnreadSelected.ForeColor = Color.White;
            btnMarkUnreadSelected.Location = new Point(2264, 5);
            btnMarkUnreadSelected.Name = "btnMarkUnreadSelected";
            btnMarkUnreadSelected.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnMarkUnreadSelected.Size = new Size(140, 30);
            btnMarkUnreadSelected.TabIndex = 1;
            btnMarkUnreadSelected.Text = "Mark unread";
            // 
            // lblSelected
            // 
            lblSelected.AutoSize = true;
            lblSelected.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSelected.Location = new Point(12, 10);
            lblSelected.Name = "lblSelected";
            lblSelected.Size = new Size(82, 19);
            lblSelected.TabIndex = 0;
            lblSelected.Text = "Selected: 0";
            // 
            // btnDeleteSelected
            // 
            btnDeleteSelected.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDeleteSelected.BorderRadius = 10;
            btnDeleteSelected.CustomizableEdges = customizableEdges15;
            btnDeleteSelected.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnDeleteSelected.ForeColor = Color.White;
            btnDeleteSelected.Location = new Point(2264, 5);
            btnDeleteSelected.Name = "btnDeleteSelected";
            btnDeleteSelected.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnDeleteSelected.Size = new Size(150, 30);
            btnDeleteSelected.TabIndex = 1;
            btnDeleteSelected.Text = "Delete selected";
            // 
            // flowFilters
            // 
            flowFilters.Controls.Add(cmbFilter);
            flowFilters.Controls.Add(txtSearch);
            flowFilters.Controls.Add(btnSelectAll);
            flowFilters.Controls.Add(btnClearAll);
            flowFilters.Controls.Add(btnRefresh);
            flowFilters.Dock = DockStyle.Fill;
            flowFilters.Location = new Point(3, 36);
            flowFilters.Name = "flowFilters";
            flowFilters.Padding = new Padding(5);
            flowFilters.Size = new Size(1004, 60);
            flowFilters.TabIndex = 4;
            // 
            // cmbFilter
            // 
            cmbFilter.BackColor = Color.Transparent;
            cmbFilter.CustomizableEdges = customizableEdges1;
            cmbFilter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilter.FocusedColor = Color.Empty;
            cmbFilter.Font = new Font("Segoe UI", 10F);
            cmbFilter.ForeColor = Color.FromArgb(68, 88, 112);
            cmbFilter.ItemHeight = 30;
            cmbFilter.Items.AddRange(new object[] { "All", "Unread", "Read", "BookingConfirmed", "BookingCancelled", "FlightCancelled", "FlightDelayed", "SeatChanged" });
            cmbFilter.Location = new Point(8, 8);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.ShadowDecoration.CustomizableEdges = customizableEdges2;
            cmbFilter.Size = new Size(220, 36);
            cmbFilter.TabIndex = 3;
            // 
            // txtSearch
            // 
            txtSearch.CustomizableEdges = customizableEdges3;
            txtSearch.DefaultText = "";
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.Location = new Point(234, 9);
            txtSearch.Margin = new Padding(3, 4, 3, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search title, message, type, booking id...";
            txtSearch.SelectedText = "";
            txtSearch.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtSearch.Size = new Size(320, 36);
            txtSearch.TabIndex = 4;
            // 
            // btnSelectAll
            // 
            btnSelectAll.BorderRadius = 10;
            btnSelectAll.CustomizableEdges = customizableEdges5;
            btnSelectAll.FillColor = Color.DarkCyan;
            btnSelectAll.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSelectAll.ForeColor = Color.White;
            btnSelectAll.Location = new Point(560, 8);
            btnSelectAll.Name = "btnSelectAll";
            btnSelectAll.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnSelectAll.Size = new Size(120, 36);
            btnSelectAll.TabIndex = 7;
            btnSelectAll.Text = "Select all";
            // 
            // btnClearAll
            // 
            btnClearAll.BorderRadius = 10;
            btnClearAll.CustomizableEdges = customizableEdges7;
            btnClearAll.FillColor = Color.DarkCyan;
            btnClearAll.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClearAll.ForeColor = Color.White;
            btnClearAll.Location = new Point(686, 8);
            btnClearAll.Name = "btnClearAll";
            btnClearAll.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnClearAll.Size = new Size(120, 36);
            btnClearAll.TabIndex = 6;
            btnClearAll.Text = "Clear all";
            // 
            // btnRefresh
            // 
            btnRefresh.BorderRadius = 10;
            btnRefresh.CustomizableEdges = customizableEdges9;
            btnRefresh.FillColor = Color.DarkCyan;
            btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(812, 8);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnRefresh.Size = new Size(120, 36);
            btnRefresh.TabIndex = 5;
            btnRefresh.Text = "Refresh";
            // 
            // UserNotifications
            // 
            BackColor = Color.White;
            Controls.Add(flow);
            Controls.Add(tableLayoutpnlSelection);
            Controls.Add(lblUnread);
            Name = "UserNotifications";
            Padding = new Padding(10);
            Size = new Size(1030, 720);
            pnlEmpty.ResumeLayout(false);
            pnlEmpty.PerformLayout();
            tableLayoutpnlSelection.ResumeLayout(false);
            tableLayoutpnlSelection.PerformLayout();
            flowLayoutpnlSelection.ResumeLayout(false);
            flowLayoutpnlSelection.PerformLayout();
            pnlSelection.ResumeLayout(false);
            pnlSelection.PerformLayout();
            flowFilters.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
        private Label lblTitle;
        private TableLayoutPanel tableLayoutpnlSelection;
        private FlowLayoutPanel flowLayoutpnlSelection;
        private Label lblCount;
        private Panel pnlSelection;
        private Guna2Button btnMarkReadSelected;
        private Guna2Button btnMarkUnreadSelected;
        private Label lblSelected;
        private Guna2Button btnDeleteSelected;
        private FlowLayoutPanel flowFilters;
        private Guna2ComboBox cmbFilter;
        private Guna2TextBox txtSearch;
        private Guna2Button btnSelectAll;
        private Guna2Button btnClearAll;
        private Guna2Button btnRefresh;
    }
}