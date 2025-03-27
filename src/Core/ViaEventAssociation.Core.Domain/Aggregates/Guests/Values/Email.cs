using System.Text.RegularExpressions;
using ViaEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guests.Values;

public record Email
{
    public string Value { get; private set; }

    private Email(string value)
    {
        Value = value;
    }

    public static Result<Email> Create(string value)
    {
        List<string> errors = Validate(value);

        if (errors.Any())
            return new Result<Email>(errors); // Returning errors using the existing Result<T> class

        return new Result<Email>(new Email(value)); // Return valid Email object
    }

    private static List<string> Validate(string value)
    {
        List<string> errors = new();

        if (string.IsNullOrWhiteSpace(value))
            errors.Add("Email cannot be empty.");

        if (!Regex.IsMatch(value, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            errors.Add("Invalid email format.");

        if (!value.EndsWith("@via.dk"))
            errors.Add("Email must end with '@via.dk'.");

        return errors;
    }
}

