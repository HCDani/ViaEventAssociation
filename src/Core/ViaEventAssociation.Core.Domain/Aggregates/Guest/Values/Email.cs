using System.Text.RegularExpressions;
using ViaEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.GuestNS.Values {

    public record Email {
        public string Value { get; }

        private Email(string value) {
            Value = value;
        }

        public static Result<Email> Create(string value) {
            return Validate(value) ?? new Result<Email>(new Email(value)); // Return valid Email object
        }

        private static Result<Email> Validate(string value) {
            if (string.IsNullOrWhiteSpace(value)) return new Result<Email>(120, "Email cannot be empty.");
            if (!Regex.IsMatch(value, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")) return new Result<Email>(121, "Invalid email format.");
            if (!value.EndsWith("@via.dk")) return new Result<Email>(122, "Email must end with '@via.dk'.");
            return null;
        }
    }
}

