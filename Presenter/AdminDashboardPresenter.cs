using System.Windows.Forms;
using Airport_Airplane_management_system.Model.Interfaces.Views;

namespace Airport_Airplane_management_system.Presenter.AdminPagesPresenters
{
    public class AdminDashboardPresenter
    {
        private readonly IAdminDashboardView _view;

        private readonly Control _dashboard;
        private readonly Control _crew;
        private readonly Control _flight;
        private readonly Control _plane;
        private readonly Control _passenger;
        private readonly Control _reports;
        private readonly Control _accountSettings;

        public AdminDashboardPresenter(
            IAdminDashboardView view,
            Control dashboard,
            Control crew,
            Control flight,
            Control plane,
            Control passenger,
            Control reports,
            Control accountSettings)
        {
            _view = view;

           // _dashboard = dashboard;
            _crew = crew;
            _flight = flight;
            _plane = plane;
            _passenger = passenger;
            _reports = reports;
            _accountSettings = accountSettings;

            HookEvents();

            // Default page
            _view.ShowPage(_dashboard);
        }

        private void HookEvents()
        {
            _view.DashboardClicked += () => _view.ShowPage(_dashboard);
            _view.CrewManagementClicked += () => _view.ShowPage(_crew);
            _view.FlightManagementClicked += () => _view.ShowPage(_flight);
            _view.PlaneManagementClicked += () => _view.ShowPage(_plane);
            _view.PassengerManagementClicked += () => _view.ShowPage(_passenger);
            _view.ReportsClicked += () => _view.ShowPage(_reports);
            _view.AccountSettingsClicked += () => _view.ShowPage(_accountSettings);
        }
    }
}
