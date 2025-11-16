using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductImageByIdSpec : Specification<ProductImage>
{
    public ProductImageByIdSpec(int productId, int Id)
    {
        Query
            .Where(x => x.ProductId == productId && x.Id == Id);
    }
}
