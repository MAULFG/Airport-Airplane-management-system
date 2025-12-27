using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Model.Core.Classes
{
    public class MidRangeA320 : Plane
    {
        public MidRangeA320(int id, string status)
            : base(id, "Airbus A320", status)
        {
        }

        public override void GenerateSeats()
        {
            // Business → Rows 1–8 (1A–8D)
            AddSeats("Business", 1, 8, 4);

            // Economy → Rows 9–31 (9A–31F)
            AddSeats("Economy", 9, 31, 6);
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
