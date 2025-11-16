using Application.Catalog.Categories.Dtos;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class CategoryListSpec : Specification<Category, CategoryDto>
{
    public CategoryListSpec()
    {
        Query
            .Select(x => new CategoryDto
            {
                Id = x.Id,
                Title = x.Title,
                Lable = x.Label ?? "",
            });
    }
}
