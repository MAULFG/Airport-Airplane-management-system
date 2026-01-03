using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Airport_Airplane_management_system.View.Controls;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    partial class UserNotifications
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitle;
        private Label lblCount;
        private Label lblUnread;

        private Guna2ComboBox cmbFilter;
        private Guna2TextBox txtSearch;

        private Guna2Button btnRefresh;
        private Guna2Button btnClearAll;

        private Panel pnlSelection;
        private Label lblSelected;
        private Guna2Button btnDeleteSelected;

        private DoubleBufferedFlowLayoutPanel flow;
        private Panel pnlEmpty;
        private Label lblEmpty;
        private Guna2Button btnMarkReadSelected;
        private Guna2Button btnMarkUnreadSelected;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
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
            lblTitle = new Label();
            lblCount = new Label();
            lblUnread = new Label();
            cmbFilter = new Guna2ComboBox();
            txtSearch = new Guna2TextBox();
            btnRefresh = new Guna2Button();
            btnClearAll = new Guna2Button();
            pnlSelection = new Panel();
            lblSelected = new Label();
            btnDeleteSelected = new Guna2Button();
            flow = new DoubleBufferedFlowLayoutPanel();
            pnlEmpty = new Panel();
            lblEmpty = new Label();
            pnlSelection.SuspendLayout();
            pnlEmpty.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.Location = new Point(30, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(202, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Notifications";
            btnMarkReadSelected = new Guna2Button();
            btnMarkUnreadSelected = new Guna2Button();

            // btnMarkReadSelected
            btnMarkReadSelected.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMarkReadSelected.BorderRadius = 10;
            btnMarkReadSelected.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnMarkReadSelected.ForeColor = Color.White;
            btnMarkReadSelected.Size = new Size(140, 30);
            btnMarkReadSelected.Text = "Mark read";
            btnMarkReadSelected.Location = new Point(888 - 465, 5); // adjust
            pnlSelection.Controls.Add(btnMarkReadSelected);

            // btnMarkUnreadSelected
            btnMarkUnreadSelected.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMarkUnreadSelected.BorderRadius = 10;
            btnMarkUnreadSelected.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnMarkUnreadSelected.ForeColor = Color.White;
            btnMarkUnreadSelected.Size = new Size(140, 30);
            btnMarkUnreadSelected.Text = "Mark unread";
            btnMarkUnreadSelected.Location = new Point(888 - 315, 5); // adjust
            pnlSelection.Controls.Add(btnMarkUnreadSelected);

            // 
            // lblCount
            // 
            lblCount.AutoSize = true;
            lblCount.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCount.ForeColor = Color.FromArgb(60, 70, 85);
            lblCount.Location = new Point(32, 62);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(156, 25);
            lblCount.TabIndex = 1;
            lblCount.Text = "Notifications (0)";
            // 
            // lblUnread
            // 
            lblUnread.AutoSize = true;
            lblUnread.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUnread.ForeColor = Color.FromArgb(120, 130, 140);
            lblUnread.Location = new Point(220, 64);
            lblUnread.Name = "lblUnread";
            lblUnread.Size = new Size(0, 23);
            lblUnread.TabIndex = 2;
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
            cmbFilter.Location = new Point(32, 95);
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
            txtSearch.Location = new Point(270, 95);
            txtSearch.Margin = new Padding(3, 4, 3, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search title, message, type, booking id...";
            txtSearch.SelectedText = "";
            txtSearch.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtSearch.Size = new Size(360, 36);
            txtSearch.TabIndex = 4;
            // 
            // btnRefresh
            // 
            btnRefresh.BorderRadius = 10;
            btnRefresh.CustomizableEdges = customizableEdges5;
            btnRefresh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRefresh.ForeColor = Color.White;
            btnRefresh.Location = new Point(650, 95);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnRefresh.Size = new Size(130, 36);
            btnRefresh.TabIndex = 5;
            btnRefresh.Text = "Refresh";
            // 
            // btnClearAll
            // 
            btnClearAll.BorderRadius = 10;
            btnClearAll.CustomizableEdges = customizableEdges7;
            btnClearAll.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnClearAll.ForeColor = Color.White;
            btnClearAll.Location = new Point(790, 95);
            btnClearAll.Name = "btnClearAll";
            btnClearAll.ShadowDecoration.CustomizableEdges = customizableEdges8;
            btnClearAll.Size = new Size(130, 36);
            btnClearAll.TabIndex = 6;
            btnClearAll.Text = "Clear all";
            // 
            // pnlSelection
            // 
            pnlSelection.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlSelection.BackColor = Color.FromArgb(240, 248, 255);
            pnlSelection.Controls.Add(lblSelected);
            pnlSelection.Controls.Add(btnDeleteSelected);
            pnlSelection.Location = new Point(32, 140);
            pnlSelection.Name = "pnlSelection";
            pnlSelection.Size = new Size(888, 40);
            pnlSelection.TabIndex = 7;
            pnlSelection.Visible = false;
            // 
            // lblSelected
            // 
            lblSelected.AutoSize = true;
            lblSelected.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblSelected.Location = new Point(12, 10);
            lblSelected.Name = "lblSelected";
            lblSelected.Size = new Size(98, 23);
            lblSelected.TabIndex = 0;
            lblSelected.Text = "Selected: 0";
            // 
            // btnDeleteSelected
            // 
            btnDeleteSelected.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDeleteSelected.BorderRadius = 10;
            btnDeleteSelected.CustomizableEdges = customizableEdges9;
            btnDeleteSelected.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            btnDeleteSelected.ForeColor = Color.White;
            btnDeleteSelected.Location = new Point(888 - 165, 5);
            btnDeleteSelected.Name = "btnDeleteSelected";
            btnDeleteSelected.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btnDeleteSelected.Size = new Size(150, 30);
            btnDeleteSelected.TabIndex = 1;
            btnDeleteSelected.Text = "Delete selected";
            // 
            // flow
            // 
            flow.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flow.AutoScroll = true;
            flow.BackColor = Color.White;
            flow.FlowDirection = FlowDirection.TopDown;
            flow.Location = new Point(32, 190);
            flow.Name = "flow";
            flow.Padding = new Padding(6);
            flow.Size = new Size(888, 460);
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
            lblEmpty.Size = new Size(206, 32);
            lblEmpty.TabIndex = 0;
            lblEmpty.Text = "No notifications.";
            // 
            // UserNotifications
            // 
            BackColor = Color.White;
            Controls.Add(lblTitle);
            Controls.Add(lblCount);
            Controls.Add(lblUnread);
            Controls.Add(cmbFilter);
            Controls.Add(txtSearch);
            Controls.Add(btnRefresh);
            Controls.Add(btnClearAll);
            Controls.Add(pnlSelection);
            Controls.Add(flow);
            Name = "UserNotifications";
            Size = new Size(1464, 826);
            pnlSelection.ResumeLayout(false);
            pnlSelection.PerformLayout();
            pnlEmpty.ResumeLayout(false);
            pnlEmpty.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}