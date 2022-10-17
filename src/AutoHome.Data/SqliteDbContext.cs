using Microsoft.EntityFrameworkCore;

namespace AutoHome.Data;

public class SqliteDbContext : DbContext
{
    public SqliteDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Device> Devices { get; set; } = null!;

    public DbSet<TimeTrigger> TimeTriggers { get; set; } = null!;
}
