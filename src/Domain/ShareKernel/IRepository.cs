namespace Domain.ShareKernel;

public interface IRepository<T> where T : class
{
    IUnitOfWork UnitOfWork { get; }
}
