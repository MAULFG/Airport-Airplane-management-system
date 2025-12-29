using Guna.UI2.WinForms;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    partial class MainUserPage
    {
        private Guna2ShadowPanel panelMain;
        private Guna2ShadowPanel panelHeader;
        private Guna2HtmlLabel lblWelcome;
        private FlowLayoutPanel flowStats;
        private Guna2ShadowPanel panelFooter;
        private Guna2HtmlLabel lblFooter;
        /// <summary> 
        /// Required designer variable.
        /// </summary>


        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelMain = new Guna2ShadowPanel();
            flowStats = new FlowLayoutPanel();
            panelFooter = new Guna2ShadowPanel();
            lblFooter = new Guna2HtmlLabel();
            panelHeader = new Guna2ShadowPanel();
            lblWelcome = new Guna2HtmlLabel();
            panelMain.SuspendLayout();
            panelFooter.SuspendLayout();
            panelHeader.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.Transparent;
            panelMain.Controls.Add(flowStats);
            panelMain.Controls.Add(panelFooter);
            panelMain.Controls.Add(panelHeader);
            panelMain.Dock = DockStyle.Fill;
            panelMain.FillColor = Color.White;
            panelMain.Location = new Point(0, 0);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(20);
            panelMain.ShadowColor = Color.Black;
            panelMain.Size = new Size(963, 683);
            panelMain.TabIndex = 0;
            // 
            // flowStats
            // 
            flowStats.AutoScroll = true;
            flowStats.Dock = DockStyle.Fill;
            flowStats.Location = new Point(20, 120);
            flowStats.Name = "flowStats";
            flowStats.Padding = new Padding(10);
            flowStats.Size = new Size(923, 493);
            flowStats.TabIndex = 1;
            flowStats.Paint += flowStats_Paint;
            // 
            // panelFooter
            // 
            panelFooter.BackColor = Color.Transparent;
            panelFooter.Controls.Add(lblFooter);
            panelFooter.Dock = DockStyle.Bottom;
            panelFooter.FillColor = Color.FromArgb(245, 245, 245);
            panelFooter.Location = new Point(20, 613);
            panelFooter.Name = "panelFooter";
            panelFooter.Padding = new Padding(10);
            panelFooter.Radius = 10;
            panelFooter.ShadowColor = Color.Black;
            panelFooter.Size = new Size(923, 50);
            panelFooter.TabIndex = 2;
            // 
            // lblFooter
            // 
            lblFooter.BackColor = Color.Transparent;
            lblFooter.Font = new Font("Segoe UI", 10F, FontStyle.Italic);
            lblFooter.ForeColor = Color.Gray;
            lblFooter.Location = new Point(20, 15);
            lblFooter.Name = "lblFooter";
            lblFooter.Size = new Size(194, 19);
            lblFooter.TabIndex = 0;
            lblFooter.Text = "Have a great day and safe travels!";
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.Transparent;
            panelHeader.Controls.Add(lblWelcome);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.FillColor = Color.DarkCyan;
            panelHeader.Location = new Point(20, 20);
            panelHeader.Name = "panelHeader";
            panelHeader.Padding = new Padding(20);
            panelHeader.Radius = 10;
            panelHeader.ShadowColor = Color.Black;
            panelHeader.Size = new Size(923, 100);
            panelHeader.TabIndex = 0;
            // 
            // lblWelcome
            // 
            lblWelcome.BackColor = Color.Transparent;
            lblWelcome.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.White;
            lblWelcome.Location = new Point(30, 30);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(300, 39);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Welcome, [UserName]!";
            // 
            // MainUserPage
            // 
            BackColor = Color.White;
            Controls.Add(panelMain);
            Name = "MainUserPage";
            Size = new Size(963, 683);
            panelMain.ResumeLayout(false);
            panelFooter.ResumeLayout(false);
            panelFooter.PerformLayout();
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}
