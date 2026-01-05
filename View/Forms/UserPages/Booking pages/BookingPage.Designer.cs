using Airport_Airplane_management_system.View.Forms.UserPages.Booking_pages;
using Guna.UI2.WinForms;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    partial class BookingPage 
    {
        private System.ComponentModel.IContainer components = null;

        private Guna2ShadowPanel panelHeader;
        private Guna2HtmlLabel lblFlightInfo;

        private Guna2ShadowPanel panelMain;
        private FlowLayoutPanel flowSeats;

        private Guna2ShadowPanel panelSummary;
        private Guna2GroupBox grpSelectedSeat;
        private Guna2GroupBox grpPrice;
        private Guna2Button btnConfirm;
        private Guna2HtmlLabel lblSeatKey;
        private Guna2HtmlLabel lblSeatValue;

        private Guna2HtmlLabel lblClassKey;
        private Guna2HtmlLabel lblClassValue;

        private Guna2HtmlLabel lblStatusKey;
        private Guna2HtmlLabel lblStatusValue;
        private Guna2HtmlLabel lblBasePriceValue;
        private Guna2HtmlLabel lblTaxValue;
        private Guna2HtmlLabel lblDivider;
        private Guna2HtmlLabel lblTotalValue;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panelHeader = new Guna2ShadowPanel();
            lblFlightInfo = new Guna2HtmlLabel();
            panelMain = new Guna2ShadowPanel();
            panelSummary = new Guna2ShadowPanel();
            btnConfirm = new Guna2Button();
            grpPrice = new Guna2GroupBox();
            guna2HtmlLabel1 = new Guna2HtmlLabel();
            lblBasePriceValue = new Guna2HtmlLabel();
            lblTaxValue = new Guna2HtmlLabel();
            lblDivider = new Guna2HtmlLabel();
            lblTotalValue = new Guna2HtmlLabel();
            grpSelectedSeat = new Guna2GroupBox();
            lblSeatKey = new Guna2HtmlLabel();
            lblSeatValue = new Guna2HtmlLabel();
            lblClassKey = new Guna2HtmlLabel();
            lblClassValue = new Guna2HtmlLabel();
            lblStatusKey = new Guna2HtmlLabel();
            lblStatusValue = new Guna2HtmlLabel();
            flowSeats = new FlowLayoutPanel();
            passengerDetails1 = new PassengerDetails();
            panelHeader.SuspendLayout();
            panelMain.SuspendLayout();
            panelSummary.SuspendLayout();
            grpPrice.SuspendLayout();
            grpSelectedSeat.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.Transparent;
            panelHeader.Controls.Add(lblFlightInfo);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.FillColor = Color.DarkCyan;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Padding = new Padding(20);
            panelHeader.Radius = 10;
            panelHeader.ShadowColor = Color.Black;
            panelHeader.Size = new Size(1084, 70);
            panelHeader.TabIndex = 1;
            // 
            // lblFlightInfo
            // 
            lblFlightInfo.BackColor = Color.Transparent;
            lblFlightInfo.Dock = DockStyle.Fill;
            lblFlightInfo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblFlightInfo.ForeColor = Color.White;
            lblFlightInfo.Location = new Point(20, 20);
            lblFlightInfo.Name = "lblFlightInfo";
            lblFlightInfo.Size = new Size(104, 23);
            lblFlightInfo.TabIndex = 0;
            lblFlightInfo.Text = "Flight Details";
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.Transparent;
            panelMain.Controls.Add(panelSummary);
            panelMain.Controls.Add(flowSeats);
            panelMain.Dock = DockStyle.Fill;
            panelMain.FillColor = Color.White;
            panelMain.Location = new Point(0, 70);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(20);
            panelMain.Radius = 10;
            panelMain.ShadowColor = Color.Black;
            panelMain.Size = new Size(1084, 680);
            panelMain.TabIndex = 0;
            // 
            // panelSummary
            // 
            panelSummary.BackColor = Color.Transparent;
            panelSummary.Controls.Add(btnConfirm);
            panelSummary.Controls.Add(grpPrice);
            panelSummary.Controls.Add(grpSelectedSeat);
            panelSummary.Dock = DockStyle.Right;
            panelSummary.FillColor = Color.FromArgb(245, 245, 245);
            panelSummary.Location = new Point(755, 20);
            panelSummary.Name = "panelSummary";
            panelSummary.Padding = new Padding(20);
            panelSummary.Radius = 10;
            panelSummary.ShadowColor = Color.Black;
            panelSummary.Size = new Size(309, 640);
            panelSummary.TabIndex = 0;
            panelSummary.Paint += panelSummary_Paint;
            // 
            // btnConfirm
            // 
            btnConfirm.BorderRadius = 10;
            btnConfirm.CustomizableEdges = customizableEdges1;
            btnConfirm.Dock = DockStyle.Bottom;
            btnConfirm.FillColor = Color.DarkCyan;
            btnConfirm.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnConfirm.ForeColor = Color.White;
            btnConfirm.ImageAlign = HorizontalAlignment.Right;
            btnConfirm.ImageOffset = new Point(65, 2);
            btnConfirm.Location = new Point(20, 575);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnConfirm.Size = new Size(269, 45);
            btnConfirm.TabIndex = 0;
            btnConfirm.Text = "Continue";
            btnConfirm.Click += btnConfirm_Click;
            // 
            // grpPrice
            // 
            grpPrice.BorderRadius = 10;
            grpPrice.Controls.Add(guna2HtmlLabel1);
            grpPrice.Controls.Add(lblBasePriceValue);
            grpPrice.Controls.Add(lblTaxValue);
            grpPrice.Controls.Add(lblDivider);
            grpPrice.Controls.Add(lblTotalValue);
            grpPrice.CustomizableEdges = customizableEdges3;
            grpPrice.Font = new Font("Segoe UI", 9F);
            grpPrice.ForeColor = Color.FromArgb(125, 137, 149);
            grpPrice.Location = new Point(23, 173);
            grpPrice.Name = "grpPrice";
            grpPrice.Padding = new Padding(5);
            grpPrice.ShadowDecoration.CustomizableEdges = customizableEdges4;
            grpPrice.Size = new Size(269, 140);
            grpPrice.TabIndex = 1;
            grpPrice.Text = "Price Summary";
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Location = new Point(10, 61);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(102, 17);
            guna2HtmlLabel1.TabIndex = 4;
            guna2HtmlLabel1.Text = "WindowSeat: $0.00";
            // 
            // lblBasePriceValue
            // 
            lblBasePriceValue.BackColor = Color.Transparent;
            lblBasePriceValue.Location = new Point(10, 40);
            lblBasePriceValue.Name = "lblBasePriceValue";
            lblBasePriceValue.Size = new Size(60, 17);
            lblBasePriceValue.TabIndex = 0;
            lblBasePriceValue.Text = "Base: $0.00";
            // 
            // lblTaxValue
            // 
            lblTaxValue.BackColor = Color.Transparent;
            lblTaxValue.Location = new Point(10, 82);
            lblTaxValue.Name = "lblTaxValue";
            lblTaxValue.Size = new Size(54, 17);
            lblTaxValue.TabIndex = 1;
            lblTaxValue.Text = "Tax: $0.00";
            // 
            // lblDivider
            // 
            lblDivider.BackColor = Color.Transparent;
            lblDivider.Location = new Point(10, 93);
            lblDivider.Name = "lblDivider";
            lblDivider.Size = new Size(87, 17);
            lblDivider.TabIndex = 2;
            lblDivider.Text = "──────────────";
            // 
            // lblTotalValue
            // 
            lblTotalValue.BackColor = Color.Transparent;
            lblTotalValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalValue.ForeColor = Color.DarkCyan;
            lblTotalValue.Location = new Point(10, 113);
            lblTotalValue.Name = "lblTotalValue";
            lblTotalValue.Size = new Size(75, 19);
            lblTotalValue.TabIndex = 3;
            lblTotalValue.Text = "Total: $0.00";
            // 
            // grpSelectedSeat
            // 
            grpSelectedSeat.BorderRadius = 10;
            grpSelectedSeat.Controls.Add(lblSeatKey);
            grpSelectedSeat.Controls.Add(lblSeatValue);
            grpSelectedSeat.Controls.Add(lblClassKey);
            grpSelectedSeat.Controls.Add(lblClassValue);
            grpSelectedSeat.Controls.Add(lblStatusKey);
            grpSelectedSeat.Controls.Add(lblStatusValue);
            grpSelectedSeat.CustomizableEdges = customizableEdges5;
            grpSelectedSeat.Font = new Font("Segoe UI", 9F);
            grpSelectedSeat.ForeColor = Color.FromArgb(125, 137, 149);
            grpSelectedSeat.Location = new Point(20, 20);
            grpSelectedSeat.Name = "grpSelectedSeat";
            grpSelectedSeat.Padding = new Padding(5);
            grpSelectedSeat.ShadowDecoration.CustomizableEdges = customizableEdges6;
            grpSelectedSeat.Size = new Size(269, 140);
            grpSelectedSeat.TabIndex = 2;
            grpSelectedSeat.Text = "Selected Seat";
            // 
            // lblSeatKey
            // 
            lblSeatKey.BackColor = Color.Transparent;
            lblSeatKey.Font = new Font("Segoe UI", 9F);
            lblSeatKey.Location = new Point(10, 40);
            lblSeatKey.Name = "lblSeatKey";
            lblSeatKey.Size = new Size(28, 17);
            lblSeatKey.TabIndex = 0;
            lblSeatKey.Text = "Seat:";
            // 
            // lblSeatValue
            // 
            lblSeatValue.BackColor = Color.Transparent;
            lblSeatValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSeatValue.Location = new Point(100, 40);
            lblSeatValue.Name = "lblSeatValue";
            lblSeatValue.Size = new Size(8, 17);
            lblSeatValue.TabIndex = 1;
            lblSeatValue.Text = "-";
            // 
            // lblClassKey
            // 
            lblClassKey.BackColor = Color.Transparent;
            lblClassKey.Location = new Point(10, 65);
            lblClassKey.Name = "lblClassKey";
            lblClassKey.Size = new Size(33, 17);
            lblClassKey.TabIndex = 2;
            lblClassKey.Text = "Class:";
            // 
            // lblClassValue
            // 
            lblClassValue.BackColor = Color.Transparent;
            lblClassValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblClassValue.Location = new Point(100, 65);
            lblClassValue.Name = "lblClassValue";
            lblClassValue.Size = new Size(8, 17);
            lblClassValue.TabIndex = 3;
            lblClassValue.Text = "-";
            // 
            // lblStatusKey
            // 
            lblStatusKey.BackColor = Color.Transparent;
            lblStatusKey.Location = new Point(10, 90);
            lblStatusKey.Name = "lblStatusKey";
            lblStatusKey.Size = new Size(38, 17);
            lblStatusKey.TabIndex = 4;
            lblStatusKey.Text = "Status:";
            // 
            // lblStatusValue
            // 
            lblStatusValue.BackColor = Color.Transparent;
            lblStatusValue.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStatusValue.Location = new Point(100, 90);
            lblStatusValue.Name = "lblStatusValue";
            lblStatusValue.Size = new Size(8, 17);
            lblStatusValue.TabIndex = 5;
            lblStatusValue.Text = "-";
            // 
            // flowSeats
            // 
            flowSeats.AutoScroll = true;
            flowSeats.BackColor = Color.White;
            flowSeats.Dock = DockStyle.Fill;
            flowSeats.Location = new Point(20, 20);
            flowSeats.Margin = new Padding(20);
            flowSeats.Name = "flowSeats";
            flowSeats.Size = new Size(1044, 640);
            flowSeats.TabIndex = 1;
            flowSeats.WrapContents = false;
            flowSeats.Paint += flowSeats_Paint;
            // 
            // passengerDetails1
            // 
            passengerDetails1.Dock = DockStyle.Fill;
            passengerDetails1.Location = new Point(0, 0);
            passengerDetails1.Name = "passengerDetails1";
            passengerDetails1.Size = new Size(1084, 750);
            passengerDetails1.TabIndex = 2;
            // 
            // BookingPage
            // 
            BackColor = Color.White;
            ClientSize = new Size(1084, 750);
            Controls.Add(panelMain);
            Controls.Add(panelHeader);
            Controls.Add(passengerDetails1);
            MaximizeBox = false;
            Name = "BookingPage";
            Load += BookingPage_Load;
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelMain.ResumeLayout(false);
            panelSummary.ResumeLayout(false);
            grpPrice.ResumeLayout(false);
            grpPrice.PerformLayout();
            grpSelectedSeat.ResumeLayout(false);
            grpSelectedSeat.PerformLayout();
            ResumeLayout(false);
        }

        private Booking_pages.PassengerDetails passengerDetails1;
        private Guna2HtmlLabel guna2HtmlLabel1;
    }
}
