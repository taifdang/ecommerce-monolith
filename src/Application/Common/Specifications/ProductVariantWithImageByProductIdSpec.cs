using Ardalis.Specification;
using Domain.Entities;

namespace Application.Common.Specifications;

public class ProductVariantWithImageByProductIdSpec : Specification<ProductVariant>
{
    public ProductVariantWithImageByProductIdSpec(Guid productId)
    {
        Query            
            .Include(x => x.Product)
                .ThenInclude(x => x.ProductImages
                    .Where(img => img.IsMain && img.OptionValueId == null))
            .Where(x => x.ProductId == productId);
    }
}

//Query
//   .Select(x => new VariantWithImageDto(
//   x.Id,
//   x.ProductId,
//   x.Product.ProductImages
//       .Where(img => img.IsMain && img.OptionValueId == null)
//       .Select(img => new VariantImageDto(img.Id, img.ImageUrl))
//       .FirstOrDefault()));

//x.Product.ProductImages.Any(img => img.IsMain && img.ProductOptionId == null));

//Query
//    .Select(x =>
//        x.Product.ProductImages
//            .Where(img => img.IsMain && img.OptionValueId == null)
//            .Select(img => new VariantImageDto(img.Id, img.ImageUrl))
//            .FirstOrDefault()
//    );
