using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductFilterSpec : Specification<Product>
{
    public ProductFilterSpec(int productId)
    {
        Query
            .Where(x => x.Id == productId);
    }
}
