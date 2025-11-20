using Application.Catalog.Options.Queries.GetOptionValueById;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class OptionValueByIdsSpec : Specification<OptionValue, OptionValueDto>
{
    public OptionValueByIdsSpec(List<Guid> optionValues)
    {
        Query
            .Where(x => optionValues.Contains(x.Id));

        Query
            .Select(x => new OptionValueDto(x.Id, x.Value, x.Label));
    }
}
