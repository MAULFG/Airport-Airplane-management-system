using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Airport_Airplane_management_system.Model.Core.Classes.Flights;
using Airport_Airplane_management_system.View.Interfaces;
using Airport_Airplane_management_system.View.Controls;

namespace Airport_Airplane_management_system.View.Forms.UserPages
{
    public partial class UpcomingFlights : UserControl, IUpcomingFlightsView
    {
        public event Action ViewLoaded;
        public event Action<Flight> FlightSelected;

        public UpcomingFlights()
        {
            InitializeComponent();
            this.Load += (s, e) => ViewLoaded?.Invoke();
        }

        public void ShowFlights(IEnumerable<Flight> flights)
        {
            flowFlights.Controls.Clear();

            foreach (var flight in flights)
            {
                var card = new UserFlightCard();
                card.Bind(flight);
                card.Clicked += () => FlightSelected?.Invoke(flight);

                flowFlights.Controls.Add(card);
            }
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

}
