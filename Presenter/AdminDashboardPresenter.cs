using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Presenter
{
    public class AdminDashboardPresenter
    {

        private readonly IAdminDashboardView _view;
        private readonly IFlightRepository _flightRepo;
        private readonly IBookingRepository _bookingRepo;
        private readonly FlightService _flightService;
        private readonly BookingService _bookingService;

        public AdminDashboardPresenter(
            IAdminDashboardView view,
            FlightService flightService,
            BookingService bookingService)
        {
            _view = view;
            _flightService = flightService;
            _bookingService = bookingService;
        }

       

        public void CancelFlight(int flightId)
        {
            bool success = _flightService.CancelFlight(flightId, out string error);
            if (success)
                _view.ShowMessage("Flight canceled successfully.");
            else
                _view.ShowMessage("Failed to cancel flight: " + error);
        }


     


        public void SearchFlights(string from = null, string to = null,
                          int? year = null, int? month = null, int? day = null)
        {
            var flights = _flightService.SearchFlights(from, to, year, month, day);
            _view.DisplaySearchResults(flights);
        }

    }
}
