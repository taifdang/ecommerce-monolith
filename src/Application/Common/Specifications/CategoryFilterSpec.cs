using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class CategoryFilterSpec : Specification<Category>
{
    public CategoryFilterSpec(int CategoryId)
    {
        Query
            .Where(x => x.Id == CategoryId)
            .AsNoTracking();
    }
}
