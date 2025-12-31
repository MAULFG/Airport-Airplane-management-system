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
            AddSeats("VIP", 1, 4, 2); // 8 seats if rows 1-4 with 2 each
            // But your fixed config says 7 seats.
            // We'll generate exactly 7 VIP seats instead of 8 to match your UI config.

            Seats.Clear();
            for (int i = 1; i <= 7; i++)
                Seats.Add(new Seat($"V{i}", "VIP"));
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
