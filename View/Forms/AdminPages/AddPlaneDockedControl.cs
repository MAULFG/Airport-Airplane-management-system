using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    public class AddPlaneDockedControl : UserControl
    {
        public event Action<string, string>? Confirmed;
        public event Action? Cancelled;

        // ---- Config model (Figma numbers) ----
        private sealed class Config
        {
            public string Key = "";      // what Presenter expects: "A320", "HighLevel", "PrivateJet"
            public string Title = "";
            public string Subtitle = "";
            public int Economy;
            public int Business;
            public int First;
            public int Total => Economy + Business + First;
        }

        // You can tune these to match your real plane models later.
        // For now I’m matching the Figma example:
        // Economy: 162/27/0 => 189
        // Business: 120/24/6 => 150
        // First: 80/40/16 => 136
        private readonly Config[] _configs =
        {
            new Config{ Key="A320",       Title="Economy Configuration",  Subtitle="High-capacity, economy-focused seating", Economy=162, Business=27, First=0 },
            new Config{ Key="HighLevel",  Title="Business Configuration", Subtitle="Balanced mix of business and economy",     Economy=120, Business=24, First=6 },
            new Config{ Key="PrivateJet", Title="First Class Configuration", Subtitle="Premium seating with first-class cabins", Economy=80, Business=40, First=16 },
        };

        // ---- UI ----
        private readonly Panel dim;
        private readonly Guna2Panel sheet;
        private readonly Panel scrollHost;
        private readonly Guna2TextBox txtPlaneName;
        private readonly Guna2RadioButton[] radios;
        private readonly Guna2Panel[] cards;
        private readonly Label lblSumTotal;
        private readonly Label lblSumEco;
        private readonly Label lblSumBus;
        private readonly Label lblSumFirst;

        private readonly Guna2Button btnCancel;
        private readonly Guna2Button btnAdd;
        private readonly Guna2Button btnClose;

        private readonly string _defaultStatus = "Available"; // map to your domain later if you want

        public AddPlaneDockedControl()
        {
            // Full-screen overlay (dim background)
            Dock = DockStyle.Fill;
            BackColor = Color.Transparent;

            dim = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(80, 0, 0, 0) // dim
            };
            Controls.Add(dim);

            // Right sheet
            sheet = new Guna2Panel
            {
                Dock = DockStyle.Right,
                Width = 440,
                FillColor = Color.White,
                Padding = new Padding(18, 16, 18, 14),
                ShadowDecoration =
                {
                    Enabled = true,
                    Depth = 25,
                    Shadow = new Padding(0, 0, 10, 0),
                }
            };
            dim.Controls.Add(sheet);

            // Clicking dim outside closes (Figma-like)
            dim.MouseDown += (_, e) =>
            {
                // if click not inside sheet
                if (!sheet.Bounds.Contains(e.Location))
                    Cancelled?.Invoke();
            };

            // Header row
            var hdr = new Panel { Dock = DockStyle.Top, Height = 42, BackColor = Color.Transparent };
            sheet.Controls.Add(hdr);

            var lblTitle = new Label
            {
                Text = "Add New Plane Plan",
                AutoSize = true,
                Font = new Font("Segoe UI", 12.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 25, 25),
                Location = new Point(0, 6)
            };
            hdr.Controls.Add(lblTitle);

            btnClose = new Guna2Button
            {
                Text = "✕",
                Size = new Size(34, 30),
                BorderRadius = 8,
                FillColor = Color.FromArgb(240, 240, 240),
                ForeColor = Color.FromArgb(80, 80, 80),
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(hdr.Width - 34, 4)
            };
            btnClose.Click += (_, __) => Cancelled?.Invoke();
            hdr.Controls.Add(btnClose);

            hdr.SizeChanged += (_, __) => btnClose.Location = new Point(hdr.Width - btnClose.Width, 4);

            // Subtitle
            var lblSub = new Label
            {
                Text = "Configure a new plane with your preferred seating arrangement",
                AutoSize = false,
                Height = 34,
                Dock = DockStyle.Top,
                Font = new Font("Segoe UI", 9.2f),
                ForeColor = Color.FromArgb(110, 110, 110),
                Padding = new Padding(0, 0, 0, 8)
            };
            sheet.Controls.Add(lblSub);

            // Scroll area
            scrollHost = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.Transparent
            };
            sheet.Controls.Add(scrollHost);

            // Bottom buttons (fixed)
            var bottom = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.White };
            sheet.Controls.Add(bottom);

            btnCancel = new Guna2Button
            {
                Text = "Cancel",
                Size = new Size(180, 40),
                BorderRadius = 10,
                FillColor = Color.White,
                ForeColor = Color.FromArgb(60, 60, 60),
                BorderColor = Color.FromArgb(220, 220, 220),
                BorderThickness = 1,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Location = new Point(0, 10)
            };
            btnCancel.Click += (_, __) => Cancelled?.Invoke();
            bottom.Controls.Add(btnCancel);

            btnAdd = new Guna2Button
            {
                Text = "Add Plan",
                Size = new Size(180, 40),
                BorderRadius = 10,
                FillColor = Color.FromArgb(45, 93, 220),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(bottom.Width - 180, 10)
            };
            btnAdd.Click += (_, __) =>
            {
                var cfg = GetSelectedConfig();
                // send the Key (A320 / HighLevel / PrivateJet) because Presenter switch depends on that
                Confirmed?.Invoke(cfg.Key, _defaultStatus);
            };
            bottom.Controls.Add(btnAdd);

            bottom.SizeChanged += (_, __) =>
            {
                btnAdd.Location = new Point(bottom.Width - btnAdd.Width, 10);
            };

            // ---- Content inside scrollHost ----
            int y = 8;

            // Plane Name label + textbox
            scrollHost.Controls.Add(MakeLabel("Plane Name", y));
            y += 22;

            txtPlaneName = new Guna2TextBox
            {
                PlaceholderText = "e.g., Boeing 737, Airbus A320",
                BorderRadius = 10,
                BorderColor = Color.FromArgb(220, 220, 220),
                FillColor = Color.White,
                Font = new Font("Segoe UI", 9.5f),
                Size = new Size(sheet.Width - 36 - 20, 36),
                Location = new Point(0, y),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            scrollHost.Controls.Add(txtPlaneName);
            y += 50;

            // Section title
            scrollHost.Controls.Add(MakeLabel("Plane Configuration Type", y));
            y += 26;

            radios = new Guna2RadioButton[_configs.Length];
            cards = new Guna2Panel[_configs.Length];

            for (int i = 0; i < _configs.Length; i++)
            {
                var cfg = _configs[i];

                var card = new Guna2Panel
                {
                    BorderRadius = 12,
                    FillColor = Color.White,
                    BorderThickness = 1,
                    BorderColor = Color.FromArgb(230, 230, 230),
                    Size = new Size(sheet.Width - 36 - 20, 170),
                    Location = new Point(0, y),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };
                scrollHost.Controls.Add(card);
                cards[i] = card;

                // top row: icon placeholder + titles + radio
                var top = new Panel { Location = new Point(12, 10), Size = new Size(card.Width - 24, 44), Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right };
                card.Controls.Add(top);

                var icon = new Panel
                {
                    BackColor = Color.FromArgb(245, 247, 252),
                    Size = new Size(34, 34),
                    Location = new Point(0, 4)
                };
                icon.Paint += (_, e) =>
                {
                    // simple "X" like figma icon placeholder
                    using var p = new Pen(Color.FromArgb(160, 160, 160), 2);
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    e.Graphics.DrawLine(p, 10, 10, 24, 24);
                    e.Graphics.DrawLine(p, 24, 10, 10, 24);
                };
                top.Controls.Add(icon);

                var t1 = new Label
                {
                    Text = cfg.Title,
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9.8f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(30, 30, 30),
                    Location = new Point(44, 2)
                };
                top.Controls.Add(t1);

                var t2 = new Label
                {
                    Text = cfg.Subtitle,
                    AutoSize = true,
                    Font = new Font("Segoe UI", 8.6f),
                    ForeColor = Color.FromArgb(120, 120, 120),
                    Location = new Point(44, 22)
                };
                top.Controls.Add(t2);

                var rb = new Guna2RadioButton
                {
                    Checked = i == 0,
                    Location = new Point(top.Width - 18, 14),
                    Anchor = AnchorStyles.Top | AnchorStyles.Right
                };
                radios[i] = rb;
                top.Controls.Add(rb);

                top.SizeChanged += (_, __) => rb.Location = new Point(top.Width - 18, 14);

                // total capacity row
                var capLeft = new Label
                {
                    Text = "Total Capacity",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9f),
                    ForeColor = Color.FromArgb(90, 90, 90),
                    Location = new Point(12, 60)
                };
                card.Controls.Add(capLeft);

                var capRight = new Label
                {
                    Text = $"{cfg.Total} seats",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(30, 30, 30),
                    Location = new Point(card.Width - 120, 60),
                    Anchor = AnchorStyles.Top | AnchorStyles.Right
                };
                card.Controls.Add(capRight);

                // Seat distribution label
                var distLbl = new Label
                {
                    Text = "Seat Distribution",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9f),
                    ForeColor = Color.FromArgb(90, 90, 90),
                    Location = new Point(12, 86)
                };
                card.Controls.Add(distLbl);

                // distribution chips like Figma lines
                var lineEco = DistLine("Economy", cfg.Economy.ToString(), Color.FromArgb(238, 246, 255), y: 108);
                var lineBus = DistLine("Business", cfg.Business.ToString(), Color.FromArgb(246, 240, 255), y: 134);
                var lineFirst = DistLine("First Class", cfg.First.ToString(), Color.FromArgb(255, 248, 235), y: 160);

                card.Controls.Add(lineEco);
                card.Controls.Add(lineBus);
                card.Controls.Add(lineFirst);

                // Click anywhere selects
                void SelectThis()
                {
                    for (int k = 0; k < radios.Length; k++)
                        radios[k].Checked = (k == i);
                    ApplySelectedStyles();
                    UpdateSummary();
                }

                rb.CheckedChanged += (_, __) =>
                {
                    if (rb.Checked)
                    {
                        ApplySelectedStyles();
                        UpdateSummary();
                    }
                };

                card.Click += (_, __) => SelectThis();
                foreach (Control cc in card.Controls)
                    cc.Click += (_, __) => SelectThis();

                y += 182;
            }

            // Summary card
            var sumCard = new Guna2Panel
            {
                BorderRadius = 12,
                FillColor = Color.White,
                BorderThickness = 1,
                BorderColor = Color.FromArgb(230, 230, 230),
                Size = new Size(sheet.Width - 36 - 20, 110),
                Location = new Point(0, y),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            scrollHost.Controls.Add(sumCard);

            var sumTitle = new Label
            {
                Text = "Configuration Summary",
                AutoSize = true,
                Font = new Font("Segoe UI", 9.8f, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 30),
                Location = new Point(12, 12)
            };
            sumCard.Controls.Add(sumTitle);

            lblSumTotal = SumLine(sumCard, "Total seats:", "0", 38);
            lblSumEco = SumLine(sumCard, "Economy seats:", "0", 58);
            lblSumBus = SumLine(sumCard, "Business seats:", "0", 78);
            lblSumFirst = SumLine(sumCard, "First class seats:", "0", 98);

            y += 130;

            scrollHost.Height = sheet.Height - bottom.Height - hdr.Height - lblSub.Height - 18;

            ApplySelectedStyles();
            UpdateSummary();
        }

        private Label MakeLabel(string text, int y)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Font = new Font("Segoe UI", 9.2f, FontStyle.Bold),
                ForeColor = Color.FromArgb(45, 45, 45),
                Location = new Point(0, y)
            };
        }

        private Panel DistLine(string left, string right, Color back, int y)
        {
            var p = new Panel
            {
                BackColor = back,
                Location = new Point(12, y),
                Size = new Size(sheet.Width - 36 - 44, 22),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            var l = new Label
            {
                Text = left,
                AutoSize = true,
                Font = new Font("Segoe UI", 8.7f),
                ForeColor = Color.FromArgb(40, 40, 40),
                Location = new Point(8, 3)
            };
            p.Controls.Add(l);

            var r = new Label
            {
                Text = right,
                AutoSize = true,
                Font = new Font("Segoe UI", 8.7f, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 40, 40),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(p.Width - 20, 3)
            };
            p.Controls.Add(r);

            p.SizeChanged += (_, __) => r.Location = new Point(p.Width - r.Width - 8, 3);

            return p;
        }

        private Label SumLine(Control parent, string key, string val, int y)
        {
            var k = new Label
            {
                Text = key,
                AutoSize = true,
                Font = new Font("Segoe UI", 8.8f),
                ForeColor = Color.FromArgb(90, 90, 90),
                Location = new Point(12, y)
            };
            parent.Controls.Add(k);

            var v = new Label
            {
                Text = val,
                AutoSize = true,
                Font = new Font("Segoe UI", 8.8f, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 30),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(parent.Width - 40, y)
            };
            parent.Controls.Add(v);

            parent.SizeChanged += (_, __) => v.Location = new Point(parent.Width - v.Width - 12, y);

            return v;
        }

        private Config GetSelectedConfig()
        {
            for (int i = 0; i < radios.Length; i++)
                if (radios[i].Checked) return _configs[i];
            return _configs[0];
        }

        private void UpdateSummary()
        {
            var c = GetSelectedConfig();
            lblSumTotal.Text = c.Total.ToString();
            lblSumEco.Text = c.Economy.ToString();
            lblSumBus.Text = c.Business.ToString();
            lblSumFirst.Text = c.First.ToString();
        }

        private void ApplySelectedStyles()
        {
            for (int i = 0; i < cards.Length; i++)
            {
                bool sel = radios[i].Checked;
                cards[i].BorderColor = sel ? Color.FromArgb(45, 93, 220) : Color.FromArgb(230, 230, 230);
                cards[i].BorderThickness = sel ? 2 : 1;
            }
        }
    }
}
