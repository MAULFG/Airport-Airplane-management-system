using System;

namespace Airport_Airplane_management_system.Model.Interfaces.Repositories
{
    public interface IUserSettingsRepository
    {
        (string Username, string Email, DateTime CreatedAt, DateTime? LastLoginAt)? GetUserHeader(int userId);

        string GetPassword(int userId);
        string GetUsername(int userId);

        bool UpdatePassword(int userId, string newPassword);
        bool UpdateEmail(int userId, string newEmail);
        bool UpdateUsername(int userId, string newUsername);

        bool EmailExists(string email, int excludeUserId);
        bool UsernameExists(string username, int excludeUserId);
    }
}
