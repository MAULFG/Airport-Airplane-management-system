using Airport_Airplane_management_system.Model.Interfaces.Views;
using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    public partial class MainUserPage : UserControl, IMainUserPageView
    {
        public MainUserPage()
        {
            InitializeComponent();
        }

        // ================= MVP =================

        public void SetWelcomeText(string text)
        {
            lblWelcome.Text = text;
        }

        public void ClearStatistics()
        {
            flowStats.Controls.Clear();
        }

        public void AddStatCard(string title, string value)
        {
            flowStats.Controls.Add(CreateStatCard(title, value));
        }

        // ================= CARD FACTORY =================

        private Control CreateStatCard(string title, string value)
        {
            Color accent = GetAccentColor(title);
            string emoji = GetEmoji(title);

            var card = new Guna2ShadowPanel
            {
                Width = 350,
                Height = 150,
                Radius = 22,
                FillColor = Color.White,
                ShadowDepth = 18,
                ShadowColor = Color.FromArgb(150, accent),
                Margin = new Padding(20),
                Padding = new Padding(24)
            };

            // ================= Emoji instead of bar =================
            var emojiLabel = new Label
            {
                Text = emoji,
                Font = new Font("Segoe UI Emoji", 36F),
                ForeColor = accent,
                AutoSize = true,
                Location = new Point(10, 35) // position at left-top
            };

            var lblTitle = new Label
            {
                Text = title.ToUpper(),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(120, 120, 120),
                Location = new Point(80,15), // shifted right of emoji
                AutoSize = true
            };

            var lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 18F,FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 30),
                Location = new Point(100,50),
                AutoSize = true
            };

            card.Controls.Add(lblValue);
            card.Controls.Add(lblTitle);
            card.Controls.Add(emojiLabel);
            emojiLabel.BringToFront();
            return card;
        }

        // ================= Map titles to emojis =================
        private string GetEmoji(string title)
        {
            title = title.ToLower();

            if (title.Contains("upcoming")) return "✈️";
            if (title.Contains("completed")) return "🛬";
            if (title.Contains("total")) return "📊";
            if (title.Contains("route")) return "🗺️";
            if (title.Contains("departure")) return "⏰";

            return "ℹ️";
        }

        private Color GetAccentColor(string title)
        {
            title = title.ToLower();

            if (title.Contains("upcoming")) return Color.FromArgb(0, 160, 150);
            if (title.Contains("completed")) return Color.FromArgb(46, 204, 113);
            if (title.Contains("total")) return Color.FromArgb(155, 93, 229);
            if (title.Contains("route")) return Color.FromArgb(58, 134, 255);
            if (title.Contains("departure")) return Color.FromArgb(244, 162, 97);

            return Color.FromArgb(120, 120, 120);
        }

        private void flowStats_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
