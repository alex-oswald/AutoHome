using AutoHome.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoHome.Data;

public class SqliteDbContext : DbContext
{
    public SqliteDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Device> Devices { get; set; } = null!;

    public DbSet<Trigger> Triggers { get; set; } = null!;

    public DbSet<TriggerEvent> TriggerEvents { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Trigger>()
            .HasOne(o => o.Device)
            .WithMany()
            .HasForeignKey(o => o.DeviceId);

        modelBuilder.Entity<TriggerEvent>()
            .HasOne(o => o.Trigger)
            .WithMany()
            .HasForeignKey(o => o.TriggerId);
    }
}
