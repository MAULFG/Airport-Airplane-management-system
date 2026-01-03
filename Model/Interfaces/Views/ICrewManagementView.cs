using Airport_Airplane_management_system.Model.Core.Classes;

public interface ICrewManagementView
{
    // Inputs
    string FullName { get; }
    string Email { get; }
    string Phone { get; }
    string Role { get; }
    string Status { get; }
    int? SelectedFlightId { get; }
    void Clear();
    // Rendering
    void RenderCrew(IEnumerable<Crew> crew);
    void RenderFlights(List<Flight> flights);
    void RenderFilterFlights(List<Flight> flights);
    void SetEditMode(bool editing);
    int? GetFlightFilter();
    void FillForm(string fullName, string role, string status, string email, string phone, int? flightId);

    // Feedback
    void ShowError(string message);
    void ShowInfo(string message);

    // Events
    event Action<Crew> EditRequested;
    event Action<Crew> DeleteRequested;
    event EventHandler LoadCrewRequested;
    event Action AddOrUpdateClicked;
    event Action CancelEditClicked;
    event Action FilterChanged;
}
