using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guests.Values;

public record GuestName
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    private GuestName(string firstName, string lastName)
    {
        FirstName = FormatName(firstName);
        LastName = FormatName(lastName);
    }

    public static Result<GuestName> Create(string firstName, string lastName)
    {
        List<string> errors = Validate(firstName, lastName);

        if (errors.Any())
            return new Result<GuestName>(errors); // Return validation errors

        return new Result<GuestName>(new GuestName(firstName, lastName)); // Return valid GuestName
    }

    private static List<string> Validate(string firstName, string lastName)
    {
        List<string> errors = new();

        if (string.IsNullOrWhiteSpace(firstName))
            errors.Add("First name cannot be empty.");
        else if (firstName.Length < 2 || firstName.Length > 25)
            errors.Add("First name must be between 2 and 25 characters.");

        if (string.IsNullOrWhiteSpace(lastName))
            errors.Add("Last name cannot be empty.");
        else if (lastName.Length < 2 || lastName.Length > 25)
            errors.Add("Last name must be between 2 and 25 characters.");

        return errors;
    }

    private static string FormatName(string name)
    {
        return char.ToUpper(name[0]) + name.Substring(1).ToLower();
    }

}