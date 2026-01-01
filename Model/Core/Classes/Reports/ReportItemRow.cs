namespace Airport_Airplane_management_system.Model.Core.Classes
{
    public class ReportItemRow
    {
        public string Title { get; set; } = "";
        public string SubTitle { get; set; } = "";
        public string BadgeText { get; set; } = "";
        public bool IsWarning { get; set; }

        public string TargetPageKey { get; set; } = "";

        // ✅ identify the entity
        public int? PlaneId { get; set; }           // for planes
        public string? CrewEmployeeId { get; set; } // for crew
    }
}
