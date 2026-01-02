using System;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface IMainUserPageView
    {
        // Sets the welcome message at the top
        void SetWelcomeText(string text);

        // Clears all KPI/statistics cards (to refresh)
        void ClearStatistics();

        // Adds a KPI/statistics card
        void AddStatCard(string title, string value);


    }
}
