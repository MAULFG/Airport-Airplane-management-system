using System;
using System.Collections.Generic;
using Airport_Airplane_management_system.Model.Core.Classes;

namespace Airport_Airplane_management_system.Model.Interfaces.Views
{
    public interface ICrewManagementView
    {
        // Events
        event Action ViewLoaded;
        event Action AddOrUpdateClicked;
        event Action CancelEditClicked;
        event Action<Crew> EditRequested;
        event Action<Crew> DeleteRequested;
        event Action FilterChanged;

        // Inputs (from form)
        string FullName { get; }
        string Role { get; }
        string Status { get; }
        string Email { get; }
        string Phone { get; }
        int? SelectedFlightId { get; }     // from form dropdown

        // Filter (top-right)
        int? GetFlightFilter();            // null => All Flights
        void SetFlightFilter(int? flightId);

        // Outputs
        void RenderFlights(List<Flight> flights); // used for both filter + form dropdown
        void RenderCrew(List<Crew> crew);
        void SetEditMode(bool isEdit);
        void FillForm(string fullName, string role, string status, string email, string phone, int? flightId);
        void SetFormFlight(int? flightId);
        void SyncFormFlightWithFilter(int? flightId);

        void ShowError(string message);
        void ShowInfo(string message);
        


    }
}
