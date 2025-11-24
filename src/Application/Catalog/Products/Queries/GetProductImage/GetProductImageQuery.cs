using Application.Catalog.Products.Services;
using MediatR;

namespace Application.Catalog.Products.Queries.GetProductImage;

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