using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.VEvents {
    public class UpdateEventMaxNumberOfGuestsController([FromServices] ICommandDispatcher dispatcher) : ApiEndpoint
         .WithRequest<UpdateEventMaxNumberOfGuestsRequest>.WithResponse<UpdateEventMaxNumberOfGuestsResponse> {

        [HttpPost("event/update_max_number_of_guests")]
        public override async Task<ActionResult<UpdateEventMaxNumberOfGuestsResponse>> HandleAsync(UpdateEventMaxNumberOfGuestsRequest request) {
            Result<UpdateEventMaxNumberOfGuestsCommand> commandResult = await dispatcher.DispatchAsync(UpdateEventMaxNumberOfGuestsCommand.Create(request.EventId.ToString(), request.MaxNumberOfGuests).payLoad);
            if (commandResult.resultCode != 0) {
                return BadRequest(commandResult.errorMessage);
            }
            return Ok(new UpdateEventMaxNumberOfGuestsResponse(commandResult.payLoad.MaxNumberOfGuests.Value));
        }
    }
    public record UpdateEventMaxNumberOfGuestsRequest(Guid EventId, string MaxNumberOfGuests);
    public record UpdateEventMaxNumberOfGuestsResponse(int? MaxNumberOfGuests);
}
