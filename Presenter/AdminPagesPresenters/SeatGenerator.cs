using Airport_Airplane_management_system.Model.Core.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airport_Airplane_management_system.Presenter.AdminPages
{
    public static class SeatGenerator
    {
        public static void GetFixedCounts(string type, out int total, out int eco, out int biz, out int first)
        {
            total = eco = biz = first = 0;

            string t = (type ?? "").Trim().ToLowerInvariant();

            if (t.Contains("highlevel") || t.Contains("boeing") || t.Contains("777"))
            {
                first = 16; biz = 48; eco = 252;
                total = first + biz + eco;
                return;
            }

            if (t.Contains("a320") || t.Contains("midrange"))
            {
                first = 0; biz = 32; eco = 138;
                total = first + biz + eco; // 170
                return;
            }

            if (t.Contains("private"))
            {
                first = 7; biz = 0; eco = 0;
                total = 7;
                return;
            }
        }

        public static List<Seat> BuildSeats(string type)
        {
            var seats = new List<Seat>();
            string t = (type ?? "").Trim().ToLowerInvariant();

            if (t.Contains("highlevel") || t.Contains("boeing") || t.Contains("777"))
            {
                // First: rows 1-4, A-D
                AddRows(seats, startRow: 1, rowCount: 4, letters: "ABCD", classType: "First Class");

                // Business: rows 5-12, A-F
                AddRows(seats, startRow: 5, rowCount: 8, letters: "ABCDEF", classType: "Business");

                // Economy: rows 13-40, A-I
                AddRows(seats, startRow: 13, rowCount: 28, letters: "ABCDEFGHI", classType: "Economy");

                return seats;
            }

            if (t.Contains("a320") || t.Contains("midrange"))
            {
                // Business: rows 1-8, A-D
                AddRows(seats, startRow: 1, rowCount: 8, letters: "ABCD", classType: "Business");

                // Economy: rows 9-31, A-F
                AddRows(seats, startRow: 9, rowCount: 23, letters: "ABCDEF", classType: "Economy");

                return seats;
            }

            if (t.Contains("private"))
            {
                // VIP: 7 seats on row 1: A-G
                foreach (char c in "ABCDEFG")
                {
                    string seatNum = $"1{c}";
                    seats.Add(new Seat(seatNum, "VIP"));
                }
                return seats;
            }

            // fallback: empty (or you can throw)
            return seats;
        }

        private static void AddRows(List<Seat> seats, int startRow, int rowCount, string letters, string classType)
        {
            for (int r = 0; r < rowCount; r++)
            {
                int rowNumber = startRow + r;
                foreach (char c in letters)
                {
                    string seatNum = $"{rowNumber}{c}";
                    seats.Add(new Seat(seatNum, classType));
                }
            }
        }
    }
}
