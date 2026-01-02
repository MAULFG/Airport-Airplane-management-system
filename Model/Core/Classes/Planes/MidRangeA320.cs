using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Model.Core.Classes
{
    public class MidRangeA320 : Plane
    {
        public MidRangeA320(int id, string status)
             : base(id, "A320", "Airbus A320", status)
        {
        }

        public override void GenerateSeats()
        {
            Seats.Clear();

            // Business → Rows 1–4 (1A–4D) => 16 (BUT your fixed config expects 32 business)
            // You said fixed config is 8×4 Business = 32. That means rows 1–8, 4 seats each.
            AddSeats("Business", 1, 8, 4);  // 32

            // Economy → 23×6 = 138 means rows 9–31, 6 seats each
            AddSeats("Economy", 9, 31, 6);  // 138
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
