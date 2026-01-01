using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    public partial class Reports : UserControl, IReportsView
    {
        // ===== MVP events (View -> Presenter) =====
        public event Action ViewLoaded;
        public event Action<string> SearchChanged;
        public event Action<ReportItemRow> ReportCardClicked;

        // ===== Navigation event (View -> Main container) =====
        // Your MainA (or Dashboard) should subscribe and open the page.
        public event Action<string> NavigateRequested;


        public Reports()
        {
            InitializeComponent();

            txtSearch.TextChanged += (s, e) => SearchChanged?.Invoke(txtSearch.Text);

            listPanel.Resize += (s, e) =>
            {
                foreach (Control c in listPanel.Controls)
                    c.Width = listPanel.ClientSize.Width - 25;
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!DesignMode)
                ViewLoaded?.Invoke();
        }

        // ===== Presenter -> View =====
        public void SetHeaderCounts(int totalIssues)
        {
            lblTotalIssuesValue.Text = totalIssues.ToString();
        }

        public void RenderReports(List<ReportItemRow> items)
        {
            items ??= new List<ReportItemRow>();

            listPanel.SuspendLayout();
            listPanel.Controls.Clear();

            foreach (var row in items)
            {
                var card = new ReportCard(row);
                card.Width = listPanel.ClientSize.Width - 25;

                card.Clicked += () =>
                {
                    ReportCardClicked?.Invoke(row);

                    // Also request navigation immediately (simple + practical)
                    if (!string.IsNullOrWhiteSpace(row.TargetPageKey))
                        NavigateRequested?.Invoke(row.TargetPageKey);

                };

                listPanel.Controls.Add(card);
            }

            listPanel.ResumeLayout();
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ========================= Report Card =========================
        private class ReportCard : Guna2Panel
        {
            public event Action Clicked;

            private readonly ReportItemRow _row;

            public ReportCard(ReportItemRow row)
            {
                _row = row;

                BorderRadius = 16;
                BorderThickness = 1;
                BorderColor = Color.FromArgb(235, 235, 235);
                FillColor = Color.White;

                Height = 92;
                Margin = new Padding(0, 0, 0, 14);
                Padding = new Padding(18, 16, 18, 16);

                Build();
                WireClickRecursive(this);
            }

            private void Build()
            {
                // left icon circle
                var icon = new Panel
                {
                    Size = new Size(48, 48),
                    Location = new Point(18, 18),
                    BackColor = Color.FromArgb(245, 245, 245)
                };
                icon.Paint += (s, e) =>
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    using var gp = new System.Drawing.Drawing2D.GraphicsPath();
                    gp.AddEllipse(0, 0, icon.Width - 1, icon.Height - 1);
                    icon.Region = new Region(gp);

                    using var pen = new Pen(Color.FromArgb(220, 220, 220), 1);
                    e.Graphics.DrawEllipse(pen, 0, 0, icon.Width - 1, icon.Height - 1);
                };
                Controls.Add(icon);

                // Title
                var lblTitle = new Label
                {
                    AutoSize = true,
                    Text = _row.Title ?? "—",
                    Font = new Font("Segoe UI", 12.5f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(30, 30, 30),
                    Location = new Point(90, 18)
                };
                Controls.Add(lblTitle);

                // Subtitle
                var lblSub = new Label
                {
                    AutoSize = true,
                    Text = _row.SubTitle ?? "",
                    Font = new Font("Segoe UI", 10f),
                    ForeColor = Color.FromArgb(110, 110, 110),
                    Location = new Point(90, 46)
                };
                Controls.Add(lblSub);

                // Badge
                var badge = new Label
                {
                    AutoSize = true,
                    Text = string.IsNullOrWhiteSpace(_row.BadgeText) ? "ISSUE" : _row.BadgeText.ToUpper(),
                    Font = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                    ForeColor = _row.IsWarning ? Color.FromArgb(220, 60, 60) : Color.FromArgb(30, 110, 255),
                    BackColor = Color.FromArgb(245, 245, 245),
                    Padding = new Padding(10, 5, 10, 5)
                };
                Controls.Add(badge);

                // Chevron
                var chevron = new Guna2Button
                {
                    Size = new Size(34, 34),
                    BorderRadius = 10,
                    FillColor = Color.Transparent,
                    ForeColor = Color.FromArgb(80, 80, 80),
                    Text = "›",
                    Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    Cursor = Cursors.Hand
                };
                chevron.Click += (s, e) => Clicked?.Invoke();
                Controls.Add(chevron);

                // Layout positions on resize
                Resize += (s, e) =>
                {
                    chevron.Location = new Point(Width - 52, 27);
                    badge.Location = new Point(Width - 220, 32);
                };

                chevron.Location = new Point(Width - 52, 27);
                badge.Location = new Point(Width - 220, 32);
            }

            private void WireClickRecursive(Control root)
            {
                root.Cursor = Cursors.Hand;
                root.Click += (s, e) => Clicked?.Invoke();

                foreach (Control child in root.Controls)
                    WireClickRecursive(child);
            }
        }
    }
}
