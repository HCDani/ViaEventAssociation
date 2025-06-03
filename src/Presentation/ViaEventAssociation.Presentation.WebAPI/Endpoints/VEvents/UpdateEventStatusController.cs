using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.VEvents {
    public class UpdateEventStatusControllerUpdateEventTitleController([FromServices] ICommandDispatcher dispatcher) : ApiEndpoint
         .WithRequest<UpdateEventStatusRequest>.WithResponse<UpdateEventStatusResponse> {

        [HttpPost("event/update_status")]
        public override async Task<ActionResult<UpdateEventStatusResponse>> HandleAsync(UpdateEventStatusRequest request) {
            Result<UpdateEventStatusCommand> commandResult = await dispatcher.DispatchAsync(UpdateEventStatusCommand.Create(request.EventId.ToString(), request.Status).payLoad);
            if (commandResult.resultCode != 0) {
                return BadRequest(commandResult.errorMessage);
            }
            return Ok(new UpdateEventStatusResponse(commandResult.payLoad.Status));
        }
    }
    public record UpdateEventStatusRequest(Guid EventId, string Status);
    public record UpdateEventStatusResponse(Enum Status);
}
