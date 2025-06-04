using Mapster;
using Microsoft.AspNetCore.Mvc;
using ViaEventAssociation.Core.QueryApplication.QueryDispatching;
using ViaEventAssociation.Core.QueryContracts.Queries;

namespace ViaEventAssociation.Presentation.WebAPI.Endpoints.Queries {
    public class EventInfoController(IQueryDispatcher dispatcher) : ApiEndpoint.WithRequest<EventInfoController.EventInfoRequest>.WithResponse<EventInfoController.EventInfoResponse> {

        [HttpGet("query/eventinfo/{EventId}")]
        public override async Task<ActionResult<EventInfoResponse>> HandleAsync([FromRoute]EventInfoRequest request) {
            EventInfo.Query query = request.Adapt<EventInfo.Query>();
            EventInfo.Answer answer = await dispatcher.DispatchAsync<EventInfo.Query, EventInfo.Answer>(query);

            if (answer == null) {
                return NotFound("Event not found.");
            }

            return Ok(answer.Adapt<EventInfoResponse>());
        }
        public record EventInfoRequest([FromRoute]string? EventId);
        public record EventInfoResponse(Guid? Id, string? Title, string? Description, DateTime? From, DateTime? To, int? MaxNumberOfGuests, int? CurrentNumberOfGuests,
            string? LocationName,int? Visibility,List<GuestDetails> GuestDetailsList);
        public record GuestDetails(Guid? Id, string? FirstName, string? LastName, string? ProfilePictureUrl);
    }
}
