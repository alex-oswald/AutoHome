using Microsoft.EntityFrameworkCore;

namespace AutoHome.Data;

public class SqliteDbContext : DbContext
{
    public SqliteDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Device> Devices { get; set; } = null!;

    public DbSet<Trigger> TimeTriggers { get; set; } = null!;
}
