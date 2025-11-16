using Application.Catalog.Options.Dtos;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class OptionValueByIdsSpec : Specification<OptionValue, OptionValueDto>
{
    public OptionValueByIdsSpec(List<int> optionValues)
    {
        Query
            .Where(x => optionValues.Contains(x.Id));

        Query
            .Select(x => new OptionValueDto
            {
                Id = x.Id,
                Value = x.Value,
                Label = x.Label
            });
    }
}
