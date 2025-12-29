namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    partial class PlaneManagements
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            root = new Panel();
            flow = new FlowLayoutPanel();
            header = new Panel();
            lblTitle = new Label();
            lblSubtitle = new Label();
            btnAddPlane = new Guna.UI2.WinForms.Guna2Button();
            root.SuspendLayout();
            header.SuspendLayout();
            SuspendLayout();
            // 
            // root
            // 
            root.BackColor = Color.Transparent;
            root.Controls.Add(flow);
            root.Controls.Add(header);
            root.Dock = DockStyle.Fill;
            root.Location = new Point(0, 0);
            root.Name = "root";
            root.Padding = new Padding(26);
            root.Size = new Size(1406, 519);
            root.TabIndex = 0;
            // 
            // flow
            // 
            flow.AutoScroll = true;
            flow.BackColor = Color.Transparent;
            flow.Dock = DockStyle.Fill;
            flow.FlowDirection = FlowDirection.TopDown;
            flow.Location = new Point(26, 136);
            flow.Margin = new Padding(0);
            flow.Name = "flow";
            flow.Padding = new Padding(0, 8, 0, 0);
            flow.Size = new Size(1354, 357);
            flow.TabIndex = 0;
            flow.WrapContents = false;
            // 
            // header
            // 
            header.BackColor = Color.Transparent;
            header.Controls.Add(lblTitle);
            header.Controls.Add(lblSubtitle);
            header.Controls.Add(btnAddPlane);
            header.Dock = DockStyle.Top;
            header.Location = new Point(26, 26);
            header.Name = "header";
            header.Size = new Size(1354, 110);
            header.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 30, 30);
            lblTitle.Location = new Point(0, 8);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(329, 46);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Plane Management";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(110, 110, 110);
            lblSubtitle.Location = new Point(2, 52);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(227, 23);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Manage your fleet of aircraft";
            // 
            // btnAddPlane
            // 
            btnAddPlane.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddPlane.BorderRadius = 14;
            btnAddPlane.CustomizableEdges = customizableEdges1;
            btnAddPlane.FillColor = Color.FromArgb(12, 18, 30);
            btnAddPlane.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnAddPlane.ForeColor = Color.White;
            btnAddPlane.Location = new Point(1154, 28);
            btnAddPlane.Name = "btnAddPlane";
            btnAddPlane.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnAddPlane.Size = new Size(150, 44);
            btnAddPlane.TabIndex = 2;
            btnAddPlane.Text = "+   Add Plane";
            // 
            // PlaneManagements
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 246, 250);
            Controls.Add(root);
            Name = "PlaneManagements";
            Size = new Size(1406, 519);
            root.ResumeLayout(false);
            header.ResumeLayout(false);
            header.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel root;
        private System.Windows.Forms.Panel header;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private Guna.UI2.WinForms.Guna2Button btnAddPlane;
        private System.Windows.Forms.FlowLayoutPanel flow;
    }
}
