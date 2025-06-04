using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.VEvents {
    public class UpdateEventVisibilityController([FromServices] ICommandDispatcher dispatcher) : ApiEndpoint
         .WithRequest<UpdateEventVisibilityRequest>.WithResponse<UpdateEventVisibilityResponse> {

        [HttpPost("event/update_visibility")]
        public override async Task<ActionResult<UpdateEventVisibilityResponse>> HandleAsync(UpdateEventVisibilityRequest request) {
            Result<UpdateEventVisibilityCommand> commandResult = await dispatcher.DispatchAsync(UpdateEventVisibilityCommand.Create(request.EventId.ToString(), request.Visibility).payLoad);
            if (commandResult.resultCode != 0) {
                return BadRequest(commandResult.errorMessage);
            }
            return Ok(new UpdateEventVisibilityResponse((int)commandResult.payLoad.Visibility));
        }
    }
    public record UpdateEventVisibilityRequest(Guid EventId, string Visibility);
    public record UpdateEventVisibilityResponse(int? Visibility);
}
