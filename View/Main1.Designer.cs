namespace Airport_Airplane_management_system.View
{
    partial class Main1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main1));
            loginPage1 = new Airport_Airplane_management_system.View.Forms.LoginPages.LoginPage();
            forgetUserControl1 = new Airport_Airplane_management_system.View.Forms.LoginPages.ForgetUserControl();
            signupusercontrol1 = new Airport_Airplane_management_system.View.Forms.LoginPages.Signupusercontrol();
            userDashboard1 = new Airport_Airplane_management_system.View.Forms.UserPages.UserDashboard();
            SuspendLayout();
            // 
            // loginPage1
            // 
            loginPage1.BackgroundImageLayout = ImageLayout.Stretch;
            loginPage1.Dock = DockStyle.Fill;
            loginPage1.Location = new Point(0, 0);
            loginPage1.Margin = new Padding(2, 3, 2, 3);
            loginPage1.Name = "loginPage1";
            loginPage1.Size = new Size(1368, 859);
            loginPage1.TabIndex = 0;
            loginPage1.Load += loginPage1_Load;
            // 
            // forgetUserControl1
            // 
            forgetUserControl1.BackgroundImage = (Image)resources.GetObject("forgetUserControl1.BackgroundImage");
            forgetUserControl1.BackgroundImageLayout = ImageLayout.Stretch;
            forgetUserControl1.Dock = DockStyle.Fill;
            forgetUserControl1.Location = new Point(0, 0);
            forgetUserControl1.Margin = new Padding(5, 4, 5, 4);
            forgetUserControl1.Name = "forgetUserControl1";
            forgetUserControl1.Size = new Size(1368, 859);
            forgetUserControl1.TabIndex = 1;
            forgetUserControl1.Load += forgetUserControl1_Load;
            // 
            // signupusercontrol1
            // 
            signupusercontrol1.BackgroundImage = (Image)resources.GetObject("signupusercontrol1.BackgroundImage");
            signupusercontrol1.BackgroundImageLayout = ImageLayout.Stretch;
            signupusercontrol1.Dock = DockStyle.Fill;
            signupusercontrol1.Location = new Point(0, 0);
            signupusercontrol1.Margin = new Padding(2, 3, 2, 3);
            signupusercontrol1.Name = "signupusercontrol1";
            signupusercontrol1.Size = new Size(1368, 859);
            signupusercontrol1.TabIndex = 2;
            signupusercontrol1.Load += signupusercontrol1_Load_1;
            // 
            // userDashboard1
            // 
            userDashboard1.Dock = DockStyle.Fill;
            userDashboard1.Location = new Point(0, 0);
            userDashboard1.Name = "userDashboard1";
            userDashboard1.Size = new Size(1368, 859);
            userDashboard1.TabIndex = 3;
            userDashboard1.Load += userDashboard1_Load;
            // 
            // Main1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1368, 859);
            Controls.Add(userDashboard1);
            Controls.Add(signupusercontrol1);
            Controls.Add(forgetUserControl1);
            Controls.Add(loginPage1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Main1";
            Text = "Main1";
            Load += Main1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Forms.LoginPages.LoginPage loginPage1;
        private Forms.LoginPages.ForgetUserControl forgetUserControl1;
        private Forms.LoginPages.Signupusercontrol signupusercontrol1;
        private Forms.UserPages.UserDashboard userDashboard1;
    }
}