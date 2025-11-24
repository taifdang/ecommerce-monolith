using Ardalis.Specification;

namespace Application.Common.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
    IUnitOfWork UnitOfWork { get; }
}
