using Airport_Airplane_management_system.Model.Core.Classes;
using System;
using System.Collections.Generic;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface IFlightManagementView
    {
        // ===== Events =====
        event EventHandler ViewLoaded;
        event EventHandler AddClicked;
        event EventHandler UpdateClicked;
        event EventHandler CancelEditClicked;
        event Action<int> EditRequested;
        event Action<int> DeleteRequested;
        event Action<int>? ViewCrewRequested;
        event EventHandler FilterChanged;

        // ===== Inputs =====
        string FromCity { get; }
        string ToCity { get; }
        DateTime Departure { get; }
        DateTime Arrival { get; }
        int? SelectedPlaneId { get; }
        string CurrentFilter { get; }

        // ===== Edit state =====
        bool IsEditMode { get; set; }
        int? EditingFlightId { get; set; }

        // ===== Outputs =====
        void SetPlanes(List<Plane> planes);
        void SetFlights(List<Flight> flights);

        void ShowInfo(string message);
        void ShowError(string message);
        bool Confirm(string message);

        void ClearForm();
        void EnterEditMode(Flight f);
        void ExitEditMode();
    }
}
