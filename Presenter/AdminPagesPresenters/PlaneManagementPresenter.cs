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

        private void LoadPlanes()
        {
            var planes = _repo.GetAllPlanes();

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




        private static List<Seat> BuildSeatsByType(
     string type, int total, int eco, int biz, int first)
        {
            var seats = new List<Seat>();

            for (int i = 1; i <= first; i++)
                seats.Add(new Seat($"F{i}", "First"));

            for (int i = 1; i <= biz; i++)
                seats.Add(new Seat($"B{i}", "Business"));

            for (int i = 1; i <= eco; i++)
                seats.Add(new Seat($"E{i}", "Economy"));

            return seats;
        }
    }
}
