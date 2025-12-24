using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.View.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Presenter
{
    public class UserFlightCardPresenter
    {
        private readonly UserFlightCard _view;
        private readonly Flight _flight;
        private bool _expanded;

        public UserFlightCardPresenter(UserFlightCard view, Flight flight)
        {
            _view = view;
            _flight = flight;

            _view.Bind(flight);
            _view.Clicked += Toggle;

            CalculateSeats();
        }

        private void Toggle()
        {
            _expanded = !_expanded;
            _view.Expand(_expanded);
        }

        private void CalculateSeats()
        {
            int total = _flight.FlightSeats.Count;
            int available = _flight.FlightSeats.Count(s => !s.IsBooked);

            _view.SetSeatInfo(total, available);
        }
    }

}
