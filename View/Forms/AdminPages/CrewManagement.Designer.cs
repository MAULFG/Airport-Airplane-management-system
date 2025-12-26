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
            lblTitle = new Label();
            lblFullName = new Label();
            txtFullName = new Guna2TextBox();
            lblRole = new Label();
            cmbRole = new Guna2ComboBox();
            lblEmail = new Label();
            txtEmail = new Guna2TextBox();
            lblPhone = new Label();
            txtPhone = new Guna2TextBox();
            lblStatus = new Label();
            cmbStatus = new Guna2ComboBox();
            lblFlight = new Label();
            cmbFlight = new Guna2ComboBox();
            btnAddOrUpdate = new Guna2Button();
            btnCancelEdit = new Guna2Button();
            rightCard = new Guna2ShadowPanel();
            lblCount = new Guna2HtmlLabel();
            cmbFilter = new Guna2ComboBox();
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
            root.Padding = new Padding(26);
            root.ShadowDecoration.CustomizableEdges = customizableEdges20;
            root.Size = new Size(963, 683);
            root.TabIndex = 0;
            // 
            // leftCard
            // 
            leftCard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            leftCard.BackColor = Color.Transparent;
            leftCard.Controls.Add(lblTitle);
            leftCard.Controls.Add(lblFullName);
            leftCard.Controls.Add(txtFullName);
            leftCard.Controls.Add(lblRole);
            leftCard.Controls.Add(cmbRole);
            leftCard.Controls.Add(lblEmail);
            leftCard.Controls.Add(txtEmail);
            leftCard.Controls.Add(lblPhone);
            leftCard.Controls.Add(txtPhone);
            leftCard.Controls.Add(lblStatus);
            leftCard.Controls.Add(cmbStatus);
            leftCard.Controls.Add(lblFlight);
            leftCard.Controls.Add(cmbFlight);
            leftCard.Controls.Add(btnAddOrUpdate);
            leftCard.Controls.Add(btnCancelEdit);
            leftCard.FillColor = Color.White;
            leftCard.Location = new Point(26, 26);
            leftCard.Name = "leftCard";
            leftCard.Padding = new Padding(20);
            leftCard.Radius = 14;
            leftCard.ShadowColor = Color.Black;
            leftCard.ShadowDepth = 18;
            leftCard.Size = new Size(353, 628);
            leftCard.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblTitle.Location = new Point(26, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(213, 23);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Add / Edit Crew Member";
            // 
            // lblFullName
            // 
            lblFullName.Location = new Point(26, 68);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(100, 23);
            lblFullName.TabIndex = 1;
            lblFullName.Text = "Full Name *";
            // 
            // txtFullName
            // 
            txtFullName.BorderRadius = 10;
            txtFullName.CustomizableEdges = customizableEdges1;
            txtFullName.DefaultText = "";
            txtFullName.Font = new Font("Segoe UI", 9F);
            txtFullName.Location = new Point(26, 91);
            txtFullName.Name = "txtFullName";
            txtFullName.PlaceholderText = "Enter FullName";
            txtFullName.SelectedText = "";
            txtFullName.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtFullName.Size = new Size(308, 42);
            txtFullName.TabIndex = 2;
            // 
            // lblRole
            // 
            lblRole.Location = new Point(26, 136);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(100, 23);
            lblRole.TabIndex = 3;
            lblRole.Text = "Role *";
            // 
            // cmbRole
            // 
            cmbRole.BackColor = Color.Transparent;
            cmbRole.BorderRadius = 10;
            cmbRole.CustomizableEdges = customizableEdges3;
            cmbRole.DrawMode = DrawMode.OwnerDrawFixed;
            cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRole.FocusedColor = Color.Empty;
            cmbRole.Font = new Font("Segoe UI", 10F);
            cmbRole.ForeColor = Color.FromArgb(68, 88, 112);
            cmbRole.ItemHeight = 30;
            cmbRole.Items.AddRange(new object[] { "Captain", "First Officer", "Flight Attendant", "Purser", "Flight Engineer" });
            cmbRole.Location = new Point(26, 159);
            cmbRole.Name = "cmbRole";
            cmbRole.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cmbRole.Size = new Size(308, 36);
            cmbRole.TabIndex = 4;
            // 
            // lblEmail
            // 
            lblEmail.Location = new Point(26, 197);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(100, 23);
            lblEmail.TabIndex = 5;
            lblEmail.Text = "Email *";
            // 
            // txtEmail
            // 
            txtEmail.BorderRadius = 10;
            txtEmail.CustomizableEdges = customizableEdges5;
            txtEmail.DefaultText = "";
            txtEmail.Font = new Font("Segoe UI", 9F);
            txtEmail.Location = new Point(26, 220);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "example@airline.com";
            txtEmail.SelectedText = "";
            txtEmail.ShadowDecoration.CustomizableEdges = customizableEdges6;
            txtEmail.Size = new Size(308, 42);
            txtEmail.TabIndex = 6;
            // 
            // lblPhone
            // 
            lblPhone.Location = new Point(26, 265);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(100, 23);
            lblPhone.TabIndex = 7;
            lblPhone.Text = "Phone *";
            // 
            // txtPhone
            // 
            txtPhone.BorderRadius = 10;
            txtPhone.CustomizableEdges = customizableEdges7;
            txtPhone.DefaultText = "";
            txtPhone.Font = new Font("Segoe UI", 9F);
            txtPhone.Location = new Point(26, 288);
            txtPhone.Name = "txtPhone";
            txtPhone.PlaceholderText = "+961 70 555 000";
            txtPhone.SelectedText = "";
            txtPhone.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtPhone.Size = new Size(308, 42);
            txtPhone.TabIndex = 8;
            // 
            // lblStatus
            // 
            lblStatus.Location = new Point(26, 334);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(100, 23);
            lblStatus.TabIndex = 9;
            lblStatus.Text = "Status *";
            // 
            // cmbStatus
            // 
            cmbStatus.BackColor = Color.Transparent;
            cmbStatus.BorderRadius = 10;
            cmbStatus.CustomizableEdges = customizableEdges9;
            cmbStatus.DrawMode = DrawMode.OwnerDrawFixed;
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.FocusedColor = Color.Empty;
            cmbStatus.Font = new Font("Segoe UI", 10F);
            cmbStatus.ForeColor = Color.FromArgb(68, 88, 112);
            cmbStatus.ItemHeight = 30;
            cmbStatus.Items.AddRange(new object[] { "Active", "Inactive" });
            cmbStatus.SelectedIndex = 1;
            cmbStatus.Location = new Point(26, 357);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.ShadowDecoration.CustomizableEdges = customizableEdges10;
            cmbStatus.Size = new Size(308, 36);
            cmbStatus.TabIndex = 10;
            // 
            // lblFlight
            // 
            lblFlight.Location = new Point(26, 396);
            lblFlight.Name = "lblFlight";
            lblFlight.Size = new Size(100, 23);
            lblFlight.TabIndex = 11;
            lblFlight.Text = "Flight (optional)";
            // 
            // cmbFlight
            // 
            cmbFlight.BackColor = Color.Transparent;
            cmbFlight.BorderRadius = 10;
            cmbFlight.CustomizableEdges = customizableEdges11;
            cmbFlight.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFlight.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFlight.FocusedColor = Color.Empty;
            cmbFlight.Font = new Font("Segoe UI", 10F);
            cmbFlight.ForeColor = Color.FromArgb(68, 88, 112);
            cmbFlight.ItemHeight = 30;
            cmbFlight.Location = new Point(26, 422);
            cmbFlight.Name = "cmbFlight";
            cmbFlight.ShadowDecoration.CustomizableEdges = customizableEdges12;
            cmbFlight.Size = new Size(308, 36);
            cmbFlight.TabIndex = 12;
            // 
            // btnAddOrUpdate
            // 
            btnAddOrUpdate.BorderRadius = 12;
            btnAddOrUpdate.CustomizableEdges = customizableEdges13;
            btnAddOrUpdate.Font = new Font("Segoe UI", 9F);
            btnAddOrUpdate.ForeColor = Color.White;
            btnAddOrUpdate.Location = new Point(89, 473);
            btnAddOrUpdate.Name = "btnAddOrUpdate";
            btnAddOrUpdate.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnAddOrUpdate.Size = new Size(180, 40);
            btnAddOrUpdate.TabIndex = 13;
            btnAddOrUpdate.Text = "Add Crew Member";
            // 
            // btnCancelEdit
            // 
            btnCancelEdit.BorderRadius = 12;
            btnCancelEdit.CustomizableEdges = customizableEdges15;
            btnCancelEdit.Font = new Font("Segoe UI", 9F);
            btnCancelEdit.ForeColor = Color.White;
            btnCancelEdit.Location = new Point(89, 528);
            btnCancelEdit.Name = "btnCancelEdit";
            btnCancelEdit.ShadowDecoration.CustomizableEdges = customizableEdges16;
            btnCancelEdit.Size = new Size(180, 40);
            btnCancelEdit.TabIndex = 14;
            btnCancelEdit.Text = "Cancel Edit";
            btnCancelEdit.Visible = false;
            // 
            // rightCard
            // 
            rightCard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rightCard.BackColor = Color.Transparent;
            rightCard.Controls.Add(lblCount);
            rightCard.Controls.Add(cmbFilter);
            rightCard.FillColor = Color.White;
            rightCard.Location = new Point(385, 26);
            rightCard.Name = "rightCard";
            rightCard.Padding = new Padding(20);
            rightCard.Radius = 14;
            rightCard.ShadowColor = Color.Black;
            rightCard.ShadowDepth = 18;
            rightCard.Size = new Size(552, 628);
            rightCard.TabIndex = 1;
            // 
            // lblCount
            // 
            lblCount.BackColor = Color.Transparent;
            lblCount.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblCount.Location = new Point(20, 20);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(151, 25);
            lblCount.TabIndex = 0;
            lblCount.Text = "Crew Members (0)";
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
            cmbFilter.Location = new Point(300, 16);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.ShadowDecoration.CustomizableEdges = customizableEdges18;
            cmbFilter.Size = new Size(180, 36);
            cmbFilter.TabIndex = 1;
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
