using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductVariantFilterSpec : Specification<ProductVariant>
{
    public ProductVariantFilterSpec(Guid? productId, Guid productVariantId)
    {
        Query
            .Where(x => (!productId.HasValue || x.ProductId == productId) && x.Id == productVariantId);
    }
}
