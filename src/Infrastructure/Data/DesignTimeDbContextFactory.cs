using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
#if (sqlServer)
 optionsBuilder.UseSqlServer("Server=LAPTOP-J20BGGNG\\SQLEXPRESS;Database=ecommerce_db;Trusted_Connection=true; MultipleActiveResultSets=true; TrustServerCertificate=True");
#endif
        optionsBuilder.UseNpgsql("Host=localhost;Database=shopdb;Username=postgres;Password=postgres;");

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
