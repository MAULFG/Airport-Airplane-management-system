using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Model.Core.Classes
{
    public class PrivateJet : Plane
    {
        public PrivateJet(int id, string status)
            : base(id, "PrivateJet", "Gulfstream G650", status)
        {
        }

        public override void GenerateSeats()
        {
            Seats.Clear();
            AddSeats("VIP", 1, 4, 2); 

            Seats.Clear();
            for (int i = 1; i <= 4; i++)
                Seats.Add(new Seat($"{i}A", "VIP"));
            for (int i = 1; i <= 3; i++)
                Seats.Add(new Seat($"{i}B", "VIP"));
        }

        private void AddSeats(string classType, int startRow, int endRow, int seatsPerRow)
        {
            for (int row = startRow; row <= endRow; row++)
            {
                for (int seat = 0; seat < seatsPerRow; seat++)
                {
                    char letter = (char)('A' + seat);
                    Seats.Add(new Seat($"{row}{letter}", classType));
                }
            }
        }
    }
}
