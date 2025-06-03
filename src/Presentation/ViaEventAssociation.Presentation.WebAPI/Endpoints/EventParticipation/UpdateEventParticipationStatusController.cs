using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Application.Commands.EventGuestParticipation;
using ViaEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Presentation.WebAPI.Endpoints.VEvents;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.EventParticipation {
    public class UpdateEventParticipationStatusController([FromServices] ICommandDispatcher dispatcher) : ApiEndpoint
         .WithRequest<UpdateEventParticipationStatusRequest>.WithResponse<UpdateEventParticipationStatusResponse> {

        [HttpPost("event_participation/update_status")]
        public override async Task<ActionResult<UpdateEventParticipationStatusResponse>> HandleAsync(UpdateEventParticipationStatusRequest request) {
            Result<UpdateEventParticipationStatusCommand> commandResult = await dispatcher.DispatchAsync(UpdateEventParticipationStatusCommand.Create(request.Id.ToString(), request.Status,request.EventId.ToString(), request.GuestId.ToString()).payLoad);
            if (commandResult.resultCode != 0) {
                return BadRequest(commandResult.errorMessage);
            }
            return Ok(new UpdateEventParticipationStatusResponse(commandResult.payLoad.Status));
        }
    }
    public record UpdateEventParticipationStatusRequest(Guid Id, string Status,Guid EventId, Guid GuestId);
    public record UpdateEventParticipationStatusResponse(Enum Status);
}
