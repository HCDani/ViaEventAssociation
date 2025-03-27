using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events.Values
{
    public record Title
    {
        public string Value { get; private set; }

        private Title(string value)
        {
            Value = value;
        }
        public static Result<Title> Create(string value)
        {
            Result<Title> errors = Validate(value);

            return errors != null ? errors : new Result<Title>(new Title(value));
        }
        private static Result<Title> Validate(string value)
        {
            if (string.IsNullOrEmpty(value)|| value.Length < 3 || value.Length > 75) return new Result<Title>(10, "Title length must be between 3 and 75 characters.");
     
            return null;
        }
    }
}
