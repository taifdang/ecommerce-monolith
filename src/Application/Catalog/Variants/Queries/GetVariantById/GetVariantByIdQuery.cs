using Application.Catalog.Variants.Dtos;
using Application.Common.Interfaces;
using Application.Common.Specifications;
using Domain.Entities;
using MediatR;

namespace Application.Catalog.Variants.Queries.GetVariantById;

public record GetVariantByIdQuery : IRequest<VariantDto>
{
    public int Id { get; init; }
}

public class GetVariantByIdQueryHandler : IRequestHandler<GetVariantByIdQuery, VariantDto>
{
    private readonly IRepository<ProductVariant> _productVariantRepository;
    public GetVariantByIdQueryHandler(IRepository<ProductVariant> productVariantRepository)
    {
        _productVariantRepository = productVariantRepository;
    }
    public async Task<VariantDto> Handle(GetVariantByIdQuery request, CancellationToken cancellationToken)
    {
        #region
        //var variant = await _unitOfWork.ProductVariantRepository.GetByIdAsync(
        //        filter: x => x.Id == request.Id,
        //        selector: x => new ProductVariantVm
        //        {
        //            Id = x.Id,
        //            Title = x.Title ?? string.Empty,
        //            ProductName = x.Product.Title,
        //            RegularPrice = x.RegularPrice,
        //            //MaxPrice = x.MaxPrice,
        //            Percent = x.Percent,
        //            Quantity = x.Quantity,
        //            Sku = x.Sku ?? string.Empty,
        //            Image = x.VariantOptionValues
        //                .SelectMany(vov => vov.OptionValue.ProductImages!
        //                    .Where(img => img.OptionValueId == vov.OptionValueId))
        //                .Select(img => new ProductImageDto
        //                {
        //                    Id = img.Id,
        //                    Url = img.Image
        //                })
        //                .OrderBy(img => img.Id)
        //                .FirstOrDefault() ?? new(),
        //            Options = x.VariantOptionValues.Select(y => new VariantOptionValueDto
        //            {
        //                Title = y.OptionValue.ProductOption.OptionName,
        //                Value = y.OptionValue.Value
        //            }).ToList()
        //        });

        //return variant;
        #endregion
        var specification = new VariantWithOptionSpec(request.Id);
        var variantVm = await _productVariantRepository.FirstOrDefaultAsync(specification, cancellationToken);
        return variantVm;
    }
}
