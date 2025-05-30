﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViaEventAssociation.Core.Domain.Common.Bases;
using ViaEventAssociation.Core.Domain.Common.RepoContracts;

namespace ViaEventAssociation.Infrastructure.Persistence.Repositories {
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity<Guid> {
        protected readonly EFCDbContext context;

        public GenericRepository(EFCDbContext _context) {
            context = _context;
        }
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
    