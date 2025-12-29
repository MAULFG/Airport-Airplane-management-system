using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.View.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.View.Forms.UserPages.Booking_pages
{
    public partial class PassengerDetails : UserControl, IPassengerDetailsView
    {

        public PassengerDetails()
        {
            InitializeComponent();

            // Wire UI → Events

            btnConfirm.Click += (s, e) => CompleteBookingClicked?.Invoke();
            guna2Button1.Click += (s, e) => CancelClicked?.Invoke();
        }

        // EVENTS
        public event Action CompleteBookingClicked;
        public event Action CancelClicked;

        // PASSENGER INPUT (Presenter reads these)
        public string FirstName => txtFullName.Text.Trim();
        public string MiddleName => guna2TextBox1.Text.Trim();
        public string LastName => guna2TextBox2.Text.Trim();
        public string Email => txtEmail.Text.Trim();
        public string Phone => txtPhone.Text.Trim();

        public string FullName
        {
            get
            {
                return string.Join(" ",
                    new[] { FirstName, MiddleName, LastName }
                    .Where(n => !string.IsNullOrWhiteSpace(n)));
            }
        }

        // DISPLAY METHODS (Presenter controls UI)
        public void ShowSelectedSeat(FlightSeats seat)
        {
            lblSeatValue.Text = $"Seat: {seat.SeatNumber}";
            lblClassValue.Text = $"Class: {seat.ClassType}";
            lblStatusValue.Text = seat.IsBooked ? "Booked" : "Available";
        }

        public void ShowPrice(decimal basePrice, decimal tax, decimal total)
        {
            lblBasePriceValue.Text = $"Base: ${basePrice:F2}";
            lblTaxValue.Text = $"Tax: ${tax:F2}";
            lblTotalValue.Text = $"Total: ${total:F2}";
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Passenger Details",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void CloseView()
        {
            this.Parent?.Controls.Remove(this);
            this.Dispose();
        }


        private void panelSummary_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblFlightInfo_Click(object sender, EventArgs e)
        {

        }

        private void panelForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
