using Ardalis.EFCore.Extensions;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.Entities;
using SmartEnum.EFCore;

namespace MobyLabWebProgramming.Infrastructure.Database;

/// <summary>
/// This is the database context used to connect with the database and links the ORM, Entity Framework, with it.
/// </summary>
public sealed class WebAppDatabaseContext : DbContext
{
    public WebAppDatabaseContext(DbContextOptions<WebAppDatabaseContext> options, bool migrate = true) : base(options)
    {
        if (migrate)
        {
            Database.Migrate();
        }
    }


    /// <summary>
    /// Here additional configuration for the ORM is performed.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasPostgresExtension("unaccent")
            .ApplyAllConfigurationsFromCurrentAssembly(); // Here all the classes that contain implement IEntityTypeConfiguration<T> are searched at runtime
                                                          // such that each entity that needs to be mapped to the database tables is configured accordingly.
        modelBuilder.ConfigureSmartEnum(); // Add support for smart enums.

        modelBuilder.Entity<Role>()
            .HasOne(r => r.Piece)
            .WithMany()
            .HasForeignKey(r => r.IdPiesa);

        modelBuilder.Entity<Role>()
            .HasOne(r => r.Actor)
            .WithMany()
            .HasForeignKey(r => r.IdActor);

        modelBuilder.Entity<Performance>()
            .HasOne(p => p.Piece)
            .WithMany()
            .HasForeignKey(p => p.IdPiesa);

        modelBuilder.Entity<Performance>()
            .HasOne(p => p.Hall)
            .WithMany()
            .HasForeignKey(p => p.HallId);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Performance)
            .WithMany()
            .HasForeignKey(t => t.PerformanceId);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId);

        modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Transaction)
            .WithMany()
            .HasForeignKey(t => t.TransactionId);
        
        modelBuilder.Entity<Role>()
            .HasKey(r => new { r.IdActor, r.IdPiesa });

        modelBuilder.Entity<Role>()
            .HasOne(r => r.Actor)
            .WithMany(a => a.Roles)
            .HasForeignKey(r => r.IdActor);

        modelBuilder.Entity<Role>()
            .HasOne(r => r.Piece)
            .WithMany(p => p.Roles)
            .HasForeignKey(r => r.IdPiesa);
    }
}