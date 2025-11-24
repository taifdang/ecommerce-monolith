using Ardalis.Specification;

namespace Application.Basket.Specifications;

public class BasketSpec : Specification<Domain.Entities.Basket>
{
    public BasketSpec()
    {

    }

    public BasketSpec ByCustomerId(Guid CustomerId)
    {
        Query.Where(x => x.CustomerId == CustomerId);
        return this;
    }

    public BasketSpec WithItems()
    {
        Query.Include(x => x.Items);
        return this;
    }
}
