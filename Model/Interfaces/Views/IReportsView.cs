using System;
using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface IReportsView
    {
        // View -> Presenter
        event Action ViewLoaded;
        event Action<string> SearchChanged;
        event Action<ReportItemRow> ReportCardClicked;

        // Presenter -> View
        void RenderReports(List<ReportItemRow> items);
        void SetHeaderCounts(int totalIssues);

        void ShowError(string message);
    }
}
