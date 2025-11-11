using Ardalis.Specification;

namespace Application.Common.Specifications;

public class BasketCustomerWithItemSpec : Specification<Domain.Entities.Basket>
{
    public BasketCustomerWithItemSpec(int customerId)
    {
        Query
            .Where(x => x.CustomerId == customerId)
            .Include(x => x.Items);
 
    }
}
