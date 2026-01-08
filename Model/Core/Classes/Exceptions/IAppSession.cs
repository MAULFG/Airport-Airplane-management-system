using Airport_Airplane_management_system.Model.Core.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Model.Core.Classes.Exceptions
{
    public interface IAppSession
    {
        User CurrentUser { get; }
        List<Flight> Flights { get;  }
        List<Plane> Planes { get;  }

        bool IsLoggedIn { get; }

        void SetUser(User user);
        void SetFlights(List<Flight> flights);
        void SetPlanes(List<Plane> planes);
        void Clear();
        event Action UserLoggedIn;
        List<PassengerSummaryRow> PassengersSummary { get; set; }
    }
}
