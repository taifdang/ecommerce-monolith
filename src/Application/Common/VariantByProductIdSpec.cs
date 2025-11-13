using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common;

public class VariantByProductIdSpec : Specification<ProductVariant>
{
    public VariantByProductIdSpec(int ProductId)
    {
        Query
            .Where(x => x.ProductId == ProductId);
    }
}
