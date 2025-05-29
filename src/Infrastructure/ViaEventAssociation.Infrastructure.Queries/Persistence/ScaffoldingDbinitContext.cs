using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ViaEventAssociation.Infrastructure.Queries.Models;

namespace ViaEventAssociation.Infrastructure.Queries.Persistence;

public partial class ScaffoldingDbinitContext : DbContext
{
    public ScaffoldingDbinitContext()
    {
    }

    public ScaffoldingDbinitContext(DbContextOptions<ScaffoldingDbinitContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventParticipation> EventParticipations { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("DataSource = C:\\Dani\\VIA\\Sem6\\DCA1\\ViaEventAssociation\\Tests\\IntegrationTests\\ScaffoldingDBInit.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasIndex(e => e.LocationId, "IX_Events_LocationId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Location).WithMany(p => p.Events).HasForeignKey(d => d.LocationId);
        });

        modelBuilder.Entity<EventParticipation>(entity =>
        {
            entity.ToTable("EventParticipation");

            entity.HasIndex(e => e.EventId, "IX_EventParticipation_EventId");

            entity.HasIndex(e => e.GuestId, "IX_EventParticipation_GuestId");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Event).WithMany(p => p.EventParticipations).HasForeignKey(d => d.EventId);

            entity.HasOne(d => d.Guest).WithMany(p => p.EventParticipations).HasForeignKey(d => d.GuestId);
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Ppurl).HasColumnName("PPUrl");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
