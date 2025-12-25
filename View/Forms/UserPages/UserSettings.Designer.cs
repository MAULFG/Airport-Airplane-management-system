namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    partial class UserSettings
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pnlRoot = new Guna.UI2.WinForms.Guna2Panel();
            this.grpSecurity = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblCurrentPass = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtCurrentPass = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblNewPass = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtNewPass = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblConfirmPass = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtConfirmPass = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnChangePassword = new Guna.UI2.WinForms.Guna2Button();
            this.grpAccount = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblUsernameCaption = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtUsername = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblEmailCaption = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.sepAccount1 = new Guna.UI2.WinForms.Guna2Separator();
            this.lblNewEmailCaption = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtNewEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblConfirmEmailPassCaption = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.txtConfirmPasswordForEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnUpdateEmail = new Guna.UI2.WinForms.Guna2Button();
            this.sepAccount2 = new Guna.UI2.WinForms.Guna2Separator();
            this.btnShowChangeUsername = new Guna.UI2.WinForms.Guna2Button();
            this.pnlChangeUsername = new Guna.UI2.WinForms.Guna2Panel();
            this.txtNewUsername = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtConfirmPassForUsername = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnConfirmUsernameChange = new Guna.UI2.WinForms.Guna2Button();
            this.btnCancelUsernameChange = new Guna.UI2.WinForms.Guna2Button();
            this.sepAccountBottom = new Guna.UI2.WinForms.Guna2Separator();
            this.lblCreatedAtCaption = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblCreatedAtValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblLastLoginCaption = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblLastLoginValue = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblSubtitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.lblTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.pnlRoot.SuspendLayout();
            this.grpSecurity.SuspendLayout();
            this.grpAccount.SuspendLayout();
            this.pnlChangeUsername.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRoot
            // 
            this.pnlRoot.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlRoot.Controls.Add(this.grpSecurity);
            this.pnlRoot.Controls.Add(this.grpAccount);
            this.pnlRoot.Controls.Add(this.lblSubtitle);
            this.pnlRoot.Controls.Add(this.lblTitle);
            this.pnlRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRoot.Location = new System.Drawing.Point(0, 0);
            this.pnlRoot.Name = "pnlRoot";
            this.pnlRoot.Padding = new System.Windows.Forms.Padding(30, 25, 30, 25);
            this.pnlRoot.Size = new System.Drawing.Size(1400, 700);
            this.pnlRoot.TabIndex = 0;
            // 
            // grpSecurity
            // 
            this.grpSecurity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpSecurity.BorderRadius = 14;
            this.grpSecurity.Controls.Add(this.lblCurrentPass);
            this.grpSecurity.Controls.Add(this.txtCurrentPass);
            this.grpSecurity.Controls.Add(this.lblNewPass);
            this.grpSecurity.Controls.Add(this.txtNewPass);
            this.grpSecurity.Controls.Add(this.lblConfirmPass);
            this.grpSecurity.Controls.Add(this.txtConfirmPass);
            this.grpSecurity.Controls.Add(this.btnChangePassword);
            this.grpSecurity.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(240)))));
            this.grpSecurity.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpSecurity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.grpSecurity.Location = new System.Drawing.Point(836, 130);
            this.grpSecurity.Name = "grpSecurity";
            this.grpSecurity.Size = new System.Drawing.Size(534, 540);
            this.grpSecurity.TabIndex = 0;
            this.grpSecurity.Text = "Security";
            // 
            // lblCurrentPass
            // 
            this.lblCurrentPass.AutoSize = false;
            this.lblCurrentPass.BackColor = System.Drawing.Color.Transparent;
            this.lblCurrentPass.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblCurrentPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(85)))));
            this.lblCurrentPass.Location = new System.Drawing.Point(22, 67);
            this.lblCurrentPass.Name = "lblCurrentPass";
            this.lblCurrentPass.Size = new System.Drawing.Size(380, 30);
            this.lblCurrentPass.TabIndex = 0;
            this.lblCurrentPass.Text = "Current Password";
            // 
            // txtCurrentPass
            // 
            this.txtCurrentPass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.txtCurrentPass.BorderRadius = 10;
            this.txtCurrentPass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCurrentPass.DefaultText = "";
            this.txtCurrentPass.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCurrentPass.IconLeftSize = new System.Drawing.Size(16, 16);
            this.txtCurrentPass.IconRightSize = new System.Drawing.Size(16, 16);
            this.txtCurrentPass.IconRight = global::Airport_Airplane_management_system.Properties.Resources.icons8_closed_eye_100;

            this.txtCurrentPass.Location = new System.Drawing.Point(22, 98);
            this.txtCurrentPass.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtCurrentPass.Name = "txtCurrentPass";
            this.txtCurrentPass.PlaceholderText = "Enter current password";
            this.txtCurrentPass.SelectedText = "";
            this.txtCurrentPass.Size = new System.Drawing.Size(445, 44);
            this.txtCurrentPass.TabIndex = 1;
            this.txtCurrentPass.UseSystemPasswordChar = true;
            // 
            // lblNewPass
            // 
            this.lblNewPass.AutoSize = false;
            this.lblNewPass.BackColor = System.Drawing.Color.Transparent;
            this.lblNewPass.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNewPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(85)))));
            this.lblNewPass.Location = new System.Drawing.Point(22, 167);
            this.lblNewPass.Name = "lblNewPass";
            this.lblNewPass.Size = new System.Drawing.Size(357, 30);
            this.lblNewPass.TabIndex = 2;
            this.lblNewPass.Text = "New Password";
            // 
            // txtNewPass
            // 
            this.txtNewPass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.txtNewPass.BorderRadius = 10;
            this.txtNewPass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNewPass.DefaultText = "";
            this.txtNewPass.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNewPass.IconRightSize = new System.Drawing.Size(16, 16);
            this.txtNewPass.IconRight = global::Airport_Airplane_management_system.Properties.Resources.icons8_closed_eye_100;
            this.txtNewPass.Location = new System.Drawing.Point(22, 198);
            this.txtNewPass.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtNewPass.Name = "txtNewPass";
            this.txtNewPass.PlaceholderText = "Enter new password";
            this.txtNewPass.SelectedText = "";
            this.txtNewPass.Size = new System.Drawing.Size(445, 44);
            this.txtNewPass.TabIndex = 3;
            this.txtNewPass.UseSystemPasswordChar = true;
            // 
            // lblConfirmPass
            // 
            this.lblConfirmPass.AutoSize = false;
            this.lblConfirmPass.BackColor = System.Drawing.Color.Transparent;
            this.lblConfirmPass.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblConfirmPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(85)))));
            this.lblConfirmPass.Location = new System.Drawing.Point(22, 267);
            this.lblConfirmPass.Name = "lblConfirmPass";
            this.lblConfirmPass.Size = new System.Drawing.Size(386, 30);
            this.lblConfirmPass.TabIndex = 4;
            this.lblConfirmPass.Text = "Confirm Password";
            // 
            // txtConfirmPass
            // 
            this.txtConfirmPass.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.txtConfirmPass.BorderRadius = 10;
            this.txtConfirmPass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtConfirmPass.DefaultText = "";
            this.txtConfirmPass.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtConfirmPass.IconRightSize = new System.Drawing.Size(16, 16);
            this.txtConfirmPass.IconRight = global::Airport_Airplane_management_system.Properties.Resources.icons8_closed_eye_100;
            this.txtConfirmPass.Location = new System.Drawing.Point(22, 298);
            this.txtConfirmPass.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtConfirmPass.Name = "txtConfirmPass";
            this.txtConfirmPass.PlaceholderText = "Confirm new password";
            this.txtConfirmPass.SelectedText = "";
            this.txtConfirmPass.Size = new System.Drawing.Size(445, 44);
            this.txtConfirmPass.TabIndex = 5;
            this.txtConfirmPass.UseSystemPasswordChar = true;
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.BorderRadius = 12;
            this.btnChangePassword.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnChangePassword.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnChangePassword.ForeColor = System.Drawing.Color.White;
            this.btnChangePassword.Location = new System.Drawing.Point(22, 375);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(445, 44);
            this.btnChangePassword.TabIndex = 6;
            this.btnChangePassword.Text = "Change Password";
            // 
            // grpAccount
            // 
            this.grpAccount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpAccount.BorderRadius = 14;
            this.grpAccount.Controls.Add(this.lblUsernameCaption);
            this.grpAccount.Controls.Add(this.txtUsername);
            this.grpAccount.Controls.Add(this.lblEmailCaption);
            this.grpAccount.Controls.Add(this.txtEmail);
            this.grpAccount.Controls.Add(this.sepAccount1);
            this.grpAccount.Controls.Add(this.lblNewEmailCaption);
            this.grpAccount.Controls.Add(this.txtNewEmail);
            this.grpAccount.Controls.Add(this.lblConfirmEmailPassCaption);
            this.grpAccount.Controls.Add(this.txtConfirmPasswordForEmail);
            this.grpAccount.Controls.Add(this.btnUpdateEmail);
            this.grpAccount.Controls.Add(this.sepAccount2);
            this.grpAccount.Controls.Add(this.btnShowChangeUsername);
            this.grpAccount.Controls.Add(this.pnlChangeUsername);
            this.grpAccount.Controls.Add(this.sepAccountBottom);
            this.grpAccount.Controls.Add(this.lblCreatedAtCaption);
            this.grpAccount.Controls.Add(this.lblCreatedAtValue);
            this.grpAccount.Controls.Add(this.lblLastLoginCaption);
            this.grpAccount.Controls.Add(this.lblLastLoginValue);
            this.grpAccount.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(240)))));
            this.grpAccount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpAccount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.grpAccount.Location = new System.Drawing.Point(30, 130);
            this.grpAccount.Name = "grpAccount";
            this.grpAccount.Size = new System.Drawing.Size(702, 540);
            this.grpAccount.TabIndex = 1;
            this.grpAccount.Text = "Account Settings";
            // 
            // lblUsernameCaption
            // 
            this.lblUsernameCaption.AutoSize = false;
            this.lblUsernameCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblUsernameCaption.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblUsernameCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(85)))));
            this.lblUsernameCaption.Location = new System.Drawing.Point(22, 57);
            this.lblUsernameCaption.Name = "lblUsernameCaption";
            this.lblUsernameCaption.Size = new System.Drawing.Size(320, 30);
            this.lblUsernameCaption.TabIndex = 0;
            this.lblUsernameCaption.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUsername.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.txtUsername.BorderRadius = 10;
            this.txtUsername.Cursor = System.Windows.Forms.Cursors.No;
            this.txtUsername.DefaultText = "";
            this.txtUsername.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(251)))));
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUsername.Location = new System.Drawing.Point(22, 88);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.PlaceholderText = "";
            this.txtUsername.ReadOnly = true;
            this.txtUsername.SelectedText = "";
            this.txtUsername.Size = new System.Drawing.Size(642, 44);
            this.txtUsername.TabIndex = 1;
            // 
            // lblEmailCaption
            // 
            this.lblEmailCaption.AutoSize = false;
            this.lblEmailCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblEmailCaption.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblEmailCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(85)))));
            this.lblEmailCaption.Location = new System.Drawing.Point(22, 145);
            this.lblEmailCaption.Name = "lblEmailCaption";
            this.lblEmailCaption.Size = new System.Drawing.Size(346, 30);
            this.lblEmailCaption.TabIndex = 2;
            this.lblEmailCaption.Text = "Current Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.txtEmail.BorderRadius = 10;
            this.txtEmail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtEmail.DefaultText = "";
            this.txtEmail.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(251)))));
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmail.Location = new System.Drawing.Point(22, 176);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "";
            this.txtEmail.ReadOnly = true;
            this.txtEmail.SelectedText = "";
            this.txtEmail.Size = new System.Drawing.Size(642, 44);
            this.txtEmail.TabIndex = 3;
            // 
            // sepAccount1
            // 
            this.sepAccount1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sepAccount1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.sepAccount1.Location = new System.Drawing.Point(22, 235);
            this.sepAccount1.Name = "sepAccount1";
            this.sepAccount1.Size = new System.Drawing.Size(654, 10);
            this.sepAccount1.TabIndex = 4;
            // 
            // lblNewEmailCaption
            // 
            this.lblNewEmailCaption.AutoSize = false;
            this.lblNewEmailCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblNewEmailCaption.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNewEmailCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(85)))));
            this.lblNewEmailCaption.Location = new System.Drawing.Point(22, 249);
            this.lblNewEmailCaption.Name = "lblNewEmailCaption";
            this.lblNewEmailCaption.Size = new System.Drawing.Size(323, 30);
            this.lblNewEmailCaption.TabIndex = 5;
            this.lblNewEmailCaption.Text = "New Email";
            // 
            // txtNewEmail
            // 
            this.txtNewEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewEmail.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.txtNewEmail.BorderRadius = 10;
            this.txtNewEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNewEmail.DefaultText = "";
            this.txtNewEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNewEmail.Location = new System.Drawing.Point(22, 280);
            this.txtNewEmail.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtNewEmail.Name = "txtNewEmail";
            this.txtNewEmail.PlaceholderText = "Enter new email";
            this.txtNewEmail.SelectedText = "";
            this.txtNewEmail.Size = new System.Drawing.Size(642, 44);
            this.txtNewEmail.TabIndex = 6;
            // 
            // lblConfirmEmailPassCaption
            // 
            this.lblConfirmEmailPassCaption.AutoSize = false;
            this.lblConfirmEmailPassCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblConfirmEmailPassCaption.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblConfirmEmailPassCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(70)))), ((int)(((byte)(85)))));
            this.lblConfirmEmailPassCaption.Location = new System.Drawing.Point(22, 333);
            this.lblConfirmEmailPassCaption.Name = "lblConfirmEmailPassCaption";
            this.lblConfirmEmailPassCaption.Size = new System.Drawing.Size(386, 30);
            this.lblConfirmEmailPassCaption.TabIndex = 7;
            this.lblConfirmEmailPassCaption.Text = "Confirm Password";
            // 
            // txtConfirmPasswordForEmail
            // 
            this.txtConfirmPasswordForEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConfirmPasswordForEmail.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.txtConfirmPasswordForEmail.BorderRadius = 10;
            this.txtConfirmPasswordForEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtConfirmPasswordForEmail.DefaultText = "";
            this.txtConfirmPasswordForEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtConfirmPasswordForEmail.Location = new System.Drawing.Point(22, 364);
            this.txtConfirmPasswordForEmail.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtConfirmPasswordForEmail.Name = "txtConfirmPasswordForEmail";
            this.txtConfirmPasswordForEmail.PlaceholderText = "Enter your password";
            this.txtConfirmPasswordForEmail.SelectedText = "";
            this.txtConfirmPasswordForEmail.Size = new System.Drawing.Size(642, 44);
            this.txtConfirmPasswordForEmail.TabIndex = 8;
            this.txtConfirmPasswordForEmail.UseSystemPasswordChar = true;
            // 
            // btnUpdateEmail
            // 
            this.btnUpdateEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdateEmail.BorderRadius = 12;
            this.btnUpdateEmail.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnUpdateEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnUpdateEmail.ForeColor = System.Drawing.Color.White;
            this.btnUpdateEmail.Location = new System.Drawing.Point(22, 420);
            this.btnUpdateEmail.Name = "btnUpdateEmail";
            this.btnUpdateEmail.Size = new System.Drawing.Size(642, 44);
            this.btnUpdateEmail.TabIndex = 9;
            this.btnUpdateEmail.Text = "Update Email";
            // 
            // sepAccount2
            // 
            this.sepAccount2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.sepAccount2.Location = new System.Drawing.Point(22, 475);
            this.sepAccount2.Name = "sepAccount2";
            this.sepAccount2.Size = new System.Drawing.Size(760, 10);
            this.sepAccount2.TabIndex = 10;
            // 
            // btnShowChangeUsername
            // 
            this.btnShowChangeUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowChangeUsername.BorderRadius = 12;
            this.btnShowChangeUsername.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(59)))), ((int)(((byte)(89)))));
            this.btnShowChangeUsername.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnShowChangeUsername.ForeColor = System.Drawing.Color.White;
            this.btnShowChangeUsername.Location = new System.Drawing.Point(22, 495);
            this.btnShowChangeUsername.Name = "btnShowChangeUsername";
            this.btnShowChangeUsername.Size = new System.Drawing.Size(642, 40);
            this.btnShowChangeUsername.TabIndex = 11;
            this.btnShowChangeUsername.Text = "Change Username...";
            // 
            // pnlChangeUsername
            // 
            this.pnlChangeUsername.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.pnlChangeUsername.BorderRadius = 12;
            this.pnlChangeUsername.BorderThickness = 1;
            this.pnlChangeUsername.Controls.Add(this.txtNewUsername);
            this.pnlChangeUsername.Controls.Add(this.txtConfirmPassForUsername);
            this.pnlChangeUsername.Controls.Add(this.btnConfirmUsernameChange);
            this.pnlChangeUsername.Controls.Add(this.btnCancelUsernameChange);
            this.pnlChangeUsername.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(251)))));
            this.pnlChangeUsername.Location = new System.Drawing.Point(22, 545);
            this.pnlChangeUsername.Name = "pnlChangeUsername";
            this.pnlChangeUsername.Size = new System.Drawing.Size(760, 140);
            this.pnlChangeUsername.TabIndex = 12;
            this.pnlChangeUsername.Visible = false;
            // 
            // txtNewUsername
            // 
            this.txtNewUsername.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.txtNewUsername.BorderRadius = 10;
            this.txtNewUsername.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNewUsername.DefaultText = "";
            this.txtNewUsername.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNewUsername.Location = new System.Drawing.Point(12, 10);
            this.txtNewUsername.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtNewUsername.Name = "txtNewUsername";
            this.txtNewUsername.PlaceholderText = "Enter new username";
            this.txtNewUsername.SelectedText = "";
            this.txtNewUsername.Size = new System.Drawing.Size(500, 40);
            this.txtNewUsername.TabIndex = 0;
            // 
            // txtConfirmPassForUsername
            // 
            this.txtConfirmPassForUsername.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.txtConfirmPassForUsername.BorderRadius = 10;
            this.txtConfirmPassForUsername.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtConfirmPassForUsername.DefaultText = "";
            this.txtConfirmPassForUsername.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtConfirmPassForUsername.Location = new System.Drawing.Point(12, 55);
            this.txtConfirmPassForUsername.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtConfirmPassForUsername.Name = "txtConfirmPassForUsername";
            this.txtConfirmPassForUsername.PlaceholderText = "Enter your password";
            this.txtConfirmPassForUsername.SelectedText = "";
            this.txtConfirmPassForUsername.Size = new System.Drawing.Size(500, 40);
            this.txtConfirmPassForUsername.TabIndex = 1;
            this.txtConfirmPassForUsername.UseSystemPasswordChar = true;
            // 
            // btnConfirmUsernameChange
            // 
            this.btnConfirmUsernameChange.BorderRadius = 10;
            this.btnConfirmUsernameChange.FillColor = System.Drawing.Color.DodgerBlue;
            this.btnConfirmUsernameChange.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnConfirmUsernameChange.ForeColor = System.Drawing.Color.White;
            this.btnConfirmUsernameChange.Location = new System.Drawing.Point(530, 10);
            this.btnConfirmUsernameChange.Name = "btnConfirmUsernameChange";
            this.btnConfirmUsernameChange.Size = new System.Drawing.Size(210, 40);
            this.btnConfirmUsernameChange.TabIndex = 2;
            this.btnConfirmUsernameChange.Text = "Confirm";
            // 
            // btnCancelUsernameChange
            // 
            this.btnCancelUsernameChange.BorderRadius = 10;
            this.btnCancelUsernameChange.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(205)))), ((int)(((byte)(210)))));
            this.btnCancelUsernameChange.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancelUsernameChange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.btnCancelUsernameChange.Location = new System.Drawing.Point(530, 55);
            this.btnCancelUsernameChange.Name = "btnCancelUsernameChange";
            this.btnCancelUsernameChange.Size = new System.Drawing.Size(210, 40);
            this.btnCancelUsernameChange.TabIndex = 3;
            this.btnCancelUsernameChange.Text = "Cancel";
            // 
            // sepAccountBottom
            // 
            this.sepAccountBottom.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.sepAccountBottom.Location = new System.Drawing.Point(22, 695);
            this.sepAccountBottom.Name = "sepAccountBottom";
            this.sepAccountBottom.Size = new System.Drawing.Size(760, 10);
            this.sepAccountBottom.TabIndex = 13;
            // 
            // lblCreatedAtCaption
            // 
            this.lblCreatedAtCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblCreatedAtCaption.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.lblCreatedAtCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(105)))), ((int)(((byte)(120)))));
            this.lblCreatedAtCaption.Location = new System.Drawing.Point(22, 715);
            this.lblCreatedAtCaption.Name = "lblCreatedAtCaption";
            this.lblCreatedAtCaption.Size = new System.Drawing.Size(87, 25);
            this.lblCreatedAtCaption.TabIndex = 14;
            this.lblCreatedAtCaption.Text = "Created at:";
            // 
            // lblCreatedAtValue
            // 
            this.lblCreatedAtValue.BackColor = System.Drawing.Color.Transparent;
            this.lblCreatedAtValue.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.lblCreatedAtValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(105)))), ((int)(((byte)(120)))));
            this.lblCreatedAtValue.Location = new System.Drawing.Point(110, 715);
            this.lblCreatedAtValue.Name = "lblCreatedAtValue";
            this.lblCreatedAtValue.Size = new System.Drawing.Size(10, 25);
            this.lblCreatedAtValue.TabIndex = 15;
            this.lblCreatedAtValue.Text = "-";
            // 
            // lblLastLoginCaption
            // 
            this.lblLastLoginCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblLastLoginCaption.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.lblLastLoginCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(105)))), ((int)(((byte)(120)))));
            this.lblLastLoginCaption.Location = new System.Drawing.Point(22, 745);
            this.lblLastLoginCaption.Name = "lblLastLoginCaption";
            this.lblLastLoginCaption.Size = new System.Drawing.Size(80, 25);
            this.lblLastLoginCaption.TabIndex = 16;
            this.lblLastLoginCaption.Text = "Last login:";
            // 
            // lblLastLoginValue
            // 
            this.lblLastLoginValue.BackColor = System.Drawing.Color.Transparent;
            this.lblLastLoginValue.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.lblLastLoginValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(105)))), ((int)(((byte)(120)))));
            this.lblLastLoginValue.Location = new System.Drawing.Point(110, 745);
            this.lblLastLoginValue.Name = "lblLastLoginValue";
            this.lblLastLoginValue.Size = new System.Drawing.Size(10, 25);
            this.lblLastLoginValue.TabIndex = 17;
            this.lblLastLoginValue.Text = "-";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = false;
            this.lblSubtitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(105)))), ((int)(((byte)(120)))));
            this.lblSubtitle.Location = new System.Drawing.Point(30, 85);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(664, 39);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "Manage your account and security.";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = false;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 28.2F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(33)))), ((int)(((byte)(45)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 17);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(555, 82);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Settings";
            // 
            // UserSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.pnlRoot);
            this.Name = "UserSettings";
            this.Size = new System.Drawing.Size(1400, 700);
            this.pnlRoot.ResumeLayout(false);
            this.grpSecurity.ResumeLayout(false);
            this.grpAccount.ResumeLayout(false);
            this.grpAccount.PerformLayout();
            this.pnlChangeUsername.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlRoot;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblTitle;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblSubtitle;

        private Guna.UI2.WinForms.Guna2GroupBox grpAccount;
        private Guna.UI2.WinForms.Guna2GroupBox grpSecurity;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblUsernameCaption;
        private Guna.UI2.WinForms.Guna2TextBox txtUsername;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblEmailCaption;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;

        private Guna.UI2.WinForms.Guna2Separator sepAccount1;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblNewEmailCaption;
        private Guna.UI2.WinForms.Guna2TextBox txtNewEmail;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblConfirmEmailPassCaption;
        private Guna.UI2.WinForms.Guna2TextBox txtConfirmPasswordForEmail;

        private Guna.UI2.WinForms.Guna2Button btnUpdateEmail;

        private Guna.UI2.WinForms.Guna2Separator sepAccount2;
        private Guna.UI2.WinForms.Guna2Button btnShowChangeUsername;

        private Guna.UI2.WinForms.Guna2Panel pnlChangeUsername;
        private Guna.UI2.WinForms.Guna2TextBox txtNewUsername;
        private Guna.UI2.WinForms.Guna2TextBox txtConfirmPassForUsername;
        private Guna.UI2.WinForms.Guna2Button btnConfirmUsernameChange;
        private Guna.UI2.WinForms.Guna2Button btnCancelUsernameChange;

        private Guna.UI2.WinForms.Guna2Separator sepAccountBottom;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCreatedAtCaption;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCreatedAtValue;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLastLoginCaption;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblLastLoginValue;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblCurrentPass;
        private Guna.UI2.WinForms.Guna2TextBox txtCurrentPass;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblNewPass;
        private Guna.UI2.WinForms.Guna2TextBox txtNewPass;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblConfirmPass;
        private Guna.UI2.WinForms.Guna2TextBox txtConfirmPass;

        private Guna.UI2.WinForms.Guna2Button btnChangePassword;
    }
}
