using Application.Catalog.Variants.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Specifications;
using Domain.Entities;
using MediatR;

namespace Application.Catalog.Variants.Queries.GetVariantByOption;

public record GetVariantByOptionQuery(Guid ProductId, List<Guid> OptionValueMap) : IRequest<VariantListDto>;

public class GetVariantByOptionQueryHandler : IRequestHandler<GetVariantByOptionQuery, VariantListDto>
{
    private readonly IRepository<ProductVariant> _productVariantRepository;
    private readonly IRepository<ProductOption> _productOptionRepository;

    public GetVariantByOptionQueryHandler(IRepository<ProductVariant> productVariantRepository, IRepository<ProductOption> productOptionRepository)
    {
        _productVariantRepository = productVariantRepository;
        _productOptionRepository = productOptionRepository;
    }
    public async Task<VariantListDto> Handle(GetVariantByOptionQuery request, CancellationToken cancellationToken)
    {
       
        var totalOptions = await _productOptionRepository.CountAsync(
            new ProductOptionFilterSpec(request.ProductId, null),
            cancellationToken);

        var isExactMatch = request.OptionValueMap.Count == totalOptions;

        var variants = await _productVariantRepository.ListAsync(
            new ProductVariantOptionFilterSpec(request.ProductId, request.OptionValueMap, isExactMatch), 
            cancellationToken);

        if (!variants.Any())
            throw new EntityNotFoundException(nameof(VariantDto), "No variant found matching the specified options.");

        var dto = variants.Select(v => v.ToVariantDto()).ToList();
        
        var exactVariant = isExactMatch && variants.Count == 1 
            ? dto.First() 
            : null;

        decimal minPrice = exactVariant?.RegularPrice ?? dto.Min(v => v.RegularPrice);
        decimal maxPrice = exactVariant?.RegularPrice ?? dto.Max(v => v.RegularPrice);


        //if(exactVariant != null)
        //    minPrice = maxPrice = exactVariant.RegularPrice;

        var image = exactVariant?.Image;

        // Combinations
        var vm = new VariantListDto(dto, image, minPrice, maxPrice, variants.Sum(v => v.Quantity));
        //{           
        //    Variants = variants,
        //    TotalStock = variants.Sum(v => v.Quantity),
        //    Image = image,
        //    MinPrice = minPrice,
        //    MaxPrice = maxPrice,
        //    //MinPrice = exactVariant?.RegularPrice,
        //};

        return vm;
    }
}
