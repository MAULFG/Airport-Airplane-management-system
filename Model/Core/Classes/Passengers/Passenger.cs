using System;

namespace Airport_Airplane_management_system.Model.Core.Classes
{
    public class Passenger
    {
        public int PassengerId { get; set; }        // passengers.passenger_id
        public string FullName { get; set; } = "";  // passengers.full_name
        public string? Email { get; set; }          // passengers.email
        public string? Phone { get; set; }          // passengers.phone
        public DateTime? MemberSince { get; set; }  // passengers.member_since
    }
}