using Application.Common.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class
{
    public EfRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
