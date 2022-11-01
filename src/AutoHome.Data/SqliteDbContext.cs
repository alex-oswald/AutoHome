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

        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(DateTime));
                foreach (var property in properties)
                {
                    // Save DateTime's in ISO 8601 format
                    // Query DateTime's and produce DateTime with the proper Kind
                    modelBuilder
                        .Entity(entityType.Name)
                        .Property<DateTime>(property.Name)
                        .HasConversion(
                            convertToProviderExpression: v => new DateTimeOffset(v).ToString("O"),
                            convertFromProviderExpression: v => new DateTime(DateTimeOffset.Parse(v).UtcTicks, DateTimeKind.Utc));
                }
            }
        }
    }
}
