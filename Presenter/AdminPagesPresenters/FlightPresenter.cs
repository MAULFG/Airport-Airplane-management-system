using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Services;
using System.Collections.Generic;

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

        public int CountUpcomingFlights() => _service.GetUpcomingFlightsNotFullyBooked();

        public HashSet<string> GetSeatClassesForPlane(int planeId)
            => _service.GetSeatClassesForPlane(planeId);

        public bool AddFlight(
            Flight flight,
            decimal economyPrice,
            decimal businessPrice,
            decimal firstPrice,
            out int newId,
            out string error)
        {
            return _service.AddFlight(flight, economyPrice, businessPrice, firstPrice, out newId, out error);
        }
    }
}
