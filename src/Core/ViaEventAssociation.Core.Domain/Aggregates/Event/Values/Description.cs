using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values {
    public record Description {
        public static readonly Description Default = new("");
        public string Value { get; }
        private Description(string value) {
            Value = value;
        }
        public static Result<Description> Create(string value) {
            return Validate(value) ?? new Result<Description>(new Description(value));
        }
        private static Result<Description> Validate(string value) {
            if (value.Length > 250) return new Result<Description>(20, "Description must be at most 250 characters");
            return null;
        }
    }
}
