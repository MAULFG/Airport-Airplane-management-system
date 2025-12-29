//namespace Airport_Airplane_management_system.View.UserControls
//{
//    partial class PlaneScheduleControl
//    {
//        private System.ComponentModel.IContainer components = null;

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null)) components.Dispose();
//            base.Dispose(disposing);
//        }

//        #region Component Designer generated code

//        private void InitializeComponent()
//        {
//            this.root = new System.Windows.Forms.Panel();
//            this.pnlHeader = new System.Windows.Forms.Panel();
//            this.btnClose = new System.Windows.Forms.Button();
//            this.lblSubtitle = new System.Windows.Forms.Label();
//            this.lblTitle = new System.Windows.Forms.Label();
//            this.sepHeader = new System.Windows.Forms.Panel();
//            this.body = new System.Windows.Forms.Panel();
//            this.pnlTop = new System.Windows.Forms.Panel();
//            this.lblSelectDate = new System.Windows.Forms.Label();
//            this.flowDates = new System.Windows.Forms.FlowLayoutPanel();
//            this.pnlTimelineTop = new System.Windows.Forms.Panel();
//            this.btnAddFlight = new System.Windows.Forms.Button();
//            this.lblTimeline = new System.Windows.Forms.Label();
//            this.scrollTimeline = new System.Windows.Forms.Panel();
//            this.tblTimeline = new System.Windows.Forms.TableLayoutPanel();
//            this.root.SuspendLayout();
//            this.pnlHeader.SuspendLayout();
//            this.body.SuspendLayout();
//            this.pnlTop.SuspendLayout();
//            this.pnlTimelineTop.SuspendLayout();
//            this.scrollTimeline.SuspendLayout();
//            this.SuspendLayout();
//            // 
//            // root
//            // 
//            this.root.BackColor = System.Drawing.Color.White;
//            this.root.Controls.Add(this.body);
//            this.root.Controls.Add(this.sepHeader);
//            this.root.Controls.Add(this.pnlHeader);
//            this.root.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.root.Location = new System.Drawing.Point(0, 0);
//            this.root.Name = "root";
//            this.root.Size = new System.Drawing.Size(980, 720);
//            this.root.TabIndex = 0;
//            // 
//            // pnlHeader
//            // 
//            this.pnlHeader.BackColor = System.Drawing.Color.White;
//            this.pnlHeader.Controls.Add(this.btnClose);
//            this.pnlHeader.Controls.Add(this.lblSubtitle);
//            this.pnlHeader.Controls.Add(this.lblTitle);
//            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
//            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
//            this.pnlHeader.Name = "pnlHeader";
//            this.pnlHeader.Padding = new System.Windows.Forms.Padding(18, 16, 18, 10);
//            this.pnlHeader.Size = new System.Drawing.Size(980, 70);
//            this.pnlHeader.TabIndex = 0;
//            // 
//            // btnClose
//            // 
//            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
//            this.btnClose.BackColor = System.Drawing.Color.White;
//            this.btnClose.FlatAppearance.BorderSize = 0;
//            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
//            this.btnClose.ForeColor = System.Drawing.Color.Black;
//            this.btnClose.Location = new System.Drawing.Point(934, 12);
//            this.btnClose.Name = "btnClose";
//            this.btnClose.Size = new System.Drawing.Size(34, 34);
//            this.btnClose.TabIndex = 2;
//            this.btnClose.Text = "✕";
//            this.btnClose.UseVisualStyleBackColor = false;
//            // 
//            // lblSubtitle
//            // 
//            this.lblSubtitle.AutoSize = true;
//            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
//            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
//            this.lblSubtitle.Location = new System.Drawing.Point(20, 42);
//            this.lblSubtitle.Name = "lblSubtitle";
//            this.lblSubtitle.Size = new System.Drawing.Size(251, 15);
//            this.lblSubtitle.TabIndex = 1;
//            this.lblSubtitle.Text = "View and manage flight schedule for this aircraft";
//            // 
//            // lblTitle
//            // 
//            this.lblTitle.AutoSize = true;
//            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
//            this.lblTitle.ForeColor = System.Drawing.Color.Black;
//            this.lblTitle.Location = new System.Drawing.Point(18, 14);
//            this.lblTitle.Name = "lblTitle";
//            this.lblTitle.Size = new System.Drawing.Size(226, 25);
//            this.lblTitle.TabIndex = 0;
//            this.lblTitle.Text = "Boeing 737-800 Schedule";
//            // 
//            // sepHeader
//            // 
//            this.sepHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
//            this.sepHeader.Dock = System.Windows.Forms.DockStyle.Top;
//            this.sepHeader.Location = new System.Drawing.Point(0, 70);
//            this.sepHeader.Name = "sepHeader";
//            this.sepHeader.Size = new System.Drawing.Size(980, 1);
//            this.sepHeader.TabIndex = 1;
//            // 
//            // body
//            // 
//            this.body.BackColor = System.Drawing.Color.White;
//            this.body.Controls.Add(this.scrollTimeline);
//            this.body.Controls.Add(this.pnlTimelineTop);
//            this.body.Controls.Add(this.flowDates);
//            this.body.Controls.Add(this.pnlTop);
//            this.body.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.body.Location = new System.Drawing.Point(0, 71);
//            this.body.Name = "body";
//            this.body.Padding = new System.Windows.Forms.Padding(18, 14, 18, 18);
//            this.body.Size = new System.Drawing.Size(980, 649);
//            this.body.TabIndex = 2;
//            // 
//            // pnlTop
//            // 
//            this.pnlTop.Controls.Add(this.lblSelectDate);
//            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
//            this.pnlTop.Location = new System.Drawing.Point(18, 14);
//            this.pnlTop.Name = "pnlTop";
//            this.pnlTop.Size = new System.Drawing.Size(944, 22);
//            this.pnlTop.TabIndex = 0;
//            // 
//            // lblSelectDate
//            // 
//            this.lblSelectDate.AutoSize = true;
//            this.lblSelectDate.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
//            this.lblSelectDate.ForeColor = System.Drawing.Color.Black;
//            this.lblSelectDate.Location = new System.Drawing.Point(0, 2);
//            this.lblSelectDate.Name = "lblSelectDate";
//            this.lblSelectDate.Size = new System.Drawing.Size(73, 17);
//            this.lblSelectDate.TabIndex = 0;
//            this.lblSelectDate.Text = "Select Date";
//            // 
//            // flowDates
//            // 
//            this.flowDates.AutoScroll = true;
//            this.flowDates.Dock = System.Windows.Forms.DockStyle.Top;
//            this.flowDates.Location = new System.Drawing.Point(18, 36);
//            this.flowDates.Name = "flowDates";
//            this.flowDates.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
//            this.flowDates.Size = new System.Drawing.Size(944, 98);
//            this.flowDates.TabIndex = 1;
//            this.flowDates.WrapContents = false;
//            // 
//            // pnlTimelineTop
//            // 
//            this.pnlTimelineTop.Controls.Add(this.btnAddFlight);
//            this.pnlTimelineTop.Controls.Add(this.lblTimeline);
//            this.pnlTimelineTop.Dock = System.Windows.Forms.DockStyle.Top;
//            this.pnlTimelineTop.Location = new System.Drawing.Point(18, 134);
//            this.pnlTimelineTop.Name = "pnlTimelineTop";
//            this.pnlTimelineTop.Size = new System.Drawing.Size(944, 46);
//            this.pnlTimelineTop.TabIndex = 2;
//            // 
//            // btnAddFlight
//            // 
//            this.btnAddFlight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
//            this.btnAddFlight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(16)))), ((int)(((byte)(24)))));
//            this.btnAddFlight.FlatAppearance.BorderSize = 0;
//            this.btnAddFlight.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
//            this.btnAddFlight.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
//            this.btnAddFlight.ForeColor = System.Drawing.Color.White;
//            this.btnAddFlight.Location = new System.Drawing.Point(825, 8);
//            this.btnAddFlight.Name = "btnAddFlight";
//            this.btnAddFlight.Size = new System.Drawing.Size(119, 30);
//            this.btnAddFlight.TabIndex = 1;
//            this.btnAddFlight.Text = "＋   Add Flight";
//            this.btnAddFlight.UseVisualStyleBackColor = false;
//            // 
//            // lblTimeline
//            // 
//            this.lblTimeline.AutoSize = true;
//            this.lblTimeline.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
//            this.lblTimeline.ForeColor = System.Drawing.Color.Black;
//            this.lblTimeline.Location = new System.Drawing.Point(0, 12);
//            this.lblTimeline.Name = "lblTimeline";
//            this.lblTimeline.Size = new System.Drawing.Size(163, 20);
//            this.lblTimeline.TabIndex = 0;
//            this.lblTimeline.Text = "Timeline for 2025-12-28";
//            // 
//            // scrollTimeline
//            // 
//            this.scrollTimeline.AutoScroll = true;
//            this.scrollTimeline.BackColor = System.Drawing.Color.White;
//            this.scrollTimeline.Controls.Add(this.tblTimeline);
//            this.scrollTimeline.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.scrollTimeline.Location = new System.Drawing.Point(18, 180);
//            this.scrollTimeline.Name = "scrollTimeline";
//            this.scrollTimeline.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
//            this.scrollTimeline.Size = new System.Drawing.Size(944, 451);
//            this.scrollTimeline.TabIndex = 3;
//            // 
//            // tblTimeline
//            // 
//            this.tblTimeline.AutoSize = true;
//            this.tblTimeline.ColumnCount = 2;
//            this.tblTimeline.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
//            this.tblTimeline.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
//            this.tblTimeline.Dock = System.Windows.Forms.DockStyle.Top;
//            this.tblTimeline.Location = new System.Drawing.Point(0, 6);
//            this.tblTimeline.Name = "tblTimeline";
//            this.tblTimeline.RowCount = 1;
//            this.tblTimeline.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
//            this.tblTimeline.Size = new System.Drawing.Size(944, 52);
//            this.tblTimeline.TabIndex = 0;
//            // 
//            // PlaneScheduleControl
//            // 
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.BackColor = System.Drawing.Color.White;
//            this.Controls.Add(this.root);
//            this.Name = "PlaneScheduleControl";
//            this.Size = new System.Drawing.Size(980, 720);
//            this.root.ResumeLayout(false);
//            this.pnlHeader.ResumeLayout(false);
//            this.pnlHeader.PerformLayout();
//            this.body.ResumeLayout(false);
//            this.pnlTop.ResumeLayout(false);
//            this.pnlTop.PerformLayout();
//            this.pnlTimelineTop.ResumeLayout(false);
//            this.pnlTimelineTop.PerformLayout();
//            this.scrollTimeline.ResumeLayout(false);
//            this.scrollTimeline.PerformLayout();
//            this.ResumeLayout(false);

//        }

//        #endregion

//        private System.Windows.Forms.Panel root;
//        private System.Windows.Forms.Panel pnlHeader;
//        private System.Windows.Forms.Label lblTitle;
//        private System.Windows.Forms.Label lblSubtitle;
//        private System.Windows.Forms.Button btnClose;
//        private System.Windows.Forms.Panel sepHeader;
//        private System.Windows.Forms.Panel body;
//        private System.Windows.Forms.Panel pnlTop;
//        private System.Windows.Forms.Label lblSelectDate;
//        private System.Windows.Forms.FlowLayoutPanel flowDates;
//        private System.Windows.Forms.Panel pnlTimelineTop;
//        private System.Windows.Forms.Label lblTimeline;
//        private System.Windows.Forms.Button btnAddFlight;
//        private System.Windows.Forms.Panel scrollTimeline;
//        private System.Windows.Forms.TableLayoutPanel tblTimeline;
//    }
//}
