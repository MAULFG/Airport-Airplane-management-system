using System;

namespace Airport_Airplane_management_system.Model.Core.Classes
{
    public class Passenger
    {
        public int PassengerId { get; set; }     
        public string FullName { get; set; } = "";  
        public string? Email { get; set; }       
        public string? Phone { get; set; }          
        public DateTime? MemberSince { get; set; }  
    }
}