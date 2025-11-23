using Application.Catalog.Products.Queries.GetProductById;
using Application.Common.Models;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductDetailsByIdSpec : Specification<Product, ProductItemDto>
{
    public ProductDetailsByIdSpec(Guid productId)
    {
        Query.Where(x => x.Id == productId).AsNoTracking();

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
//Query
//            .Select(x => new ProductItemDto
//            {
//                Id = x.Id,
//                Title = x.Title,
//                MinPrice = x.ProductVariants.Min(x => x.Price),
//                MaxPrice = x.ProductVariants.Max(x => x.Price),
//                Description = x.Description ?? string.Empty,
//                Category = x.Category.Title,
//                Images = x.ProductImages.Select(img => new ImageLookupDto
//                {
//                    Id = img.Id,
//                    Url = img.ImageUrl,
//                }).ToList(),
//                Options = x.ProductOptions.Select(po => new ProductOptionDto
//                {
//                    Title = po.OptionName,
//                    OptionValues = po.OptionValues.Select(v => v.Value).ToList()
//                }).ToList(),
//                OptionValues = x.ProductOptions.Select(po => new ProductOptionValueDto
//                {
//                    Title = po.OptionName,
//                    OptionValues = po.OptionValues.Select(v => v.Value).ToList(),
//                    Variants = po.OptionValues.Select(ov => new ProductOptionVariantDto
//                    {
//                        Title = ov.Value,
//                        Label = ov.Label,
//                        Images = ov.ProductImages!.Select(pi => new ImageLookupDto
//                        {
//                            Id = pi.Id,
//                            Url = pi.ImageUrl
//                        }).ToList()
//                    }).ToList()
//                }).ToList(),
//            });