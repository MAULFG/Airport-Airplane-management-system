namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    partial class MainUserPage
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblWelcome;
        private Label lblSub;
        private FlowLayoutPanel flowStats;
        private Guna.UI2.WinForms.Guna2Panel bodyPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lblWelcome = new Label();
            lblSub = new Label();
            flowStats = new FlowLayoutPanel();
            headerPanel = new Guna.UI2.WinForms.Guna2Panel();
            bodyPanel = new Guna.UI2.WinForms.Guna2Panel();
            headerPanel.SuspendLayout();
            bodyPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lblWelcome
            // 
            lblWelcome.BackColor = Color.Transparent;
            lblWelcome.Dock = DockStyle.Top;
            lblWelcome.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.White;
            lblWelcome.Location = new Point(40, 35);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(1200, 50);
            lblWelcome.TabIndex = 1;
            lblWelcome.Text = "Welcome";
            // 
            // lblSub
            // 
            lblSub.BackColor = Color.Transparent;
            lblSub.Dock = DockStyle.Top;
            lblSub.Font = new Font("Segoe UI", 12F);
            lblSub.ForeColor = Color.FromArgb(210, 235, 235);
            lblSub.Location = new Point(40, 85);
            lblSub.Name = "lblSub";
            lblSub.Size = new Size(1200, 28);
            lblSub.TabIndex = 0;
            lblSub.Text = "Passenger dashboard overview";
            // 
            // flowStats
            // 
            flowStats.AutoScroll = true;
            flowStats.Dock = DockStyle.Fill;
            flowStats.Location = new Point(30, 30);
            flowStats.Name = "flowStats";
            flowStats.Size = new Size(1220, 516);
            flowStats.TabIndex = 0;
            // 
            // headerPanel
            // 
            headerPanel.BorderRadius = 20;
            headerPanel.Controls.Add(lblSub);
            headerPanel.Controls.Add(lblWelcome);
            headerPanel.CustomizableEdges = customizableEdges1;
            headerPanel.Dock = DockStyle.Top;
            headerPanel.FillColor = Color.DarkCyan;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.Padding = new Padding(40, 35, 40, 25);
            headerPanel.ShadowDecoration.BorderRadius = 20;
            headerPanel.ShadowDecoration.CustomizableEdges = customizableEdges2;
            headerPanel.Size = new Size(1280, 144);
            headerPanel.TabIndex = 1;
            // 
            // bodyPanel
            // 
            bodyPanel.Controls.Add(flowStats);
            bodyPanel.CustomizableEdges = customizableEdges3;
            bodyPanel.Dock = DockStyle.Fill;
            bodyPanel.FillColor = Color.FromArgb(242, 244, 246);
            bodyPanel.Location = new Point(0, 144);
            bodyPanel.Name = "bodyPanel";
            bodyPanel.Padding = new Padding(30);
            bodyPanel.ShadowDecoration.CustomizableEdges = customizableEdges4;
            bodyPanel.Size = new Size(1280, 576);
            bodyPanel.TabIndex = 0;
            // 
            // MainUserPage
            // 
            Controls.Add(bodyPanel);
            Controls.Add(headerPanel);
            Padding = new Padding(10,10,10,10);
            Name = "MainUserPage";
            Size = new Size(1280, 720);
            headerPanel.ResumeLayout(false);
            bodyPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        internal Guna.UI2.WinForms.Guna2Panel headerPanel;
    }
}
