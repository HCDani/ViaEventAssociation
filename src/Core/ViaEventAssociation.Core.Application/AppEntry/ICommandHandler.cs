using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.AppEntry {
    public interface ICommandHandler<TCommand> {
        public Task<Result<TCommand>> HandleAsync(TCommand command);
    }
}
