using Application.Catalog.Variants.Dtos;
using Application.Common.Interfaces;
using Application.Common.Specifications;
using Domain.Entities;
using MediatR;

namespace Application.Catalog.Variants.Queries.GetVariantById;

public record GetVariantByIdQuery(Guid Id) : IRequest<VariantDto>;

public class GetVariantByIdQueryHandler : IRequestHandler<GetVariantByIdQuery, VariantDto>
{
    private readonly IRepository<ProductVariant> _productVariantRepository;
    public GetVariantByIdQueryHandler(IRepository<ProductVariant> productVariantRepository)
    {
        _productVariantRepository = productVariantRepository;
    }
    public async Task<VariantDto> Handle(GetVariantByIdQuery request, CancellationToken cancellationToken)
    {
        var variant = await _productVariantRepository.FirstOrDefaultAsync(new ProductVariantByIdSpec(request.Id), cancellationToken);
       
        if (variant is null)
            return null!;

        var dto = variant.ToVariantDto();

        // fallback mainImage if image with optionValue null
        if (dto!.Image?.Url == null)
        {
            var mainImage = await _productVariantRepository.FirstOrDefaultAsync(new ProductVariantWithImageByProductIdSpec(variant.ProductId), cancellationToken);
            var image = mainImage?.GetMainImage();           
            dto = dto with { Image = image };
        }

        return dto;
    }
}
