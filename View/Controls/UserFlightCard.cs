using System;
using System.Windows.Forms;
using Airport_Airplane_management_system.Model.Core.Classes;

namespace Airport_Airplane_management_system.View.Controls
{
    public partial class UserFlightCard : UserControl
    {
        public event Action Clicked;

        public UserFlightCard()
        {
            InitializeComponent();
            pnlHeader.Click += (s, e) => Clicked?.Invoke();
            this.Click += (s, e) => Clicked?.Invoke();
        }

        public void Bind(Flight flight)
        {
            lblRoute.Text = $"{flight.From} → {flight.To}";
            lblDate.Text = flight.Departure.ToString("yyyy-MM-dd");
            lblFlightId.Text = $"#{flight.FlightID}";
        }

        public void SetSeatInfo(int total, int available)
        {
            lblSeats.Text = $"Seats: {available}/{total}";
        }

        public void Expand(bool expanded)
        {
            pnlDetails.Visible = expanded;
            Height = expanded ? 230 : 70;
        }
    }
}
