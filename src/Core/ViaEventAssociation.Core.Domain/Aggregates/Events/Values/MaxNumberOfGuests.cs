using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events.Values
{
    public record MaxNumberOfGuests
    {
       public int Value { get; private set; }

        private MaxNumberOfGuests(int value){
            Value = value;
        }
        public static Result<MaxNumberOfGuests> Create(int value)
        {
            List<string> errors = Validate(value);

            return errors.Any() ? new Result<MaxNumberOfGuests>(errors) : new Result<MaxNumberOfGuests>(new MaxNumberOfGuests(value));
        }
        private static List<string> Validate(int? value)
        {
            List<string> errors = new();
            if (value == 0 || value == null) errors.Add("Max number of guests cannot be 0 or empty");


            return errors;
        }
    }
}
