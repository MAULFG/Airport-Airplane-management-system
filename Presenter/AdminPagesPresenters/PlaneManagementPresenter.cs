using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Views;
using System;

namespace Airport_Airplane_management_system.Presenter.AdminPages
{
    public class PlaneManagementPresenter
    {
        private readonly IPlaneManagementView _view;
        private readonly IPlaneRepository _repo;

        public PlaneManagementPresenter(
            IPlaneManagementView view,
            IPlaneRepository repo)
        {
            _view = view;
            _repo = repo;

            _view.ViewLoaded += OnLoad;
            _view.DeleteRequested += OnDelete;
            _view.AddPlaneClicked += OnAddPlane;
        }

        private void OnLoad(object? sender, EventArgs e)
        {
            LoadPlanes();
        }

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
            _view.ShowInfo("Add Plane dialog not wired yet.");
        }
    }
}
