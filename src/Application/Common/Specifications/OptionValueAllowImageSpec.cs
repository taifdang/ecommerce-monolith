using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class OptionValueAllowImageSpec : Specification<OptionValue>
{
    public OptionValueAllowImageSpec(int optionValueId, int productId)
    {
        Query
            .Include(x => x.ProductOption)
            .Where(x => 
                x.ProductOption.ProductId == productId &&
                x.Id == optionValueId && 
                x.ProductOption.AllowImage);
    }
}
