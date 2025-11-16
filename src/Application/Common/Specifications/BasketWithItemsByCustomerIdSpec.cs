using Ardalis.Specification;

namespace Application.Common.Specifications;

public class BasketWithItemsByCustomerIdSpec : Specification<Domain.Entities.Basket>
{
    public BasketWithItemsByCustomerIdSpec(int customerId)
    {
        Query
            .Where(x => x.CustomerId == customerId)
            .Include(x => x.Items);
    }
}
