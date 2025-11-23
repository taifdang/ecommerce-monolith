using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductImageByProductIdSpec : Specification<ProductImage>
{
    public ProductImageByProductIdSpec(Guid ProductId)
    {
        Query.Where(x => x.ProductId == ProductId);
    }
}
