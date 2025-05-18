using System.Text.RegularExpressions;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.GuestNS.Values {

    public record ProfilePictureUrl {
        public string Value { get; }

        private ProfilePictureUrl(string value) {
            Value = value;
        }

        public static Result<ProfilePictureUrl> Create(string value) {
            return Validate(value) ?? new Result<ProfilePictureUrl>(new ProfilePictureUrl(value)); // Return valid ProfilePictureURL
        }

        private static Result<ProfilePictureUrl> Validate(string value) {
            if (string.IsNullOrWhiteSpace(value)) return new Result<ProfilePictureUrl>(140, "Profile picture URL cannot be empty.");
            if (!IsValidUrl(value)) return new Result<ProfilePictureUrl>(141, "Invalid profile picture URL format.");
            return null;
        }

        private static bool IsValidUrl(string url) {
            try {
                Uri uri = new Uri(url);
                return uri != null;
            } catch {
                return false;
            }
        }
    }
}

