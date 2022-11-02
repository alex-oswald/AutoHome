using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AutoHome.Data;

/// <summary>
/// This class is used when adding migrations.
/// </summary>
public class SqlDbContextFactory : IDesignTimeDbContextFactory<MySqlDbContext>
{
    public MySqlDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MySqlDbContext>();
        optionsBuilder.UseMySql("server=localhost;user=root;password=;database=AutoHome",
            new MariaDbServerVersion(new Version(10, 8, 5)));

        return new MySqlDbContext(optionsBuilder.Options);
    }
}
