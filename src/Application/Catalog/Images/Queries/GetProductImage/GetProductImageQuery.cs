using Application.Catalog.Images.Services;
using MediatR;

namespace Application.Catalog.Images.Queries.GetProductImage;

public record GetProductImageQuery(Guid ProductId,Guid? OptionValueId = null) : IRequest<ProductImageResult>;

public class GetProductImageQueryHandler : IRequestHandler<GetProductImageQuery, ProductImageResult>
{
    private readonly IProductImageService _productImageService;

    public GetProductImageQueryHandler(IProductImageService productImageService)
    {
        _productImageService = productImageService;
    }

    public async Task<ProductImageResult> Handle(GetProductImageQuery request, CancellationToken cancellationToken)
    {
        return await _productImageService.GetOrderedImagesAsync(request.ProductId, request.OptionValueId);
    }
}
//var selectedVariantId = request.OptionValueId;
//    var orderedList = images
//        .Select(img => new
//        {
//            Dto = new ImageLookupDto { Id = img.Id, Url = img.ImageUrl! },
//            Priority = img.OptionValueId == selectedVariantId ? 100 : // variant
//                       img.IsMain && img.OptionValueId == null ? 50 : // main
//                       img.OptionValueId == null ? 20 : 0, // common
//            PriorityOrder = img.OptionValueId == selectedVariantId ? 0 :
//                           img.IsMain && img.OptionValueId == null ? 1 :
//                           img.OptionValueId == null ? 2 : 3
//        })
//        .OrderByDescending(x => x.Priority)
//        .ThenBy(x => x.PriorityOrder)
//        .ThenBy(x => x.Dto.Id)
//        .Select(x => x.Dto)
//        .ToList();
//var mainImage = images.FirstOrDefault();
//var result = new ProductImageResult
//{
//    MainImage = new ImageLookupDto { Id = mainImage.Id, Url = mainImage.ImageUrl},
//    CommonImages = orderedList,
//    VariantImages = images
//        .Where(x => x.OptionValueId.HasValue)
//        .ToDictionary(
//            x => x.OptionValueId!.Value,
//            x => new ImageLookupDto { Id = x.Id, Url = x.ImageUrl! }
//        )
//};