using Airport_Airplane_management_system.Model.Core.Classes.Flights;
using System;
using System.Collections.Generic;
using System.Text;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface IAdminDashboardView
    {
        void DisplaySearchResults(List<Flight> flights);
     
        void ShowMessage(string message);
    }
}
