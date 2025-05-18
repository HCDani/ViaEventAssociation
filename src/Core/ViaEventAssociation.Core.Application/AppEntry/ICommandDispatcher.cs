using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.AppEntry {
    public interface ICommandDispatcher {
        public Task<Result<TCommand>> DispatchAsync<TCommand>(TCommand command);
    }
}
