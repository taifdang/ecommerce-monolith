using Application.Catalog.Options.Queries.GetOptionValueById;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class OptionValuesByIdsSpec : Specification<OptionValue, OptionValueDto>
{
    public OptionValuesByIdsSpec(List<Guid> optionValues)
    {
        Query
            .Where(x => optionValues.Contains(x.Id));

        Query
            .Select(x => new OptionValueDto(x.Id, x.Value, x.Label));
    }
}
