using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Application.Commands.LocationNS {
    public class GetLocationByIdCommand {
        public Guid LocationId { get; }
        public GetLocationByIdCommand(Guid locationId) {
            LocationId = locationId;
        }
        public static Result<GetLocationByIdCommand> Create(string locationId) {
            if (string.IsNullOrEmpty(locationId)) {
                return new Result<GetLocationByIdCommand>(1, "Location ID cannot be null or empty");
            }
            if (!Guid.TryParse(locationId, out Guid parsedLocationId)) {
                return new Result<GetLocationByIdCommand>(2, "Invalid Location ID format");
            }
            return new Result<GetLocationByIdCommand>(new GetLocationByIdCommand(parsedLocationId));
        }
        public Result<GetLocationByIdCommand> AddResponse(Guid locationId) {
            return new Result<GetLocationByIdCommand>(this);
        }
    }
}
