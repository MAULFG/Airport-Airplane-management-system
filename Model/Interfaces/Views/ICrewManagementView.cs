using Airport_Airplane_management_system.Model.Core.Classes;
using System;
using System.Collections.Generic;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{

    public interface ICrewManagementView
    {
        // ===== Inputs (read from UI) =====
        string FullName { get; }
        string Email { get; }
        string Phone { get; }
        string Role { get; }
        string Status { get; }
        int? SelectedFlightId { get; }

        // ===== Rendering =====
        void RenderCrew(List<Crew> crew);
        void RenderFlights(List<Flight> flights);
        void SetEditMode(bool editing);

        // ===== Feedback =====
        void ShowError(string message);
        void ShowInfo(string message);

        // ===== UI Events =====
        event Action<Crew> EditRequested;
        event Action<Crew> DeleteRequested;


        event Action ViewLoaded;
        event Action AddOrUpdateClicked;
        event Action CancelEditClicked;
        event Action FilterChanged;

    }
}