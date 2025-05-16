using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViaEventAssociation.Core.Tools.OperationResult
{
    public abstract class Resultable {
        public int resultCode { get; init; } // Success is 0, anything else is error.
        public String errorMessage { get; private set; }
        public List<string> errors { get; init; }

        public Resultable(int resultCode, string errorMessage) {
            this.resultCode = resultCode;
            this.errorMessage = errorMessage;
        }
        // Constructor if more than one error occoured.
        public Resultable(List<string> errors) {
            this.resultCode = 1;
            this.errors = errors;
        }

        // More than one error occoured. The following error messages are appendid to the 1st one.
        public void AddError(int resultCode, string errorMessage) {
            this.errorMessage += "," + resultCode + "-" + errorMessage;
        }
    }
    public class Result<T> : Resultable  {
        public T payLoad { get; init; }

        // Constructor if an error occoured.
        public Result(int resultCode, string errorMessage) :base(resultCode, errorMessage) {
        }

        // Constructor if the call finished successfully.
        public Result(T payload) : base(0,null){
            this.payLoad = payload;
        }

        public Result(List<string> errors) : base(errors) {
        }

        public static Result<T> CombineResultsInto(params Resultable[] results) {
            foreach (var result in results) {
                if (result.resultCode != 0) {
                    return new Result<T>(result.resultCode, result.errorMessage);
                }
            }
            return new Result<T>(0,null);
        }
        public Result<T> WithPayLoadIfSuccess(Func<T> value) {
            if (resultCode == 0) {
                return new Result<T>(value());
            }
            return this;
        }
        public bool IsSuccess() {
            return resultCode == 0;
        }
    }
}
