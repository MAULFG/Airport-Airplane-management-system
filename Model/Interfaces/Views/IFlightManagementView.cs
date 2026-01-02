using Airport_Airplane_management_system.Model.Core.Classes;
using System;
using System.Collections.Generic;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface IFlightManagementView
    {
        event EventHandler ViewLoaded;
        event EventHandler AddClicked;
        event EventHandler UpdateClicked;
        event EventHandler CancelEditClicked;
        event EventHandler FilterChanged;

        event Action<int> EditRequested;
        event Action<int> DeleteRequested;
        event Action<int> ViewCrewRequested;

        event Action<int> PlaneChanged;

        string FromCity { get; }
        string ToCity { get; }
        DateTime Departure { get; }
        DateTime Arrival { get; }
        int? SelectedPlaneId { get; }
        string CurrentFilter { get; }

        decimal EconomyPrice { get; }
        decimal BusinessPrice { get; }
        decimal FirstPrice { get; }

        void SetPlanes(List<Plane> planes);
        void SetFlights(List<Flight> flights);

        void ShowInfo(string message);
        void ShowError(string message);
        bool Confirm(string message);

        void ClearForm();

        bool IsEditMode { get; }
        int? EditingFlightId { get; }
        void EnterEditMode(
    Flight flight,
    Dictionary<string, decimal> seatPrices
);

        void ExitEditMode();

        void SetSeatClassAvailability(HashSet<string> classesLower);

        // ✅ NEW: change the label next to txtFirstPrice ("First" -> "VIP")
        void SetFirstLabel(string text);
    }
}
