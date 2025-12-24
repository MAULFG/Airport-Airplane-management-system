using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.View.Interfaces;

namespace Airport_Airplane_management_system.Presenter
{
    public class UpcomingFlightsPresenter
    {
        private readonly IUpcomingFlightsView _view;
        private readonly FlightService _flightService;

        public UpcomingFlightsPresenter(
            IUpcomingFlightsView view,
            FlightService flightService)
        {
            _view = view;
            _flightService = flightService;

            _view.ViewLoaded += LoadFlights;
            _view.FlightSelected += OnFlightSelected;
        }

        private void LoadFlights()
        {
            try
            {
                var flights = _flightService.GetFlights();

                foreach (var flight in flights)
                    _flightService.LoadSeatsForFlight(flight);

                _view.ShowFlights(flights);
            }
            catch (Exception ex)
            {
                _view.ShowError(ex.Message);
            }
        }

        private void OnFlightSelected(Flight flight)
        {
            // Navigation, booking page, etc.
            Console.WriteLine($"Flight selected: {flight.FlightID}");
        }
    }

}
