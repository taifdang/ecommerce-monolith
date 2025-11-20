using Application.Common.Interfaces;
using Application.Common.Specifications;
using MediatR;

namespace Application.Catalog.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductItemDto>;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductItemDto>
{
    private readonly IRepository<Domain.Entities.Product> _productRepository;

    public GetProductByIdQueryHandler(IRepository<Domain.Entities.Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductItemDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        // *When user click product, we will load all options, option values, images for product to client side
        // *Then user select option values, we will generate variant on client side (based on option values selected) to show price, sku, quantity, image...
        var product = await _productRepository.FirstOrDefaultAsync(new ProductDetailSpec(request.Id), cancellationToken);
        if (product == null)
        {
            return null!;
        }
        return product.ToDto();
        #region
        //var productVm = await _unitOfWork.ProductRepository.GetByIdAsync(
        //    filter: x => x.ProductOptionId == request.ProductOptionId,
        //    selector: x => new ProductDetailVm
        //    {
        //        ProductOptionId = x.ProductOptionId,
        //        Title = x.Title,
        //        MinPrice = x.ProductVariants.Min(x => x.RegularPrice),
        //        MaxPrice = x.ProductVariants.Max(x => x.RegularPrice),
        //        Description = x.Description ?? string.Empty,
        //        Category = x.Category.Title,
        //        ProductType = x.Category.ProductType.Title,
        //        Images = x.ProductImages.Select(img => new ImageLookupDto
        //        {
        //            ProductOptionId = img.ProductOptionId,
        //            Url = img.Image,
        //        }).ToList(),
        //        Options = x.ProductOptions.Select(po => new OptionLookupDto 
        //        { 
        //            Title = po.OptionName,
        //            Values = po.OptionValues.Select(v => v.Value).ToList()
        //        }).ToList(),
        //        OptionValues = x.ProductOptions.Select(po => new OptionValueDto
        //        {
        //            Title = po.OptionName,
        //            Values = po.OptionValues.Select(v => v.Value).ToList(),
        //            Options = po.OptionValues.Select(ov => new OptionValueImageDto
        //            {
        //                Title = ov.Value,
        //                Label = ov.Label,
        //                Image = ov.ProductImages!.Select(pi => new ImageLookupDto
        //                {
        //                    ProductOptionId = pi.ProductOptionId,
        //                    Url = pi.Image
        //                }).ToList()
        //            }).ToList()
        //        }).ToList(),
        //    });
        #endregion

    }
}
