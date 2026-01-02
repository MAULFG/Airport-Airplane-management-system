using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    partial class AddPlaneForm
    {
        private Guna2Panel root;
        private Label lblTitle;
        private Guna2Button btnClose;

        private Label lblPlaneName;
        private Guna2TextBox txtPlaneName;

        private Label lblSection;
        private Panel line;

        private Guna2Panel scrollHost;
        private Panel listPanel;

        private ConfigCard cardB777;
        private ConfigCard cardA320;
        private ConfigCard cardJet;

        private Guna2Panel summaryBox;
        private Label lblSumTitle;
        private Label lblTotalSeats;
        private Label lblEcoSeats;
        private Label lblBizSeats;
        private Label lblFirstSeats;

        private Guna2Button btnCancel;
        private Guna2Button btnAdd;

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // --- Root panel ---
            root = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(18, 14, 18, 14),
                BackColor = Color.White,
                BorderRadius = 14,
                BorderThickness = 1,
                BorderColor = Color.FromArgb(230, 230, 230)
            };
            Controls.Add(root);

            // --- Title ---
            lblTitle = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                Text = "Add New Plane Plan",
                Location = new Point(10, 18)
            };
            root.Controls.Add(lblTitle);

            // --- Close button ---
            btnClose = new Guna2Button
            {
                Text = "×",
                Size = new Size(34, 30),
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                FillColor = Color.FromArgb(245, 245, 245),
                BorderRadius = 8,
                ForeColor = Color.FromArgb(80, 80, 80),
                Location = new Point(root.Width - 52, 14), // 18 padding + 34 width
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            root.Controls.Add(btnClose);

            // --- Plane Name ---
            lblPlaneName = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Text = "Plane Name",
                Location = new Point(10, 60)
            };
            root.Controls.Add(lblPlaneName);

            txtPlaneName = new Guna2TextBox
            {
                Height = 36,
                Width = root.Width - 56, // root padding + 10+10 margin
                Location = new Point(10, 82),
                BorderRadius = 10,
                BorderColor = Color.FromArgb(230, 230, 230),
                PlaceholderText = "e.g., Boeing 777, Airbus A320",
                Font = new Font("Segoe UI", 9.5f),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            root.Controls.Add(txtPlaneName);

            // --- Divider line ---
            line = new Panel
            {
                Height = 1,
                BackColor = Color.FromArgb(230, 230, 230),
                Location = new Point(10, txtPlaneName.Bottom + 10),
                Width = root.Width - 20
            };
            root.Controls.Add(line);

            // --- Section label ---
            lblSection = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                Text = "Plane Configuration Type",
                Location = new Point(10, line.Bottom + 12)
            };
            root.Controls.Add(lblSection);

            // --- Scroll host ---
            scrollHost = new Guna2Panel
            {
                BackColor = Color.Transparent,
                Location = new Point(10, lblSection.Bottom + 8),
                Width = root.Width - 36, // 18 padding left/right
                Height = 250, // enough for 3 cards
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            root.Controls.Add(scrollHost);

            listPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };
            scrollHost.Controls.Add(listPanel);

            // --- Config Cards ---
            cardB777 = new ConfigCard("Boeing 777 (High)", "4×4 First, 8×6 Business, 28×9 Economy", 316, 252, 48, 16, RowEco, RowBiz, RowFirst);
            cardA320 = new ConfigCard("Airbus A320 (Med)", "8×4 Business, 23×6 Economy", 170, 138, 32, 0, RowEco, RowBiz, RowFirst);
            cardJet = new ConfigCard("Private Jet", "VIP cabin seating", 7, 0, 0, 7, RowEco, RowBiz, RowFirst);

            foreach (var c in new[] { cardB777, cardA320, cardJet })
            {
                listPanel.Controls.Add(c);
                c.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            }

            // --- Summary Box ---
            summaryBox = new Guna2Panel
            {
                Height = 120,
                Width = root.Width - 36,
                Location = new Point(2, 789 - 14 - 46 - 12 - 120), // formHeight - padding - footer - gap - summary
                BackColor = Color.White,
                BorderRadius = 12,
                BorderThickness = 1,
                BorderColor = Color.FromArgb(230, 230, 230),
                Padding = new Padding(5, 10, 5, 10),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            root.Controls.Add(summaryBox);

            lblSumTitle = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                Text = "Configuration Summary",
                Location = new Point(2, 2)
            };
            summaryBox.Controls.Add(lblSumTitle);

            lblTotalSeats = new Label { AutoSize = true ,Height = 18, Location = new Point(2, 10) };
            lblEcoSeats = new Label { AutoSize = true, Height = 18, Location = new Point(2, 30) };
            lblBizSeats = new Label { AutoSize = true, Height = 18, Location = new Point(2, 50) };
            lblFirstSeats = new Label { AutoSize = true, Height = 18, Location = new Point(2, 70) };
            summaryBox.Controls.AddRange(new Control[] { lblTotalSeats, lblEcoSeats, lblBizSeats, lblFirstSeats });
            foreach (var lbl in new[] { lblTotalSeats, lblEcoSeats, lblBizSeats, lblFirstSeats })
            {
                lbl.AutoSize = true;
                lbl.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
                lbl.ForeColor = Color.FromArgb(60, 60, 60);
            }

            // --- Footer buttons ---
            btnCancel = new Guna2Button
            {
                Text = "Cancel",
                Size = new Size(120, 40),
                Location = new Point(14, 789 - 14 - 46), // bottom padding + footer height
                BorderRadius = 12,
                FillColor = Color.DarkRed,
                ForeColor =Color.White,
                BorderColor = Color.DarkRed,
                BorderThickness = 1,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };

            root.Controls.Add(btnCancel);

            btnAdd = new Guna2Button
            {
                Text = "Add Plane",
                Size = new Size(140, 40),
                Location = new Point(1016 - 140 - 14, 789 - 14 - 46),
                BorderRadius = 12,
                FillColor = Blue,
                ForeColor = Color.White,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            root.Controls.Add(btnAdd);

            // --- Initial Reflow ---
            ReflowCards();

            this.ClientSize = new Size(1016, 789);
            this.ResumeLayout(false);
        }

    }
}
