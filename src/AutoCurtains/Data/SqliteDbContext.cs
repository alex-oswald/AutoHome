using Microsoft.EntityFrameworkCore;

namespace AutoCurtains.Data;

public class SqliteDbContext : DbContext
{
    public SqliteDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<TimeTrigger> TimeTriggers { get; set; }
}
