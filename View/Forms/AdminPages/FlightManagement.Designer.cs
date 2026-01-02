using Guna.UI2.WinForms;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    partial class FlightManagement
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Guna.UI2.WinForms.Guna2Panel rootPanel;
        private System.Windows.Forms.TableLayoutPanel layout;

        private Guna.UI2.WinForms.Guna2ShadowPanel leftCard;
        private Guna.UI2.WinForms.Guna2ShadowPanel rightCard;

        private Guna.UI2.WinForms.Guna2HtmlLabel lblLeftTitle;

        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblDeparture;
        private System.Windows.Forms.Label lblArrival;
        private System.Windows.Forms.Label lblPlane;

        private Guna.UI2.WinForms.Guna2TextBox txtFrom;
        private Guna.UI2.WinForms.Guna2TextBox txtTo;

        private Guna.UI2.WinForms.Guna2DateTimePicker dtDeparture;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtArrival;

        private Guna.UI2.WinForms.Guna2ComboBox cmbPlane;

        // ===== NEW: Seat Prices fields =====
        private System.Windows.Forms.Label lblSeatPrices;

        private Guna.UI2.WinForms.Guna2Panel rowEconomy;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblEconomy;
        private Guna.UI2.WinForms.Guna2TextBox txtEconomyPrice;

        private Guna.UI2.WinForms.Guna2Panel rowBusiness;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblBusiness;
        private Guna.UI2.WinForms.Guna2TextBox txtBusinessPrice;

        private Guna.UI2.WinForms.Guna2Panel rowFirst;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblFirst;
        private Guna.UI2.WinForms.Guna2TextBox txtFirstPrice;
        // ===== END NEW =====

        private Guna.UI2.WinForms.Guna2Button btnAddOrUpdate;
        private Guna.UI2.WinForms.Guna2Button btnCancelEdit;

        private Guna.UI2.WinForms.Guna2Panel rightHeader;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCount;
        private Guna.UI2.WinForms.Guna2ComboBox cmbFilter;

        private System.Windows.Forms.FlowLayoutPanel flow;
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges31 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges32 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges17 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges18 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges21 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges22 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges19 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges20 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges23 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges24 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges25 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges26 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges29 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges30 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges27 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges28 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            rootPanel = new Guna2Panel();
            layout = new TableLayoutPanel();
            leftCard = new Guna2ShadowPanel();
            txtFrom = new Guna2TextBox();
            txtTo = new Guna2TextBox();
            dtDeparture = new Guna2DateTimePicker();
            dtArrival = new Guna2DateTimePicker();
            cmbPlane = new Guna2ComboBox();
            rowEconomy = new Guna2Panel();
            lblEconomy = new Guna2HtmlLabel();
            txtEconomyPrice = new Guna2TextBox();
            rowBusiness = new Guna2Panel();
            lblBusiness = new Guna2HtmlLabel();
            txtBusinessPrice = new Guna2TextBox();
            rowFirst = new Guna2Panel();
            lblFirst = new Guna2HtmlLabel();
            txtFirstPrice = new Guna2TextBox();
            lblLeftTitle = new Guna2HtmlLabel();
            lblFrom = new Label();
            lblTo = new Label();
            lblDeparture = new Label();
            lblArrival = new Label();
            lblPlane = new Label();
            lblSeatPrices = new Label();
            btnAddOrUpdate = new Guna2Button();
            btnCancelEdit = new Guna2Button();
            rightCard = new Guna2ShadowPanel();
            flow = new FlowLayoutPanel();
            rightHeader = new Guna2Panel();
            lblCount = new Guna2HtmlLabel();
            cmbFilter = new Guna2ComboBox();
            rootPanel.SuspendLayout();
            layout.SuspendLayout();
            leftCard.SuspendLayout();
            rowEconomy.SuspendLayout();
            rowBusiness.SuspendLayout();
            rowFirst.SuspendLayout();
            rightCard.SuspendLayout();
            rightHeader.SuspendLayout();
            SuspendLayout();
            // 
            // rootPanel
            // 
            rootPanel.BackColor = SystemColors.Control;
            rootPanel.Controls.Add(layout);
            rootPanel.CustomizableEdges = customizableEdges31;
            rootPanel.Dock = DockStyle.Fill;
            rootPanel.Location = new Point(0, 0);
            rootPanel.Name = "rootPanel";
            rootPanel.Padding = new Padding(15);
            rootPanel.ShadowDecoration.CustomizableEdges = customizableEdges32;
            rootPanel.Size = new Size(1280, 720);
            rootPanel.TabIndex = 0;
            // 
            // layout
            // 
            layout.BackColor = Color.Transparent;
            layout.ColumnCount = 2;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 430F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layout.Controls.Add(leftCard, 0, 0);
            layout.Controls.Add(rightCard, 1, 0);
            layout.Dock = DockStyle.Fill;
            layout.Location = new Point(15, 15);
            layout.Name = "layout";
            layout.RowCount = 1;
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.Size = new Size(1250, 690);
            layout.TabIndex = 0;
            // 
            // leftCard
            // 
            leftCard.BackColor = Color.Transparent;
            leftCard.Controls.Add(txtFrom);
            leftCard.Controls.Add(txtTo);
            leftCard.Controls.Add(dtDeparture);
            leftCard.Controls.Add(dtArrival);
            leftCard.Controls.Add(cmbPlane);
            leftCard.Controls.Add(rowEconomy);
            leftCard.Controls.Add(rowBusiness);
            leftCard.Controls.Add(rowFirst);
            leftCard.Controls.Add(lblLeftTitle);
            leftCard.Controls.Add(lblFrom);
            leftCard.Controls.Add(lblTo);
            leftCard.Controls.Add(lblDeparture);
            leftCard.Controls.Add(lblArrival);
            leftCard.Controls.Add(lblPlane);
            leftCard.Controls.Add(lblSeatPrices);
            leftCard.Controls.Add(btnAddOrUpdate);
            leftCard.Controls.Add(btnCancelEdit);
            leftCard.Dock = DockStyle.Fill;
            leftCard.FillColor = Color.White;
            leftCard.Location = new Point(0, 0);
            leftCard.Margin = new Padding(0, 0, 26, 0);
            leftCard.Name = "leftCard";
            leftCard.Padding = new Padding(15, 15, 15, 26);
            leftCard.Radius = 14;
            leftCard.ShadowColor = Color.Black;
            leftCard.ShadowDepth = 18;
            leftCard.ShadowShift = 2;
            leftCard.Size = new Size(404, 690);
            leftCard.TabIndex = 0;
            // 
            // txtFrom
            // 
            txtFrom.BorderRadius = 10;
            txtFrom.CustomizableEdges = customizableEdges1;
            txtFrom.DefaultText = "";
            txtFrom.FocusedState.BorderColor = Color.FromArgb(35, 93, 220);
            txtFrom.Font = new Font("Segoe UI", 10F);
            txtFrom.HoverState.BorderColor = Color.FromArgb(35, 93, 220);
            txtFrom.Location = new Point(26, 65);
            txtFrom.Name = "txtFrom";
            txtFrom.PlaceholderText = "Beirut";
            txtFrom.SelectedText = "";
            txtFrom.ShadowDecoration.CustomizableEdges = customizableEdges2;
            txtFrom.Size = new Size(360, 42);
            txtFrom.TabIndex = 2;
            // 
            // txtTo
            // 
            txtTo.BorderRadius = 10;
            txtTo.CustomizableEdges = customizableEdges3;
            txtTo.DefaultText = "";
            txtTo.FocusedState.BorderColor = Color.FromArgb(35, 93, 220);
            txtTo.Font = new Font("Segoe UI", 10F);
            txtTo.HoverState.BorderColor = Color.FromArgb(35, 93, 220);
            txtTo.Location = new Point(26, 129);
            txtTo.Name = "txtTo";
            txtTo.PlaceholderText = "Paris";
            txtTo.SelectedText = "";
            txtTo.ShadowDecoration.CustomizableEdges = customizableEdges4;
            txtTo.Size = new Size(360, 42);
            txtTo.TabIndex = 4;
            txtTo.TextChanged += txtTo_TextChanged;
            // 
            // dtDeparture
            // 
            dtDeparture.BorderRadius = 5;
            dtDeparture.Checked = true;
            dtDeparture.CustomFormat = "dd MMM yyyy HH:mm";
            dtDeparture.CustomizableEdges = customizableEdges5;
            dtDeparture.FillColor = Color.White;
            dtDeparture.Font = new Font("Segoe UI", 10F);
            dtDeparture.Format = DateTimePickerFormat.Custom;
            dtDeparture.Location = new Point(26, 194);
            dtDeparture.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtDeparture.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dtDeparture.Name = "dtDeparture";
            dtDeparture.ShadowDecoration.CustomizableEdges = customizableEdges6;
            dtDeparture.ShowUpDown = true;
            dtDeparture.Size = new Size(360, 26);
            dtDeparture.TabIndex = 6;
            dtDeparture.Value = new DateTime(2026, 1, 1, 18, 49, 32, 971);
            // 
            // dtArrival
            // 
            dtArrival.BorderRadius = 5;
            dtArrival.Checked = true;
            dtArrival.CustomFormat = "dd MMM yyyy HH:mm";
            dtArrival.CustomizableEdges = customizableEdges7;
            dtArrival.FillColor = Color.White;
            dtArrival.Font = new Font("Segoe UI", 10F);
            dtArrival.Format = DateTimePickerFormat.Custom;
            dtArrival.Location = new Point(26, 243);
            dtArrival.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtArrival.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dtArrival.Name = "dtArrival";
            dtArrival.ShadowDecoration.CustomizableEdges = customizableEdges8;
            dtArrival.ShowUpDown = true;
            dtArrival.Size = new Size(360, 26);
            dtArrival.TabIndex = 8;
            dtArrival.Value = new DateTime(2026, 1, 1, 18, 49, 32, 991);
            // 
            // cmbPlane
            // 
            cmbPlane.BackColor = Color.Transparent;
            cmbPlane.BorderRadius = 10;
            cmbPlane.CustomizableEdges = customizableEdges9;
            cmbPlane.DrawMode = DrawMode.OwnerDrawFixed;
            cmbPlane.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlane.FocusedColor = Color.FromArgb(35, 93, 220);
            cmbPlane.FocusedState.BorderColor = Color.FromArgb(35, 93, 220);
            cmbPlane.Font = new Font("Segoe UI", 10F);
            cmbPlane.ForeColor = Color.FromArgb(60, 60, 60);
            cmbPlane.ItemHeight = 36;
            cmbPlane.Location = new Point(26, 293);
            cmbPlane.Name = "cmbPlane";
            cmbPlane.ShadowDecoration.CustomizableEdges = customizableEdges10;
            cmbPlane.Size = new Size(360, 42);
            cmbPlane.TabIndex = 10;
            cmbPlane.SelectedIndexChanged += cmbPlane_SelectedIndexChanged;
            // 
            // rowEconomy
            // 
            rowEconomy.BackColor = Color.Transparent;
            rowEconomy.Controls.Add(lblEconomy);
            rowEconomy.Controls.Add(txtEconomyPrice);
            rowEconomy.CustomizableEdges = customizableEdges13;
            rowEconomy.Location = new Point(26, 358);
            rowEconomy.Name = "rowEconomy";
            rowEconomy.ShadowDecoration.CustomizableEdges = customizableEdges14;
            rowEconomy.Size = new Size(360, 42);
            rowEconomy.TabIndex = 12;
            // 
            // lblEconomy
            // 
            lblEconomy.BackColor = Color.Transparent;
            lblEconomy.Font = new Font("Segoe UI", 9.5F);
            lblEconomy.ForeColor = Color.FromArgb(70, 70, 70);
            lblEconomy.Location = new Point(5, 12);
            lblEconomy.Name = "lblEconomy";
            lblEconomy.Size = new Size(56, 19);
            lblEconomy.TabIndex = 0;
            lblEconomy.Text = "Economy";
            // 
            // txtEconomyPrice
            // 
            txtEconomyPrice.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtEconomyPrice.BorderRadius = 10;
            txtEconomyPrice.CustomizableEdges = customizableEdges11;
            txtEconomyPrice.DefaultText = "";
            txtEconomyPrice.FocusedState.BorderColor = Color.FromArgb(35, 93, 220);
            txtEconomyPrice.Font = new Font("Segoe UI", 10F);
            txtEconomyPrice.HoverState.BorderColor = Color.FromArgb(35, 93, 220);
            txtEconomyPrice.Location = new Point(218, 0);
            txtEconomyPrice.Name = "txtEconomyPrice";
            txtEconomyPrice.PlaceholderText = "0.00";
            txtEconomyPrice.SelectedText = "";
            txtEconomyPrice.ShadowDecoration.CustomizableEdges = customizableEdges12;
            txtEconomyPrice.Size = new Size(140, 42);
            txtEconomyPrice.TabIndex = 1;
            // 
            // rowBusiness
            // 
            rowBusiness.BackColor = Color.Transparent;
            rowBusiness.Controls.Add(lblBusiness);
            rowBusiness.Controls.Add(txtBusinessPrice);
            rowBusiness.CustomizableEdges = customizableEdges17;
            rowBusiness.Location = new Point(26, 408);
            rowBusiness.Name = "rowBusiness";
            rowBusiness.ShadowDecoration.CustomizableEdges = customizableEdges18;
            rowBusiness.Size = new Size(360, 42);
            rowBusiness.TabIndex = 13;
            // 
            // lblBusiness
            // 
            lblBusiness.BackColor = Color.Transparent;
            lblBusiness.Font = new Font("Segoe UI", 9.5F);
            lblBusiness.ForeColor = Color.FromArgb(70, 70, 70);
            lblBusiness.Location = new Point(4, 12);
            lblBusiness.Name = "lblBusiness";
            lblBusiness.Size = new Size(52, 19);
            lblBusiness.TabIndex = 0;
            lblBusiness.Text = "Business";
            // 
            // txtBusinessPrice
            // 
            txtBusinessPrice.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBusinessPrice.BorderRadius = 10;
            txtBusinessPrice.CustomizableEdges = customizableEdges15;
            txtBusinessPrice.DefaultText = "";
            txtBusinessPrice.FocusedState.BorderColor = Color.FromArgb(35, 93, 220);
            txtBusinessPrice.Font = new Font("Segoe UI", 10F);
            txtBusinessPrice.HoverState.BorderColor = Color.FromArgb(35, 93, 220);
            txtBusinessPrice.Location = new Point(218, 0);
            txtBusinessPrice.Name = "txtBusinessPrice";
            txtBusinessPrice.PlaceholderText = "0.00";
            txtBusinessPrice.SelectedText = "";
            txtBusinessPrice.ShadowDecoration.CustomizableEdges = customizableEdges16;
            txtBusinessPrice.Size = new Size(140, 42);
            txtBusinessPrice.TabIndex = 1;
            // 
            // rowFirst
            // 
            rowFirst.BackColor = Color.Transparent;
            rowFirst.Controls.Add(lblFirst);
            rowFirst.Controls.Add(txtFirstPrice);
            rowFirst.CustomizableEdges = customizableEdges21;
            rowFirst.Location = new Point(26, 458);
            rowFirst.Name = "rowFirst";
            rowFirst.ShadowDecoration.CustomizableEdges = customizableEdges22;
            rowFirst.Size = new Size(360, 42);
            rowFirst.TabIndex = 14;
            rowFirst.Paint += rowFirst_Paint;
            // 
            // lblFirst
            // 
            lblFirst.BackColor = Color.Transparent;
            lblFirst.Font = new Font("Segoe UI", 9.5F);
            lblFirst.ForeColor = Color.FromArgb(70, 70, 70);
            lblFirst.Location = new Point(4, 12);
            lblFirst.Name = "lblFirst";
            lblFirst.Size = new Size(27, 19);
            lblFirst.TabIndex = 0;
            lblFirst.Text = "First";
            // 
            // txtFirstPrice
            // 
            txtFirstPrice.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtFirstPrice.BorderRadius = 10;
            txtFirstPrice.CustomizableEdges = customizableEdges19;
            txtFirstPrice.DefaultText = "";
            txtFirstPrice.FocusedState.BorderColor = Color.FromArgb(35, 93, 220);
            txtFirstPrice.Font = new Font("Segoe UI", 10F);
            txtFirstPrice.HoverState.BorderColor = Color.FromArgb(35, 93, 220);
            txtFirstPrice.Location = new Point(218, 0);
            txtFirstPrice.Name = "txtFirstPrice";
            txtFirstPrice.PlaceholderText = "0.00";
            txtFirstPrice.SelectedText = "";
            txtFirstPrice.ShadowDecoration.CustomizableEdges = customizableEdges20;
            txtFirstPrice.Size = new Size(140, 42);
            txtFirstPrice.TabIndex = 1;
            // 
            // lblLeftTitle
            // 
            lblLeftTitle.BackColor = Color.Transparent;
            lblLeftTitle.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblLeftTitle.ForeColor = Color.FromArgb(30, 30, 30);
            lblLeftTitle.Location = new Point(15, 13);
            lblLeftTitle.Name = "lblLeftTitle";
            lblLeftTitle.Size = new Size(139, 25);
            lblLeftTitle.TabIndex = 0;
            lblLeftTitle.Text = "Add / Edit Flight";
            // 
            // lblFrom
            // 
            lblFrom.AutoSize = true;
            lblFrom.Font = new Font("Segoe UI", 9.5F);
            lblFrom.ForeColor = Color.FromArgb(70, 70, 70);
            lblFrom.Location = new Point(21, 45);
            lblFrom.Name = "lblFrom";
            lblFrom.Size = new Size(47, 17);
            lblFrom.TabIndex = 1;
            lblFrom.Text = "From *";
            // 
            // lblTo
            // 
            lblTo.AutoSize = true;
            lblTo.Font = new Font("Segoe UI", 9.5F);
            lblTo.ForeColor = Color.FromArgb(70, 70, 70);
            lblTo.Location = new Point(21, 109);
            lblTo.Name = "lblTo";
            lblTo.Size = new Size(31, 17);
            lblTo.TabIndex = 3;
            lblTo.Text = "To *";
            // 
            // lblDeparture
            // 
            lblDeparture.AutoSize = true;
            lblDeparture.Font = new Font("Segoe UI", 9.5F);
            lblDeparture.ForeColor = Color.FromArgb(70, 70, 70);
            lblDeparture.Location = new Point(21, 174);
            lblDeparture.Name = "lblDeparture";
            lblDeparture.Size = new Size(76, 17);
            lblDeparture.TabIndex = 5;
            lblDeparture.Text = "Departure *";
            // 
            // lblArrival
            // 
            lblArrival.AutoSize = true;
            lblArrival.Font = new Font("Segoe UI", 9.5F);
            lblArrival.ForeColor = Color.FromArgb(70, 70, 70);
            lblArrival.Location = new Point(21, 223);
            lblArrival.Name = "lblArrival";
            lblArrival.Size = new Size(54, 17);
            lblArrival.TabIndex = 7;
            lblArrival.Text = "Arrival *";
            // 
            // lblPlane
            // 
            lblPlane.AutoSize = true;
            lblPlane.Font = new Font("Segoe UI", 9.5F);
            lblPlane.ForeColor = Color.FromArgb(70, 70, 70);
            lblPlane.Location = new Point(21, 272);
            lblPlane.Name = "lblPlane";
            lblPlane.Size = new Size(48, 17);
            lblPlane.TabIndex = 9;
            lblPlane.Text = "Plane *";
            // 
            // lblSeatPrices
            // 
            lblSeatPrices.AutoSize = true;
            lblSeatPrices.Font = new Font("Segoe UI", 9.5F);
            lblSeatPrices.ForeColor = Color.FromArgb(70, 70, 70);
            lblSeatPrices.Location = new Point(21, 337);
            lblSeatPrices.Name = "lblSeatPrices";
            lblSeatPrices.Size = new Size(131, 17);
            lblSeatPrices.TabIndex = 11;
            lblSeatPrices.Text = "Seat Prices (per seat)";
            // 
            // btnAddOrUpdate
            // 
            btnAddOrUpdate.BorderRadius = 12;
            btnAddOrUpdate.CustomizableEdges = customizableEdges23;
            btnAddOrUpdate.Dock = DockStyle.Bottom;
            btnAddOrUpdate.FillColor = Color.FromArgb(35, 93, 220);
            btnAddOrUpdate.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnAddOrUpdate.ForeColor = Color.White;
            btnAddOrUpdate.Location = new Point(15, 624);
            btnAddOrUpdate.Margin = new Padding(5);
            btnAddOrUpdate.Name = "btnAddOrUpdate";
            btnAddOrUpdate.Padding = new Padding(5);
            btnAddOrUpdate.ShadowDecoration.CustomizableEdges = customizableEdges24;
            btnAddOrUpdate.Size = new Size(374, 40);
            btnAddOrUpdate.TabIndex = 15;
            btnAddOrUpdate.Text = "Add Flight";
            btnAddOrUpdate.Click += btnAddOrUpdate_Click;
            // 
            // btnCancelEdit
            // 
            btnCancelEdit.BorderRadius = 12;
            btnCancelEdit.CustomizableEdges = customizableEdges25;
            btnCancelEdit.Enabled = false;
            btnCancelEdit.FillColor = Color.FromArgb(235, 235, 235);
            btnCancelEdit.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            btnCancelEdit.ForeColor = Color.FromArgb(60, 60, 60);
            btnCancelEdit.Location = new Point(15, 505);
            btnCancelEdit.Margin = new Padding(5);
            btnCancelEdit.Name = "btnCancelEdit";
            btnCancelEdit.Padding = new Padding(5);
            btnCancelEdit.ShadowDecoration.CustomizableEdges = customizableEdges26;
            btnCancelEdit.Size = new Size(374, 40);
            btnCancelEdit.TabIndex = 16;
            btnCancelEdit.Text = "Cancel Edit";
            btnCancelEdit.Visible = false;
            // 
            // rightCard
            // 
            rightCard.BackColor = Color.Transparent;
            rightCard.Controls.Add(flow);
            rightCard.Controls.Add(rightHeader);
            rightCard.Dock = DockStyle.Fill;
            rightCard.FillColor = Color.White;
            rightCard.Location = new Point(430, 0);
            rightCard.Margin = new Padding(0);
            rightCard.Name = "rightCard";
            rightCard.Padding = new Padding(20);
            rightCard.Radius = 14;
            rightCard.ShadowColor = Color.Black;
            rightCard.ShadowDepth = 18;
            rightCard.ShadowShift = 2;
            rightCard.Size = new Size(820, 690);
            rightCard.TabIndex = 1;
            // 
            // flow
            // 
            flow.AutoScroll = true;
            flow.AutoScrollMargin = new Size(780, 0);
            flow.BackColor = Color.Transparent;
            flow.Dock = DockStyle.Fill;
            flow.FlowDirection = FlowDirection.TopDown;
            flow.Location = new Point(20, 72);
            flow.Name = "flow";
            flow.Padding = new Padding(18, 60, 18, 500);
            flow.Size = new Size(780, 598);
            flow.TabIndex = 0;
            flow.WrapContents = false;
            flow.Paint += flow_Paint;
            // 
            // rightHeader
            // 
            rightHeader.BackColor = Color.Transparent;
            rightHeader.Controls.Add(lblCount);
            rightHeader.Controls.Add(cmbFilter);
            rightHeader.CustomizableEdges = customizableEdges29;
            rightHeader.Dock = DockStyle.Top;
            rightHeader.Location = new Point(20, 20);
            rightHeader.Name = "rightHeader";
            rightHeader.ShadowDecoration.CustomizableEdges = customizableEdges30;
            rightHeader.Size = new Size(780, 52);
            rightHeader.TabIndex = 1;
            // 
            // lblCount
            // 
            lblCount.BackColor = Color.Transparent;
            lblCount.Font = new Font("Segoe UI", 12.5F, FontStyle.Bold);
            lblCount.ForeColor = Color.FromArgb(30, 30, 30);
            lblCount.Location = new Point(18, 16);
            lblCount.Name = "lblCount";
            lblCount.Size = new Size(84, 25);
            lblCount.TabIndex = 0;
            lblCount.Text = "Flights (0)";
            lblCount.Click += lblCount_Click;
            // 
            // cmbFilter
            // 
            cmbFilter.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbFilter.BackColor = Color.Transparent;
            cmbFilter.BorderRadius = 10;
            cmbFilter.CustomizableEdges = customizableEdges27;
            cmbFilter.DrawMode = DrawMode.OwnerDrawFixed;
            cmbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFilter.FocusedColor = Color.FromArgb(35, 93, 220);
            cmbFilter.FocusedState.BorderColor = Color.FromArgb(35, 93, 220);
            cmbFilter.Font = new Font("Segoe UI", 10F);
            cmbFilter.ForeColor = Color.FromArgb(60, 60, 60);
            cmbFilter.ItemHeight = 36;
            cmbFilter.Location = new Point(569, 5);
            cmbFilter.Name = "cmbFilter";
            cmbFilter.ShadowDecoration.CustomizableEdges = customizableEdges28;
            cmbFilter.Size = new Size(190, 42);
            cmbFilter.TabIndex = 1;
            // 
            // FlightManagement
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 246, 250);
            Controls.Add(rootPanel);
            Name = "FlightManagement";
            Size = new Size(1030, 720);
            Load += FlightManagement_Load;
            rootPanel.ResumeLayout(false);
            layout.ResumeLayout(false);
            leftCard.ResumeLayout(false);
            leftCard.PerformLayout();
            rowEconomy.ResumeLayout(false);
            rowEconomy.PerformLayout();
            rowBusiness.ResumeLayout(false);
            rowBusiness.PerformLayout();
            rowFirst.ResumeLayout(false);
            rowFirst.PerformLayout();
            rightCard.ResumeLayout(false);
            rightHeader.ResumeLayout(false);
            rightHeader.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}
