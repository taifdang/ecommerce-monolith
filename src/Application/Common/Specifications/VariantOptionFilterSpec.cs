using Application.Catalog.Variants.Dtos;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class VariantOptionFilterSpec : Specification<ProductVariant, VariantDto>
{
    public VariantOptionFilterSpec(
        int productId, 
        //Dictionary<int, int>? options,
        List<int> optionValues, 
        bool exact)
    {
        Query
            .Where(c => c.ProductId == productId);

        if (optionValues != null && optionValues.Count > 0)
        {
            // Not enough to option values, need to match all provided options
            //Query.Where(x =>
                //options.All(opt =>
                //    x.VariantOptionValues.Any(v =>
                //        v.OptionValue.ProductOptionId == opt.Key &&
                //        v.OptionValue.Id == opt.Value))
                //);
                Query
                    .Where(x =>
                        optionValues.All(opt =>
                            x.VariantOptionValues.Any(v =>
                                v.OptionValue.Id == opt))
                        );

            if (exact)
            {
                // Corrected to ensure exact match of option values
                // Ex: Color : Red, Size: M  should not match Color: Red, Size: M, Material: Cotton
                //Query
                //.Where(x => x.VariantOptionValues.Count == options.Count);

                Query
                  .Where(x => x.VariantOptionValues.Count == optionValues.Count);

            }
        }

        Query
            .Select(x => new VariantDto
            {
                Id = x.Id,
                Title = x.Title ?? string.Empty,
                RegularPrice = x.RegularPrice,
                //MaxPrice = x.MaxPrice,
                Percent = x.Percent,
                Quantity = x.Quantity,
                Sku = x.Sku ?? string.Empty,
                Image = x.VariantOptionValues
                        .SelectMany(vov => vov.OptionValue.ProductImages!
                        .Where(img => img.OptionValueId == vov.OptionValueId))
                        .OrderBy(img => img.Id)
                        .Select(img => new VariantImageDto
                        {
                            Id = img.Id,
                            Url = img.ImageUrl
                        })                
                        .FirstOrDefault(),
                Options = x.VariantOptionValues.Select(y => new VariantOptionValueDto
                {
                    Title = y.OptionValue.ProductOption.OptionName,
                    Value = y.OptionValue.Value
                }).ToList()
            });
    }
}
