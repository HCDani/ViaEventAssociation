using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events.Values
{
    public record EventId
    {
        public Guid Value { get; init; }

        public static Result<EventId> Create(Guid value){
            List<string> errors = Validate();

            return errors.Any() ? new Result<EventId>(errors) : new Result<EventId>(new EventId() { Value = value });
        }
        private static List<string> Validate(){
            List<string> errors = new();
            return errors;
        }
    }
}
