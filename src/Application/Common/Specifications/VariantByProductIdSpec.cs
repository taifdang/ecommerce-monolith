using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class VariantByProductIdSpec : Specification<ProductVariant>
{
    public VariantByProductIdSpec(Guid ProductId)
    {
        Query
            .Where(x => x.ProductId == ProductId);
    }
}
