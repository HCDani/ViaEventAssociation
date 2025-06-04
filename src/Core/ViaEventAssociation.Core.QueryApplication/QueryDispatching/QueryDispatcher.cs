using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.QueryContracts.Contract;
using ViaEventAssociation.Core.QueryContracts.Queries;
using ViaEventAssociation.Infrastructure.Queries.Persistence;
using ViaEventAssociation.Infrastructure.Queries.Queries;

namespace ViaEventAssociation.Core.QueryApplication.QueryDispatching {
    public class QueryDispatcher : IQueryDispatcher {
        ScaffoldingDbinitContext context;
        public QueryDispatcher(ScaffoldingDbinitContext _context) {
            context = _context;
        }

        public Task<TAnswer> DispatchAsync<TQuery, TAnswer>(IQuery<TQuery, TAnswer> query) {
            Type serviceType = typeof(IQuery<TQuery, TAnswer>);
            IQueryHandler<TQuery, TAnswer> handler = null;
            switch (serviceType) {
                case Type t when t == typeof(IQuery<GuestEvents.Query, GuestEvents.Answer>):
                    handler = (IQueryHandler<TQuery, TAnswer>)new GuestEventsHandler(context);
                    break;
                case Type t when t == typeof(IQuery<UnpublishedEvents.Query, UnpublishedEvents.Answer>):
                    handler = (IQueryHandler<TQuery, TAnswer>)new GetUnpublishedEventsHandler(context);
                    break;
                case Type t when t == typeof(IQuery<UpcomingEvents.Query, UpcomingEvents.Answer>):
                    handler = (IQueryHandler<TQuery, TAnswer>)new GetUpcomingEventsHandler(context);
                    break;
                case Type t when t == typeof(IQuery<GuestInfo.Query, GuestInfo.Answer>):
                    handler = (IQueryHandler<TQuery, TAnswer>)new GetGuestInfoHandler(context);
                    break;
                case Type t when t == typeof(IQuery<EventInfo.Query, EventInfo.Answer>):
                    handler = (IQueryHandler<TQuery, TAnswer>)new GetEventInfoHandler(context);
                    break;
                // Add more cases for other query handlers as needed
                default:
                    throw new NotImplementedException($"No handler implemented for query type {serviceType.Name}");
            }
            return handler.HandleAsync((TQuery)query);
        }
    }
}
