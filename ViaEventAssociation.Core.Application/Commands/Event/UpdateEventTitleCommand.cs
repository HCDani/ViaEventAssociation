using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Commands.Event {
    public class UpdateEventTitleCommand {
        public Title title { get; }

        private UpdateEventTitleCommand(Title title) {
            this.title = title;
        }
        public static Result<UpdateEventTitleCommand> Create(string title) {
            Result<Title> titleResult = Title.Create(title);
            return new Result<UpdateEventTitleCommand>(new UpdateEventTitleCommand(titleResult.payLoad));
        }
    }
}
