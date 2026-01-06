using Airport_Airplane_management_system.Model.Core.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Model.Core.Classes.Exceptions
{
    public class AppSession : IAppSession
    {
        public User CurrentUser { get; set; }
        public List<Flight> Flights { get;  set; }
        public List<Plane> Planes { get; set; }
        public List<PassengerSummaryRow> PassengersSummary { get; set; }
        public bool IsLoggedIn => CurrentUser != null;
        public event Action UserLoggedIn;
        public void SetUser(User user)
        {
            CurrentUser = user;
            UserLoggedIn?.Invoke();
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
