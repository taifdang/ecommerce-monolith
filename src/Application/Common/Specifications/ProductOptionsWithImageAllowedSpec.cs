using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductOptionsWithImageAllowedSpec : Specification<ProductOption>
{
    public ProductOptionsWithImageAllowedSpec(Guid productId)
    {
        Query
            .Where(x => x.ProductId == productId && x.AllowImage);
    }
}
