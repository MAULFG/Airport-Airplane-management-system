using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Airport_Airplane_management_system.Model.Core.Classes
{
    public class Crew
    {
        public static readonly HashSet<string> AllowedRoles = new(StringComparer.OrdinalIgnoreCase)
        {
            "Captain",
            "First Officer",
            "Flight Attendant",
            "Purser",
            "Flight Engineer"
        };

        private static readonly Regex EmpIdRegex = new(@"^EMP\d{3}$", RegexOptions.Compiled);

        public int? FlightId { get; set; } // Nullable: can be unassigned
        public string FullName { get; private set; }
        public string Role { get; private set; }
        public string Status { get; private set; }   // "active" / "inactive"
        public string EmployeeId { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }

        public Crew(
            string fullName,
            string role,
            string status,
            string employeeId,
            string email,
            string phone)
        {
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            Role = role ?? throw new ArgumentNullException(nameof(role));
            Status = status ?? throw new ArgumentNullException(nameof(status));
            EmployeeId = employeeId ?? throw new ArgumentNullException(nameof(employeeId));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Phone = phone ?? throw new ArgumentNullException(nameof(phone));

            // Apply validation immediately
            Update(fullName, role, status, employeeId, email, phone);
        }

        public void Update(string fullName, string role, string status,
                           string employeeId, string email, string phone)
        {
            SetFullName(fullName);
            SetRole(role);
            SetStatus(status);
            SetEmployeeId(employeeId);
            SetEmail(email);
            SetPhone(phone);
        }

        #region Setters with Validation

        private void SetFullName(string fullName)
        {
            fullName = (fullName ?? "").Trim();
            if (fullName.Length < 2)
                throw new ArgumentException("Full name must be at least 2 characters.");
            if (!fullName.Any(char.IsLetter))
                throw new ArgumentException("Full name must contain at least one letter.");
            FullName = fullName;
        }

        private void SetRole(string role)
        {
            role = (role ?? "").Trim();
            if (string.IsNullOrWhiteSpace(role))
                throw new ArgumentException("Role is required.");
            if (!AllowedRoles.Contains(role))
                throw new ArgumentException("Role is not valid. Choose from the role list.");
            Role = AllowedRoles.First(r => r.Equals(role, StringComparison.OrdinalIgnoreCase));
        }

        private void SetStatus(string status)
        {
            status = (status ?? "").Trim().ToLowerInvariant();
            if (status != "active" && status != "inactive")
                throw new ArgumentException("Status must be Active or Inactive.");
            Status = status;
        }

        private void SetEmployeeId(string employeeId)
        {
            employeeId = (employeeId ?? "").Trim().ToUpperInvariant();
            if (!EmpIdRegex.IsMatch(employeeId))
                throw new ArgumentException("Employee ID must look like EMP001 (EMP + 3 digits).");
            EmployeeId = employeeId;
        }

        private void SetEmail(string email)
        {
            email = (email ?? "").Trim();
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.");
            if (email.Any(char.IsWhiteSpace))
                throw new ArgumentException("Email must be a plain address like name@domain.com.");
            try
            {
                var addr = new MailAddress(email);
                if (!addr.Address.Equals(email, StringComparison.OrdinalIgnoreCase))
                    throw new ArgumentException("Email must be a plain address like name@domain.com.");
                Email = email;
            }
            catch
            {
                throw new ArgumentException("Email format is invalid.");
            }
        }

        private void SetPhone(string phone)
        {
            phone = (phone ?? "").Trim();
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Phone is required.");

            int plusCount = phone.Count(c => c == '+');
            if (plusCount > 1 || (plusCount == 1 && phone[0] != '+'))
                throw new ArgumentException("Phone '+' is allowed only once and only at the start.");

            if (!phone.All(ch => char.IsDigit(ch) || ch == '+' || ch == ' ' || ch == '-' || ch == '(' || ch == ')'))
                throw new ArgumentException("Phone contains invalid characters.");

            if (phone.Count(char.IsDigit) < 7)
                throw new ArgumentException("Phone number looks too short.");

            Phone = phone;
        }

        #endregion

        #region Flight Assignment

        public void AssignToFlight(int flightId) => FlightId = flightId;
        public void UnassignFromFlight() => FlightId = null;

        #endregion

        #region Status Management

        public bool IsActive() => Status == "active";
        public void Activate() => Status = "active";
        public void Deactivate() => Status = "inactive";

        #endregion

        public override string ToString() => $"{FullName} ({Role}) - {EmployeeId}";
    }
}