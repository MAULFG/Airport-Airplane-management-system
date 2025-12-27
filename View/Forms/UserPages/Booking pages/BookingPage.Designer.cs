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
        private Guna2HtmlLabel lblSeatDetails;
        private Guna2HtmlLabel lblPriceDetails;
        private Guna2Button btnConfirm;

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
            lblPriceDetails = new Guna2HtmlLabel();
            grpSelectedSeat = new Guna2GroupBox();
            lblSeatDetails = new Guna2HtmlLabel();
            flowSeats = new FlowLayoutPanel();
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
            panelHeader.Size = new Size(947, 70);
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
            panelMain.Size = new Size(947, 574);
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
            panelSummary.Location = new Point(618, 20);
            panelSummary.Name = "panelSummary";
            panelSummary.Padding = new Padding(20);
            panelSummary.Radius = 10;
            panelSummary.ShadowColor = Color.Black;
            panelSummary.Size = new Size(309, 534);
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
            btnConfirm.Location = new Point(20, 469);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btnConfirm.Size = new Size(269, 45);
            btnConfirm.TabIndex = 0;
            btnConfirm.Text = "Confirm Booking";
            // 
            // grpPrice
            // 
            grpPrice.BorderRadius = 10;
            grpPrice.Controls.Add(lblPriceDetails);
            grpPrice.CustomizableEdges = customizableEdges3;
            grpPrice.Font = new Font("Segoe UI", 9F);
            grpPrice.ForeColor = Color.FromArgb(125, 137, 149);
            grpPrice.Location = new Point(20, 146);
            grpPrice.Name = "grpPrice";
            grpPrice.Padding = new Padding(5);
            grpPrice.ShadowDecoration.CustomizableEdges = customizableEdges4;
            grpPrice.Size = new Size(269, 140);
            grpPrice.TabIndex = 1;
            grpPrice.Text = "Price Summary";
            // 
            // lblPriceDetails
            // 
            lblPriceDetails.BackColor = Color.Transparent;
            lblPriceDetails.Font = new Font("Segoe UI", 10F);
            lblPriceDetails.Location = new Point(5, 45);
            lblPriceDetails.Name = "lblPriceDetails";
            lblPriceDetails.Size = new Size(3, 2);
            lblPriceDetails.TabIndex = 0;
            lblPriceDetails.Text = null;
            // 
            // grpSelectedSeat
            // 
            grpSelectedSeat.BorderRadius = 10;
            grpSelectedSeat.Controls.Add(lblSeatDetails);
            grpSelectedSeat.CustomizableEdges = customizableEdges5;
            grpSelectedSeat.Font = new Font("Segoe UI", 9F);
            grpSelectedSeat.ForeColor = Color.FromArgb(125, 137, 149);
            grpSelectedSeat.Location = new Point(20, 20);
            grpSelectedSeat.Name = "grpSelectedSeat";
            grpSelectedSeat.Padding = new Padding(5);
            grpSelectedSeat.ShadowDecoration.CustomizableEdges = customizableEdges6;
            grpSelectedSeat.Size = new Size(269, 120);
            grpSelectedSeat.TabIndex = 2;
            grpSelectedSeat.Text = "Selected Seat";
            // 
            // lblSeatDetails
            // 
            lblSeatDetails.BackColor = Color.Transparent;
            lblSeatDetails.Font = new Font("Segoe UI", 10F);
            lblSeatDetails.Location = new Point(5, 45);
            lblSeatDetails.Name = "lblSeatDetails";
            lblSeatDetails.Size = new Size(3, 2);
            lblSeatDetails.TabIndex = 0;
            lblSeatDetails.Text = null;
            // 
            // flowSeats
            // 
            flowSeats.AutoScroll = true;
            flowSeats.BackColor = Color.White;
            flowSeats.Dock = DockStyle.Fill;
            flowSeats.Location = new Point(20, 20);
            flowSeats.Margin = new Padding(20);
            flowSeats.Name = "flowSeats";
            flowSeats.Size = new Size(907, 534);
            flowSeats.TabIndex = 1;
            flowSeats.WrapContents = false;
            flowSeats.Paint += flowSeats_Paint;
            // 
            // BookingPage
            // 
            BackColor = Color.White;
            ClientSize = new Size(947, 644);
            Controls.Add(panelMain);
            Controls.Add(panelHeader);
            Name = "BookingPage";
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
    }
}
