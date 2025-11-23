using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductImageListSpec : Specification<ProductImage>
{
    public ProductImageListSpec(Guid ProductId)
    {
        Query.Where(x => x.ProductId == ProductId);
             //.OrderBy(y => y.Id);
    }
}
