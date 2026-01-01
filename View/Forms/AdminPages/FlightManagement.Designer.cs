using Guna.UI2.WinForms;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    partial class FlightManagement
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Guna2Panel root;
        private Guna2ShadowPanel leftCard;
        private Guna2ShadowPanel rightCard;

        private Label lblTitle;
        private Label lblFlightNumber;
        private Label lblOrigin;
        private Label lblDestination;
        private Label lblDate;
        private Label lblTime;
        private Label lblStatus;

        private Guna2TextBox txtFlightNumber;
        private Guna2TextBox txtOrigin;
        private Guna2TextBox txtDestination;
        private Guna2TextBox txtDate;
        private Guna2TextBox txtTime;

        private Guna2ComboBox cmbStatus;

        private Guna2Button btnAddOrUpdate;
        private Guna2Button btnCancelEdit;

        private Guna2HtmlLabel lblCount;
        private Guna2ComboBox cmbFilter;
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new();

            root = new Guna2Panel();
            leftCard = new Guna2ShadowPanel();
            txtFlightNumber = new Guna2TextBox();
            txtOrigin = new Guna2TextBox();
            txtDestination = new Guna2TextBox();
            txtDate = new Guna2TextBox();
            txtTime = new Guna2TextBox();
            cmbStatus = new Guna2ComboBox();
            lblTitle = new Label();
            lblFlightNumber = new Label();
            lblOrigin = new Label();
            lblDestination = new Label();
            lblDate = new Label();
            lblTime = new Label();
            lblStatus = new Label();
            btnAddOrUpdate = new Guna2Button();
            btnCancelEdit = new Guna2Button();

            rightCard = new Guna2ShadowPanel();
            cmbFilter = new Guna2ComboBox();
            lblCount = new Guna2HtmlLabel();

            root.SuspendLayout();
            leftCard.SuspendLayout();
            rightCard.SuspendLayout();
            SuspendLayout();

            // root
            root.Controls.Add(leftCard);
            root.Controls.Add(rightCard);
            root.Dock = DockStyle.Fill;
            root.Padding = new Padding(10);
            root.Size = new Size(963, 683);

            // leftCard
            leftCard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            leftCard.FillColor = Color.White;
            leftCard.Padding = new Padding(10);
            leftCard.Radius = 14;
            leftCard.ShadowColor = Color.Black;
            leftCard.ShadowDepth = 18;
            leftCard.Location = new Point(10, 10);
            leftCard.Size = new Size(353, 660);
            leftCard.Controls.AddRange(new Control[]
            {
                txtFlightNumber, txtOrigin, txtDestination, txtDate, txtTime, cmbStatus,
                lblTitle, lblFlightNumber, lblOrigin, lblDestination, lblDate, lblTime, lblStatus,
                btnAddOrUpdate, btnCancelEdit
            });

            // Labels
            lblTitle.Text = "Add / Edit Flight";
            lblTitle.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblTitle.Location = new Point(27, 15);

            lblFlightNumber.Text = "Flight Number *"; lblFlightNumber.Location = new Point(26, 68);
            lblOrigin.Text = "Origin *"; lblOrigin.Location = new Point(26, 143);
            lblDestination.Text = "Destination *"; lblDestination.Location = new Point(26, 218);
            lblDate.Text = "Date *"; lblDate.Location = new Point(26, 293);
            lblTime.Text = "Time *"; lblTime.Location = new Point(26, 368);
            lblStatus.Text = "Status *"; lblStatus.Location = new Point(26, 443);

            // TextBoxes
            void SetTextBox(Guna2TextBox tb, Point loc, string placeholder)
            {
                tb.BorderRadius = 10;
                tb.PlaceholderText = placeholder;
                tb.Location = loc;
                tb.Size = new Size(308, 42);
            }

            SetTextBox(txtFlightNumber, new Point(26, 98), "Enter Flight Number");
            SetTextBox(txtOrigin, new Point(26, 168), "Enter Origin");
            SetTextBox(txtDestination, new Point(26, 243), "Enter Destination");
            SetTextBox(txtDate, new Point(26, 318), "dd/MM/yyyy");
            SetTextBox(txtTime, new Point(26, 393), "HH:mm");

            // ComboBox
            cmbStatus.BackColor = Color.Transparent;
            cmbStatus.BorderRadius = 10;
            cmbStatus.DrawMode = DrawMode.OwnerDrawFixed;
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Items.AddRange(new object[] { "Scheduled", "Delayed", "Cancelled" });
            cmbStatus.Location = new Point(26, 468);
            cmbStatus.Size = new Size(308, 36);

            // Buttons
            btnAddOrUpdate.BorderRadius = 12;
            btnAddOrUpdate.Size = new Size(308, 40);
            btnAddOrUpdate.Location = new Point(26, 515);
            btnAddOrUpdate.Text = "Add Flight";

            btnCancelEdit.BorderRadius = 12;
            btnCancelEdit.Size = new Size(308, 40);
            btnCancelEdit.Location = new Point(26, 560);
            btnCancelEdit.Text = "Cancel Edit";
            btnCancelEdit.Visible = false;

            // rightCard
            rightCard.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rightCard.FillColor = Color.White;
            rightCard.Padding = new Padding(10);
            rightCard.Radius = 14;
            rightCard.ShadowColor = Color.Black;
            rightCard.ShadowDepth = 18;
            rightCard.Location = new Point(369, 10);
            rightCard.Size = new Size(584, 660);
            rightCard.Controls.Add(cmbFilter);
            rightCard.Controls.Add(lblCount);

            // Filter ComboBox
            cmbFilter.BackColor = Color.Transparent;
            cmbFilter.BorderRadius = 10;
            cmbFilter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilter.Location = new Point(300, 16);
            cmbFilter.Size = new Size(180, 36);
            cmbFilter.Items.Add("All Flights");

            // Count Label
            lblCount.BackColor = Color.Transparent;
            lblCount.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblCount.Location = new Point(23, 16);
            lblCount.Text = "Flights (0)";

            // FlightManagement UserControl
            BackColor = Color.FromArgb(245, 246, 250);
            Controls.Add(root);
    

            root.ResumeLayout(false);
            leftCard.ResumeLayout(false);
            leftCard.PerformLayout();
            rightCard.ResumeLayout(false);
            rightCard.PerformLayout();
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
        
            Name = "FlightManagement";
            Size = new Size(963, 683);
            Load += FlightManagement_Load;
            ResumeLayout(false);
        }

        #endregion
    }
}
