using Microsoft.EntityFrameworkCore;

namespace AutoCurtains.Data;

public class SqliteDbContext : DbContext
{
    public SqliteDbContext(DbContextOptions options)
        : base(options)
    {
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<TimeTrigger>? TimeTriggers { get; set; }
}
