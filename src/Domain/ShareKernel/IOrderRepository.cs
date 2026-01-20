namespace Domain.ShareKernel;

public interface IOrderRepository : IRepository<Entities.Order>
{
    Task<Entities.Order?> GetByIdAsync(Guid id);
    Task<Entities.Order?> GetAsync(Guid id);
    Task<List<Entities.Order>?> GetListByCustomerAsync(Guid customerId);
    Task<Entities.Order?> GetByOrderNumber(long orderNumber);
    Task AddAsync(Entities.Order order);
}

