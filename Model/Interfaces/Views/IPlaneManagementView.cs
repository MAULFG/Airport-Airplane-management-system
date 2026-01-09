using System;
using System.Collections.Generic;
using System.Text;
using Airport_Airplane_management_system.Model.Core.Classes;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface IPlaneManagementView
    {
        event EventHandler ViewLoaded;
        event EventHandler AddPlaneClicked;
        event Action<int> DeleteRequested;
        event Action<int>? PlaneSelected;

        void SetPlanes(List<Plane> planes);
        void ShowInfo(string message);
        void ShowError(string message);
        bool Confirm(string message);

        void HighlightPlane(int planeId);
        void ClearView();

        bool TryGetNewPlaneInput(
    out string model,
    out string type,
    out string status,
    out int total,
    out int eco,
    out int biz,
    out int first);

    }
}
