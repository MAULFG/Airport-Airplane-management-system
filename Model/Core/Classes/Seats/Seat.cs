using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Model.Core.Classes.Seats
{
    public class Seat
    {
        public string SeatNumber { get; private set; }
        public string ClassType { get; private set; }

        public Seat(string seatNumber, string classType)
        {
            SeatNumber = seatNumber;
            ClassType = classType;
        }
    }
}
