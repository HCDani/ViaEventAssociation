using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.VEvents {
    public class UpdateEventDurationController([FromServices] ICommandDispatcher dispatcher) : ApiEndpoint
         .WithRequest<UpdateEventDurationRequest>.WithResponse<UpdateEventDurationResponse> {

        [HttpPost("event/update_duration")]
        public override async Task<ActionResult<UpdateEventDurationResponse>> HandleAsync(UpdateEventDurationRequest request) {
            Result<UpdateEventDurationCommand> commandResult = await dispatcher.DispatchAsync(UpdateEventDurationCommand.Create(request.EventId.ToString(), request.StartTime, request.EndTime).payLoad);
            if (commandResult.resultCode != 0) {
                return BadRequest(commandResult.errorMessage);
            }
            return Ok(new UpdateEventDurationResponse(commandResult.payLoad.Duration.From, commandResult.payLoad.Duration.To));
        }
    }
    public record UpdateEventDurationRequest(Guid EventId, DateTime StartTime, DateTime EndTime);
    public record UpdateEventDurationResponse(DateTime? StartTime, DateTime? EndTime);
}
