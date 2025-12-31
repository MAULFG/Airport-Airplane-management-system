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
            root = new System.Windows.Forms.Panel();
            content = new System.Windows.Forms.Panel();
            flow = new System.Windows.Forms.FlowLayoutPanel();
            header = new System.Windows.Forms.Panel();
            lblTitle = new System.Windows.Forms.Label();
            lblSubtitle = new System.Windows.Forms.Label();
            btnAddPlane = new Guna.UI2.WinForms.Guna2Button();
            root.SuspendLayout();
            content.SuspendLayout();
            header.SuspendLayout();
            SuspendLayout();
            // 
            // root
            // 
            root.BackColor = System.Drawing.Color.Transparent;
            root.Dock = System.Windows.Forms.DockStyle.Fill;
            root.Location = new System.Drawing.Point(0, 0);
            root.Name = "root";
            root.Padding = new System.Windows.Forms.Padding(26);
            root.Size = new System.Drawing.Size(1406, 519);
            root.TabIndex = 0;
            root.Controls.Add(content);
            root.Controls.Add(header);
            // 
            // header
            // 
            header.BackColor = System.Drawing.Color.Transparent;
            header.Controls.Add(lblTitle);
            header.Controls.Add(lblSubtitle);
            header.Controls.Add(btnAddPlane);
            header.Dock = System.Windows.Forms.DockStyle.Top;
            header.Location = new System.Drawing.Point(26, 26);
            header.Name = "header";
            header.Size = new System.Drawing.Size(1354, 110);
            header.TabIndex = 1;
            // 
            // content
            // 
            content.BackColor = System.Drawing.Color.Transparent;
            content.Dock = System.Windows.Forms.DockStyle.Fill;

            // IMPORTANT: fixes "first card covered" by giving space for shadow/top clipping
            content.Padding = new System.Windows.Forms.Padding(0, 12, 16, 0);

            content.Location = new System.Drawing.Point(26, 136);
            content.Name = "content";
            content.TabIndex = 2;
            content.Controls.Add(flow);
            // 
            // flow
            // 
            flow.AutoScroll = true;
            flow.BackColor = System.Drawing.Color.Transparent;
            flow.Dock = System.Windows.Forms.DockStyle.Fill;
            flow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            flow.Margin = new System.Windows.Forms.Padding(0);
            flow.Name = "flow";
            flow.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
            flow.TabIndex = 0;
            flow.WrapContents = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            lblTitle.Location = new System.Drawing.Point(0, 8);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(329, 46);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Plane Management";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(110, 110, 110);
            lblSubtitle.Location = new System.Drawing.Point(2, 52);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new System.Drawing.Size(227, 23);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Manage your fleet of aircraft";
            // 
            // btnAddPlane
            // 
            btnAddPlane.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnAddPlane.BorderRadius = 14;
            btnAddPlane.CustomizableEdges = customizableEdges1;
            btnAddPlane.FillColor = System.Drawing.Color.FromArgb(12, 18, 30);
            btnAddPlane.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            btnAddPlane.ForeColor = System.Drawing.Color.White;
            btnAddPlane.Location = new System.Drawing.Point(1154, 28);
            btnAddPlane.Name = "btnAddPlane";
            btnAddPlane.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnAddPlane.Size = new System.Drawing.Size(150, 44);
            btnAddPlane.TabIndex = 2;
            btnAddPlane.Text = "+   Add Plane";
            // 
            // PlaneManagements
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(245, 246, 250);
            Controls.Add(root);
            Name = "PlaneManagements";
            Size = new System.Drawing.Size(1406, 519);
            root.ResumeLayout(false);
            content.ResumeLayout(false);
            header.ResumeLayout(false);
            header.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel root;
        private System.Windows.Forms.Panel header;
        private System.Windows.Forms.Panel content;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private Guna.UI2.WinForms.Guna2Button btnAddPlane;
        private System.Windows.Forms.FlowLayoutPanel flow;
    }
}
