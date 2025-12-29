using Airport_Airplane_management_system.Model.Interfaces.Exceptions;
using Guna.UI2.WinForms;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    public partial class MainUserPage : UserControl, IMainUserPageView
    {
        public MainUserPage()
        {
            InitializeComponent(); // ✅ KEEP DESIGNER
        }

        // ===== IMainUserPageView implementation =====

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
            var card = new Guna2ShadowPanel
            {
                Size = new Size(220, 120),
                Radius = 10,
                ShadowColor = Color.Gray,
                FillColor = Color.White,
                Margin = new Padding(10)
            };

            var lblTitle = new Guna2HtmlLabel
            {
                Text = title,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(12, 10),
                AutoSize = true
            };

            var lblValue = new Guna2HtmlLabel
            {
                Text = value,
                ForeColor = Color.DarkCyan,
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                Location = new Point(12, 45),
                AutoSize = true
            };
            // Add labels first so we can calculate size
            card.Controls.Add(lblTitle);
            card.Controls.Add(lblValue);

            // Center horizontally
            lblTitle.Left = (card.Width - lblTitle.Width) / 2;
            lblTitle.Top = 20; // some padding from top

            lblValue.Left = (card.Width - lblValue.Width) / 2;
            lblValue.Top = lblTitle.Bottom + 10; // space between title and value

            flowStats.Controls.Add(card);
        }



private void MainUserPage_Load(object sender, EventArgs e)
        {

        }

        private void flowStats_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
