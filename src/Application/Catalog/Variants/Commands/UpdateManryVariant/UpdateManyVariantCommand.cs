using Application.Common;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Catalog.Variants.Commands.UpdateManyVariant;

public record UpdateManyVariantCommand(Guid ProductId, decimal? Price, int? Quantity, string? Sku) : IRequest<Guid>;

public class UpdateManyVariantCommandHandler : IRequestHandler<UpdateManyVariantCommand, Guid>
{
    private readonly IRepository<ProductVariant> _productVariantRepository;

    public UpdateManyVariantCommandHandler(IRepository<ProductVariant> productVariantRepository)
    {
        _productVariantRepository = productVariantRepository;
    }

    public async Task<Guid> Handle(UpdateManyVariantCommand request, CancellationToken cancellationToken)
    {
        var variants = await _productVariantRepository.ListAsync(new VariantByProductIdSpec(request.ProductId))
            ?? throw new EntityNotFoundException(nameof(Product), "null");

        foreach (var v in variants)
        {
            if (request.Price.HasValue)
            {
                v.RegularPrice = request.Price.Value;
            }
            if (request.Quantity.HasValue)
            {
                v.Quantity = request.Quantity.Value;
            }
            if(!string.IsNullOrWhiteSpace(request.Sku)) 
            { 
                v.Sku = request.Sku;
            }       
        }

        await _productVariantRepository.UpdateRangeAsync(variants, cancellationToken);

        return request.ProductId;
    }
}


