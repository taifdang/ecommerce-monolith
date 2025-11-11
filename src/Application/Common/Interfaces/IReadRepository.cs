using Ardalis.Specification;

namespace Application.Common.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
{
}
