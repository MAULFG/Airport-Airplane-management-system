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

        public PlaneManagementPresenter(IPlaneManagementView view, IPlaneRepository repo)
        {
            _view = view;
            _repo = repo;

            _view.ViewLoaded += (_, __) => LoadPlanes();
            _view.AddPlaneClicked += OnAddPlane;
            _view.DeleteRequested += OnDeletePlane;
        }

        private void LoadPlanes()
        {
            var planes = _repo.GetAllPlanes();
            _view.SetPlanes(planes);
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
            LoadPlanes(); // ✅ refresh
        }

        private void OnAddPlane(object? sender, EventArgs e)
        {
            if (!_view.TryGetNewPlaneInput(out string model,
                                          out string type,
                                          out string status,
                                          out int _total,
                                          out int _eco,
                                          out int _biz,
                                          out int _first))
                return;

            int planeId = _repo.AddPlane(model, type, status, out string error);
            if (planeId <= 0)
            {
                _view.ShowError(error);
                return;
            }

            // ✅ build seats WITHOUT SeatGenerator (3 fixed types)
            List<Seat> seats = BuildSeatsByType(type);

            if (!_repo.InsertSeats(planeId, seats, out error))
            {
                _view.ShowError(error);
                return;
            }

            _view.ShowInfo("Plane added successfully.");
            LoadPlanes();
        }

        // ----------------------------
        // Fixed seat mapping (3 types)
        // ----------------------------
        private static List<Seat> BuildSeatsByType(string type)
        {
            type = (type ?? "").Trim();

            Plane p = type switch
            {
                "HighLevel" => new HighLevel(-1, "active"),
                "A320" => new MidRangeA320(-1, "active"),
                "PrivateJet" => new PrivateJet(-1, "active"),
                _ => new MidRangeA320(-1, "active")
            };

            p.GenerateSeats();
            return p.Seats;
        }
    }
}
