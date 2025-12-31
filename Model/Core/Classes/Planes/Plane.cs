using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Model.Core.Classes
{
    public abstract class Plane
    {
        public int PlaneID { get; set; }

        // ✅ DB type / configuration key: "HighLevel", "A320", "PrivateJet"
        public string Type { get; set; }

        // ✅ Display/name/model text (you can store what you want in DB column "model")
        public string Model { get; set; }

        public string Status { get; set; }

        public List<Seat> Seats { get; set; } = new List<Seat>();

        protected Plane(int id, string type, string model, string status)
        {
            PlaneID = id;
            Type = type ?? "";
            Model = model ?? "";
            Status = status ?? "";
        }

        // Each derived plane type creates its own seats
        public abstract void GenerateSeats();
    }
}
