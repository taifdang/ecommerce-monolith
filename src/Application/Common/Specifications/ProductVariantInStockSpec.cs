using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductVariantInStockSpec : Specification<ProductVariant>
{
    public ProductVariantInStockSpec(Guid Id)
    {
        Query
            .Where(x => 
                x.Id == Id &&
                x.Status == Domain.Enums.IntentoryStatus.InStock);
    }
}
