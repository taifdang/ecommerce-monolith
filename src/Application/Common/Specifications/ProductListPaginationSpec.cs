using Application.Catalog.Products.Queries.GetListProduct;
using Application.Common.Models;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductListPaginationSpec : Specification<Product, ProductListDto>
{
    public ProductListPaginationSpec(int skip, int take)
        : base()
    {
        if(take == 0)
        {
            take = int.MaxValue;
        }
        Query
            .Where(x => x.Status == Domain.Enums.ProductStatus.Published)
            .AsSplitQuery();

        Query.Select(x => new ProductListDto
        {
            Id = x.Id,
            Title = x.Title,
            Price = x.ProductVariants.Min(pv => pv.RegularPrice), // Min Price of all variants
            Description = x.Description ?? string.Empty,
            Category = x.Category.Title,
            Image = x.ProductImages
                    .Where(c => c.IsMain && c.OptionValueId == null) // Main image not linked to option value
                    .Select(pi => new ImageLookupDto
                    {
                        Id = pi.Id,
                        Url = pi.ImageUrl
                    })
                    .FirstOrDefault() ?? new(),
        });

        Query.OrderBy(x=>x.Id).Skip(skip).Take(take);
    }
}
