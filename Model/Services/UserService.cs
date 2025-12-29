using Airport_Airplane_management_system.Model.Core.Classes;
using Airport_Airplane_management_system.Model.Core.Enums;
using Airport_Airplane_management_system.Model.Interfaces.Exceptions;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Airport_Airplane_management_system.Model.Services
{
    public class UserService
    {
        private readonly IUserRepository _repo;
        private readonly IAppSession _session;

        public UserService(IUserRepository repo, IAppSession session)
        {
            _repo = repo;
            _session = session;
        }

        // Authenticate by username + password
        public User Authenticate(string username, string password)
        {
            var user = _repo.GetUserByUsername(username);
            if (user != null && user.Password == password)
                return user;

            return null;
        }

        public bool Login(string username, string password, out User user)
        {
            user = Authenticate(username, password);

            if (user == null)
                return false;

            _session.SetUser(user);
            return true;
        }
        // Add new user

        public  AddUserResult  AddUser(User user)
        {
            if (_repo.UsernameExists(user.UserName))
                return AddUserResult.UsernameExists;

            if (_repo.EmailExists(user.Email))
                return AddUserResult.EmailExists;

            _repo.AddUser(user);
            return AddUserResult.Success;
        }

        // Remove user
        public bool RemoveUser(int userId)
        {
            return _repo.RemoveUser(userId);
        }

        // Change password
        public void ChangePassword(int userId, string newPassword)
        {
            _repo.UpdatePassword(userId, newPassword);
        }
        public User GetUserByUsername(string username)
        {
            return _repo.GetUserByUsername(username);
        }

        public void UpdatePassword(int userId, string newPassword)
        {
            _repo.UpdatePassword(userId, newPassword);
        }

        // Get all users
        public List<User> GetAllUsers()
        {
            return _repo.GetAllUsers();
        }
    }
}
