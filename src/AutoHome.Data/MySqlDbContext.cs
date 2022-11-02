using Microsoft.EntityFrameworkCore;

namespace AutoHome.Data;

public class MySqlDbContext : DbContext
{
    public MySqlDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Device> Devices { get; set; } = null!;

    public DbSet<Trigger> Triggers { get; set; } = null!;

    public DbSet<TriggerEvent> TriggerEvents { get; set; } = null!;

    public DbSet<Variable> Variables { get; set; } = null!;

    public DbSet<WeatherReading> WeatherReadings { get; set; } = null!;

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

        modelBuilder.Entity<Variable>()
            .HasKey(o => new { o.Id, o.Key });
    }
}
