using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Common.RepoContracts;
using ViaEventAssociation.Infrastructure.Persistence;

namespace UnitTests.Features.Tools.Fakes {
    public class InMemGenericRepoStub<T>(EFCDbContext context) : IGenericRepository<T> where T : class {
        public async Task CreateAsync(T entity) {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }
        public async Task<T> GetAsync(Guid id) {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task DeleteAsync(Guid id) {
            context.Set<T>().Remove(await context.Set<T>().FindAsync(id));
        }
    }
}
