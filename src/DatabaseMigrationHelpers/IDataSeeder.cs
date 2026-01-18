using Microsoft.EntityFrameworkCore;

namespace DatabaseMigrationHelpers;

public interface IDataSeeder<in TContext> where TContext : DbContext
{
    Task SeedAsync(CancellationToken cancellationToken);
}
