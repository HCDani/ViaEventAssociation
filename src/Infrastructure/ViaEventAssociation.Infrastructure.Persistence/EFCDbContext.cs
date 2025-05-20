using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS;
using ViaEventAssociation.Core.Domain.Aggregates.EventNS.Values;
using ViaEventAssociation.Core.Domain.Aggregates.GuestNS;
using ViaEventAssociation.Core.Domain.Aggregates.LocationNS;
using ViaEventAssociation.Core.Domain.Entities.EventGuestParticipation;

namespace ViaEventAssociation.Infrastructure.Persistence {
    public class EFCDbContext(DbContextOptions options) : DbContext(options) {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var DbPath = System.IO.Path.Join(path, "viaeventassociation.db");
            optionsBuilder.UseSqlite($"Data Source = {DbPath}");
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFCDbContext).Assembly);

            // Configure VEvent value objects as an owned type
            modelBuilder.Entity<VEvent>(builder => {
                builder.HasKey(g => g.Id);
                builder.OwnsOne(v => v.Description, descriptionBuilder => {
                    descriptionBuilder.Property(d => d.Value)
                        .HasColumnName("Description").IsRequired(false); // Column name in the VEvent table
                });
                // Configure EventDuration as an owned type
                builder.OwnsOne(v => v.Duration, durationBuilder => {
                    durationBuilder.Property(d => d.From)
                        .HasColumnName("DurationFrom").IsRequired(false); // Column name for the 'From' property

                    durationBuilder.Property(d => d.To)
                        .HasColumnName("DurationTo").IsRequired(false); // Column name for the 'To' property
                });
                builder.OwnsOne(v => v.MaxNumberOfGuests, maxNumberOfGuestBuilder => {
                    maxNumberOfGuestBuilder.Property(d => d.Value)
                        .HasColumnName("MaxNumberOfGuests").IsRequired(false); // Column name in the VEvent table
                });
                builder.OwnsOne(v => v.Title, titleBuilder => {
                    titleBuilder.Property(d => d.Value)
                        .HasColumnName("Title").IsRequired(false); // Column name in the VEvent table
                });
                builder.HasOne<Location>(v => v.Location).WithMany().IsRequired(false);
                //                builder.HasMany<Location>(v => v.Locations).WithOne(l => l.VEvent).HasForeignKey(l => l.VEventId).OnDelete(DeleteBehavior.Cascade);

            });
            // Configure Guest value objects as an owned type
            modelBuilder.Entity<Guest>(builder => {
                builder.HasKey(g => g.Id);
                builder.OwnsOne(v => v.Email, emailBuilder => {
                    emailBuilder.Property(d => d.Value).HasColumnName("Email").IsRequired(false);
                });
                builder.OwnsOne(v => v.Name, nameBuilder => {
                    nameBuilder.Property(d => d.FirstName).HasColumnName("FirstName").IsRequired(false);
                    nameBuilder.Property(d => d.LastName).HasColumnName("LastName").IsRequired(false);
                });
                builder.OwnsOne(v => v.ProfilePictureUrl, profilePictureUrlBuilder => {
                    profilePictureUrlBuilder.Property(d => d.Value).HasColumnName("PPUrl").IsRequired(false);
                });
            });
            // Configure Location value objects as an owned type
            modelBuilder.Entity<Location>(builder => {
                builder.HasKey(g => g.Id);
                builder.OwnsOne(v => v.LocationName, locationNameBuilder => {
                    locationNameBuilder.Property(d => d.Value).HasColumnName("LocationName").IsRequired(false);
                });
                builder.OwnsOne(v => v.MaxCapacity, maxCapacityBuilder => {
                    maxCapacityBuilder.Property(d => d.Value).HasColumnName("MaxCapacity").IsRequired(false);
                });
                builder.OwnsOne(v => v.Availability, availabilityBuilder => {
                    availabilityBuilder.Property(d => d.From).HasColumnName("AvailabilityFrom").IsRequired(false);
                    availabilityBuilder.Property(d => d.To).HasColumnName("AvailabilityTo").IsRequired(false);
                });
                builder.OwnsOne(v => v.Address, addressBuilder => {
                    addressBuilder.Property(d => d.Street).HasColumnName("Street").IsRequired(false);
                    addressBuilder.Property(d => d.City).HasColumnName("City").IsRequired(false);
                    addressBuilder.Property(d => d.HouseNumber).HasColumnName("HouseNumber").IsRequired(false);
                    addressBuilder.Property(d => d.PostalCode).HasColumnName("PostalCode").IsRequired(false);
                });
            });
            // Configure Location value objects as an owned type
            modelBuilder.Entity<EventParticipation>(builder => {
                builder.HasKey(g => g.Id);
            });
        }
        public DbSet<VEvent> Events => Set<VEvent>();
        public DbSet<Guest> Guests => Set<Guest>();
        public DbSet<Location> Locations => Set<Location>();
    }
    public class EFCDesignTimeDbContextFactory : IDesignTimeDbContextFactory<EFCDbContext> {
        public EFCDbContext CreateDbContext(String[] args) {
            var optionsBuilder = new DbContextOptionsBuilder<EFCDbContext>();
            optionsBuilder.UseSqlite("Data Source=viaeventassociation.db");
            return new EFCDbContext(optionsBuilder.Options);
        }
    }
}