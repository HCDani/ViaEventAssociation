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
        public Guid EventId { get; }
        private UpdateEventTitleCommand(Title title, Guid eventId) {
            this.title = title;
            EventId = eventId;
        }
        public static Result<UpdateEventTitleCommand> Create(string title, string eventId) {
            Result<Title> titleResult = Title.Create(title);
            return new Result<UpdateEventTitleCommand>(new UpdateEventTitleCommand(titleResult.payLoad, Guid.Parse(eventId)));
        }
        public Result<UpdateEventTitleCommand> AddResponse(Result<Title> title) {
            if (title.resultCode != 0) {
                return new Result<UpdateEventTitleCommand>(title.resultCode, title.errorMessage);
            }
            return new Result<UpdateEventTitleCommand>(this);
        }
    }
}
