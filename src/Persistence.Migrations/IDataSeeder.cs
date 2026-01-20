using Microsoft.EntityFrameworkCore;

namespace Persistence.Migrations;

public interface IDataSeeder<in TContext> where TContext : DbContext
{
    Task SeedAsync(CancellationToken cancellationToken);
}
