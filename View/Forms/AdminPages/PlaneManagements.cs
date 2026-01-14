using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    public partial class PlaneManagements : UserControl, IPlaneManagementView
    {
        public event EventHandler ViewLoaded;
        public event EventHandler AddPlaneClicked;
        public event Action<int> DeleteRequested;
        public event Action<int>? PlaneSelected;

        private Panel flowPlanes;
        private List<Control> planeCards = new();
        private AddPlaneForm addplane;
        private List<Plane> _planes = new();

        private string _pendingPlaneName = "";
        private string _pendingPlaneType = "";
        private string _pendingPlaneStatus = "";
        private int _pendingTotal, _pendingEco, _pendingBiz, _pendingFirst;
        public int? HighlightedPlaneId { get; private set; }
        public PlaneManagements()
        {
            InitializeComponent();

            BuildFlow();
            WireEvents();

            Load += (_, __) => ViewLoaded?.Invoke(this, EventArgs.Empty);
        }
        // IPlaneManagementView Implementation
        public void ShowInfo(string message) =>
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public void ShowError(string message) =>
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public bool Confirm(string message) =>
            MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;

        public void HighlightPlane(int planeId)
        {
            HighlightedPlaneId = planeId;
            RenderCards(_planes); // refresh to show highlight
        }

        public bool TryGetNewPlaneInput(out string model,
                                out string type,
                                out string status,
                                out int total,
                                out int eco,
                                out int biz,
                                out int first)
        {
            model = _pendingPlaneName;
            type = _pendingPlaneType;
            status = _pendingPlaneStatus;

            total = eco = biz = first = 0;

            if (string.IsNullOrWhiteSpace(model))
            {
                ShowError("Enter plane name.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(type))
            {
                ShowError("Select plane configuration.");
                return false;
            }

            // INFO ONLY (not logic)
            GetCountsByType(type, out total, out eco, out biz, out first, out _);

            return true;
        }



        private void BuildFlow()
        {

            flowPlanes = new Panel
            {

                Dock = DockStyle.Fill,      // fill only the content panel
                AutoScroll = true,          // enable scrolling
                BackColor = Color.Transparent
            };
            content.Controls.Add(flowPlanes);

            // Enable double buffering
            typeof(Panel).InvokeMember(
                "DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetProperty,
                null,
                flowPlanes,
                new object[] { true });

            //  Add to the parent container
            root.Controls.Add(flowPlanes);
            flowPlanes.BringToFront();

            flowPlanes.Resize += (_, __) => RepositionCards();
        }

        private void WireEvents()
        {
            btnAddPlane.Click += (_, __) => ShowAddPlaneOverlay();
        }

        public void SetPlanes(List<Plane> planes)
        {
            _planes = planes ?? new();
            RenderCards(_planes);
        }

        private async void RenderCards(IEnumerable<Plane> planes)
        {
            if (planes == null || flowPlanes == null) return;

            flowPlanes.SuspendLayout();
            flowPlanes.Controls.Clear();
            planeCards.Clear();
            flowPlanes.ResumeLayout();

            int y = 12;
            int spacing = 12;
            int cardWidth = flowPlanes.ClientSize.Width - 24;

            const int batchSize = 4;
            var ordered = planes.OrderBy(p => p.PlaneID).ToList();

            for (int i = 0; i < ordered.Count; i += batchSize)
            {
                var batch = ordered.Skip(i).Take(batchSize);

                flowPlanes.SuspendLayout();
                foreach (var p in batch)
                {
                    var card = CreatePlaneCard(p);
                    card.Width = cardWidth;
                    card.Top = y;
                    card.Left = 12;

                    planeCards.Add(card);
                    flowPlanes.Controls.Add(card);

                    y += card.Height + spacing;
                }
                flowPlanes.ResumeLayout(false);

                await System.Threading.Tasks.Task.Delay(1); // let UI breathe
            }

            flowPlanes.PerformLayout();
        }



        // Reposition cards on flowPlanes resize
        private void RepositionCards()
        {
            int y = 12;
            int spacing = 12;
            int cardWidth = flowPlanes.ClientSize.Width - 24;

            foreach (var card in planeCards)
            {
                // move/resize all cards at once
                card.SuspendLayout();
                card.Width = cardWidth;
                card.Left = 12;

                // update buttons & rightRow inside card
                var btnDelete = card.Controls.OfType<Guna2Button>().FirstOrDefault(b => b.Text == "Delete");
                var btnSchedule = card.Controls.OfType<Guna2Button>().FirstOrDefault(b => b.Text == "See Schedule");
                var rightRow = card.Controls.OfType<FlowLayoutPanel>().FirstOrDefault();

                if (btnDelete != null) btnDelete.Location = new Point(card.Width - 16 - btnDelete.Width, 10);
                if (btnSchedule != null && btnDelete != null) btnSchedule.Location = new Point(btnDelete.Left - 10 - btnSchedule.Width, 10);
                if (rightRow != null) rightRow.Width = Math.Max(200, card.Width - rightRow.Left - 16);

                card.ResumeLayout();
                card.Top = y;
                y += card.Height + spacing;
            }
        }


        private Control CreatePlaneCard(Plane p)
        {
            //  Read counts from actual Seats list
            int total = p.Seats.Count;
            int eco = p.Seats.Count(s => s.ClassType == "Economy");
            int biz = p.Seats.Count(s => s.ClassType == "Business");
            int first = p.Seats.Count(s => s.ClassType == "First");
            int vip = p.Seats.Count(s => s.ClassType == "VIP");

            var card = new Guna2ShadowPanel
            {
                BackColor = Color.Transparent,
                FillColor = Color.White,
                Radius = 12,
                Padding = new Padding(16),
                Margin = new Padding(0, 0, 0, 12),
                Height = 150
            };

            string typeText = GetFriendlyTypeName(p.Type);
            string modelText = string.IsNullOrWhiteSpace(p.Model) ? "-" : p.Model.Trim();
            string statusText = NormalizePlaneStatus(p.Status);

            var title = new Guna2HtmlLabel
            {
                Text = $"Plane #{p.PlaneID}",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                AutoSize = true,
                BackColor = Color.Transparent,
                ForeColor = Color.FromArgb(30, 30, 30),
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

            // Buttons
            var btnSchedule = MakeTopButton("See Schedule");
            btnSchedule.Click += (_, __) => PlaneSelected?.Invoke(p.PlaneID);
            card.Controls.Add(btnSchedule);

            var btnDelete = MakeTopButton("Delete");
            btnDelete.ForeColor = Color.FromArgb(220, 45, 45);
            btnDelete.Click += (_, __) => DeleteRequested?.Invoke(p.PlaneID);
            card.Controls.Add(btnDelete);

            // Info lines
            card.Controls.Add(InfoLine("Type:", typeText, 16, 60));
            card.Controls.Add(InfoLine("Total Seats:", total.ToString(), 16, 86));

            int rightRowX = 380;
            int rightRowY = 64;

            Control rightRow;
            if (vip > 0)
                rightRow = HorizontalRow(new[] { ("VIP", vip.ToString()) }, rightRowX, rightRowY);
            else
                rightRow = HorizontalRow(new[] { ("First", first.ToString()), ("Business", biz.ToString()), ("Economy", eco.ToString()) }, rightRowX, rightRowY);

            card.Controls.Add(rightRow);

            // Card click selects plane
            void Open() => PlaneSelected?.Invoke(p.PlaneID);
            card.Click += (_, __) => Open();
            title.Click += (_, __) => Open();
            modelBadge.Click += (_, __) => Open();
            stBadge.Click += (_, __) => Open();

            card.SizeChanged += (_, __) =>
            {
                btnDelete.Location = new Point(card.Width - 16 - btnDelete.Width, 10);
                btnSchedule.Location = new Point(btnDelete.Left - 10 - btnSchedule.Width, 10);
                rightRow.Width = Math.Max(200, card.Width - rightRow.Left - 16);
                rightRow.PerformLayout();
            };

            return card;
        }


        private Control InfoLine(string label, string value, int x, int y)
        {
            var p = new Guna2Panel { Location = new Point(x, y), Size = new Size(320, 22), BackColor = Color.Transparent };
            var l1 = new Label { Text = label, AutoSize = true, Font = new Font("Segoe UI", 9F), ForeColor = Color.FromArgb(120, 120, 120), Location = new Point(0, 2) };
            var l2 = new Label { Text = value ?? "", AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(60, 60, 60), Location = new Point(l1.Right + 6, 2) };
            p.Controls.Add(l1);
            p.Controls.Add(l2);
            p.SizeChanged += (_, __) => l2.Location = new Point(l1.Right + 6, 2);
            return p;
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
            };

            foreach (var it in items)
            {
                var chip = new Panel { AutoSize = true, Margin = new Padding(0, 0, 18, 0), BackColor = Color.Transparent };
                var l1 = new Label { Text = it.label + ":", AutoSize = true, Font = new Font("Segoe UI", 9F), ForeColor = Color.FromArgb(120, 120, 120), Location = new Point(0, 6) };
                var l2 = new Label { Text = it.value, AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(60, 60, 60), Location = new Point(l1.Right + 6, 6) };
                chip.Controls.Add(l1); chip.Controls.Add(l2);
                chip.SizeChanged += (_, __) => l2.Location = new Point(l1.Right + 6, 6);
                row.Controls.Add(chip);
            }
            return row;
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

        private Guna2HtmlLabel Badge(string text, Color back, Color fore)
        {
            return new Guna2HtmlLabel { AutoSize = true, Text = $"  {text}  ", BackColor = back, ForeColor = fore, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
        }


        private static string GetFriendlyTypeName(string typeKey)
        {
            return typeKey switch
            {
                "HighLevel" => "Boeing 777 (High)",
                "A320" => "Airbus A320 (Med)",
                "PrivateJet" => "Private Jet",
                _ => typeKey
            };
        }

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
            if (status == "active") return (Color.FromArgb(220, 245, 228), Color.FromArgb(28, 140, 60));
            if (status == "inactive") return (Color.FromArgb(235, 235, 235), Color.FromArgb(90, 90, 90));
            return (Color.FromArgb(222, 235, 255), Color.FromArgb(35, 93, 220));
        }

        private void ShowAddPlaneOverlay()
        {
            addplane = new AddPlaneForm();
            addplane.Show();

            addplane.Confirmed += (planeName, type, status, total, eco, biz, first) =>
            {
                _pendingPlaneName = planeName;
                _pendingPlaneType = type;
                _pendingPlaneStatus = status;
                _pendingTotal = total;
                _pendingEco = eco;
                _pendingBiz = biz;
                _pendingFirst = first;

                AddPlaneClicked?.Invoke(this, EventArgs.Empty);
            };

        }
        // ===== Presenter -> View =====
        public void ClearView()
        {
            // close add plane form if open
            if (addplane != null && !addplane.IsDisposed)
            {
                addplane.Close();
                addplane = null;
            }

            // reset internal state
            _planes.Clear();
            planeCards.Clear();
            HighlightedPlaneId = null;

            _pendingPlaneName = "";
            _pendingPlaneType = "";
            _pendingPlaneStatus = "";
            _pendingTotal = _pendingEco = _pendingBiz = _pendingFirst = 0;

            // clear UI
            if (flowPlanes != null)
            {
                flowPlanes.SuspendLayout();
                flowPlanes.Controls.Clear();
                flowPlanes.ResumeLayout();
            }
        }

        private static void GetCountsByType(string type, out int total, out int eco, out int biz, out int first, out int vip)
        {
            total = eco = biz = first = vip = 0;
            type = (type ?? "").Trim();

            if (type == "HighLevel") { first = 16; biz = 48; eco = 252; total = first + biz + eco; return; }
            if (type == "A320") { first = 0; biz = 32; eco = 138; total = biz + eco; return; }
            if (type == "PrivateJet") { vip = 7; total = 7; return; }
        }

        private void flow_Paint(object sender, PaintEventArgs e)
        {

        }

        private void header_Paint(object sender, PaintEventArgs e)
        {

        }

        private void content_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
