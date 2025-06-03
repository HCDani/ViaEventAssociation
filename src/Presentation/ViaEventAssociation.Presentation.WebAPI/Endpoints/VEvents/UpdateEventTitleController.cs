using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.VEvents {
    public class UpdateEventTitleController([FromServices] ICommandDispatcher dispatcher) : ApiEndpoint
         .WithRequest<UpdateEventTitleRequest>.WithResponse<UpdateEventTitleResponse> {

        [HttpPost("event/update_title")]
        public override async Task<ActionResult<UpdateEventTitleResponse>> HandleAsync(UpdateEventTitleRequest request) {
            Result<UpdateEventTitleCommand> commandResult = await dispatcher.DispatchAsync(UpdateEventTitleCommand.Create(request.EventId.ToString(), request.Title).payLoad);
            if (commandResult.resultCode != 0) {
                return BadRequest(commandResult.errorMessage);
            }
            return Ok(new UpdateEventDescriptionResponse(commandResult.payLoad.title.Value));
        }
    }
    public record UpdateEventTitleRequest(Guid EventId, string Title);
    public record UpdateEventTitleResponse(string Title);
}
