using Airport_Airplane_management_system.Model.Interfaces.Views;
using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    public partial class MainUserPage : UserControl, IMainUserPageView
    {
        private Guna2ShadowPanel nextFlightPanel;
        private Label lblRoute;
        private Label lblInfo;

        public MainUserPage()
        {
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Dpi;

            CreateNextFlightPanel();

            // ✅ FORCE ORDER: cards first, flight under
            bodyPanel.Controls.SetChildIndex(flowStats, 0);

            flowStats.Resize += (s, e) => AdjustCardWidthsToMax();
        }

        // ================= MVP =================
        public void SetWelcomeText(string text) => lblWelcome.Text = text;

        public void ClearStatistics() => flowStats.Controls.Clear();

        public void AddStatCard(string title, string value)
        {
            var card = CreateStatCard(title, value);
            flowStats.Controls.Add(card);
            AdjustCardWidthsToMax(); // match width of largest card
        }

        public void SetNextFlight(string route, string info)
        {
            lblRoute.Text = route;
            lblInfo.Text = info;
            nextFlightPanel.Visible = true;
        }

        public void HideNextFlight() => nextFlightPanel.Visible = false;

        // ================= ADJUST CARD WIDTHS =================
        private void AdjustCardWidthsToMax()
        {
            if (flowStats.Controls.Count == 0) return;

            int maxWidth = 0;

            // First pass: find largest width
            foreach (Control c in flowStats.Controls)
            {
                if (c.Width > maxWidth) maxWidth = c.Width;
            }

            // Second pass: set all cards to max width & fixed height
            foreach (Control c in flowStats.Controls)
            {
                c.Width = maxWidth;
                c.Height = 130; // doubled height
            }
        }

        // ================= NEXT FLIGHT PANEL =================
        private void CreateNextFlightPanel()
        {
            nextFlightPanel = new Guna2ShadowPanel
            {
                Height = 150,
                Radius = 24,
                FillColor = Color.FromArgb(245, 248, 255),
                ShadowDepth = 14,
                Dock = DockStyle.Bottom,
                Padding = new Padding(25),
                Margin = new Padding(0, 20, 0, 0)
            };

            // ===== Title =====
            var title = new Label
            {
                Text = "✈️ Your Next Flight",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                AutoSize = true,
                Dock = DockStyle.Fill
            };

            // ===== Route =====
            lblRoute = new Label
            {
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                AutoSize = true,
                Dock = DockStyle.Fill
            };

            // ===== Info =====
            lblInfo = new Label
            {
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(90, 90, 90),
                AutoSize = true,
                Dock = DockStyle.Fill
            };

            // ===== Layout =====
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3
            };

            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // title
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // route
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize)); // info

            layout.Controls.Add(title, 0, 0);
            layout.Controls.Add(lblRoute, 0, 1);
            layout.Controls.Add(lblInfo, 0, 2);

            nextFlightPanel.Controls.Add(layout);

            bodyPanel.Controls.Add(nextFlightPanel);
            nextFlightPanel.Visible = false;
        }



        // ================= STAT CARD =================
        private Control CreateStatCard(string title, string value)
        {
            var accent = GetAccentColor(title);

            var card = new Guna2ShadowPanel
            {
                Radius = 22,
                FillColor = Color.White,
                ShadowColor = Color.FromArgb(120, accent),
                ShadowDepth = 18,
                Margin = new Padding(15),
                Padding = new Padding(20),
                AutoSize = false,   // crucial
                Width = 500,        // initial width
                Height = 130       // doubled height
            };

            var emoji = new Label
            {
                Text = GetEmoji(title),
                Font = new Font("Segoe UI Emoji", 36F),
                ForeColor = accent,
                AutoSize = true,
                Dock = DockStyle.Left
            };

            var lblTitle = new Label
            {
                Text = title.ToUpper(),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.Gray,
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Top
            };

            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                AutoSize = true,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Top
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                AutoSize = false // crucial
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            layout.Controls.Add(emoji, 0, 0);
            layout.SetRowSpan(emoji, 2);
            layout.Controls.Add(lblTitle, 1, 0);
            layout.Controls.Add(lblValue, 1, 1);

            card.Controls.Add(layout);
            return card;
        }

        private string GetEmoji(string title)
        {
            title = title.ToLower();
            if (title.Contains("upcoming")) return "✈️";
            if (title.Contains("completed")) return "🛬";
            if (title.Contains("total")) return "📊";
            if (title.Contains("route")) return "🗺️";
            if (title.Contains("check-in")) return "🛂";
            if (title.Contains("notifications")) return "🔔";
            return "ℹ️";
        }

        private Color GetAccentColor(string title)
        {
            title = title.ToLower();
            if (title.Contains("upcoming")) return Color.FromArgb(0, 160, 150);
            if (title.Contains("completed")) return Color.FromArgb(46, 204, 113);
            if (title.Contains("total")) return Color.FromArgb(155, 93, 229);
            if (title.Contains("route")) return Color.FromArgb(58, 134, 255);
            if (title.Contains("check-in")) return Color.FromArgb(244, 162, 97);
            if (title.Contains("notifications")) return Color.FromArgb(255, 165, 0);
            return Color.Gray;
        }

        // ================= FILLER / EXTRA CARDS =================
        public void AddExtraCards()
        {
            AddStatCard("Favorite Route", "Beirut → Paris via Istanbul");
            AddStatCard("Next Check-in", "Available 24h before flight at Gate A12");
            AddStatCard("Notifications", "2 new alerts");
            AddStatCard("Most Frequent Route", "Beirut → Dubai → London");

        }

        private void flowStats_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
