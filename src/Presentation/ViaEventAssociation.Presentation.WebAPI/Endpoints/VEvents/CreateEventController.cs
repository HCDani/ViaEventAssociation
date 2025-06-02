using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.VEvents {
    
    public class CreateEventController([FromServices] ICommandDispatcher dispatcher):ApiEndpoint
        .WithoutRequest.WithResponse<CreateEventResponse>{

        [HttpPost("event/create")]
        public override async Task<ActionResult<CreateEventResponse>> HandleAsync() {
            Result<CreateEventCommand> result = await dispatcher.DispatchAsync(CreateEventCommand.Create().payLoad);
            return Ok( new CreateEventResponse(result.payLoad.EventId));
        }
    }
    public record CreateEventResponse(Guid Id);
}
