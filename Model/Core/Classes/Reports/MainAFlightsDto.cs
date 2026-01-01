using System;
using System.Collections.Generic;

namespace Airport_Airplane_management_system.Model.Core.Classes
{
    public class MainAFlightsDto
    {
        public List<Flight> InAir { get; set; } = new();
        public List<Flight> Upcoming48h { get; set; } = new();
        public List<Flight> Past { get; set; } = new();
        public DateTime Now { get; set; } = DateTime.Now;
    }
}
