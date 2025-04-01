using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.LocationNS.Values {
    public record MaxCapacity {
        public static readonly MaxCapacity Default = new(5);
        public int Value { get; }

        private MaxCapacity(int value) {
            Value = value;
        }
        public static Result<MaxCapacity> Create(int? value) {
            return Validate(value) ?? new Result<MaxCapacity>(new MaxCapacity((int)value));
        }
        private static Result<MaxCapacity>? Validate(int? value) {
            if (value == null || value < 5) return new Result<MaxCapacity>(90, "Max capacity must be at least 5");
            return null;
        }
    }
}