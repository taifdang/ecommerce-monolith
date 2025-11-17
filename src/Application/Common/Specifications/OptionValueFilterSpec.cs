using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class OptionValueFilterSpec : Specification<OptionValue>
{
    public OptionValueFilterSpec(int? optionValueId, int productOptionId)
    {        
        Query
            .Include(x => x.ProductOption)
            .Where(x => 
            (!optionValueId.HasValue || x.Id == optionValueId) && 
            x.ProductOption.Id == productOptionId);
    }
}
