using Microsoft.EntityFrameworkCore;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Domain.Aggregates.GuestNS;
using ViaEventAssociation.Core.Domain.Aggregates.LocationNS;

namespace ViaEventAssociation.Infrastructure.Persistence {
    public class EFCDbContext(DbContextOptions options) : DbContext(options) {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var DbPath = System.IO.Path.Join(path, "viaeventassociation.db");
            optionsBuilder.UseSqlite($"Data Source = {DbPath}");
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFCDbContext).Assembly);
        }
        public DbSet<VEvent> Events => Set<VEvent>();
        public DbSet<Guest> Guests => Set<Guest>();
        public DbSet<Location> Locations => Set<Location>();
    }
}
