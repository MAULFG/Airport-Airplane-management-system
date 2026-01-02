using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Presenter.UserPagesPresenters;
using Airport_Airplane_management_system.View.Forms.UserPages;
using Airport_Airplane_management_system.View.Interfaces;
using System;
using System.Configuration;
using System.Linq;

namespace Airport_Airplane_management_system.Presenter
{
    public class SearchAndBookingPresenter
    {
        private readonly ISearchAndBookingView _view;
        private readonly FlightService _flightService;
        private readonly INavigationService _navigation;
        private readonly UserDashboardPresenter _userdashpresenter;

        public SearchAndBookingPresenter( ISearchAndBookingView view,FlightService flightService,INavigationService navigation,UserDashboardPresenter userDashboardPresenter)  // Add this parameter
        {
            _view = view;
            _flightService = flightService;
            _navigation = navigation;
            _userdashpresenter = userDashboardPresenter;  // Assign it

            _view.BookFlightRequested += OpenBookingPage;
            _view.SearchClicked += OnSearchClicked;
        }

        private void OnSearchClicked(object sender, EventArgs e)
        {
            // Clear the view first
            _view.DisplayFlights(new List<Flight>()); // clear existing cards

            // Load flights WITH seats
            var flights = _flightService.LoadFlightsWithSeats().AsEnumerable();

            // Filter by "From" and "To"
            if (!string.IsNullOrWhiteSpace(_view.From))
                flights = flights.Where(f => f.From.Equals(_view.From, StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(_view.To))
                flights = flights.Where(f => f.To.Equals(_view.To, StringComparison.OrdinalIgnoreCase));

            // Filter by date if selected
            if (_view.IsDateSelected && _view.DepartureDate.HasValue)
            {
                var date = _view.DepartureDate.Value.Date;
                flights = flights.Where(f => f.Departure.Date == date);
            }

            // Filter by class + passengers
            if (!string.IsNullOrWhiteSpace(_view.Class))
            {
                flights = flights.Where(f => f.GetAvailableSeats(_view.Class).Count >= _view.Passengers);
            }

            var finalFlights = flights.ToList();

            if (!finalFlights.Any())
            {
                _view.ShowMessage("No flights match your criteria.");
                return;
            }

            // Pass the Flight objects to the view
            _view.DisplayFlights(finalFlights);
        }


        private void OpenBookingPage(int flightId)
        {
            _userdashpresenter.OpenBookingp(flightId);
        }

    }
}
