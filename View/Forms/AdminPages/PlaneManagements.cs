using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    public partial class PlaneManagements : UserControl, IPlaneManagementView
    {
        public event EventHandler ViewLoaded;
        public event EventHandler AddPlaneClicked;
        public event Action<int> DeleteRequested;
        public event Action<int>? PlaneSelected;

        private AddPlaneDockedControl? _addPlanePanel;
        private List<Plane> _planes = new();

        private string _pendingPlaneName = "";
        private string _pendingPlaneType = "";
        private string _pendingPlaneStatus = "";
        private int _pendingTotal, _pendingEco, _pendingBiz, _pendingFirst;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int? HighlightedPlaneId { get; private set; }

        public PlaneManagements()
        {
            InitializeComponent();

            Load += (_, __) => ViewLoaded?.Invoke(this, EventArgs.Empty);

            btnAddPlane.Click += (_, __) => ShowAddPlaneOverlay();

            Resize += (_, __) =>
            {
                LayoutContentAreas();
                RefreshCardsWidth();
            };
        }

        // =======================
        // IPlaneManagementView
        // =======================
        public void SetPlanes(List<Plane> planes)
        {
            _planes = planes ?? new();
            RenderCards();
        }

        public void ShowInfo(string message) =>
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public void ShowError(string message) =>
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public bool Confirm(string message) =>
            MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;

        public void HighlightPlane(int planeId)
        {
            HighlightedPlaneId = planeId;
            RenderCards();
        }

        public bool TryGetNewPlaneInput(out string model, out string type, out string status,
                                       out int total, out int eco, out int biz, out int first)
        {
            model = _pendingPlaneName;
            type = _pendingPlaneType;
            status = _pendingPlaneStatus;

            total = _pendingTotal;
            eco = _pendingEco;
            biz = _pendingBiz;
            first = _pendingFirst;

            if (string.IsNullOrWhiteSpace(model))
            {
                ShowError("Please enter a plane name.");
                return false;
            }

            return true;
        }


        // =======================
        // Rendering
        // =======================
        private void RenderCards()
        {
            flow.SuspendLayout();
            flow.Controls.Clear();

            foreach (var p in _planes.OrderBy(p => p.PlaneID))
                flow.Controls.Add(CreatePlaneCard(p));

            flow.ResumeLayout();

            LayoutContentAreas();
            RefreshCardsWidth();
        }

        private void RefreshCardsWidth()
        {
            if (flow == null) return;

            int w = Math.Max(200, flow.ClientSize.Width - SystemInformation.VerticalScrollBarWidth - 8);
            foreach (Control c in flow.Controls)
                c.Width = w;
        }

        private Control CreatePlaneCard(Plane p)
        {
            if (p.Seats == null || p.Seats.Count == 0)
                p.GenerateSeats();

            int total = p.Seats.Count;
            int first = p.Seats.Count(s => Eq(s.ClassType, "First"));
            int business = p.Seats.Count(s => Eq(s.ClassType, "Business"));
            int economy = p.Seats.Count(s => Eq(s.ClassType, "Economy"));
            int vip = p.Seats.Count(s => Eq(s.ClassType, "VIP"));

            string modelText = p.Model ?? p.GetType().Name;
            string typeText = p.GetType().Name;
            string statusText = NormalizePlaneStatus(p.Status);

            var card = new Guna2ShadowPanel
            {
                BackColor = Color.Transparent,
                FillColor = Color.White,
                Radius = 12,
                Padding = new Padding(16, 14, 16, 14),
                Margin = new Padding(0, 0, 0, 12),
                Height = 150
            };

            if (HighlightedPlaneId.HasValue && HighlightedPlaneId.Value == p.PlaneID)
            {
                card.ShadowColor = Color.FromArgb(35, 93, 220);
                card.ShadowDepth = 18;
            }

            var title = new Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Text = $"Plane #{p.PlaneID}",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 30),
                AutoSize = true,
                Location = new Point(16, 14)
            };
            card.Controls.Add(title);

            var modelBadge = Badge(modelText, Color.FromArgb(222, 235, 255), Color.FromArgb(35, 93, 220));
            modelBadge.Location = new Point(title.Right + 14, 12);
            card.Controls.Add(modelBadge);

            var (stBack, stFore) = GetPlaneStatusColors(statusText);
            var stBadge = Badge(statusText, stBack, stFore);
            stBadge.Location = new Point(modelBadge.Right + 10, 12);
            card.Controls.Add(stBadge);

            var btnSchedule = MakeTopButton("See Schedule");
            btnSchedule.Click += (_, __) => PlaneSelected?.Invoke(p.PlaneID);

            var btnDelete = MakeTopButton("Delete");
            btnDelete.ForeColor = Color.FromArgb(220, 45, 45);
            btnDelete.Click += (_, __) => DeleteRequested?.Invoke(p.PlaneID);

            card.Controls.Add(btnSchedule);
            card.Controls.Add(btnDelete);

            card.Controls.Add(InfoLine("Type:", typeText, 16, 60));
            card.Controls.Add(InfoLine("Total Seats:", total.ToString(), 16, 86));

            Control rightRow;
            if (vip > 0 && first == 0 && business == 0 && economy == 0)
            {
                rightRow = HorizontalRow(new[]
                {
                    ("VIP", vip.ToString()),
                    ("Status", statusText)
                }, 330, 64);
            }
            else
            {
                rightRow = HorizontalRow(new[]
                {
                    ("First", first.ToString()),
                    ("Business", business.ToString()),
                    ("Economy", economy.ToString())
                }, 330, 64);
            }
            card.Controls.Add(rightRow);

            void Open() => PlaneSelected?.Invoke(p.PlaneID);
            card.Click += (_, __) => Open();
            title.Click += (_, __) => Open();
            modelBadge.Click += (_, __) => Open();
            stBadge.Click += (_, __) => Open();

            card.SizeChanged += (_, __) =>
            {
                btnDelete.Location = new Point(card.Width - 16 - btnDelete.Width, 10);
                btnSchedule.Location = new Point(btnDelete.Left - 10 - btnSchedule.Width, 10);

                modelBadge.Location = new Point(title.Right + 14, 12);
                stBadge.Location = new Point(modelBadge.Right + 10, 12);
            };

            btnDelete.Location = new Point(card.Width - 16 - btnDelete.Width, 10);
            btnSchedule.Location = new Point(btnDelete.Left - 10 - btnSchedule.Width, 10);

            return card;
        }

        private Guna2Button MakeTopButton(string text)
        {
            return new Guna2Button
            {
                Text = text,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 93, 220),
                FillColor = Color.Transparent,
                BorderRadius = 8,
                BorderThickness = 1,
                BorderColor = Color.FromArgb(230, 230, 230),
                Size = new Size(120, 30),
                HoverState = { FillColor = Color.FromArgb(245, 245, 245) }
            };
        }

        private Control HorizontalRow((string label, string value)[] items, int x, int y)
        {
            var row = new FlowLayoutPanel
            {
                Location = new Point(x, y),
                Size = new Size(650, 30),
                BackColor = Color.Transparent,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = false
            };

            foreach (var it in items)
            {
                var chip = new Panel
                {
                    BackColor = Color.Transparent,
                    AutoSize = true,
                    Margin = new Padding(0, 0, 18, 0)
                };

                var l1 = new Label
                {
                    Text = it.label + ":",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9F),
                    ForeColor = Color.FromArgb(120, 120, 120),
                    Location = new Point(0, 6)
                };

                var l2 = new Label
                {
                    Text = it.value ?? "",
                    AutoSize = true,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(60, 60, 60),
                    Location = new Point(l1.Right + 6, 6)
                };

                chip.Controls.Add(l1);
                chip.Controls.Add(l2);

                chip.SizeChanged += (_, __) => l2.Location = new Point(l1.Right + 6, 6);

                row.Controls.Add(chip);
            }

            return row;
        }

        private Control InfoLine(string label, string value, int x, int y)
        {
            var p = new Guna2Panel
            {
                BackColor = Color.Transparent,
                Location = new Point(x, y),
                Size = new Size(320, 22)
            };

            var l1 = new Label
            {
                Text = label,
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(120, 120, 120),
                Location = new Point(0, 2)
            };

            var l2 = new Label
            {
                Text = value ?? "",
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(l1.Right + 6, 2)
            };

            p.Controls.Add(l1);
            p.Controls.Add(l2);

            p.SizeChanged += (_, __) => l2.Location = new Point(l1.Right + 6, 2);

            return p;
        }

        private Region RoundRegion(Rectangle r, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return new Region(path);
        }

        private Guna2HtmlLabel Badge(string text, Color back, Color fore)
        {
            if (string.IsNullOrWhiteSpace(text)) text = "-";

            var b = new Guna2HtmlLabel
            {
                AutoSize = true,
                BackColor = back,
                ForeColor = fore,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Text = $"  {text}  "
            };

            void ApplyRound()
            {
                var rect = new Rectangle(0, 0, b.Width + 2, b.Height + 2);
                b.Region = RoundRegion(rect, 10);
            }

            b.HandleCreated += (_, __) => ApplyRound();
            b.SizeChanged += (_, __) => ApplyRound();

            return b;
        }

        private static bool Eq(string? a, string b) =>
            string.Equals(a?.Trim(), b, StringComparison.OrdinalIgnoreCase);

        private static string NormalizePlaneStatus(string? st)
        {
            if (string.IsNullOrWhiteSpace(st)) return "active";
            var t = st.Trim().ToLowerInvariant();
            if (t == "available") return "active";
            if (t == "unavailable") return "inactive";
            if (t == "deleted") return "inactive";
            return t;
        }

        private static (Color back, Color fore) GetPlaneStatusColors(string status)
        {
            if (status == "active")
                return (Color.FromArgb(220, 245, 228), Color.FromArgb(28, 140, 60));
            if (status == "inactive")
                return (Color.FromArgb(235, 235, 235), Color.FromArgb(90, 90, 90));
            return (Color.FromArgb(222, 235, 255), Color.FromArgb(35, 93, 220));
        }

        // ============================
        // AddPlane Right Panel (TOP)
        // ============================
        private void ShowAddPlaneOverlay()
        {
            if (_addPlanePanel != null) return;

            btnAddPlane.Enabled = false;

            _addPlanePanel = new AddPlaneDockedControl
            {
                Width = 430,
                Dock = DockStyle.None, // we position manually
                BackColor = Color.White,
                Margin = Padding.Empty
            };

            // ✅ START FROM TOP (y=0) and full height
            _addPlanePanel.Height = root.ClientSize.Height;
            _addPlanePanel.Location = new Point(root.ClientSize.Width - _addPlanePanel.Width, 0);

            root.SizeChanged += Root_SizeChanged;

            _addPlanePanel.Confirmed += (planeName, type, status, total, eco, biz, first) =>
            {
                _pendingPlaneName = planeName;
                _pendingPlaneType = type;
                _pendingPlaneStatus = status;

                _pendingTotal = total;
                _pendingEco = eco;
                _pendingBiz = biz;
                _pendingFirst = first;

                CloseAddPlaneOverlay();
                AddPlaneClicked?.Invoke(this, EventArgs.Empty);
            };


            _addPlanePanel.Cancelled += CloseAddPlaneOverlay;

            root.Controls.Add(_addPlanePanel);
            root.Controls.SetChildIndex(_addPlanePanel, 0);

            LayoutContentAreas();
            RefreshCardsWidth();
        }

        private void Root_SizeChanged(object? sender, EventArgs e)
        {
            if (_addPlanePanel == null) return;

            // ✅ keep it from top and full height
            _addPlanePanel.Height = root.ClientSize.Height;
            _addPlanePanel.Location = new Point(root.ClientSize.Width - _addPlanePanel.Width, 0);

            LayoutContentAreas();
            RefreshCardsWidth();
        }

        private void CloseAddPlaneOverlay()
        {
            btnAddPlane.Enabled = true;

            if (_addPlanePanel == null) return;

            root.SizeChanged -= Root_SizeChanged;

            root.Controls.Remove(_addPlanePanel);
            _addPlanePanel.Dispose();
            _addPlanePanel = null;

            LayoutContentAreas();
            RefreshCardsWidth();
        }

        private void LayoutContentAreas()
        {
            if (flow == null || header == null || root == null) return;

            int shadowFix = 12;

            // Keep cards starting under header (nice layout)
            int top = header.Bottom + shadowFix;
            int rightGap = 16;

            flow.Location = new Point(0, top);
            flow.Height = root.ClientSize.Height - top;

            if (_addPlanePanel == null)
                flow.Width = root.ClientSize.Width;
            else
                flow.Width = root.ClientSize.Width - _addPlanePanel.Width - rightGap;

            flow.BringToFront();
        }
    }
}
