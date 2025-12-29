using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.AdminPages
{
    public partial class SlotDialog : Form
    {
        public DateTime SelectedDeparture { get; private set; }
        public DateTime SelectedArrival { get; private set; }

        private readonly DateTime _day;

        private Panel card;
        private Button btnClose;
        private Label lblTitle;
        private Label lblSub;

        private Label lblDep;
        private DateTimePicker dtDep;

        private Label lblArr;
        private DateTimePicker dtArr;

        private Panel note;
        private Label lblNoteTitle;
        private Label lblNoteText;

        private Button btnCancel;
        private Button btnContinue;

        public SlotDialog(DateTime day, DateTime suggestedDeparture, DateTime suggestedArrival)
        {
            _day = day.Date;

            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.FromArgb(150, 0, 0, 0); // semi overlay
            ShowInTaskbar = false;
            Width = 560;
            Height = 420;

            BuildUI();

            // initialize
            dtDep.Value = suggestedDeparture;
            dtArr.Value = suggestedArrival;

            SelectedDeparture = suggestedDeparture;
            SelectedArrival = suggestedArrival;
        }

        private void BuildUI()
        {
            card = new Panel
            {
                Size = new Size(500, 340),
                BackColor = Color.White
            };
            Controls.Add(card);

            // center card
            card.Location = new Point((ClientSize.Width - card.Width) / 2, (ClientSize.Height - card.Height) / 2);
            card.SizeChanged += (_, __) => MakeRounded(card, 16);
            MakeRounded(card, 16);

            btnClose = new Button
            {
                Text = "✕",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(60, 60, 60),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(36, 32),
                Location = new Point(card.Width - 46, 12)
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (_, __) => { DialogResult = DialogResult.Cancel; Close(); };
            card.Controls.Add(btnClose);

            lblTitle = new Label
            {
                AutoSize = true,
                Text = "Add Flight at Time Slot",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 20, 20),
                Location = new Point(26, 18)
            };
            card.Controls.Add(lblTitle);

            lblSub = new Label
            {
                AutoSize = true,
                Text = $"Schedule a flight on {_day:yyyy-MM-dd}",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(120, 120, 120),
                Location = new Point(26, 46)
            };
            card.Controls.Add(lblSub);

            lblDep = new Label
            {
                AutoSize = true,
                Text = "Departure Time",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 40, 40),
                Location = new Point(26, 88)
            };
            card.Controls.Add(lblDep);

            dtDep = new DateTimePicker
            {
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true,
                Font = new Font("Segoe UI", 10F),
                Width = 440,
                Location = new Point(26, 112)
            };
            card.Controls.Add(dtDep);

            lblArr = new Label
            {
                AutoSize = true,
                Text = "Arrival Time",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 40, 40),
                Location = new Point(26, 156)
            };
            card.Controls.Add(lblArr);

            dtArr = new DateTimePicker
            {
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true,
                Font = new Font("Segoe UI", 10F),
                Width = 440,
                Location = new Point(26, 180)
            };
            card.Controls.Add(dtArr);

            note = new Panel
            {
                BackColor = Color.FromArgb(235, 245, 255),
                Location = new Point(26, 224),
                Size = new Size(440, 64)
            };
            MakeRounded(note, 12);
            card.Controls.Add(note);

            lblNoteTitle = new Label
            {
                AutoSize = true,
                Text = "Note:",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 90, 200),
                Location = new Point(14, 12)
            };
            note.Controls.Add(lblNoteTitle);

            lblNoteText = new Label
            {
                AutoSize = false,
                Text = "After confirming the time slot, you'll be able to complete the flight details.",
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.FromArgb(30, 90, 200),
                Location = new Point(14, 30),
                Size = new Size(412, 28)
            };
            note.Controls.Add(lblNoteText);

            btnCancel = new Button
            {
                Text = "Cancel",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(40, 40, 40),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(120, 36),
                Location = new Point(206, 300)
            };
            btnCancel.FlatAppearance.BorderColor = Color.FromArgb(220, 220, 220);
            btnCancel.FlatAppearance.BorderSize = 1;
            MakeRounded(btnCancel, 12);
            btnCancel.Click += (_, __) => { DialogResult = DialogResult.Cancel; Close(); };
            card.Controls.Add(btnCancel);

            btnContinue = new Button
            {
                Text = "Continue to Flight Details",
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(15, 20, 30),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Size = new Size(220, 36),
                Location = new Point(246 + 90, 300) // aligned right
            };
            btnContinue.FlatAppearance.BorderSize = 0;
            MakeRounded(btnContinue, 12);
            btnContinue.Click += (_, __) =>
            {
                // force same day date, use time from picker
                var dep = _day.Date.AddHours(dtDep.Value.Hour).AddMinutes(dtDep.Value.Minute);
                var arr = _day.Date.AddHours(dtArr.Value.Hour).AddMinutes(dtArr.Value.Minute);

                SelectedDeparture = dep;
                SelectedArrival = arr;

                DialogResult = DialogResult.OK;
                Close();
            };
            card.Controls.Add(btnContinue);

            // keep close button aligned
            card.SizeChanged += (_, __) =>
            {
                btnClose.Location = new Point(card.Width - 46, 12);
                btnContinue.Location = new Point(card.Width - btnContinue.Width - 26, 300);
                btnCancel.Location = new Point(btnContinue.Left - btnCancel.Width - 10, 300);
            };
        }

        private static void MakeRounded(Control c, int radius)
        {
            if (c.Width < 4 || c.Height < 4) return;
            using var path = RoundedRect(new Rectangle(0, 0, c.Width, c.Height), radius);
            c.Region = new Region(path);
        }

        private static GraphicsPath RoundedRect(Rectangle rect, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
