using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events.Values
{
    public record Status
    {
        public enum StatusEnum { 
            Draft,
            Ready,
            Cancelled,
            Active
        }
        public StatusEnum Value { get; init; }

        private Status()
        {
            Value = StatusEnum.Draft;
        }
        public static Result<Status> Create(StatusEnum value)
        {
            List<string> errors = Validate();

            return errors.Any() ? new Result<Status>(errors) : new Result<Status>(new Status() { Value = value});
        }
        private static List<string> Validate()
        {
            List<string> errors = new();

            return errors;
        }
    }
}
