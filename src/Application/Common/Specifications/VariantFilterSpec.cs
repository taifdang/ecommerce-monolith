using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class VariantFilterSpec : Specification<ProductVariant>
{
    public VariantFilterSpec(int? productId, int productVariantId)
    {
        Query
            .Where(x => (!productId.HasValue || x.ProductId == productId) && x.Id == productVariantId);
    }
}
