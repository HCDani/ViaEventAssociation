using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.VEvents {
    public class UpdateEventDescriptionController([FromServices] ICommandDispatcher dispatcher) : ApiEndpoint
        .WithRequest<UpdateEventDescriptionRequest>.WithResponse<UpdateEventDescriptionResponse> {

        [HttpPost("event/update_description")]
        public override async Task<ActionResult<UpdateEventDescriptionResponse>> HandleAsync(UpdateEventDescriptionRequest request) {
            Result<UpdateEventDescriptionCommand> commandResult = await dispatcher.DispatchAsync(UpdateEventDescriptionCommand.Create(request.EventId.ToString(), request.Description).payLoad);
            if (commandResult.resultCode != 0) {
                return BadRequest(commandResult.errorMessage);
            }
            return Ok(new UpdateEventDescriptionResponse(commandResult.payLoad.Description.Value));
        }

    }
    public record UpdateEventDescriptionRequest(Guid EventId, string Description);
    public record UpdateEventDescriptionResponse(string Description);
}
