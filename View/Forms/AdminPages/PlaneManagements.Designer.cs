using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    partial class PlaneManagements
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            root = new Panel();
            content = new Panel();
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
            root.Controls.Add(content);
            root.Controls.Add(header);
            root.Dock = DockStyle.Fill;
            root.Location = new Point(0, 0);
            root.Margin = new Padding(3, 2, 3, 2);
            root.Name = "root";
            root.Padding = new Padding(23, 20, 23, 20);
            root.Size = new Size(1120, 540);
            root.TabIndex = 0;
            // 
            // content
            // 
            content.BackColor = Color.Transparent;
            content.Dock = DockStyle.Fill;
            content.Location = new Point(23, 102);
            content.Margin = new Padding(3, 2, 3, 2);
            content.Name = "content";
            content.Padding = new Padding(0, 9, 14, 0);
            content.Size = new Size(1074, 418);
            content.TabIndex = 2;
            // 
            // header
            // 
            header.BackColor = Color.Transparent;
            header.Controls.Add(lblTitle);
            header.Controls.Add(lblSubtitle);
            header.Controls.Add(btnAddPlane);
            header.Dock = DockStyle.Top;
            header.Location = new Point(23, 20);
            header.Margin = new Padding(3, 2, 3, 2);
            header.Name = "header";
            header.Size = new Size(1074, 82);
            header.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 30, 30);
            lblTitle.Location = new Point(3, 2);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(266, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Plane Management";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.Font = new Font("Segoe UI", 10F);
            lblSubtitle.ForeColor = Color.FromArgb(110, 110, 110);
            lblSubtitle.Location = new Point(3, 40);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(183, 19);
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
            btnAddPlane.Location = new Point(899, 21);
            btnAddPlane.Margin = new Padding(3, 2, 3, 2);
            btnAddPlane.Name = "btnAddPlane";
            btnAddPlane.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnAddPlane.Size = new Size(131, 33);
            btnAddPlane.TabIndex = 2;
            btnAddPlane.Text = "+   Add Plane";
            // 
            // PlaneManagements
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 246, 250);
            Controls.Add(root);
            Margin = new Padding(3, 2, 3, 2);
            Name = "PlaneManagements";
            Size = new Size(1030, 720);
            root.ResumeLayout(false);
            header.ResumeLayout(false);
            header.PerformLayout();
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel root;
        private System.Windows.Forms.Panel header;
        private System.Windows.Forms.Panel content;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private Guna.UI2.WinForms.Guna2Button btnAddPlane;
        #endregion
    }
}
