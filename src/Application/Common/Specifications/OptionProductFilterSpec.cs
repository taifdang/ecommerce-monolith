using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class OptionProductFilterSpec : Specification<ProductOption>
{
    public OptionProductFilterSpec(int productId, int? Id)
    {
        Query
            .Where(x => x.ProductId == productId && (!Id.HasValue || x.Id == Id));
    }
}
