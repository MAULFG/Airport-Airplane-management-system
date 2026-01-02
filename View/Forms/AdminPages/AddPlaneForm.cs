using System;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    public partial class AddPlaneForm : Form
    {
        public event Action<string, string, string, int, int, int, int>? Confirmed;
        public event Action? Cancelled;

        private string _selectedType;
        private int _total, _eco, _biz, _first;

        public AddPlaneForm()
        {
            InitializeComponent();

            // 1️⃣ Hook click events for cards
            cardB777.Clicked += () => SelectCard(cardB777);
            cardA320.Clicked += () => SelectCard(cardA320);
            cardJet.Clicked += () => SelectCard(cardJet);

            // 2️⃣ Hook buttons
            btnClose.Click += (_, __) => { Cancelled?.Invoke(); Close(); };
            btnCancel.Click += (_, __) => { Cancelled?.Invoke(); Close(); };
            btnAdd.Click += (_, __) =>
            {
                var planeName = txtPlaneName.Text.Trim();
                Confirmed?.Invoke(planeName, _selectedType, "Active", _total, _eco, _biz, _first);
                Close();
            };

            // 3️⃣ Default selection **after labels exist**
            SelectCard(cardB777);

            // 4️⃣ Resize events
            root.Resize += (_, __) => { Relayout(); ReflowCards(); };
            scrollHost.Resize += (_, __) => ReflowCards();

            // 5️⃣ Initial layout
            Relayout();
            ReflowCards();
        }



        private void SelectCard(ConfigCard card)
        {
            cardB777.SetSelected(false, Blue, Border);
            cardA320.SetSelected(false, Blue, Border);
            cardJet.SetSelected(false, Blue, Border);

            card.SetSelected(true, Blue, Border);

            if (card == cardB777)
            {
                _selectedType = "HighLevel";
                _total = 316;
                _eco = 252;
                _biz = 48;
                _first = 16;
            }
            else if (card == cardA320)
            {
                _selectedType = "A320";
                _total = 170;
                _eco = 138;
                _biz = 32;
                _first = 0;
            }
            else if (card == cardJet)
            {
                _selectedType = "PrivateJet";
                _total = 7;
                _eco = 0;
                _biz = 0;
                _first = 7;
            }

            lblTotalSeats.Text = $"Total seats: {_total}";
            lblEcoSeats.Text = $"Economy seats: {_eco}";
            lblBizSeats.Text = $"Business seats: {_biz}";
            lblFirstSeats.Text = $"First class seats: {_first}";
            RefreshSummary();
        }


        private void RefreshSummary()
        {
            int y = 14;

            foreach (Control c in summaryBox.Controls)
            {
                c.Top = y;
                c.Left = 14;

                // Only set width for non-AutoSize controls if needed
                if (!c.AutoSize)
                    c.Width = summaryBox.ClientSize.Width - 28;

                y += c.Height ;
            }
        }








        private void Relayout()
        {
            // Form dimensions for reference
            int formWidth = 1016;
            int formHeight = 789;
            int paddingH = root.Padding.Left + root.Padding.Right;
            int paddingV = root.Padding.Top + root.Padding.Bottom;

            // Close button
            btnClose.Left = root.Width - root.Padding.Right - btnClose.Width;
            btnClose.Top = root.Padding.Top;

            // Title
            lblTitle.Top = root.Padding.Top + 4;
            lblTitle.Left = 10;

            // Plane Name label and textbox
            lblPlaneName.Top = lblTitle.Bottom + 20;
            lblPlaneName.Left = 10;

            txtPlaneName.Top = lblPlaneName.Bottom + 6;
            txtPlaneName.Left = 10;
            txtPlaneName.Width = root.Width - paddingH - 20;

            // Divider line
            line.Top = txtPlaneName.Bottom + 10;
            line.Left = 10;
            line.Width = root.Width - 20;

            // Section label
            lblSection.Top = line.Bottom + 12;
            lblSection.Left = 10;

            // Scroll host
            scrollHost.Top = lblSection.Bottom + 8;
            scrollHost.Left = 10;
            scrollHost.Width = root.Width - paddingH - 20;
            scrollHost.Height = 250; // slightly bigger for 1016 width
            scrollHost.AutoScroll = true;
            scrollHost.HorizontalScroll.Enabled = false;
            scrollHost.HorizontalScroll.Visible = false;
            scrollHost.VerticalScroll.Enabled = true;
            scrollHost.Controls.Clear();
            scrollHost.Controls.Add(cardB777);
            scrollHost.Controls.Add(cardA320);
            scrollHost.Controls.Add(cardJet);

            // Reflow cards inside scrollHost
            ReflowCards();

            // Footer and summary
            int footerHeight = 46;
            int summaryHeight = 108;
            int summaryGap = 14;
            int bottomPad = root.Padding.Bottom;

            summaryBox.Width = root.Width - paddingH - 4;
            summaryBox.Height = summaryHeight;
            summaryBox.Top = root.Height - bottomPad - footerHeight - summaryGap - summaryHeight;
            summaryBox.Left = 2;

            btnCancel.Width = 120;
            btnCancel.Height = 40;
            btnCancel.Top = root.Height - bottomPad - footerHeight;
            btnCancel.Left = 14;

            btnAdd.Width = 140;
            btnAdd.Height = 40;
            btnAdd.Top = root.Height - bottomPad - footerHeight;
            btnAdd.Left = root.Width - btnAdd.Width - 14;

            // Adjust scroll height to fit remaining space
            scrollHost.Height = Math.Max(80, summaryBox.Top - 10 - scrollHost.Top);
        }

        private void ReflowCards()
        {
            int y = 0;
            int margin = 12;

            foreach (var c in new[] { cardB777, cardA320, cardJet })
            {
                c.Width = scrollHost.ClientSize.Width - 10; // fill width
                c.Left = 5;
                c.Top = y;
                y += c.Height + margin;
            }
        }



        // Colors
        private readonly System.Drawing.Color Border = System.Drawing.Color.FromArgb(230, 230, 230);
        private readonly System.Drawing.Color Blue = System.Drawing.Color.FromArgb(47, 111, 237);

        private readonly System.Drawing.Color RowEco = System.Drawing.Color.FromArgb(234, 243, 255);
        private readonly System.Drawing.Color RowBiz = System.Drawing.Color.FromArgb(244, 238, 255);
        private readonly System.Drawing.Color RowFirst = System.Drawing.Color.FromArgb(252, 246, 230);
    }
}
