using Application.Catalog.Products.Dtos;
using Application.Common.Interfaces;
using Application.Common.Specifications;
using MediatR;

namespace Application.Catalog.Products.Queries.GetProductById;

public record GetProductByIdQuery : IRequest<ProductItemDto>
{
    public int Id { get; set; } // product id
}

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductItemDto>
{
    //private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Domain.Entities.Product> _productRepository;

    public GetProductByIdQueryHandler(IRepository<Domain.Entities.Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ProductItemDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        // *When user click product, we will load all options, option values, images for product to client side
        // *Then user select option values, we will generate variant on client side (based on option values selected) to show price, sku, quantity, image...
        var productDetail = await _productRepository.FirstOrDefaultAsync(new ProductWithOptionSpec(request.Id));
        return productDetail;
        #region
        //var productVm = await _unitOfWork.ProductRepository.GetByIdAsync(
        //    filter: x => x.Id == request.Id,
        //    selector: x => new ProductDetailVm
        //    {
        //        Id = x.Id,
        //        Title = x.Title,
        //        MinPrice = x.ProductVariants.Min(x => x.RegularPrice),
        //        MaxPrice = x.ProductVariants.Max(x => x.RegularPrice),
        //        Description = x.Description ?? string.Empty,
        //        Category = x.Category.Title,
        //        ProductType = x.Category.ProductType.Title,
        //        Images = x.ProductImages.Select(img => new ProductImageDto
        //        {
        //            Id = img.Id,
        //            Url = img.Image,
        //        }).ToList(),
        //        Options = x.ProductOptions.Select(po => new OptionDto 
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
        //                Image = ov.ProductImages!.Select(pi => new ProductImageDto
        //                {
        //                    Id = pi.Id,
        //                    Url = pi.Image
        //                }).ToList()
        //            }).ToList()
        //        }).ToList(),
        //    });
        #endregion
        
    }
}
