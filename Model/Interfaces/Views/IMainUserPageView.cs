using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface IMainUserPageView
    {
        void SetWelcomeText(string text);

        void ClearStatistics();
        void AddStatCard(string title, string value);
    }
}
