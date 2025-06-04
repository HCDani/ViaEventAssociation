using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.Application.AppEntry;
using ViaEventAssociation.Core.Application.Commands.Event;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Infrastructure.Queries.Models;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.VEvents {
    public class GetEventByIdController([FromServices] ICommandDispatcher dispatcher) : ApiEndpoint
        .WithRequest<GetEventByIdRequest>.WithResponse<GetEventByIdResponse> {

        [HttpGet("event/get_by_id")]
        public override async Task<ActionResult<GetEventByIdResponse>> HandleAsync(GetEventByIdRequest request) {
            Result<GetEventByIdCommand> commandResult = await dispatcher.DispatchAsync(GetEventByIdCommand.Create(request.EventId.ToString()).payLoad);
            if (commandResult.resultCode != 0) {
                return BadRequest(commandResult.errorMessage);
            }
            VEvent? vEvent = commandResult.payLoad.VEvent;
            return Ok(new GetEventByIdResponse(vEvent?.Title.Value,vEvent?.Description.Value, vEvent?.Duration.From,vEvent?.Duration.To,(int?)vEvent?.Status,
                (int?)vEvent?.Visibility, vEvent?.MaxNumberOfGuests.Value, vEvent?.Location?.Id));
        }
    }
    public record GetEventByIdRequest(Guid EventId);
    public record GetEventByIdResponse(string? Title, string? Description, DateTime? From, DateTime? To, int? Status, int? Visibility, int? MaxNumberOfGuests, Guid? LocationId);
}
