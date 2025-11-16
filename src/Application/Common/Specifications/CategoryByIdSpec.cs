using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class CategoryByIdSpec : Specification<Category>
{
    public CategoryByIdSpec(int CategoryId)
    {
        Query
            .Where(x => x.Id == CategoryId)
            .AsNoTracking();
    }
}
