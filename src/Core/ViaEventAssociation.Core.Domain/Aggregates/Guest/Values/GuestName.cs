using System.Text.RegularExpressions;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.GuestNS.Values {

    public record GuestName {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        private GuestName(string firstName, string lastName) {
            FirstName = FormatName(firstName);
            LastName = FormatName(lastName);
        }

        public static Result<GuestName> Create(string firstName, string lastName) {
            return Validate(firstName, lastName) ?? new Result<GuestName>(new GuestName(firstName, lastName)); // Return valid GuestName
        }

        private static Result<GuestName> Validate(string firstName, string lastName) {
            if (string.IsNullOrWhiteSpace(firstName)) return new Result<GuestName>(130, "First name cannot be empty.");
            if (firstName.Length < 2 || firstName.Length > 25) return new Result<GuestName>(131, "First name must be between 2 and 25 characters.");
            if (!Regex.IsMatch(firstName, @"^[a-zA-Z]+$")) return new Result<GuestName>(135, "First name must contain only letters.");
            if (string.IsNullOrWhiteSpace(lastName)) return new Result<GuestName>(132, "Last name cannot be empty.");
            if (!Regex.IsMatch(lastName, @"^[a-zA-Z]+$")) return new Result<GuestName>(136, "Last name must contain only letters.");
            if (lastName.Length < 2 || lastName.Length > 25) return new Result<GuestName>(133, "Last name must be between 2 and 25 characters.");
            return null;
        }

        private static string FormatName(string name) {
            return char.ToUpper(name[0]) + name.Substring(1).ToLower();
        }

    }
}