using System;
using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;

namespace Airport_Airplane_management_system.View.Interfaces
{
    public interface IFlightManagementView
    {
        // lifecycle
        event EventHandler ViewLoaded;

        // actions
        event EventHandler AddClicked;
        event EventHandler UpdateClicked;
        event EventHandler CancelEditClicked;
        event Action<int> EditRequested;
        event Action<int> DeleteRequested;
        event Action<int> ViewCrewRequested;
        event EventHandler FilterChanged;

        // inputs
        string FromCity { get; }
        string ToCity { get; }
        DateTime Departure { get; }
        DateTime Arrival { get; }
        int? SelectedPlaneId { get; }
        string CurrentFilter { get; }   // "All Flights", "Upcoming", "Past", "Plane #3", ...

        // edit state
        bool IsEditMode { get; set; }
        int? EditingFlightId { get; set; }

        // outputs
        void SetPlanes(List<Plane> planes);
        void SetFlights(List<Flight> flights);

        void ShowInfo(string message);
        void ShowError(string message);
        bool Confirm(string message);

        void EnterEditMode(Flight flight);
        void ExitEditMode();
        void ClearForm();
    }
}
