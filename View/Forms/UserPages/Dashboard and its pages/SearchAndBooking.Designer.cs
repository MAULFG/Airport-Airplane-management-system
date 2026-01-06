using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    partial class SearchAndBooking
    {
        private Guna2Panel panelSearch;
        private FlowLayoutPanel flowFilters;
        private Guna2TextBox cbFrom, cbTo;
        private Guna2ComboBox  cbClass;
        private Guna2DateTimePicker dtDeparture, dtReturn;
        private Guna2Button btnSearch;
        private FlowLayoutPanel flowFlights;

        private Label  lblDeparture, lblPassengers ;

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelSearch = new Guna2Panel();
            flowFilters = new FlowLayoutPanel();
            cbFrom = new Guna2TextBox();
            cbTo = new Guna2TextBox();
            lblDeparture = new Label();
            dtDeparture = new Guna2DateTimePicker();
            lblPassengers = new Label();
            numPassengers = new Guna2NumericUpDown();
            cbClass = new Guna2ComboBox();
            btnSearch = new Guna2Button();
            flowFlights = new FlowLayoutPanel();
            panelSearch.SuspendLayout();
            flowFilters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPassengers).BeginInit();
            SuspendLayout();
            // 
            // panelSearch
            // 
            panelSearch.BorderRadius = 10;
            panelSearch.Controls.Add(flowFilters);
            panelSearch.CustomizableEdges = customizableEdges13;
            panelSearch.Dock = DockStyle.Top;
            panelSearch.FillColor = Color.FromArgb(245, 245, 245);
            panelSearch.Location = new Point(0, 0);
            panelSearch.Name = "panelSearch";
            panelSearch.Padding = new Padding(15);
            panelSearch.ShadowDecoration.CustomizableEdges = customizableEdges14;
            panelSearch.Size = new Size(1030, 91);
            panelSearch.TabIndex = 1;
            // 
            // flowFilters
            // 
            flowFilters.Controls.Add(cbFrom);
            flowFilters.Controls.Add(cbTo);
            flowFilters.Controls.Add(lblDeparture);
            flowFilters.Controls.Add(dtDeparture);
            flowFilters.Controls.Add(lblPassengers);
            flowFilters.Controls.Add(numPassengers);
            flowFilters.Controls.Add(cbClass);
            flowFilters.Controls.Add(btnSearch);
            flowFilters.Dock = DockStyle.Fill;
            flowFilters.Location = new Point(15, 15);
            flowFilters.Name = "flowFilters";
            flowFilters.Padding = new Padding(5);
            flowFilters.Size = new Size(1000, 61);
            flowFilters.TabIndex = 0;
            // 
            // cbFrom
            // 
            cbFrom.BackColor = Color.Transparent;
            cbFrom.BorderRadius = 10;
            cbFrom.CustomizableEdges = customizableEdges1;
            cbFrom.DefaultText = "";
            cbFrom.Font = new Font("Segoe UI", 10F);
            cbFrom.ForeColor = Color.FromArgb(68, 88, 112);
            cbFrom.Location = new Point(10, 10);
            cbFrom.Margin = new Padding(5);
            cbFrom.Name = "cbFrom";
            cbFrom.PlaceholderForeColor = Color.LightGray;
            cbFrom.PlaceholderText = "Search From";
            cbFrom.SelectedText = "";
            cbFrom.ShadowDecoration.CustomizableEdges = customizableEdges2;
            cbFrom.Size = new Size(140, 36);
            cbFrom.TabIndex = 1;
            cbFrom.Tag = "From:";
            cbFrom.TextAlign = HorizontalAlignment.Center;
            // 
            // cbTo
            // 
            cbTo.BackColor = Color.Transparent;
            cbTo.BorderRadius = 10;
            cbTo.CustomizableEdges = customizableEdges3;
            cbTo.DefaultText = "";
            cbTo.Font = new Font("Segoe UI", 10F);
            cbTo.ForeColor = Color.FromArgb(68, 88, 112);
            cbTo.Location = new Point(160, 10);
            cbTo.Margin = new Padding(5);
            cbTo.Name = "cbTo";
            cbTo.PlaceholderForeColor = Color.LightGray;
            cbTo.PlaceholderText = "Search To";
            cbTo.SelectedText = "";
            cbTo.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cbTo.Size = new Size(140, 36);
            cbTo.TabIndex = 3;
            // 
            // lblDeparture
            // 
            lblDeparture.AutoSize = true;
            lblDeparture.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDeparture.Location = new Point(310, 15);
            lblDeparture.Margin = new Padding(5, 10, 0, 0);
            lblDeparture.Name = "lblDeparture";
            lblDeparture.Size = new Size(84, 20);
            lblDeparture.TabIndex = 4;
            lblDeparture.Text = "Departure:";
            // 
            // dtDeparture
            // 
            dtDeparture.BorderRadius = 10;
            dtDeparture.CausesValidation = false;
            dtDeparture.Checked = true;
            dtDeparture.CustomizableEdges = customizableEdges5;
            dtDeparture.FillColor = Color.DarkCyan;
            dtDeparture.Font = new Font("Segoe UI", 9F);
            dtDeparture.Format = DateTimePickerFormat.Short;
            dtDeparture.Location = new Point(399, 10);
            dtDeparture.Margin = new Padding(5);
            dtDeparture.MaxDate = new DateTime(9998, 12, 31, 0, 0, 0, 0);
            dtDeparture.MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
            dtDeparture.Name = "dtDeparture";
            dtDeparture.ShadowDecoration.CustomizableEdges = customizableEdges6;
            dtDeparture.ShowCheckBox = true;
            dtDeparture.Size = new Size(125, 36);
            dtDeparture.TabIndex = 5;
            dtDeparture.Value = new DateTime(2025, 12, 28, 12, 49, 44, 87);
            // 
            // lblPassengers
            // 
            lblPassengers.AutoSize = true;
            lblPassengers.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblPassengers.Location = new Point(534, 15);
            lblPassengers.Margin = new Padding(5, 10, 0, 0);
            lblPassengers.Name = "lblPassengers";
            lblPassengers.Size = new Size(91, 20);
            lblPassengers.TabIndex = 8;
            lblPassengers.Text = "Passengers:";
            // 
            // numPassengers
            // 
            numPassengers.BackColor = Color.Transparent;
            numPassengers.BorderRadius = 10;
            numPassengers.CustomizableEdges = customizableEdges7;
            numPassengers.Font = new Font("Segoe UI", 9F);
            numPassengers.Location = new Point(630, 10);
            numPassengers.Margin = new Padding(5);
            numPassengers.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            numPassengers.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPassengers.Name = "numPassengers";
            numPassengers.ShadowDecoration.CustomizableEdges = customizableEdges8;
            numPassengers.Size = new Size(61, 36);
            numPassengers.TabIndex = 9;
            numPassengers.UpDownButtonFillColor = Color.DarkCyan;
            numPassengers.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // cbClass
            // 
            cbClass.BackColor = Color.Transparent;
            cbClass.BorderRadius = 10;
            cbClass.CustomizableEdges = customizableEdges9;
            cbClass.DrawMode = DrawMode.OwnerDrawFixed;
            cbClass.DropDownStyle = ComboBoxStyle.DropDownList;
            cbClass.FocusedColor = Color.Empty;
            cbClass.Font = new Font("Segoe UI", 10F);
            cbClass.ForeColor = Color.FromArgb(68, 88, 112);
            cbClass.ItemHeight = 30;
            cbClass.Items.AddRange(new object[] { "Economy", "Business", "First" });
            cbClass.Location = new Point(701, 10);
            cbClass.Margin = new Padding(5);
            cbClass.Name = "cbClass";
            cbClass.ShadowDecoration.CustomizableEdges = customizableEdges10;
            cbClass.Size = new Size(140, 36);
            cbClass.TabIndex = 11;
            // 
            // btnSearch
            // 
            btnSearch.BorderRadius = 12;
            btnSearch.CustomizableEdges = customizableEdges11;
            btnSearch.FillColor = Color.DarkCyan;
            btnSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSearch.ForeColor = Color.White;
            btnSearch.Location = new Point(10, 56);
            btnSearch.Margin = new Padding(5);
            btnSearch.Name = "btnSearch";
            btnSearch.PressedColor = Color.DarkCyan;
            btnSearch.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnSearch.Size = new Size(140, 40);
            btnSearch.TabIndex = 12;
            btnSearch.Text = "Search";
            // 
            // flowFlights
            // 
            flowFlights.AutoScroll = true;
            flowFlights.BackColor = Color.White;
            flowFlights.Dock = DockStyle.Fill;
            flowFlights.FlowDirection = FlowDirection.TopDown;
            flowFlights.Location = new Point(0, 91);
            flowFlights.Name = "flowFlights";
            flowFlights.Padding = new Padding(10);
            flowFlights.RightToLeft = RightToLeft.Yes;
            flowFlights.Size = new Size(1030, 629);
            flowFlights.TabIndex = 0;
            flowFlights.WrapContents = false;
            flowFlights.Paint += flowFlights_Paint;
            // 
            // SearchAndBooking
            // 
            BackColor = Color.White;
            Controls.Add(flowFlights);
            Controls.Add(panelSearch);
            Name = "SearchAndBooking";
            Size = new Size(1030, 720);
            panelSearch.ResumeLayout(false);
            flowFilters.ResumeLayout(false);
            flowFilters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPassengers).EndInit();
            ResumeLayout(false);
        }

        private Guna2NumericUpDown numPassengers;
    }
}
