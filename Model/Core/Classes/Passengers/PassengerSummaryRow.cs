using System;

namespace Airport_Airplane_management_system.Model.Core.Classes
{
    public class PassengerSummaryRow
    {
        public int PassengerId { get; set; }
        public string FullName { get; set; } = "";
        public string? Email { get; set; }
        public string? Phone { get; set; }

        public DateTime? CreatedAt { get; set; }

        public int UpcomingCount { get; set; }
        public int PastCount { get; set; }
        public int TotalCount { get; set; }
    }
}