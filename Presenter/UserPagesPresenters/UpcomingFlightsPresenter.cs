using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.View.Forms.UserPages;
using System;

namespace Airport_Airplane_management_system.Presenter.UserPagesPresenters
{
    public class UpcomingFlightsPresenter
    {
        private readonly UpcomingFlights _view;
        private readonly FlightService _service;

        public UpcomingFlightsPresenter(UpcomingFlights view, FlightService service)
        {
            _view = view;
            _service = service;

            _view.LoadFlightsRequested += OnLoadFlights;
        }

        private void OnLoadFlights(object sender, EventArgs e)
        {
            var flights = _service.LoadFlightsWithSeats();
            _view.LoadFlights(flights);
        }
    }
}
