using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ViaEventAssociation.Core.Domain.Aggregates.Events.Values.Status;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events.Values
{
    public record Visibility
    {
        public enum VisibilityEnum
        {
            Private,
            Public
        }
        public VisibilityEnum Value { get; init; }

        private Visibility()
        {
            Value = VisibilityEnum.Private;
        }
        public static Result<Visibility> Create(VisibilityEnum value)
        {
            List<string> errors = Validate();

            return errors.Any() ? new Result<Visibility>(errors) : new Result<Visibility>(new Visibility() { Value = value });
        }
        private static List<string> Validate()
        {
            List<string> errors = new();

            return errors;
        }
    }
}
