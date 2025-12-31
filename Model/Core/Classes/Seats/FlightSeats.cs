using System;

namespace Airport_Airplane_management_system.Model.Core.Classes
{
    public class FlightSeats
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public int PlaneSeatIndex { get; set; }
        public int? UserId { get; private set; }   // maps to passenger_id
        public decimal SeatPrice { get; private set; }

        public bool IsBooked { get; private set; }
        public User Passenger { get; private set; }

        public string SeatNumber { get; private set; }
        public string ClassType { get; private set; }

        public FlightSeats(Seat planeSeat, int planeSeatIndex)
        {
            SeatNumber = planeSeat.SeatNumber;
            ClassType = planeSeat.ClassType;
            PlaneSeatIndex = planeSeatIndex;
            IsBooked = false;
            UserId = null;
            SeatPrice = 0m;
        }

        public FlightSeats(int id, int flightId, int planeSeatIndex, string seatNumber, string classType,
                           bool isBooked, int? userId, decimal seatPrice)
        {
            Id = id;
            FlightId = flightId;
            PlaneSeatIndex = planeSeatIndex;
            SeatNumber = seatNumber;
            ClassType = classType;
            IsBooked = isBooked;
            UserId = userId;
            SeatPrice = seatPrice;
        }

        public void AssignPassenger(User user)
        {
            Passenger = user;
            IsBooked = true;
            UserId = user?.UserID;
        }

        public void ReleaseSeat()
        {
            Passenger = null;
            IsBooked = false;
            UserId = null;
        }
    }
}
