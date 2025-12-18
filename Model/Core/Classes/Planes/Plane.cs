using Airport_Airplane_management_system.Model.Core.Classes.Seats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Model.Core.Classes.Planes
{
    public abstract class Plane
    {
        public int PlaneID { get; set; }
        public string Model { get; set; }
        public string Status { get; set; }

        public List<Seat> Seats { get; set; } = new List<Seat>();

        protected Plane(int id, string model, string status)
        {
            PlaneID = id;
            Model = model;
            Status = status;
        }

        // Each derived plane type creates its own seats
        public abstract void GenerateSeats();
    }
}
