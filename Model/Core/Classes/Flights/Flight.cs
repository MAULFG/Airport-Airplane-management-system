using Airport_Airplane_management_system.Model.Core.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using CrewEntity = Airport_Airplane_management_system.Model.Core.Classes.Crew;

namespace Airport_Airplane_management_system.Model.Core.Classes
{
    public class Flight
    {
        public int FlightID { get; set; }
        public Plane? Plane { get; set; } 
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Departure { get; set; }
        public DateTime Arrival { get; set; }

        // Crew assigned to this flight
        public List<CrewEntity> CrewList { get; set; } = new List<CrewEntity>();

        // Prices per category (Economy, Business, etc.)
        public Dictionary<string, decimal> CategoryPrices { get; set; } = new Dictionary<string, decimal>();

        // Seats specific to this flight
        public List<FlightSeats> FlightSeats { get; set; } = new List<FlightSeats>();

        public Flight(int flightID, Plane? plane, string from, string to,
              DateTime departure, DateTime arrival,
              Dictionary<string, decimal> categoryPrices)
        {
            FlightID = flightID;
            Plane = plane;
            From = from;
            To = to;
            Departure = departure;
            Arrival = arrival;
            CategoryPrices = categoryPrices;
        }



        public List<FlightSeats> GetAvailableSeats(string category)
        {
            return FlightSeats
                .Where(s => s.ClassType == category && !s.IsBooked)
                .ToList();
        }

        // Optional: business logic
        public decimal GetSeatPrice(string category)
        {
            return CategoryPrices.ContainsKey(category) ? CategoryPrices[category] : 0;
        }
    }
}
