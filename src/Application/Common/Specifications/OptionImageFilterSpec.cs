using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class OptionImageFilterSpec : Specification<ProductOption>
{
    public OptionImageFilterSpec(int productId)
    {
        Query
            .Where(x => x.ProductId == productId && x.AllowImage);
    }
}
