using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Controls
{
    public class AddPlaneDockedControl : UserControl
    {
        public event Action<string, string>? Confirmed;
        public event Action? Cancelled;

        private readonly Guna2Panel card;
        private readonly Guna2Button btnClose;
        private readonly Guna2Button btnConfirm;

        private readonly Guna2Button optA320;
        private readonly Guna2Button optHighLevel;
        private readonly Guna2Button optPrivateJet;

        private readonly Guna2ComboBox cmbStatus;

        private bool _dragging;
        private Point _dragStart;

        public string SelectedType { get; private set; } = "A320";
        public string SelectedStatus => cmbStatus.SelectedItem?.ToString() ?? "Available";

        public AddPlaneDockedControl()
        {
            // Transparent overlay background
            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(20, 0, 0, 0); // subtle dim
            DoubleBuffered = true;

            // Main card (no ugly border)
            card = new Guna2Panel
            {
                Size = new Size(520, 330),
                BorderRadius = 18,
                FillColor = Color.FromArgb(245, 246, 250),
                ShadowDecoration =
                {
                    Enabled = true,
                    BorderRadius = 18,
                    Depth = 18,
                    Shadow = new Padding(0, 10, 0, 10),
                }
            };
            Controls.Add(card);

            // Title
            var lblTitle = new Label
            {
                Text = "Add New Plane",
                AutoSize = true,
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 30),
                Location = new Point(22, 18)
            };
            card.Controls.Add(lblTitle);

            var lblSub = new Label
            {
                Text = "Select a plane model and status.",
                AutoSize = true,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Regular),
                ForeColor = Color.FromArgb(90, 90, 90),
                Location = new Point(24, 52)
            };
            card.Controls.Add(lblSub);

            // Close button (top-right)
            btnClose = new Guna2Button
            {
                Text = "✕",
                Size = new Size(36, 30),
                BorderRadius = 10,
                FillColor = Color.FromArgb(235, 235, 235),
                ForeColor = Color.FromArgb(80, 80, 80),
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Location = new Point(card.Width - 52, 16),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnClose.Click += (_, __) => Cancelled?.Invoke();
            card.Controls.Add(btnClose);

            // Options label
            var lblModels = new Label
            {
                Text = "Plane Model",
                AutoSize = true,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                Location = new Point(24, 88)
            };
            card.Controls.Add(lblModels);

            // 3 option buttons (Figma-like cards)
            optA320 = MakeOption("Mid Range", "Airbus A320", new Point(24, 116));
            optHighLevel = MakeOption("High Level", "Boeing 777-300ER", new Point(24, 180));
            optPrivateJet = MakeOption("VIP", "Gulfstream G650", new Point(24, 244));

            optA320.Click += (_, __) => Select("A320");
            optHighLevel.Click += (_, __) => Select("HighLevel");
            optPrivateJet.Click += (_, __) => Select("PrivateJet");

            card.Controls.Add(optA320);
            card.Controls.Add(optHighLevel);
            card.Controls.Add(optPrivateJet);

            // Status
            var lblStatus = new Label
            {
                Text = "Status",
                AutoSize = true,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                Location = new Point(310, 88)
            };
            card.Controls.Add(lblStatus);

            cmbStatus = new Guna2ComboBox
            {
                Location = new Point(310, 116),
                Size = new Size(185, 36),
                BorderRadius = 12,
                DrawMode = DrawMode.OwnerDrawFixed,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbStatus.Items.AddRange(new object[] { "Available", "Unavailable" });
            cmbStatus.SelectedIndex = 0;
            card.Controls.Add(cmbStatus);

            // Confirm button
            btnConfirm = new Guna2Button
            {
                Text = "Add Plane",
                Size = new Size(185, 42),
                BorderRadius = 14,
                FillColor = Color.FromArgb(35, 93, 220),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Location = new Point(310, 262)
            };
            btnConfirm.Click += (_, __) => Confirmed?.Invoke(SelectedType, SelectedStatus);
            card.Controls.Add(btnConfirm);

            // Make card movable (drag anywhere on the card top area)
            card.MouseDown += StartDrag;
            card.MouseMove += DoDrag;
            card.MouseUp += EndDrag;

            lblTitle.MouseDown += StartDrag;
            lblTitle.MouseMove += DoDrag;
            lblTitle.MouseUp += EndDrag;

            lblSub.MouseDown += StartDrag;
            lblSub.MouseMove += DoDrag;
            lblSub.MouseUp += EndDrag;

            // default selection
            Select("A320");

            // center initially
            Resize += (_, __) => CenterCard();
            CenterCard();

            // clicking outside closes (optional; comment out if you don’t want)
            this.MouseDown += (s, e) =>
            {
                if (!card.Bounds.Contains(e.Location))
                    Cancelled?.Invoke();
            };
        }

        private Guna2Button MakeOption(string tag, string title, Point p)
        {
            var b = new Guna2Button
            {
                Text = $"{title}\n{tag}",
                TextAlign = HorizontalAlignment.Left,
                Size = new Size(270, 54),
                Location = p,
                BorderRadius = 14,
                FillColor = Color.White,
                ForeColor = Color.FromArgb(45, 45, 45),
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                CustomBorderThickness = new Padding(1),
                CustomBorderColor = Color.FromArgb(220, 220, 220),
                HoverState = { FillColor = Color.FromArgb(248, 249, 252) }
            };
            b.Padding = new Padding(12, 6, 12, 6);
            return b;
        }

        private void Select(string type)
        {
            SelectedType = type;

            SetOptStyle(optA320, type == "A320");
            SetOptStyle(optHighLevel, type == "HighLevel");
            SetOptStyle(optPrivateJet, type == "PrivateJet");
        }

        private void SetOptStyle(Guna2Button btn, bool selected)
        {
            if (selected)
            {
                btn.FillColor = Color.FromArgb(235, 242, 255);
                btn.CustomBorderColor = Color.FromArgb(35, 93, 220);
                btn.CustomBorderThickness = new Padding(2);
            }
            else
            {
                btn.FillColor = Color.White;
                btn.CustomBorderColor = Color.FromArgb(220, 220, 220);
                btn.CustomBorderThickness = new Padding(1);
            }
        }

        private void CenterCard()
        {
            if (card == null) return;
            card.Left = (Width - card.Width) / 2;
            card.Top = (Height - card.Height) / 2;
        }

        private void StartDrag(object? s, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            _dragging = true;
            _dragStart = e.Location;
        }

        private void DoDrag(object? s, MouseEventArgs e)
        {
            if (!_dragging) return;
            var dx = e.X - _dragStart.X;
            var dy = e.Y - _dragStart.Y;
            card.Left += dx;
            card.Top += dy;
        }

        private void EndDrag(object? s, MouseEventArgs e) => _dragging = false;
    }
}
