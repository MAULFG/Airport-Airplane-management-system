using Airport_Airplane_management_system.Model.Core.Classes;
using System.Collections.Generic;

namespace Airport_Airplane_management_system.Model.Interfaces.Repositories
{
    public interface ICrewRepository
    {
        List<Crew> GetAll();
        bool Insert(Crew crew, out string err);
        bool Update(Crew crew, out string err);
        bool DeleteByEmployeeId(string employeeId, out string err);
        string GenerateNextEmployeeId();
    }
}
