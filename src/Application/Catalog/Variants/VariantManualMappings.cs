using Application.Catalog.Variants.Dtos;
using Domain.Entities;

namespace Application.Catalog.Variants;

public static class VariantManualMappings
{
    public static VariantDto ToVariantDto(this ProductVariant variant)
    {
        return new VariantDto(
            variant.Id,
            variant.ProductId,
            variant.Product.Title,
            variant.Title ?? string.Empty,
            variant.RegularPrice,
            variant.Percent,
            variant.Quantity,
            variant.Sku ?? string.Empty,
            GetVariantImage(variant),
            GetVariantOptionValue(variant));
    }

    public static VariantImageDto? GetVariantImage(this ProductVariant variant)
    {
        var image = 
            variant.VariantOptionValues
                .SelectMany(vov => vov.OptionValue.ProductImages!
                    .Where(pi => pi.OptionValueId == vov.OptionValueId))
                .OrderBy(x => x.Id)
                .FirstOrDefault();

        return image is null
            ? null
            : new VariantImageDto(image.Id, image.ImageUrl);
    }

    public static VariantImageDto? GetMainImage(this ProductVariant variant)
    {
        var image = variant.Product.ProductImages
            .FirstOrDefault();

        return image is null
            ? null
            : new VariantImageDto(image.Id, image.ImageUrl);
    }

    private static List<VariantOptionValueDto> GetVariantOptionValue(ProductVariant variant)
    {
        return variant.VariantOptionValues
            .Select(vov => new VariantOptionValueDto(
                vov.OptionValue.ProductOption.OptionName,
                vov.OptionValue.Value))
            .ToList();
    }
}
