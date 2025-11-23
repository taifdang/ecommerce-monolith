using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductVariantInStockByIdSpec : Specification<ProductVariant>
{
    public ProductVariantInStockByIdSpec(Guid Id)
    {
        Query
            .Where(x => 
                x.Id == Id &&
                x.Status == Domain.Enums.IntentoryStatus.InStock);
    }
}
