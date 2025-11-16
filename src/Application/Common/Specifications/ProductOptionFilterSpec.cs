using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductOptionFilterSpec : Specification<ProductOption>
{
    public ProductOptionFilterSpec(int productId, int? Id)
    {
        Query
            .Where(x => x.ProductId == productId && (!Id.HasValue || x.Id == Id));
    }
}
