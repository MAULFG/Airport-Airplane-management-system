using System;
using System.Collections.Generic;
using System.Text;

using Airport_Airplane_management_system.Model.Core.Classes;
using System;
using System.Collections.Generic;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface IFlightManagementView
    {
        // Form properties
        string FlightNumber { get; }
        string Origin { get; }
        string Destination { get; }
        string Date { get; }
        string Time { get; }
        string Status { get; }
        bool IsInEditMode { get; }
        int? SelectedFlightId { get; }

        // Methods
        void RenderFlights(IEnumerable<Flight> flights);
        void RenderFilterFlights(List<Flight> flights);
        void SetEditMode(bool editing);
        void FillForm(Flight f);
        int? GetFilterFlightId();
        void ShowError(string msg);
        void ShowInfo(string msg);

        // Events
        event Action AddOrUpdateClicked;
        event Action CancelEditClicked;
        event Action<Flight> EditRequested;
        event Action<Flight> DeleteRequested;
        event Action FilterChanged;
        event EventHandler LoadFlightsRequested;
    }
}
