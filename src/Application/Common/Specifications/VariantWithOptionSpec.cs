using Application.Catalog.Variants.Dtos;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class VariantWithOptionSpec : Specification<ProductVariant, VariantDto>
{
    public VariantWithOptionSpec(int productVariantId)
    {
        Query
            .Where(x => x.Id == productVariantId);

        Query
            .Select(x => new VariantDto
            {
                Id = x.Id,
                Title = x.Title ?? string.Empty,
                ProductName = x.Product.Title,
                RegularPrice = x.RegularPrice,
                Percent = x.Percent,
                Quantity = x.Quantity,
                Sku = x.Sku ?? string.Empty,
                Image = x.VariantOptionValues
                        .SelectMany(vov => vov.OptionValue.ProductImages!
                            .Where(img => img.OptionValueId == vov.OptionValueId))
                        .Select(img => new VariantImageDto
                        {
                            Id = img.Id,
                            Url = img.ImageUrl
                        })
                        .OrderBy(img => img.Id)
                        .FirstOrDefault() ?? new(),
                Options = x.VariantOptionValues.Select(y => new VariantOptionValueDto
                {
                    Title = y.OptionValue.ProductOption.OptionName,
                    Value = y.OptionValue.Value
                }).ToList()
            });

    }
}
