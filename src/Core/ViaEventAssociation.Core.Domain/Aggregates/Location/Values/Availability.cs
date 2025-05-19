using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.LocationNS.Values {
    public record Availability
    {
        public Nullable<DateTime> From { get; }
        public Nullable<DateTime> To { get; }

        private Availability(Nullable<DateTime> from, Nullable<DateTime> to)
        {
            From = from;
            To = to;
        }
        public static Result<Availability> Create(DateTime from, DateTime to) {
            return Validate(from, to) ?? new Result<Availability>(new Availability(from, to));
        }
        private static Result<Availability> Validate(DateTime from, DateTime to) {
            if (from == null) return new Result<Availability>(80, "From cannot be null.");
            if (to == null) return new Result<Availability>(81, "To cannot be null.");
            if (from >= to) return new Result<Availability>(82, "The 'From' date must be earlier than the 'To' date.");
            TimeSpan duration = to - from;
            if (duration < TimeSpan.FromHours(1)) return new Result<Availability>(83, "The duration between 'From' and 'To' must be at least 1 hour.");
            return null;
        }
    }
}
