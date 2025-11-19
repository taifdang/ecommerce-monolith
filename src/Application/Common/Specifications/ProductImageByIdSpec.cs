using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductImageByIdSpec : Specification<ProductImage>
{
    public ProductImageByIdSpec(Guid productId, Guid Id)
    {
        Query
            .Where(x => x.ProductId == productId && x.Id == Id);
    }
}
