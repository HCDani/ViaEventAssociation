using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events.Values
{
    public record EventDuration
    {
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        private EventDuration(DateTime from, DateTime to)
        {
            From = from;
            To = to;
        }
        public static Result<EventDuration> Create(DateTime from, DateTime to)
        {
            Result<EventDuration> errors = Validate(from,to);

            return errors != null ? errors : new Result<EventDuration>(new EventDuration(from, to));
        }
        private static Result<EventDuration> Validate(DateTime from, DateTime to)
        {
            if (from >= to) return new Result<EventDuration>(30,"The 'From' date must be earlier than the 'To' date.");

            DateTime eightAM = from.Date.AddHours(8);
            DateTime oneAm = from.Date.AddDays(1).AddHours(1);

            if (from < eightAM) return new Result<EventDuration>(31, "The 'From' time cannot be before 8:00 AM.");
            if (to > oneAm) return new Result<EventDuration>(32, "The 'To' time cannot be after 1 AM");

            // Validate that the period between 'from' and 'to' is smaller or equal to 10 hours
            TimeSpan duration = to - from;
            if (duration > TimeSpan.FromHours(10)){
                return new Result<EventDuration>(34, "The duration between 'From' and 'To' cannot be more than 10 hours.");
            }
            if (duration < TimeSpan.FromHours(1)) return new Result<EventDuration>(35, "The duration between 'From' and 'To' must be at least 1 hour.");
            return null;
        }
    }
}
