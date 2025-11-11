using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ImageFilterSpec : Specification<ProductImage>
{
    public ImageFilterSpec(int productId, int? optionValueId)
    {
        Query
            .Where(x => 
            x.ProductId == productId &&
            (!optionValueId.HasValue || x.OptionValueId == optionValueId));         
    }
}
