using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductImageFilterSpec : Specification<ProductImage>
{
    public ProductImageFilterSpec(Guid productId, Guid? optionValueId)
    {
        Query
            .Where(x => 
            x.ProductId == productId &&
            (!optionValueId.HasValue || x.OptionValueId == optionValueId));         
    }
}
