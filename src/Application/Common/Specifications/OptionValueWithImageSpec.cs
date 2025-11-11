using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class OptionValueWithImageSpec : Specification<OptionValue>
{
    public OptionValueWithImageSpec(int optionValueId, int productId)
    {
        Query
            .Include(x => x.ProductOption)
            .Where(x => 
                x.ProductOption.ProductId == productId &&
                x.Id == optionValueId && 
                x.ProductOption.AllowImage);
    }
}
