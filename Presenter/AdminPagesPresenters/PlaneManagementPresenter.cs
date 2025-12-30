using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using System;

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
            if (!_view.Confirm("Delete this plane?")) return;

            if (!_repo.SetPlaneStatus(planeId, "Deleted", out string error))
            {
                _view.ShowError(error);
                return;
            }

            LoadPlanes();
        }

        private void OnAddPlane(object? sender, EventArgs e)
        {
            if (!_view.TryGetNewPlaneInput(out string planeName, out string type, out string status))
                return;

            // 1) Insert plane row (model+type+status)
            int planeId = _repo.AddPlane(planeName, type, status, out string error);
            if (planeId <= 0)
            {
                _view.ShowError(error);
                return;
            }

            // 2) Create plane object in memory (so we can generate seats)
            Plane plane = type switch
            {
                "HighLevel" => new HighLevel(planeId, status),
                "A320" => new MidRangeA320(planeId, status),
                "PrivateJet" => new PrivateJet(planeId, status),
                _ => new MidRangeA320(planeId, status)
            };

            // Persist chosen model name
            plane.Model = planeName;

            // 3) Generate seats then persist
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
