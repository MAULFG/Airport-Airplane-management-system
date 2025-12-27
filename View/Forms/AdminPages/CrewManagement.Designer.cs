using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;


namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    partial class CrewManagement
    {
        private IContainer components = null;

        private Guna2Panel root;
        private Guna2ShadowPanel leftCard;
        private Guna2ShadowPanel rightCard;

        private Label lblTitle;
        private Label lblFullName;
        private Label lblRole;
        private Label lblEmail;
        private Label lblPhone;
        private Label lblStatus;
        private Label lblFlight;

        private Guna2TextBox txtFullName;
        private Guna2TextBox txtEmail;
        private Guna2TextBox txtPhone;

        private Guna2ComboBox cmbRole;
        private Guna2ComboBox cmbStatus;
        private Guna2ComboBox cmbFlight;

        private Guna2Button btnAddOrUpdate;
        private Guna2Button btnCancelEdit;

        private Guna2HtmlLabel lblCount;
        private Guna2ComboBox cmbFilter;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            root = new Guna2Panel();
            leftCard = new Guna2ShadowPanel();
            cmbFlight = new Guna2ComboBox();
            cmbStatus = new Guna2ComboBox();
            txtPhone = new Guna2TextBox();
            txtEmail = new Guna2TextBox();
            cmbRole = new Guna2ComboBox();
            txtFullName = new Guna2TextBox();
            lblTitle = new Label();
            lblFullName = new Label();
            lblRole = new Label();
            lblEmail = new Label();
            lblPhone = new Label();
            lblStatus = new Label();
            lblFlight = new Label();
            btnAddOrUpdate = new Guna2Button();
            btnCancelEdit = new Guna2Button();
            rightCard = new Guna2ShadowPanel();
            cmbFilter = new Guna2ComboBox();
            lblCount = new Guna2HtmlLabel();
            root.SuspendLayout();
            leftCard.SuspendLayout();
            rightCard.SuspendLayout();
            SuspendLayout();
            // 
            // root
            // 
            root.Controls.Add(leftCard);
            root.Controls.Add(rightCard);
            root.CustomizableEdges = customizableEdges19;
            root.Dock = DockStyle.Fill;
            root.Location = new Point(0, 0);
            root.Name = "root";
            root.Padding = new Padding(10);
            root.ShadowDecoration.CustomizableEdges = customizableEdges20;
            root.Size = new Size(963, 683);
            root.TabIndex = 0;
            // 
            // leftCard
            // 
            leftCard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            leftCard.BackColor = Color.Transparent;
            leftCard.Controls.Add(cmbFlight);
            leftCard.Controls.Add(cmbStatus);
            leftCard.Controls.Add(txtPhone);
            leftCard.Controls.Add(txtEmail);
            leftCard.Controls.Add(cmbRole);
            leftCard.Controls.Add(txtFullName);
            leftCard.Controls.Add(lblTitle);
            leftCard.Controls.Add(lblFullName);
            leftCard.Controls.Add(lblRole);
            leftCard.Controls.Add(lblEmail);
            leftCard.Controls.Add(lblPhone);
            leftCard.Controls.Add(lblStatus);
            leftCard.Controls.Add(lblFlight);
            leftCard.Controls.Add(btnAddOrUpdate);
            leftCard.Controls.Add(btnCancelEdit);
            leftCard.FillColor = Color.White;
            leftCard.Location = new Point(10, 10);
            leftCard.Name = "leftCard";
            leftCard.Padding = new Padding(10);
            leftCard.Radius = 14;
            leftCard.ShadowColor = Color.Black;
            leftCard.ShadowDepth = 18;
            leftCard.Size = new Size(357, 660);
            leftCard.TabIndex = 0;
            // 
            // cmbFlight
            // 
            cmbFlight.BackColor = Color.Transparent;
            cmbFlight.BorderRadius = 10;
            cmbFlight.CustomizableEdges = customizableEdges1;
            cmbFlight.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFlight.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFlight.FocusedColor = Color.Empty;
            cmbFlight.Font = new Font("Segoe UI", 10F);
            cmbFlight.ForeColor = Color.FromArgb(68, 88, 112);
            cmbFlight.ItemHeight = 30;
            cmbFlight.Location = new Point(17, 453);
            cmbFlight.Name = "cmbFlight";
            cmbFlight.ShadowDecoration.CustomizableEdges = customizableEdges2;
            cmbFlight.Size = new Size(327, 36);
            cmbFlight.TabIndex = 12;
            // 
            // cmbStatus
            // 
            cmbStatus.BackColor = Color.Transparent;
            cmbStatus.BorderRadius = 10;
            cmbStatus.CustomizableEdges = customizableEdges3;
            cmbStatus.DrawMode = DrawMode.OwnerDrawFixed;
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.FocusedColor = Color.Empty;
            cmbStatus.Font = new Font("Segoe UI", 10F);
            cmbStatus.ForeColor = Color.FromArgb(68, 88, 112);
            cmbStatus.ItemHeight = 30;
            cmbStatus.Items.AddRange(new object[] { "Active", "Inactive" });
            cmbStatus.Location = new Point(17, 388);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cmbStatus.Size = new Size(327, 36);
            cmbStatus.TabIndex = 10;
            // 
            // txtPhone
            // 
            txtPhone.BorderRadius = 10;
            txtPhone.CustomizableEdges = customizableEdges5;
            txtPhone.DefaultText = "";
            txtPhone.Font = new Font("Segoe UI", 9F);
            txtPhone.Location = new Point(17, 316);
            txtPhone.Margin = new Padding(3, 4, 3, 4);
            txtPhone.Name = "txtPhone";
            txtPhone.PlaceholderText = "+961 70 555 000";
            txtPhone.SelectedText = "";
            txtPhone.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtPhone.Size = new Size(327, 42);
            txtPhone.TabIndex = 8;
            // 
            // txtEmail
            // 
            txtEmail.BorderRadius = 10;
            txtEmail.CustomizableEdges = customizableEdges7;
            txtEmail.DefaultText = "";
            txtEmail.Font = new Font("Segoe UI", 9F);
            txtEmail.Location = new Point(17, 243);
            txtEmail.Margin = new Padding(3, 4, 3, 4);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "example@airline.com";
            txtEmail.SelectedText = "";
            txtEmail.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtEmail.Size = new Size(327, 42);
            txtEmail.TabIndex = 6;
            // 
            // cmbRole
            // 
            cmbRole.BackColor = Color.Transparent;
            cmbRole.BorderRadius = 10;
            cmbRole.CustomizableEdges = customizableEdges9;
            cmbRole.DrawMode = DrawMode.OwnerDrawFixed;
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.FocusedColor = Color.Empty;
            cmbRole.Font = new Font("Segoe UI", 10F);
            cmbRole.ForeColor = Color.FromArgb(68, 88, 112);
            cmbRole.ItemHeight = 30;
            cmbRole.Items.AddRange(new object[] { "Captain", "First Officer", "Flight Attendant", "Purser", "Flight Engineer" });
            cmbRole.Location = new Point(17, 177);
            cmbRole.Name = "cmbRole";
            cmbRole.ShadowDecoration.CustomizableEdges = customizableEdges10;
            cmbRole.Size = new Size(327, 36);
            cmbRole.TabIndex = 4;
            // 
            // txtFullName
            // 
            txtFullName.BorderRadius = 10;
            txtFullName.CustomizableEdges = customizableEdges11;
            txtFullName.DefaultText = "";
            txtFullName.Font = new Font("Segoe UI", 9F);
            txtFullName.Location = new Point(17, 105);
            txtFullName.Margin = new Padding(3, 4, 3, 4);
            txtFullName.Name = "txtFullName";
            txtFullName.PlaceholderText = "Enter FullName";
            txtFullName.SelectedText = "";
            txtFullName.ShadowDecoration.CustomizableEdges = customizableEdges12;
            txtFullName.Size = new Size(327, 42);
            txtFullName.TabIndex = 2;
            txtFullName.TextChanged += txtFullName_TextChanged;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblTitle.Location = new Point(26, 22);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(256, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Add / Edit Crew Member";
            // 
            // lblFullName
            // 
            lblFullName.Location = new Point(17, 78);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(100, 23);
            lblFullName.TabIndex = 1;
            lblFullName.Text = "Full Name *";
            // 
            // lblRole
            // 
            lblRole.Location = new Point(17, 151);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(100, 23);
            lblRole.TabIndex = 3;
            lblRole.Text = "Role *";
            // 
            // lblEmail
            // 
            lblEmail.Location = new Point(17, 216);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(100, 23);
            lblEmail.TabIndex = 5;
            lblEmail.Text = "Email *";
            // 
            // lblPhone
            // 
            lblPhone.Location = new Point(17, 289);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(100, 23);
            lblPhone.TabIndex = 7;
            lblPhone.Text = "Phone *";
            // 
            // lblStatus
            // 
            lblStatus.Location = new Point(17, 362);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(100, 23);
            lblStatus.TabIndex = 9;
            lblStatus.Text = "Status *";
            // 
            // lblFlight
            // 
            lblFlight.Location = new Point(17, 427);
            lblFlight.Name = "lblFlight";
            lblFlight.Size = new Size(100, 23);
            lblFlight.TabIndex = 11;
            lblFlight.Text = "Flight (optional)";
            // 
            // btnAddOrUpdate
            // 
            btnAddOrUpdate.BorderRadius = 12;
            btnAddOrUpdate.CustomizableEdges = customizableEdges13;
            btnAddOrUpdate.Font = new Font("Segoe UI", 9F);
            btnAddOrUpdate.ForeColor = Color.White;
            btnAddOrUpdate.Location = new Point(17, 512);
            btnAddOrUpdate.Name = "btnAddOrUpdate";
            btnAddOrUpdate.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnAddOrUpdate.Size = new Size(327, 40);
            btnAddOrUpdate.TabIndex = 13;
            btnAddOrUpdate.Text = "Add Crew Member";
            // 
            // btnCancelEdit
            // 
            btnCancelEdit.BorderRadius = 12;
            btnCancelEdit.CustomizableEdges = customizableEdges15;
            btnCancelEdit.Font = new Font("Segoe UI", 9F);
            btnCancelEdit.ForeColor = Color.White;
            btnCancelEdit.Location = new Point(17, 571);
            btnCancelEdit.Name = "btnCancelEdit";
            btnCancelEdit.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnCancelEdit.Size = new Size(327, 40);
            btnCancelEdit.TabIndex = 14;
            btnCancelEdit.Text = "Cancel Edit";
            btnCancelEdit.Visible = false;
            // 
            // rightCard
            // 
            rightCard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rightCard.BackColor = Color.Transparent;
            rightCard.Controls.Add(cmbFilter);
            rightCard.Controls.Add(lblCount);
            rightCard.FillColor = Color.White;
            rightCard.Location = new Point(373, 10);
            rightCard.Name = "rightCard";
            rightCard.Padding = new Padding(10);
            rightCard.Radius = 14;
            rightCard.ShadowColor = Color.Black;
            rightCard.ShadowDepth = 18;
            rightCard.Size = new Size(577, 660);
            rightCard.TabIndex = 1;
            // 
            // cmbFilter
            // 
            cmbFilter.BackColor = Color.Transparent;
            cmbFilter.BorderRadius = 10;
            cmbFilter.CustomizableEdges = customizableEdges17;
            cmbFilter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilter.FocusedColor = Color.Empty;
            cmbFilter.Font = new Font("Segoe UI", 10F);
            cmbFilter.ForeColor = Color.FromArgb(68, 88, 112);
            cmbFilter.ItemHeight = 30;
            cmbFilter.Items.AddRange(new object[] { "All Flights" });
            cmbFilter.Location = new Point(384, 16);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.ShadowDecoration.CustomizableEdges = customizableEdges18;
            cmbFilter.Size = new Size(180, 36);
            cmbFilter.TabIndex = 1;
            // 
            // lblCount
            // 
            lblCount.BackColor = Color.Transparent;
            lblCount.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblCount.Location = new Point(23, 16);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(183, 32);
            lblCount.TabIndex = 0;
            lblCount.Text = "Crew Members (0)";
            // 
            // CrewManagement
            // 
            BackColor = Color.FromArgb(245, 246, 250);
            Controls.Add(root);
            Name = "CrewManagement";
            Size = new Size(963, 683);
            root.ResumeLayout(false);
            leftCard.ResumeLayout(false);
            leftCard.PerformLayout();
            rightCard.ResumeLayout(false);
            rightCard.PerformLayout();
            ResumeLayout(false);
        }
    }
}
