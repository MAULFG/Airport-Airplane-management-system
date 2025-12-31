using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.View.Interfaces;
using System;

namespace Airport_Airplane_management_system.Presenter.AdminPagesPresenters
{
    public class AdminDashboardPresenter
    {
        private readonly IAdminDashboardView _view;
        private readonly INavigationService _nav;

        public AdminDashboardPresenter(IAdminDashboardView view, INavigationService nav)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _nav = nav ?? throw new ArgumentNullException(nameof(nav));

            _view.MainAClicked += (_, __) => _view.MainA();
            _view.FlightManagementClicked += (_, __) => _view.FlightMangement();
            _view.PlaneManagementClicked += (_, __) => _view.PlaneMangement();
            _view.CrewManagementClicked += (_, __) => _view.CrewMangement();
            _view.PassengerManagementClicked += (_, __) => _view.PassengerMangement();
            _view.ReportsClicked += (_, __) => _view.Reports();          // ✅ fixed
            _view.NotrificationAClicked += (_, __) => _view.NotrificationA();
            _view.LogoutAClicked += (_, __) => _view.Logout();
        }
    }
}
