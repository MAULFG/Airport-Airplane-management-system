using System;
using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;

namespace Airport_Airplane_management_system.Presenter.AdminPages
{
    public static class SeatGenerator
    {
        public static List<Seat> BuildSeats(string type)
        {
            return type switch
            {
                "HighLevel" => Boeing777High(),
                "A320" => AirbusA320Med(),
                "PrivateJet" => PrivateJet(),
                _ => AirbusA320Med()
            };
        }

        private static List<Seat> Boeing777High()
        {
            var list = new List<Seat>();
            int row = 1;

            AddRows(list, startRow: row, rowCount: 4, seatsPerRow: 4, classType: "First");
            row += 4;

            AddRows(list, startRow: row, rowCount: 8, seatsPerRow: 6, classType: "Business");
            row += 8;

            AddRows(list, startRow: row, rowCount: 28, seatsPerRow: 9, classType: "Economy");

            return list;
        }

        private static List<Seat> AirbusA320Med()
        {
            var list = new List<Seat>();
            int row = 1;

            AddRows(list, startRow: row, rowCount: 8, seatsPerRow: 4, classType: "Business");
            row += 8;

            AddRows(list, startRow: row, rowCount: 23, seatsPerRow: 6, classType: "Economy");

            return list;
        }

        private static List<Seat> PrivateJet()
        {
            var list = new List<Seat>();
            foreach (var L in LettersFor(7))
                list.Add(new Seat($"1{L}", "First"));
            return list;
        }

        private static void AddRows(List<Seat> list, int startRow, int rowCount, int seatsPerRow, string classType)
        {
            var letters = LettersFor(seatsPerRow);

            for (int r = 0; r < rowCount; r++)
            {
                int rowNum = startRow + r;
                foreach (var L in letters)
                {
                    // ✅ use constructor (immutable Seat)
                    list.Add(new Seat($"{rowNum}{L}", classType));
                }
            }
        }

        private static char[] LettersFor(int seatsPerRow)
        {
            return seatsPerRow switch
            {
                4 => new[] { 'A', 'B', 'C', 'D' },
                6 => new[] { 'A', 'B', 'C', 'D', 'E', 'F' },
                7 => new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G' },
                9 => new[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J' }, // skip I
                _ => throw new ArgumentException("Unsupported seatsPerRow: " + seatsPerRow)
            };
        }
    }
}
