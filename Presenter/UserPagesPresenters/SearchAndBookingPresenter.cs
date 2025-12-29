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

            // Call the service to search flights
            var flights = _flightService?.SearchFlights(
             from: _view?.From ?? "",
               to: _view?.To ?? "",
                year: (_view?.IsDateSelected == true && _view.DepartureDate.HasValue) ? _view.DepartureDate.Value.Year : (int?)null,
               month: (_view?.IsDateSelected == true && _view.DepartureDate.HasValue) ? _view.DepartureDate.Value.Month : (int?)null,
                day: (_view?.IsDateSelected == true && _view.DepartureDate.HasValue) ? _view.DepartureDate.Value.Day : (int?)null
                 );


            // Optional: filter by class + passengers
            if (!string.IsNullOrWhiteSpace(_view.Class))
            {
                flights = flights
                    .Where(f => f.GetAvailableSeats(_view.Class).Count >= _view.Passengers)
                    .ToList();
            }

            if (!flights.Any())
            {
                _view.ShowMessage("No flights match your criteria.");
                // Already cleared above, so display is empty
                return;
            }

            // Pass the Flight objects to the view
            _view.DisplayFlights(flights);
        }
        private void OpenBookingPage(int flightId)
        {
            _userdashpresenter.OpenBookingp(flightId);
        }

    }
}
