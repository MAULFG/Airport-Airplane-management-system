using System;
using System.Collections.Generic;
using System.Text;
using Airport_Airplane_management_system.Model.Core.Classes.Users;

namespace Airport_Airplane_management_system.Model.Core.Classes.Seats
{
    public class FlightSeats
    {
        public int Id { get; set; }               // FlightSeat ID in DB
        public int FlightId { get; set; }         // Associated flight
        public int PlaneSeatIndex { get; set; }   // Index in plane's seat list
        public int? UserId { get; private set; }  // Nullable passenger ID

        public bool IsBooked { get; private set; }
        public User Passenger { get; private set; }

        public string SeatNumber { get; private set; }
        public string ClassType { get; private set; }

        // Constructor for creating from Plane.Seat
        public FlightSeats(Seat planeSeat, int planeSeatIndex)
        {
            SeatNumber = planeSeat.SeatNumber;
            ClassType = planeSeat.ClassType;
            PlaneSeatIndex = planeSeatIndex;
            IsBooked = false;
            UserId = null;
        }

        // Constructor for loading from DB
        public FlightSeats(int id, int flightId, int planeSeatIndex, string seatNumber, string classType, bool isBooked, int? userId)
        {
            Id = id;
            FlightId = flightId;
            PlaneSeatIndex = planeSeatIndex;
            SeatNumber = seatNumber;
            ClassType = classType;
            IsBooked = isBooked;
            UserId = userId;
        }

        // Assign a passenger to the seat
        public void AssignPassenger(User user)
        {
            Passenger = user;
            IsBooked = true;
            UserId = user?.UserID;
        }

        // Release the seat
        public void ReleaseSeat()
        {
            Passenger = null;
            IsBooked = false;
            UserId = null;
        }
    }
}
