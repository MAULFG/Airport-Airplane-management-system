using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Model.Core.Classes
{
    public class HighLevel : Plane
    {
        public HighLevel(int id, string status)
            : base(id, "HighLevel", "Boeing 777-300ER", status)
        {
        }

        public override void GenerateSeats()
        {
            Seats.Clear();
            AddSeats("First", 1, 4, 4);       // 16
            AddSeats("Business", 5, 12, 6);   // 48
            AddSeats("Economy", 13, 40, 9);   // 252
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
