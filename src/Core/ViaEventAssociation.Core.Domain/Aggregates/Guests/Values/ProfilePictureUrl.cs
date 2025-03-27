using System.Text.RegularExpressions;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guests.Values;

public record ProfilePictureUrl
{
    public string Value { get; private set; }

    private ProfilePictureUrl(string value)
    {
        Value = value;
    }

    public static Result<ProfilePictureUrl> Create(string value)
    {
        List<string> errors = Validate(value);

        if (errors.Count > 0)
            return new Result<ProfilePictureUrl>(errors); // Return validation errors

        return new Result<ProfilePictureUrl>(new ProfilePictureUrl(value)); // Return valid ProfilePictureURL
    }

    private static List<string> Validate(string value)
    {
        List<string> errors = new();

        if (string.IsNullOrWhiteSpace(value))
            errors.Add("Profile picture URL cannot be empty.");

        if (!IsValidUrl(value))
            errors.Add("Invalid profile picture URL format.");

        return errors;
    }

    private static bool IsValidUrl(string url)
    {
        string pattern = @"^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$";
        return Regex.IsMatch(url, pattern);
    }
}
    
