using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductOptionAllowImage : Specification<ProductOption>
{
    public ProductOptionAllowImage(int productId)
    {
        Query
            .Where(x => x.ProductId == productId && x.AllowImage);
    }
}
