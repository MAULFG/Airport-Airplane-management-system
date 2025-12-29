using Airport_Airplane_management_system.Model.Core.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Model.Interfaces.Exceptions
{
    public class AppSession : IAppSession
    {
        public User CurrentUser { get; private set; }
        public List<Flight> Flights { get; private set; }
        public List<Plane> Planes { get; private set; }
        public List<PassengerSummaryRow> PassengersSummary { get; set; }
        public bool IsLoggedIn => CurrentUser != null;

        public void SetUser(User user)
        {
            CurrentUser = user;
        }

        public void SetFlights(List<Flight> flights)
        {
            Flights = flights;
        }

        public void SetPlanes(List<Plane> planes)
        {
            Planes = planes;
        }

        public void Clear()
        {
            CurrentUser = null;
            Flights = null;
            Planes = null;
        }
    }
}
