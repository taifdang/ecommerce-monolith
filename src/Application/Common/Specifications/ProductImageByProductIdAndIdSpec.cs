using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductImageByProductIdAndIdSpec : Specification<ProductImage>
{
    public ProductImageByProductIdAndIdSpec(Guid productId, Guid Id)
    {
        Query
            .Where(x => x.ProductId == productId && x.Id == Id);
    }
}
