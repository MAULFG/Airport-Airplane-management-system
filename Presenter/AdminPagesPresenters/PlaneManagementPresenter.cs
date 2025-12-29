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

            _view.ViewLoaded += OnLoad;
            _view.DeleteRequested += OnDelete;
            _view.AddPlaneClicked += OnAddPlane;
        }

        private void OnLoad(object? sender, EventArgs e) => LoadPlanes();

        private void LoadPlanes()
        {
            var planes = _repo.GetAllPlanes();
            _view.SetPlanes(planes);
        }

        private void OnDelete(int planeId)
        {
            if (!_view.Confirm("Delete this plane?"))
                return;

            if (!_repo.SetPlaneStatus(planeId, "Deleted", out string error))
            {
                _view.ShowError(error);
                return;
            }

            LoadPlanes();
        }

        private void OnAddPlane(object? sender, EventArgs e)
        {
            // View provides selected model + status (from docked panel or dialog)
            if (!_view.TryGetNewPlaneInput(out string type, out string status))
                return;

            // 1) Insert plane
            int planeId = _repo.AddPlane(type, status, out string error);
            if (planeId <= 0)
            {
                _view.ShowError(error);
                return;
            }

            // 2) Create plane object in Presenter (MVP: business logic here)
            Plane plane = type switch
            {
                "HighLevel" => new HighLevel(planeId, status),
                "A320" => new MidRangeA320(planeId, status),
                "PrivateJet" => new PrivateJet(planeId, status),
                _ => null
            };

            if (plane == null)
            {
                _view.ShowError("Unknown plane type.");
                return;
            }

            // 3) Generate seats in memory, then persist
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
