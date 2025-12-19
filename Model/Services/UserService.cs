using System;
using System.Collections.Generic;
using System.Linq;
using Airport_Airplane_management_system.Model.Core.Classes.Users;
using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Core.Enums;
namespace Airport_Airplane_management_system.Model.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepo;
    
        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        // Authenticate by username + password
        public User Authenticate(string username, string password)
        {
            var user = _userRepo.GetUserByUsername(username);
            if (user != null && user.Password == password)
                return user;

            return null;
        }

        // Add new user
        
        public  AddUserResult  AddUser(User user)
        {
            if (_userRepo.UsernameExists(user.UserName))
                return AddUserResult.UsernameExists;

            if (_userRepo.EmailExists(user.Email))
                return AddUserResult.EmailExists;

            _userRepo.AddUser(user);
            return AddUserResult.Success;
        }

        // Remove user
        public bool RemoveUser(int userId)
        {
            return _userRepo.RemoveUser(userId);
        }

        // Change password
        public void ChangePassword(int userId, string newPassword)
        {
            _userRepo.UpdatePassword(userId, newPassword);
        }
        public User GetUserByUsername(string username)
        {
            return _userRepo.GetUserByUsername(username);
        }

        public void UpdatePassword(int userId, string newPassword)
        {
            _userRepo.UpdatePassword(userId, newPassword);
        }

        // Get all users
        public List<User> GetAllUsers()
        {
            return _userRepo.GetAllUsers();
        }
    }
}
