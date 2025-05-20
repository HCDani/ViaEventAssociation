using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Common.Bases;

namespace ViaEventAssociation.Core.Domain.Common.RepoContracts {
    public interface IGenericRepository<T> where T : Entity<Guid> {
        Task CreateAsync(T entity);
        Task<T> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
    }
}
