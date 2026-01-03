using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Airport_Airplane_management_system.Presenter.AdminPagesPresenters
{
    public class AdminDashboardPresenter
    {
        private readonly IAdminDashboardView _view;
        private readonly INavigationService _navigationService;



        public AdminDashboardPresenter(IAdminDashboardView view, INavigationService navigationService)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _navigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));

            _view.LogoutAClicked += LogoutClicked;
            _view.ReportsClicked += ReportClicked;
            _view.PlaneManagementClicked += PlaneClicked;
            _view.CrewManagementClicked += CrewClicked;
            _view.FlightManagementClicked += FlightClicked;
            _view.MainAClicked += MianClicked;
            _view.PassengerManagementClicked += PassengerClicked;
        }

        private void LogoutClicked(object sender, EventArgs e)
        {
            _view.Logout();
            _navigationService.NavigateToLogin();

        }
        private void ReportClicked(object sender, EventArgs e)
        {
            _view.Report();
        }
        private void PlaneClicked(object sender, EventArgs e)
        {
            _view.PlaneMangement();
        }
        private void CrewClicked(object sender, EventArgs e)
        {
            _view.CrewMangement();
        }
        private void FlightClicked(object sender, EventArgs e)
        {
            _view.FlightMangement();
        }
        private void MianClicked(object sender, EventArgs e)
        {
            _view.MainA();
        }

        private void PassengerClicked(object sender, EventArgs e)
        {
            _view.PassengerMangement();
        }

    }
}
