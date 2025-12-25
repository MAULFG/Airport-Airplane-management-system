using System;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface IAdminDashboardView
    {
        event Action ViewLoaded;

        event Action DashboardClicked;
        event Action CrewManagementClicked;
        event Action FlightManagementClicked;
        event Action PlaneManagementClicked;
        event Action PassengerManagementClicked;
        event Action ReportsClicked;
        event Action AccountSettingsClicked;

        void ShowPage(Control page);

        void ShowError(string message);
        void ShowInfo(string message);
    }
}
