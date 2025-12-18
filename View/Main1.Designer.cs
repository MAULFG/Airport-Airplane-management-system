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
            loginPage1 = new Airport_Airplane_management_system.View.Forms.LoginPages.LoginPage();
            SuspendLayout();
            // 
            // loginPage1
            // 
            loginPage1.BackgroundImageLayout = ImageLayout.Stretch;
            loginPage1.Dock = DockStyle.Fill;
            loginPage1.Location = new Point(0, 0);
            loginPage1.Margin = new Padding(2);
            loginPage1.Name = "loginPage1";
            loginPage1.Size = new Size(1197, 644);
            loginPage1.TabIndex = 0;
            loginPage1.Load += loginPage1_Load;
            // 
            // Main1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1197, 644);
            Controls.Add(loginPage1);
            Name = "Main1";
            Text = "Main1";
            Load += Main1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Forms.LoginPages.LoginPage loginPage1;
    }
}