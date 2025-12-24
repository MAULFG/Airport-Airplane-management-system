using System;
using System.Collections.Generic;
using System.Text;
using Airport_Airplane_management_system.Model.Core.Classes;
namespace Airport_Airplane_management_system.Model.Interfaces.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUserById(int userId);
        User GetUserByUsername(string username);
        void AddUser(User user);
        void UpdatePassword(int userId, string newPassword);
        bool RemoveUser(int userId);      
        
        bool UsernameExists(string name);
        bool EmailExists(string name);

    }

}
