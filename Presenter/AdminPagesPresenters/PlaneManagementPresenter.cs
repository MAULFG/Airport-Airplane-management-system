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
            // We only need: model + type + status (seat counts are fixed by type)
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

            // ✅ FIXED: Generate seats based ONLY on type (fixed mapping)
            List<Seat> seats = SeatGenerator.BuildSeats(type);

            if (!_repo.InsertSeats(planeId, seats, out error))
            {
                _view.ShowError(error);
                return;
            }

            _view.ShowInfo("Plane added successfully.");
            LoadPlanes();
        }
    }
}
