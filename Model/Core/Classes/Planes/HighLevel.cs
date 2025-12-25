using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Model.Core.Classes
{
    public class HighLevel : Plane
    {
        public HighLevel(int id, string status)
            : base(id, "Boeing 777-300ER", status)
        {
        }

        public override void GenerateSeats()
        {
            AddSeats("First", 1, 4, 4);       // First Class
            AddSeats("Business", 5, 12, 6);   // Business Class
            AddSeats("Economy", 13, 40, 9);   // Economy
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
