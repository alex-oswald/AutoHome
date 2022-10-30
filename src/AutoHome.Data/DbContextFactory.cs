using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AutoHome.Data;

/// <summary>
/// This class is used when adding migrations.
/// </summary>
public class SqlDbContextFactory : IDesignTimeDbContextFactory<SqliteDbContext>
{
    public SqliteDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SqliteDbContext>();
        optionsBuilder.UseSqlite("Data Source=AutoHome.db");

        return new SqliteDbContext(optionsBuilder.Options);
    }
}
