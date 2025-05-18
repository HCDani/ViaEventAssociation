using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.LocationNS.Values {
    public record LocationName {
        public string Value { get; }

        private LocationName(string value) {
            Value = value;
        }
        public static Result<LocationName> Create(string value) {
            return Validate(value) ?? new Result<LocationName>(new LocationName(value));
        }
        private static Result<LocationName> Validate(string value) {
            if (string.IsNullOrEmpty(value)) return new Result<LocationName>(100, "Location name cannot be empty.");
            return null;
        }
    }
}