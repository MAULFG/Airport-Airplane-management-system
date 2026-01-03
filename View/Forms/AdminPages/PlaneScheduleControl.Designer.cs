namespace Airport_Airplane_management_system.View.UserControls
{
    partial class PlaneScheduleControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            root = new Panel();
            body = new Panel();
            scrollTimeline = new Panel();
            tblTimeline = new TableLayoutPanel();
            pnlTimelineTop = new Panel();
            lblTimeline = new Label();
            flowDates = new FlowLayoutPanel();
            pnlTop = new Panel();
            lblSelectDate = new Label();
            sepHeader = new Panel();
            pnlHeader = new Panel();
            btnClose = new Button();
            lblTitle = new Label();
            root.SuspendLayout();
            body.SuspendLayout();
            scrollTimeline.SuspendLayout();
            pnlTimelineTop.SuspendLayout();
            pnlTop.SuspendLayout();
            pnlHeader.SuspendLayout();
            SuspendLayout();
            // 
            // root
            // 
            root.BackColor = Color.White;
            root.Controls.Add(body);
            root.Controls.Add(sepHeader);
            root.Controls.Add(pnlHeader);
            root.Dock = DockStyle.Fill;
            root.Location = new Point(0, 0);
            root.Name = "root";
            root.Size = new Size(1100, 760);
            root.TabIndex = 0;
            // 
            // body
            // 
            body.BackColor = Color.White;
            body.Controls.Add(scrollTimeline);
            body.Controls.Add(pnlTimelineTop);
            body.Controls.Add(flowDates);
            body.Controls.Add(pnlTop);
            body.Dock = DockStyle.Fill;
            body.Location = new Point(0, 58);
            body.Name = "body";
            body.Padding = new Padding(18, 14, 18, 18);
            body.Size = new Size(1100, 702);
            body.TabIndex = 2;
            // 
            // scrollTimeline
            // 
            scrollTimeline.AutoScroll = true;
            scrollTimeline.BackColor = Color.White;
            scrollTimeline.Controls.Add(tblTimeline);
            scrollTimeline.Dock = DockStyle.Fill;
            scrollTimeline.Location = new Point(18, 180);
            scrollTimeline.Name = "scrollTimeline";
            scrollTimeline.Padding = new Padding(0, 6, 0, 0);
            scrollTimeline.Size = new Size(1064, 504);
            scrollTimeline.TabIndex = 3;
            // 
            // tblTimeline
            // 
            tblTimeline.ColumnCount = 2;
            tblTimeline.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tblTimeline.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblTimeline.Dock = DockStyle.Top;
            tblTimeline.Location = new Point(0, 6);
            tblTimeline.Name = "tblTimeline";
            tblTimeline.RowCount = 1;
            tblTimeline.RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            tblTimeline.Size = new Size(1064, 52);
            tblTimeline.TabIndex = 0;
            // 
            // pnlTimelineTop
            // 
            pnlTimelineTop.Controls.Add(lblTimeline);
            pnlTimelineTop.Dock = DockStyle.Top;
            pnlTimelineTop.Location = new Point(18, 134);
            pnlTimelineTop.Name = "pnlTimelineTop";
            pnlTimelineTop.Size = new Size(1064, 46);
            pnlTimelineTop.TabIndex = 2;
            // 
            // lblTimeline
            // 
            lblTimeline.AutoSize = true;
            lblTimeline.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            lblTimeline.ForeColor = Color.Black;
            lblTimeline.Location = new Point(0, 12);
            lblTimeline.Name = "lblTimeline";
            lblTimeline.Size = new Size(221, 25);
            lblTimeline.TabIndex = 0;
            lblTimeline.Text = "Timeline for 2025-12-28";
            // 
            // flowDates
            // 
            flowDates.AutoScroll = true;
            flowDates.Dock = DockStyle.Top;
            flowDates.Location = new Point(18, 36);
            flowDates.Name = "flowDates";
            flowDates.Size = new Size(1064, 98);
            flowDates.TabIndex = 4;
            flowDates.WrapContents = false;
            flowDates.Paint += flowDates_Paint;
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(lblSelectDate);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(18, 14);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1064, 22);
            pnlTop.TabIndex = 0;
            // 
            // lblSelectDate
            // 
            lblSelectDate.AutoSize = true;
            lblSelectDate.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            lblSelectDate.ForeColor = Color.Black;
            lblSelectDate.Location = new Point(0, -2);
            lblSelectDate.Name = "lblSelectDate";
            lblSelectDate.Size = new Size(93, 21);
            lblSelectDate.TabIndex = 0;
            lblSelectDate.Text = "Select Date";
            lblSelectDate.Click += lblSelectDate_Click;
            // 
            // sepHeader
            // 
            sepHeader.BackColor = Color.FromArgb(235, 235, 235);
            sepHeader.Dock = DockStyle.Top;
            sepHeader.Location = new Point(0, 57);
            sepHeader.Name = "sepHeader";
            sepHeader.Size = new Size(1100, 1);
            sepHeader.TabIndex = 1;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(btnClose);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(18, 16, 18, 10);
            pnlHeader.Size = new Size(1100, 57);
            pnlHeader.TabIndex = 0;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BackColor = Color.White;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 12F);
            btnClose.ForeColor = Color.Black;
            btnClose.Location = new Point(1054, 12);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(34, 34);
            btnClose.TabIndex = 2;
            btnClose.Text = "✕";
            btnClose.UseVisualStyleBackColor = false;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            lblTitle.ForeColor = Color.Black;
            lblTitle.Location = new Point(18, 14);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(129, 32);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "1 Schedule";
            // 
            // PlaneScheduleControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(root);
            Name = "PlaneScheduleControl";
            Size = new Size(1100, 760);
            root.ResumeLayout(false);
            body.ResumeLayout(false);
            scrollTimeline.ResumeLayout(false);
            pnlTimelineTop.ResumeLayout(false);
            pnlTimelineTop.PerformLayout();
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel root;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel sepHeader;
        private System.Windows.Forms.Panel body;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblSelectDate;
        private System.Windows.Forms.FlowLayoutPanel flowDates;
        private System.Windows.Forms.Panel pnlTimelineTop;
        private System.Windows.Forms.Label lblTimeline;
        private System.Windows.Forms.Panel scrollTimeline;
        private System.Windows.Forms.TableLayoutPanel tblTimeline;
    }
}
