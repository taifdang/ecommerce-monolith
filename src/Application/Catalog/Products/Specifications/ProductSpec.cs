using Application.Catalog.Products.Queries.GetListProduct;
using Application.Catalog.Products.Queries.GetProductById;
using Application.Common.Models;
using Ardalis.Specification;

namespace Application.Catalog.Products.Specifications;

public class ProductSpec : Specification<Domain.Entities.Product>
{
    public ProductSpec()
    {
        
    }

    public ProductSpec ById(Guid productId)
    {
        Query.Where(x => x.Id == productId);

        return this;
    }

    public ProductSpec WithIsPublished()
    {
        Query.Where(x => x.Status == Domain.Enums.ProductStatus.Published);

        return this;
    }

    public ProductSpec ApplyPaging(int skip, int take)
    {
        if (take == 0)
        {
            take = int.MaxValue;
        }

        Query.OrderBy(x => x.Id).Skip(skip).Take(take);

        return this;
    }
}

public class ProductListProjectionSpec : Specification<Domain.Entities.Product, ProductListDto>
{
    public ProductListProjectionSpec()
    {
        Query.Select(x => new ProductListDto
        {
            Id = x.Id,
            Title = x.Title,
            Price = x.ProductVariants.Min(pv => pv.Price),
            Category = x.Category.Title,
            Image = x.ProductImages
                 .Where(c => c.IsMain && c.OptionValueId == null) // main image
                 .Select(pi => new ImageLookupDto
                 {
                     Id = pi.Id,
                     Url = pi.ImageUrl
                 })
                 .FirstOrDefault() ?? new(),
        });
    }
}

public class ProductItemProjectionSpec : Specification<Domain.Entities.Product, ProductItemDto>
{
    public ProductItemProjectionSpec()
    {
        Query.Select(p => new ProductItemDto
        {
            Id = p.Id,
            Title = p.Title,
            MinPrice = p.ProductVariants.Min(pv => pv.Price),
            MaxPrice = p.ProductVariants.Max(pv => pv.Price),
            Description = p.Description ?? "",
            Category = p.Category.Title,
            MainImage = null,
            Images = null,
            Options = p.ProductOptions.Select(po => new ProductOptionDto
            {
                Title = po.OptionName,
                OptionValues = po.OptionValues.Select(ov => new ProductOptionValueDto
                {
                    Id = ov.Id,
                    Value = ov.Value,
                    Image = null
                }).ToList()
            }).ToList()
        });
    }
}


