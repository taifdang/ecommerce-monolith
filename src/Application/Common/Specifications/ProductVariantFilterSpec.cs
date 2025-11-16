using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductVariantFilterSpec : Specification<ProductVariant>
{
    public ProductVariantFilterSpec(int? productId, int productVariantId)
    {
        Query
            .Where(x => (!productId.HasValue || x.ProductId == productId) && x.Id == productVariantId);
    }
}
