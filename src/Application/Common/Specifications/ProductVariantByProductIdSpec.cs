using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductVariantByProductIdSpec : Specification<ProductVariant>
{
    public ProductVariantByProductIdSpec(Guid ProductId)
    {
        Query
            .Where(x => x.ProductId == ProductId);
    }
}
