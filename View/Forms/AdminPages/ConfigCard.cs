using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    public class ConfigCard : Guna2Panel
    {
        public int Total { get; }
        public int Economy { get; }
        public int Business { get; }
        public int First { get; }

        public event Action? Clicked;

        private readonly Color RowEco;
        private readonly Color RowBiz;
        private readonly Color RowFirst;

        private Label capRight;
        private Guna2CircleButton radio;

        private Guna2Panel rowEcoPanel;
        private Guna2Panel rowBizPanel;
        private Guna2Panel rowFirstPanel;

        public ConfigCard(string title, string desc, int total, int eco, int biz, int first,
                          Color rowEco, Color rowBiz, Color rowFirst)
        {
            Total = total;
            Economy = eco;
            Business = biz;
            First = first;

            RowEco = rowEco;
            RowBiz = rowBiz;
            RowFirst = rowFirst;

            BorderRadius = 12;
            BorderThickness = 1;
            BorderColor = Color.FromArgb(230, 230, 230);
            FillColor = Color.White;
            Padding = new Padding(14, 12, 14, 12);

            Cursor = Cursors.Hand; // make whole card clickable
            Click += (s, e) => Clicked?.Invoke();

            // Icon badge
            var iconBadge = new Guna2Panel
            {
                Size = new Size(28, 28),
                Location = new Point(12, 12),
                BorderRadius = 8,
                FillColor = Color.FromArgb(242, 247, 255)
            };
            Controls.Add(iconBadge);

            var icon = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                ForeColor = Color.FromArgb(47, 111, 237),
                Text = "✈"
            };
            iconBadge.Controls.Add(icon);

            // Title & description
            var lblTitle = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 20, 20),
                Text = title,
                Location = new Point(46, 12)
            };
            Controls.Add(lblTitle);

            var lblDesc = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9f),
                ForeColor = Color.FromArgb(120, 120, 120),
                Text = desc,
                Location = new Point(46, 34)
            };
            Controls.Add(lblDesc);

            // Radio button
            radio = new Guna2CircleButton
            {
                Size = new Size(16, 16),
                FillColor = Color.White,
                BorderColor = Color.FromArgb(170, 170, 170),
                BorderThickness = 2,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                ShadowDecoration = { Enabled = false },
                Text = ""
            };
            Controls.Add(radio);
            radio.Click += (s, e) => Clicked?.Invoke(); // clicking radio also triggers Clicked

            // Total capacity
            var lblCap = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9f),
                ForeColor = Color.FromArgb(90, 90, 90),
                Text = "Total Capacity",
                Location = new Point(12, 70)
            };
            Controls.Add(lblCap);

            capRight = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 20, 20),
                Text = $"{total} seats",
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            Controls.Add(capRight);

            // Seat distribution rows
            var lblDist = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9f),
                ForeColor = Color.FromArgb(90, 90, 90),
                Text = "Seat Distribution",
                Location = new Point(12, 94)
            };
            Controls.Add(lblDist);

            rowEcoPanel = MakeRow("Economy", eco, RowEco, 118);
            rowBizPanel = MakeRow("Business", biz, RowBiz, 146);
            rowFirstPanel = MakeRow("First Class", first, RowFirst, 174);

            Controls.Add(rowEcoPanel);
            Controls.Add(rowBizPanel);
            Controls.Add(rowFirstPanel);

            rowEcoPanel.Visible = eco > 0;
            rowBizPanel.Visible = biz > 0;
            rowFirstPanel.Visible = first > 0;

            SizeChanged += (_, __) => LayoutRightAndSize();
            LayoutRightAndSize();
        }

        private void LayoutRightAndSize()
        {
            radio.Location = new Point(Width - 14 - radio.Width, 18);
            capRight.Location = new Point(Width - 14 - capRight.Width, 70);

            int rowW = Width - 28;
            rowEcoPanel.Width = rowW;
            rowBizPanel.Width = rowW;
            rowFirstPanel.Width = rowW;

            int bottom = 118;
            if (rowFirstPanel.Visible) bottom = rowFirstPanel.Bottom;
            else if (rowBizPanel.Visible) bottom = rowBizPanel.Bottom;
            else if (rowEcoPanel.Visible) bottom = rowEcoPanel.Bottom;

            Height = bottom + 12;
        }

        private Guna2Panel MakeRow(string left, int value, Color bg, int y)
        {
            var p = new Guna2Panel
            {
                Height = 22,
                FillColor = bg,
                BorderRadius = 6,
                Location = new Point(12, y),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Padding = new Padding(8, 3, 8, 3)
            };

            var l = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9f),
                ForeColor = Color.FromArgb(25, 25, 25),
                Text = left,
                Location = new Point(8, 3)
            };

            var r = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 25, 25),
                Text = value.ToString(),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            p.Controls.Add(l);
            p.Controls.Add(r);

            p.SizeChanged += (_, __) => r.Location = new Point(p.Width - r.Width - 10, 3);
            r.Location = new Point(p.Width - r.Width - 10, 3);

            return p;
        }

        public void SetSelected(bool on, Color blue, Color border)
        {
            BorderColor = on ? blue : border;
            BorderThickness = on ? 2 : 1;

            if (on)
            {
                radio.FillColor = blue;
                radio.BorderThickness = 0;
            }
            else
            {
                radio.FillColor = Color.White;
                radio.BorderColor = Color.FromArgb(170, 170, 170);
                radio.BorderThickness = 2;
            }
        }
    }
}
