using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.AppEntry.Decorators {
    public class LoggingForDispatcher : ICommandDispatcher {
        readonly ICommandDispatcher next;
        public LoggingForDispatcher(ICommandDispatcher next) {
            this.next = next;
        }
        public Task<Result<TCommand>> DispatchAsync<TCommand>(TCommand command) {
            // todo logging comes here.
            return next.DispatchAsync(command);
        }
    }
}
