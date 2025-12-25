using System;

namespace Airport_Airplane_management_system.Model.Interfaces.Services
{
    public interface IUserSettingsService
    {
        (string Username, string Email, DateTime CreatedAt, DateTime? LastLoginAt)? GetHeader(int userId);

        void ChangePassword(int userId, string currentPassword, string newPassword, string confirmPassword);
        void ChangeUsername(int userId, string newUsername, string confirmPassword);
        void ChangeEmail(int userId, string newEmail, string confirmPassword);
    }
}
