using System;

namespace Airport_Airplane_management_system.Model.Core.Classes
{
    public class FlightSeats
    {
        public int Id { get; set; }               // FlightSeat ID in DB
        public int FlightId { get; set; }         // Associated flight
        public int PlaneSeatIndex { get; set; }   // Index in plane's seat list

        public bool IsBooked { get; private set; }
        public int? PassengerId { get; private set; }

        public string SeatNumber { get; private set; }
        public string ClassType { get; private set; }

        // Add this property
        public decimal SeatPrice { get; set; }
        // Constructor for creating from Plane.Seat
        public FlightSeats(Seat planeSeat, int planeSeatIndex)
        {
            SeatNumber = planeSeat.SeatNumber;
            ClassType = planeSeat.ClassType;
            PlaneSeatIndex = planeSeatIndex;
            IsBooked = false;
            PassengerId = null;
        }

        // Constructor for loading from DB
        public FlightSeats(int id, int flightId, int planeSeatIndex, string seatNumber, string classType, bool isBooked, int? passengerId, decimal seatPrice = 0)
        {
            Id = id;
            FlightId = flightId;
            PlaneSeatIndex = planeSeatIndex;
            SeatNumber = seatNumber;
            ClassType = classType;
            IsBooked = isBooked;
            PassengerId = passengerId;
            SeatPrice = seatPrice;
        }


        // -----------------------------
        // ASSIGN PASSENGER
        // -----------------------------
        public void AssignPassenger(User passenger)
        {
            if (passenger == null) throw new ArgumentNullException(nameof(passenger));

            PassengerId = passenger.UserID;
            IsBooked = true;
        }
        public void Book(User user)
        {
            
            AssignPassenger(user);
        }

        // -----------------------------
        // REMOVE PASSENGER / RELEASE SEAT
        // -----------------------------
        public void RemovePassenger()
        {
            PassengerId = null;
            IsBooked = false;
        }

        // helper to check if seat belongs to a passenger
        public bool IsPassenger(int userId)
        {
            return PassengerId.HasValue && PassengerId.Value == userId;
        }
    }
}
