using Airport_Airplane_management_system.Model.Interfaces.Repositories;
using Airport_Airplane_management_system.Model.Interfaces.Services;
using System;

namespace Airport_Airplane_management_system.Model.Services
{
    public class UserSettingsService : IUserSettingsService
    {
        private readonly IUserSettingsRepository _repo;

        public UserSettingsService(IUserSettingsRepository repo)
        {
            _repo = repo;
        }

        public (string Username, string Email, DateTime CreatedAt, DateTime? LastLoginAt)? GetHeader(int userId)
        {
            return _repo.GetUserHeader(userId);
        }

        public void ChangePassword(int userId, string currentPassword, string newPassword, string confirmPassword)
        {
            if (userId <= 0) throw new Exception("No logged-in user detected.");
            if (string.IsNullOrWhiteSpace(currentPassword)) throw new Exception("Please enter your current password.");
            if (string.IsNullOrWhiteSpace(newPassword)) throw new Exception("Please enter a new password.");
            if (string.IsNullOrWhiteSpace(confirmPassword)) throw new Exception("Please confirm your new password.");
            if (newPassword == currentPassword) throw new Exception("New password must be different from current password.");
            if (newPassword != confirmPassword) throw new Exception("New password and confirmation do not match.");

            var dbPass = _repo.GetPassword(userId);
            if (dbPass == null) throw new Exception("User not found in database.");
            if (dbPass != currentPassword) throw new Exception("Current password is incorrect.");

            if (!_repo.UpdatePassword(userId, newPassword))
                throw new Exception("Password update failed.");
        }

        public void ChangeUsername(int userId, string newUsername, string confirmPassword)
        {
            if (userId <= 0) throw new Exception("No logged-in user detected.");
            newUsername = (newUsername ?? "").Trim();

            if (string.IsNullOrWhiteSpace(newUsername)) throw new Exception("Please enter a new username.");
            if (newUsername.Contains(" ")) throw new Exception("Username cannot contain spaces.");
            if (string.IsNullOrWhiteSpace(confirmPassword)) throw new Exception("Please confirm your password.");

            var currentUsername = _repo.GetUsername(userId);
            if (currentUsername != null && string.Equals(currentUsername.Trim(), newUsername, StringComparison.OrdinalIgnoreCase))
                throw new Exception("New username must be different from your current username.");

            var dbPass = _repo.GetPassword(userId);
            if (dbPass == null) throw new Exception("User not found in database.");
            if (dbPass != confirmPassword) throw new Exception("Password is incorrect.");

            if (_repo.UsernameExists(newUsername, userId))
                throw new Exception("This username is already taken. Please choose another one.");

            if (!_repo.UpdateUsername(userId, newUsername))
                throw new Exception("Username update failed.");
        }

        public void ChangeEmail(int userId, string newEmail, string confirmPassword)
        {
            if (userId <= 0) throw new Exception("No logged-in user detected.");
            newEmail = (newEmail ?? "").Trim();

            if (string.IsNullOrWhiteSpace(newEmail)) throw new Exception("Please enter a new email.");
            if (!newEmail.Contains("@") || !newEmail.Contains(".")) throw new Exception("Please enter a valid email address.");
            if (string.IsNullOrWhiteSpace(confirmPassword)) throw new Exception("Please confirm your password.");

            var dbPass = _repo.GetPassword(userId);
            if (dbPass == null) throw new Exception("User not found in database.");
            if (dbPass != confirmPassword) throw new Exception("Password is incorrect.");

            var header = _repo.GetUserHeader(userId);
            if (header.HasValue && string.Equals(header.Value.Email.Trim(), newEmail, StringComparison.OrdinalIgnoreCase))
                throw new Exception("New email must be different from your current email.");

            if (_repo.EmailExists(newEmail, userId))
                throw new Exception("This email is already used by another account.");

            if (!_repo.UpdateEmail(userId, newEmail))
                throw new Exception("Email update failed.");
        }
    }
}
