using Airport_Airplane_management_system.Model.Core.Classes.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using Airport_Airplane_management_system.Model.Repositories;
using Airport_Airplane_management_system.Model.Services;
using Airport_Airplane_management_system.Presenter.AdminPages;
using Airport_Airplane_management_system.Repositories;
using Airport_Airplane_management_system.View.Interfaces;
using System;
using System.Linq;
using Ticket_Booking_System_OOP.Model.Repositories;

namespace Airport_Airplane_management_system.Presenter.AdminPagesPresenters
{
    public class AdminDashboardPresenter
    {
        private readonly IAdminDashboardView _view;
        private readonly INavigationService _navigation;
        private readonly IAppSession _session;

        private readonly IFlightRepository _flightRepo;
        private readonly IPlaneRepository _planeRepo;
        private readonly ICrewRepository _crewRepo;
        private readonly IPassengerRepository _passRepo;

        private readonly MainAPresenter _mainPresenter;

        // lazy-loaded child presenters
        private CrewManagementPresenter _crewPresenter;
        private FlightManagementPresenter _flightPresenter;
        private PassengerManagementPresenter _passengerPresenter;
        private PlaneManagementPresenter _planePresenter;
        private ReportsPresenter _reportsPresenter;

        public AdminDashboardPresenter(IAdminDashboardView view,INavigationService navigation, IAppSession session)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
            _session = session ?? throw new ArgumentNullException(nameof(session));

            _flightRepo = new MySqlFlightRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            _planeRepo = new MySqlPlaneRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            _crewRepo = new MySqlCrewRepository("server=localhost;port=3306;database=user;user=root;password=2006");
            _passRepo = new MySqlPassengerRepository("server=localhost;port=3306;database=user;user=root;password=2006");

            _mainPresenter = new MainAPresenter(_view.MainAView, _flightRepo, _planeRepo, _crewRepo, _passRepo);

            HookMenu();
            HookMainACards();

            // default
            ShowMain();
        }

        public void ShowMain()
        {
            _mainPresenter.RefreshData();
            _view.MainA();
        }

        private void HookMenu()
        {
            _view.MainAClicked += (_, __) => ShowMain();

            _view.FlightManagementClicked += (_, __) =>
            {
                EnsureFlightPresenter();
                _flightPresenter.RefreshData();
                _view.FlightMangement();
            };

            _view.PlaneManagementClicked += (_, __) =>
            {
                EnsurePlanePresenter();
                _planePresenter.RefreshData();
                _view.PlaneMangement();
            };

            _view.CrewManagementClicked += (_, __) =>
            {
                EnsureCrewPresenter();
                _crewPresenter.RefreshData();
                _view.CrewMangement();
            };

            _view.PassengerManagementClicked += (_, __) =>
            {
                EnsurePassengerPresenter();
                _passengerPresenter.RefreshData();
                _view.PassengerMangement();
            };

            _view.ReportsClicked += (_, __) =>
            {
                EnsureReportsPresenter();
                _reportsPresenter.RefreshData();
                _view.Report();
            };

            _view.LogoutAClicked += (_, __) =>
            {
                _session.Clear();
                _view.Logout();              // UI clear only
                _navigation.NavigateToLogin();
            };
        }

        private void HookMainACards()
        {
            _view.MainAView.GoToFlightsRequested += () =>
            {
                EnsureFlightPresenter();
                _flightPresenter.RefreshData();
                _view.FlightMangement();
            };

            _view.MainAView.GoToPlanesRequested += () =>
            {
                EnsurePlanePresenter();
                _planePresenter.RefreshData();
                _view.PlaneMangement();
            };

            _view.MainAView.GoToCrewRequested += () =>
            {
                EnsureCrewPresenter();
                _crewPresenter.RefreshData();
                _view.CrewMangement();
            };

            _view.MainAView.GoToPassengersRequested += () =>
            {
                EnsurePassengerPresenter();
                _passengerPresenter.RefreshData();
                _view.PassengerMangement();
            };
        }

        private void EnsureCrewPresenter()
        {
            if (_crewPresenter != null) return;
            _crewPresenter = new CrewManagementPresenter(_view.CrewManagementView);
        }

        private void EnsureFlightPresenter()
        {
            if (_flightPresenter != null) return;
            _flightPresenter = new FlightManagementPresenter(
                _view.FlightManagementView,
                openCrewForFlight: null,
                openScheduleForPlane: OpenPlaneScheduleFromFlightPage
            );
        }

        private void EnsurePlanePresenter()
        {
            if (_planePresenter != null) return;
            _planePresenter = new PlaneManagementPresenter(
                _view.PlaneManagementView,
                OpenPlaneScheduleFromPlanePage
            );
        }

        private void EnsurePassengerPresenter()
        {
            if (_passengerPresenter != null) return;
            _passengerPresenter = new PassengerManagementPresenter(
                _view.PassengerManagementView,
                () => _flightRepo.CountUpcomingFlightsNotFullyBooked()
            );
        }

        private void EnsureReportsPresenter()
        {
            if (_reportsPresenter != null) return;
            _reportsPresenter = new ReportsPresenter(_view.ReportsView);
        }

        private void OpenPlaneScheduleFromPlanePage(int planeId)
        {
            var plane = _planeRepo.GetAllPlanes().FirstOrDefault(p => p.PlaneID == planeId);
            if (plane == null)
            {
                _view.ShowError("Plane not found.");
                return;
            }

            var flights = _flightRepo.GetAllFlights();
            _view.ShowPlaneScheduleOnPlanePage($"{plane.Model} Schedule", planeId, flights);
        }

        private void OpenPlaneScheduleFromFlightPage(int planeId)
        {
            var plane = _planeRepo.GetAllPlanes().FirstOrDefault(p => p.PlaneID == planeId);
            if (plane == null)
            {
                _view.ShowError("Plane not found.");
                return;
            }

            var flights = _flightRepo.GetAllFlights();
            _view.ShowPlaneScheduleOnFlightPage($"{plane.Model} Schedule", planeId, flights);
        }
    }
}
