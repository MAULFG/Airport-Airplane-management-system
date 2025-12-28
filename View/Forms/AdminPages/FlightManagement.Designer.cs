namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    partial class FlightManagement
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
            this.components = new System.ComponentModel.Container();

            this.rootPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.layout = new System.Windows.Forms.TableLayoutPanel();

            this.leftCard = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.rightCard = new Guna.UI2.WinForms.Guna2ShadowPanel();

            this.lblLeftTitle = new Guna.UI2.WinForms.Guna2HtmlLabel();

            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblDeparture = new System.Windows.Forms.Label();
            this.lblArrival = new System.Windows.Forms.Label();
            this.lblPlane = new System.Windows.Forms.Label();

            this.txtFrom = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtTo = new Guna.UI2.WinForms.Guna2TextBox();

            this.dtDeparture = new System.Windows.Forms.DateTimePicker();
            this.dtArrival = new System.Windows.Forms.DateTimePicker();

            this.cmbPlane = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnAddOrUpdate = new Guna.UI2.WinForms.Guna2Button();
            this.btnCancelEdit = new Guna.UI2.WinForms.Guna2Button();

            this.rightHeader = new System.Windows.Forms.Panel();
            this.lblCount = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.cmbFilter = new Guna.UI2.WinForms.Guna2ComboBox();
            this.flow = new System.Windows.Forms.FlowLayoutPanel();

            this.SuspendLayout();

            // ===================== UserControl =====================
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(245, 246, 250);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Name = "FlightManagement";
            this.Size = new System.Drawing.Size(1200, 720);

            // ===================== rootPanel =====================
            this.rootPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rootPanel.Padding = new System.Windows.Forms.Padding(26);
            this.rootPanel.BackColor = this.BackColor;

            // ===================== layout =====================
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.BackColor = System.Drawing.Color.Transparent;
            this.layout.ColumnCount = 2;
            this.layout.RowCount = 1;
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 430F));
            this.layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.layout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));

            // ===================== leftCard =====================
            this.leftCard.BackColor = System.Drawing.Color.Transparent;
            this.leftCard.FillColor = System.Drawing.Color.White;
            this.leftCard.Radius = 14;
            this.leftCard.ShadowColor = System.Drawing.Color.Black;
            this.leftCard.ShadowDepth = 18;
            this.leftCard.ShadowShift = 2;
            this.leftCard.Padding = new System.Windows.Forms.Padding(20);
            this.leftCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftCard.Margin = new System.Windows.Forms.Padding(0, 0, 26, 0);

            // ===================== rightCard =====================
            this.rightCard.BackColor = System.Drawing.Color.Transparent;
            this.rightCard.FillColor = System.Drawing.Color.White;
            this.rightCard.Radius = 14;
            this.rightCard.ShadowColor = System.Drawing.Color.Black;
            this.rightCard.ShadowDepth = 18;
            this.rightCard.ShadowShift = 2;
            this.rightCard.Padding = new System.Windows.Forms.Padding(20);
            this.rightCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightCard.Margin = new System.Windows.Forms.Padding(0);

            // ===================== LEFT CONTENT =====================
            this.lblLeftTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblLeftTitle.Text = "Add / Edit Flight";
            this.lblLeftTitle.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.lblLeftTitle.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.lblLeftTitle.AutoSize = true;
            this.lblLeftTitle.Location = new System.Drawing.Point(20, 18);

            int x = 26;
            int w = 360;
            int y = 70;

            // From *
            this.lblFrom.AutoSize = true;
            this.lblFrom.Text = "From *";
            this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblFrom.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
            this.lblFrom.Location = new System.Drawing.Point(x, y);
            y += 22;

            this.txtFrom.BorderRadius = 10;
            this.txtFrom.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtFrom.PlaceholderText = "Beirut";
            this.txtFrom.Location = new System.Drawing.Point(x, y);
            this.txtFrom.Size = new System.Drawing.Size(w, 42);
            this.txtFrom.FocusedState.BorderColor = System.Drawing.Color.FromArgb(35, 93, 220);
            this.txtFrom.HoverState.BorderColor = System.Drawing.Color.FromArgb(35, 93, 220);
            y += 58;

            // To *
            this.lblTo.AutoSize = true;
            this.lblTo.Text = "To *";
            this.lblTo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTo.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
            this.lblTo.Location = new System.Drawing.Point(x, y);
            y += 22;

            this.txtTo.BorderRadius = 10;
            this.txtTo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTo.PlaceholderText = "Paris";
            this.txtTo.Location = new System.Drawing.Point(x, y);
            this.txtTo.Size = new System.Drawing.Size(w, 42);
            this.txtTo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(35, 93, 220);
            this.txtTo.HoverState.BorderColor = System.Drawing.Color.FromArgb(35, 93, 220);
            y += 58;

            // Departure *
            this.lblDeparture.AutoSize = true;
            this.lblDeparture.Text = "Departure *";
            this.lblDeparture.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDeparture.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
            this.lblDeparture.Location = new System.Drawing.Point(x, y);
            y += 22;

            this.dtDeparture.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtDeparture.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDeparture.CustomFormat = "dd MMM yyyy  HH:mm";
            this.dtDeparture.ShowUpDown = true;
            this.dtDeparture.Location = new System.Drawing.Point(x, y);
            this.dtDeparture.Size = new System.Drawing.Size(w, 25);
            y += 58;

            // Arrival *
            this.lblArrival.AutoSize = true;
            this.lblArrival.Text = "Arrival *";
            this.lblArrival.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblArrival.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
            this.lblArrival.Location = new System.Drawing.Point(x, y);
            y += 22;

            this.dtArrival.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtArrival.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtArrival.CustomFormat = "dd MMM yyyy  HH:mm";
            this.dtArrival.ShowUpDown = true;
            this.dtArrival.Location = new System.Drawing.Point(x, y);
            this.dtArrival.Size = new System.Drawing.Size(w, 25);
            y += 58;

            // Plane *
            this.lblPlane.AutoSize = true;
            this.lblPlane.Text = "Plane *";
            this.lblPlane.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblPlane.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
            this.lblPlane.Location = new System.Drawing.Point(x, y);
            y += 22;

            this.cmbPlane.BackColor = System.Drawing.Color.Transparent;
            this.cmbPlane.BorderRadius = 10;
            this.cmbPlane.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbPlane.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlane.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPlane.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.cmbPlane.ItemHeight = 36;
            this.cmbPlane.Location = new System.Drawing.Point(x, y);
            this.cmbPlane.Size = new System.Drawing.Size(w, 42);
            this.cmbPlane.FocusedState.BorderColor = System.Drawing.Color.FromArgb(35, 93, 220);
            y += 76;

            this.btnAddOrUpdate.Text = "Add Flight";
            this.btnAddOrUpdate.BorderRadius = 12;
            this.btnAddOrUpdate.FillColor = System.Drawing.Color.FromArgb(35, 93, 220);
            this.btnAddOrUpdate.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnAddOrUpdate.ForeColor = System.Drawing.Color.White;
            this.btnAddOrUpdate.Size = new System.Drawing.Size(w, 48);
            this.btnAddOrUpdate.Location = new System.Drawing.Point(x, y);

            this.btnCancelEdit.Text = "Cancel Edit";
            this.btnCancelEdit.BorderRadius = 12;
            this.btnCancelEdit.FillColor = System.Drawing.Color.FromArgb(235, 235, 235);
            this.btnCancelEdit.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.btnCancelEdit.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.btnCancelEdit.Size = new System.Drawing.Size(w, 44);
            this.btnCancelEdit.Location = new System.Drawing.Point(x, y + 56);
            this.btnCancelEdit.Visible = false;
            this.btnCancelEdit.Enabled = false;

            this.leftCard.Controls.Add(this.lblLeftTitle);
            this.leftCard.Controls.Add(this.lblFrom);
            this.leftCard.Controls.Add(this.txtFrom);
            this.leftCard.Controls.Add(this.lblTo);
            this.leftCard.Controls.Add(this.txtTo);
            this.leftCard.Controls.Add(this.lblDeparture);
            this.leftCard.Controls.Add(this.dtDeparture);
            this.leftCard.Controls.Add(this.lblArrival);
            this.leftCard.Controls.Add(this.dtArrival);
            this.leftCard.Controls.Add(this.lblPlane);
            this.leftCard.Controls.Add(this.cmbPlane);
            this.leftCard.Controls.Add(this.btnAddOrUpdate);
            this.leftCard.Controls.Add(this.btnCancelEdit);

            // ===================== RIGHT HEADER =====================
            this.rightHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.rightHeader.Height = 52;
            this.rightHeader.BackColor = System.Drawing.Color.Transparent;

            this.lblCount.BackColor = System.Drawing.Color.Transparent;
            this.lblCount.Text = "Flights (0)";
            this.lblCount.Font = new System.Drawing.Font("Segoe UI", 12.5F, System.Drawing.FontStyle.Bold);
            this.lblCount.ForeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            this.lblCount.AutoSize = true;
            this.lblCount.Location = new System.Drawing.Point(18, 16);

            this.cmbFilter.BackColor = System.Drawing.Color.Transparent;
            this.cmbFilter.BorderRadius = 10;
            this.cmbFilter.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbFilter.ForeColor = System.Drawing.Color.FromArgb(60, 60, 60);
            this.cmbFilter.ItemHeight = 36;
            this.cmbFilter.Size = new System.Drawing.Size(190, 42);
            this.cmbFilter.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.cmbFilter.Location = new System.Drawing.Point(0, 10);
            this.cmbFilter.FocusedState.BorderColor = System.Drawing.Color.FromArgb(35, 93, 220);

            this.rightHeader.Controls.Add(this.lblCount);
            this.rightHeader.Controls.Add(this.cmbFilter);

            // ===================== FLOW =====================
            this.flow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flow.AutoScroll = true;
            this.flow.WrapContents = false;
            this.flow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flow.Padding = new System.Windows.Forms.Padding(18, 60, 18, 18);
            this.flow.BackColor = System.Drawing.Color.Transparent;

            this.rightCard.Controls.Add(this.flow);
            this.rightCard.Controls.Add(this.rightHeader);

            // ===================== PUT IN TABLE =====================
            this.layout.Controls.Add(this.leftCard, 0, 0);
            this.layout.Controls.Add(this.rightCard, 1, 0);

            this.rootPanel.Controls.Add(this.layout);
            this.Controls.Add(this.rootPanel);

            this.ResumeLayout(false);
        }

        #endregion

        // ===== Designer fields =====
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

        private System.Windows.Forms.DateTimePicker dtDeparture;
        private System.Windows.Forms.DateTimePicker dtArrival;

        private Guna.UI2.WinForms.Guna2ComboBox cmbPlane;

        private Guna.UI2.WinForms.Guna2Button btnAddOrUpdate;
        private Guna.UI2.WinForms.Guna2Button btnCancelEdit;

        private System.Windows.Forms.Panel rightHeader;
        private Guna.UI2.WinForms.Guna2HtmlLabel lblCount;
        private Guna.UI2.WinForms.Guna2ComboBox cmbFilter;

        private System.Windows.Forms.FlowLayoutPanel flow;
    }
}
