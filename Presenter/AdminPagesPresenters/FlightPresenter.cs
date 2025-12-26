using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Presenter.AdminPagesPresenters
{
    public class FlightPresenter
    {
        private readonly FlightService _service;

        public FlightPresenter(FlightService service)
        {
            _service = service;
        }
        public List<Flight> GetFlights() => _service.GetFlights();
        
        public void LoadSeatsForFlight(Flight flight) => _service.LoadSeatsForFlight(flight);
       
        public int CountUpcomingFlights()
        {
            return _service.GetUpcomingFlightsNotFullyBooked();
        }

    }


}
