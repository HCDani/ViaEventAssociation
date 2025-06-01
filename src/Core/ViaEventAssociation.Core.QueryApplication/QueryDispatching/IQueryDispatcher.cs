using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.QueryContracts.Contract;

namespace ViaEventAssociation.Core.QueryApplication.QueryDispatching {
    public interface IQueryDispatcher {
        public Task<TAnswer> DispatchAsync<TQuery,TAnswer>(IQuery<TQuery,TAnswer> query);
    }
}
