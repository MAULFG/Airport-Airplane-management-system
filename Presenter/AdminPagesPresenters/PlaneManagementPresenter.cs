using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using System;
using System.Collections.Generic;

namespace Airport_Airplane_management_system.Presenter.AdminPages
{
    public class PlaneManagementPresenter
    {
        private readonly IPlaneManagementView _view;
        private readonly IPlaneRepository _repo;

        // Callback from AdminDashboard to open schedule UI
        private readonly Action<int>? _openSchedule;

        public PlaneManagementPresenter(
            IPlaneManagementView view,
            IPlaneRepository repo,
            Action<int>? openSchedule = null)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _openSchedule = openSchedule;

            // Event bindings
            _view.ViewLoaded += (_, __) => LoadPlanes();
            _view.AddPlaneClicked += OnAddPlane;
            _view.DeleteRequested += OnDeletePlane;
            _view.PlaneSelected += OnPlaneSelected;
        }

        // 🔥 Public method for AdminDashboard to refresh planes every time
        public void RefreshData()
        {
            LoadPlanes();
        }

        private void OnPlaneSelected(int planeId)
        {
            if (_openSchedule == null)
            {
                _view.ShowError("Schedule action is not wired from AdminDashboard.");
                return;
            }

            _openSchedule.Invoke(planeId);
        }

        private void OnDeletePlane(int planeId)
        {
            if (!_view.Confirm($"Delete Plane #{planeId}?")) return;

            if (!_repo.DeletePlane(planeId, out var err))
            {
                _view.ShowError("Delete failed: " + err);
                return;
            }

            _view.ShowInfo("Plane deleted successfully.");
            LoadPlanes(); // refresh
        }

        private void LoadPlanes()
        {
            var planes = _repo.GetAllPlanes() ?? new List<Plane>();

            // Load seats for each plane
            foreach (var p in planes)
                p.Seats = _repo.GetSeatsByPlaneId(p.PlaneID);

            _view.SetPlanes(planes);
        }

        private void OnAddPlane(object? sender, EventArgs e)
        {
            if (!_view.TryGetNewPlaneInput(out string model,
                                          out string type,
                                          out string status,
                                          out _, out _, out _, out _))
                return;

            int planeId = _repo.AddPlane(model, type, status, out string error);
            if (planeId <= 0)
            {
                _view.ShowError(error);
                return;
            }

            Plane plane = type switch
            {
                "HighLevel" => new HighLevel(planeId, status),
                "A320" => new MidRangeA320(planeId, status),
                "PrivateJet" => new PrivateJet(planeId, status),
                _ => throw new InvalidOperationException("Unknown plane type")
            };

            plane.GenerateSeats();

            if (!_repo.InsertSeats(planeId, plane.Seats, out error))
            {
                _view.ShowError(error);
                return;
            }

            _view.ShowInfo("Plane added successfully.");
            LoadPlanes();
        }
    }
}
