using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductByIdSpec : Specification<Product>
{
    public ProductByIdSpec(int productId)
    {
        Query
            .Where(x => x.Id == productId);
    }
}
