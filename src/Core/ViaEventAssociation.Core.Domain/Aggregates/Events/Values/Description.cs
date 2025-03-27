using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events.Values
{
    public record Description
    {
        public string Value { get; private set; }
        private Description(string value)
        {
            Value = value;
        }
        public static Result<Description> Create(string value)
        {
            Result<Description> errors = Validate(value);

            return errors != null ? errors : new Result<Description>(new Description(value));
        }
        private static Result<Description> Validate(string value)
        {
            List<string> errors = new();
            if (value.Length > 250) return new Result<Description>(20,"Description must be at most 250 characters");

            return null;
        }
    }
}
