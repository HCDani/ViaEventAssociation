using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViaEventAssociation.Core.Tools.OperationResult
{
    public class Result<T>
    {
        public int resultCode { get; init; } // Success is 0, anything else is error.
        public string errorMessage { get; private set; }
        public T payLoad { get; init; }
        public List<string> errors { get; init; }

        // Constructor if an error occoured.
        public Result(int resultCode, string errorMessage){
            this.resultCode = resultCode;
            this.errorMessage = errorMessage;
        }

        // Constructor if the call finished successfully.
        public Result(T payload){
            this.resultCode = 0;
            this.payLoad = payload;
        }

        // Constructor if more than one error occoured.
        public Result(List<string> errors)
        {
            this.resultCode = 1;
            this.errors = errors;
        }

        // More than one error occoured. The following error messages are appendid to the 1st one.
        public void AddError(int resultCode, string errorMessage){
            this.errorMessage += "," + resultCode + "-" + errorMessage;
        }
    }
}
