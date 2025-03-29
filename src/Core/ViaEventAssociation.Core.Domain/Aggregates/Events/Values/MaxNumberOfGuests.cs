using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events.Values
{
    public record MaxNumberOfGuests
    {
       public static readonly MaxNumberOfGuests Default = new(5);
        public int Value { get;}

        private MaxNumberOfGuests(int value){
            Value = value;
        }
        public static Result<MaxNumberOfGuests> Create(int? value)
        {
            Result<MaxNumberOfGuests>? errors = Validate(value);

            return errors ?? new Result<MaxNumberOfGuests>(new MaxNumberOfGuests((int)value));
        }
        private static Result<MaxNumberOfGuests>? Validate(int? value)
        {
            if(value == null || value < 5) return new Result<MaxNumberOfGuests>(53,"Max number of guests must be at least 5");
            if (value > 50) return new Result<MaxNumberOfGuests>(54, "Max number of guests cannot be more than 50");

            return null;
        }
    }
}
