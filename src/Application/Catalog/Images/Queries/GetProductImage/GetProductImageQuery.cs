using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Specifications;
using Domain.Entities;
using MediatR;

namespace Application.Catalog.Images.Queries.GetProductImage;

public record GetProductImageQuery(Guid ProductId,Guid? OptionValueId = null) : IRequest<ProductImageResult>;

public class GetProductImageQueryHandler : IRequestHandler<GetProductImageQuery, ProductImageResult>
{
    private readonly IRepository<ProductImage> _productImageRepo;

    public GetProductImageQueryHandler(IRepository<ProductImage> productImageRepo)
    {
        _productImageRepo = productImageRepo;
    }

    public async Task<ProductImageResult> Handle(GetProductImageQuery request, CancellationToken cancellationToken)
    {
        var spec = new ProductImageListSpec(request.ProductId);
        var images = await _productImageRepo.ListAsync(spec, cancellationToken);

        var selectedVariantId = request.OptionValueId;

        var orderedList = images
            .Select(img => new 
            {
                Dto = new ImageLookupDto { Id = img.Id, Url = img.ImageUrl! },
                Priority = img.OptionValueId == selectedVariantId ? 100 : // variant
                           img.IsMain && img.OptionValueId == null ? 50 : // main
                           img.OptionValueId == null ? 20 : 0, // common
                PriorityOrder = img.OptionValueId == selectedVariantId ? 0 : 
                               img.IsMain && img.OptionValueId == null ? 1 :
                               img.OptionValueId == null ? 2 : 3
            })
            .OrderByDescending(x => x.Priority)
            .ThenBy(x => x.PriorityOrder)
            .ThenBy(x => x.Dto.Id)
            .Select(x => x.Dto)
            .ToList();

        var mainImage = orderedList.FirstOrDefault();
        var result = new ProductImageResult
        {
            MainImage = mainImage,
            CommonImages = orderedList,
            VariantImages = images
                .Where(x => x.OptionValueId.HasValue)
                .ToDictionary(
                    x => x.OptionValueId!.Value,
                    x => new ImageLookupDto { Id = x.Id, Url = x.ImageUrl! }
                )
        };

        return result;
    }
}
