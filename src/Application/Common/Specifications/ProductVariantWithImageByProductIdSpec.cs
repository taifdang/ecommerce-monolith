using Application.Catalog.Variants.Dtos;
using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductVariantWithImageByProductIdSpec : Specification<ProductVariant, VariantWithImageDto>
{
    public ProductVariantWithImageByProductIdSpec(int productId)
    {
        Query
            .Include(x => x.Product)
                .ThenInclude(x => x.ProductImages)
            .Where(x => x.ProductId == productId);

        //x.Product.ProductImages.Any(img => img.IsMain && img.OptionValueId == null));

        Query.Select(x => new VariantWithImageDto
        {
            Id = x.Id,
            ProductId = x.ProductId,
            Image = x.Product.ProductImages
                .Where(img => img.IsMain && img.OptionValueId == null)
                .Select(img => new VariantImageDto
                {
                    Id = img.Id,
                    Url = img.ImageUrl,
                }).FirstOrDefault() ?? new VariantImageDto()
        }); 
    }
}
