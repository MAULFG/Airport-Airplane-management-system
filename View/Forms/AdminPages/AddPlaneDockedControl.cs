using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    public class AddPlaneDockedControl : UserControl
    {
        // (planeName, type, status)
        public event Action<string, string, string, int, int, int, int>? Confirmed;
        public event Action? Cancelled;

        private readonly Color Border = Color.FromArgb(230, 230, 230);
        private readonly Color Blue = Color.FromArgb(47, 111, 237);

        private readonly Color RowEco = Color.FromArgb(234, 243, 255);
        private readonly Color RowBiz = Color.FromArgb(244, 238, 255);
        private readonly Color RowFirst = Color.FromArgb(252, 246, 230);

        private Guna2Panel root;
        private Label lblTitle;
        private Guna2Button btnClose;

        private Label lblPlaneName;
        private Guna2TextBox txtPlaneName;

        private Label lblSection;
        private Panel line;

        private Guna2Panel scrollHost;
        private Panel listPanel;

        // 3 planes only
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

        private string _selectedType = "HighLevel";
        private int _total, _eco, _biz, _first;

        public AddPlaneDockedControl()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            BuildUI();
            SelectCard(cardB777);
        }

        private void BuildUI()
        {
            SuspendLayout();

            root = new Guna2Panel
            {
                BackColor = Color.White,
                BorderColor = Border,
                BorderRadius = 14,
                BorderThickness = 1,
                Dock = DockStyle.Fill,
                Padding = new Padding(18, 14, 18, 14),
                ShadowDecoration =
                {
                    Enabled = true,
                    Depth = 8,
                    Shadow = new Padding(0, 3, 0, 6),
                    BorderRadius = 14
                }
            };
            Controls.Add(root);

            lblTitle = new Label
            {
                AutoSize = true,
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                Text = "Add New Plane Plan",
                Location = new Point(2, 8)
            };
            root.Controls.Add(lblTitle);

            btnClose = new Guna2Button
            {
                Text = "×",
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                ForeColor = Color.FromArgb(80, 80, 80),
                Size = new Size(34, 30),
                BorderRadius = 8,
                FillColor = Color.FromArgb(245, 245, 245),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnClose.Click += (_, __) => Cancelled?.Invoke();
            root.Controls.Add(btnClose);

            // Plane name
            lblPlaneName = new Label
            {
                AutoSize = true,
                ForeColor = Color.Black,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Text = "Plane Name"
            };
            root.Controls.Add(lblPlaneName);

            txtPlaneName = new Guna2TextBox
            {
                BorderRadius = 10,
                BorderColor = Border,
                PlaceholderText = "e.g., Boeing 777, Airbus A320",
                Font = new Font("Segoe UI", 9.5f),
                Height = 36,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                FillColor = Color.White
            };
            root.Controls.Add(txtPlaneName);

            // Divider
            line = new Panel
            {
                Height = 1,
                BackColor = Border,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            root.Controls.Add(line);

            lblSection = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                ForeColor = Color.Black,
                Text = "Plane Configuration Type"
            };
            root.Controls.Add(lblSection);

            // Scroll list container (✅ no padding so first card can go higher)
            scrollHost = new Guna2Panel
            {
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Padding = new Padding(0) // ✅ was 2
            };
            root.Controls.Add(scrollHost);

            listPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.Transparent
            };
            scrollHost.Controls.Add(listPanel);

            // 1) Boeing 777 High: total 316, eco 252, biz 48, first 16
            cardB777 = new ConfigCard(
                title: "Boeing 777 (High)",
                desc: "4×4 First, 8×6 Business, 28×9 Economy",
                total: 316, eco: 252, biz: 48, first: 16,
                rowEco: RowEco, rowBiz: RowBiz, rowFirst: RowFirst);

            // 2) A320 Med: total 170, eco 138, biz 32, first 0
            cardA320 = new ConfigCard(
                title: "Airbus A320 (Med)",
                desc: "8×4 Business, 23×6 Economy",
                total: 170, eco: 138, biz: 32, first: 0,
                rowEco: RowEco, rowBiz: RowBiz, rowFirst: RowFirst);

            // 3) Private Jet: total 7, first 7 (VIP)
            cardJet = new ConfigCard(
                title: "Private Jet",
                desc: "VIP cabin seating",
                total: 7, eco: 0, biz: 0, first: 7,
                rowEco: RowEco, rowBiz: RowBiz, rowFirst: RowFirst);

            foreach (var c in new[] { cardB777, cardA320, cardJet })
            {
                c.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                c.Width = 10;
                c.Clicked += () => SelectCard(c);
                listPanel.Controls.Add(c);
            }

            // Summary
            summaryBox = new Guna2Panel
            {
                BackColor = Color.White,
                BorderColor = Border,
                BorderRadius = 12,
                BorderThickness = 1,
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Padding = new Padding(14, 10, 14, 10)
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

            lblTotalSeats = MakeSumRow(28);
            lblEcoSeats = MakeSumRow(48);
            lblBizSeats = MakeSumRow(68);
            lblFirstSeats = MakeSumRow(88);

            summaryBox.Controls.Add(lblTotalSeats);
            summaryBox.Controls.Add(lblEcoSeats);
            summaryBox.Controls.Add(lblBizSeats);
            summaryBox.Controls.Add(lblFirstSeats);

            // Footer buttons
            btnCancel = new Guna2Button
            {
                Text = "Cancel",
                BorderRadius = 12,
                FillColor = Color.White,
                ForeColor = Color.Black,
                BorderColor = Border,
                BorderThickness = 1,
                Size = new Size(120, 40),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            btnCancel.Click += (_, __) => Cancelled?.Invoke();
            root.Controls.Add(btnCancel);

            btnAdd = new Guna2Button
            {
                Text = "Add Plane",
                BorderRadius = 12,
                FillColor = Blue,
                ForeColor = Color.White,
                Size = new Size(140, 40),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right
            };
            btnAdd.Click += (_, __) =>
            {
                var planeName = txtPlaneName.Text.Trim();
                Confirmed?.Invoke(planeName, _selectedType, "Active",
                  _total, _eco, _biz, _first);
                // fixed status
            };
            root.Controls.Add(btnAdd);

            root.Resize += (_, __) =>
            {
                Relayout();
                ReflowCards();
            };

            scrollHost.Resize += (_, __) =>
            {
                foreach (Control ctrl in listPanel.Controls)
                    if (ctrl is ConfigCard cc)
                        cc.Width = scrollHost.Width - 18;

                ReflowCards();
            };

            Relayout();
            ReflowCards();

            ResumeLayout();
        }

        private void Relayout()
        {
            btnClose.Location = new Point(root.Width - root.Padding.Right - btnClose.Width, 6);

            lblPlaneName.Location = new Point(2, 54);

            txtPlaneName.Location = new Point(2, 76);
            txtPlaneName.Width = root.Width - root.Padding.Left - root.Padding.Right;

            int dividerY = 142;
            line.Location = new Point(0, dividerY);
            line.Width = root.Width;

            lblSection.Location = new Point(2, dividerY + 12);

            int scrollTop = dividerY + 40;
            int footerH = 46;
            int summaryH = 108;
            int summaryGap = 12;
            int bottomPad = root.Padding.Bottom;

            summaryBox.Size = new Size(root.Width - 4, summaryH);
            summaryBox.Location = new Point(2, root.Height - bottomPad - footerH - summaryGap - summaryH);

            btnCancel.Location = new Point(2, root.Height - bottomPad - footerH);
            btnAdd.Location = new Point(root.Width - 2 - btnAdd.Width, root.Height - bottomPad - footerH);

            int scrollBottom = summaryBox.Top - 10;
            scrollHost.Location = new Point(0, scrollTop);
            scrollHost.Size = new Size(root.Width, Math.Max(80, scrollBottom - scrollTop));
        }

        private void ReflowCards()
        {
            int y = 0; // ✅ was 6 (shifts first card up)

            foreach (var c in new[] { cardB777, cardA320, cardJet })
            {
                c.Width = scrollHost.Width - 18; // ✅ tighter
                c.Location = new Point(0, y);    // ✅ start at top-left
                y += c.Height + 12;
            }

            // ✅ refresh scrollbars (hide when not needed)
            listPanel.AutoScroll = false;
            listPanel.AutoScroll = true;
        }

        private Label MakeSumRow(int y)
        {
            return new Label
            {
                AutoSize = false,
                Height = 18,
                Location = new Point(2, y),
                Font = new Font("Segoe UI", 9.5f),
                ForeColor = Color.FromArgb(70, 70, 70),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                Width = 10
            };
        }

        private void SelectCard(ConfigCard card)
        {
            cardB777.SetSelected(false, Blue, Border);
            cardA320.SetSelected(false, Blue, Border);
            cardJet.SetSelected(false, Blue, Border);

            card.SetSelected(true, Blue, Border);

            if (card == cardB777) _selectedType = "HighLevel";
            else if (card == cardA320) _selectedType = "A320";
            else _selectedType = "PrivateJet";

            _total = card.Total;
            _eco = card.Economy;
            _biz = card.Business;
            _first = card.First;

            UpdateSummary();
        }

        private void UpdateSummary()
        {
            lblTotalSeats.Text = $"Total seats:  {_total}";
            lblEcoSeats.Text = $"Economy seats:  {_eco}";
            lblBizSeats.Text = $"Business seats:  {_biz}";
            lblFirstSeats.Text = $"First class seats:  {_first}";
        }

        // ==========================
        // ConfigCard (Figma style)
        // ==========================
        private class ConfigCard : Guna2Panel
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

                void Hook(Control c)
                {
                    c.Cursor = Cursors.Hand;
                    c.Click += (_, __) => Clicked?.Invoke();
                    foreach (Control child in c.Controls) Hook(child);
                }
                Hook(this);

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
}
