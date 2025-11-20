using Application.Catalog.Products.Queries.GetProductById;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Catalog.Products;

public static class ProductManualMappings
{
    public static ProductItemDto ToDto(this Product product)
    {
        return new ProductItemDto
        {
            Id = product.Id,
            Title = product.Title,
            MinPrice = product.ProductVariants.Min(x => x.RegularPrice),
            MaxPrice = product.ProductVariants.Max(x => x.RegularPrice),
            Description = product.Description ?? string.Empty,
            Category = product.Category.Title,
            Images = product.ProductImages.Select(img => new ImageLookupDto
            {
                Id = img.Id,
                Url = img.ImageUrl,
            }).ToList(),
            Options = product.ProductOptions.Select(po => new ProductOptionDto
            {
                Title = po.OptionName,
                Values = po.OptionValues.Select(v => v.Value).ToList()
            }).ToList(),
            OptionValues = product.ProductOptions.Select(po => new ProductOptionValueDto
            {
                Title = po.OptionName,
                Values = po.OptionValues.Select(v => v.Value).ToList(),
                Variants = po.OptionValues.Select(ov => new ProductOptionVariantDto
                {
                    Title = ov.Value,
                    Label = ov.Label,
                    Images = ov.ProductImages!.Select(pi => new ImageLookupDto
                    {
                        Id = pi.Id,
                        Url = pi.ImageUrl
                    }).ToList()
                }).ToList()
            }).ToList(),
        };
    }
}
