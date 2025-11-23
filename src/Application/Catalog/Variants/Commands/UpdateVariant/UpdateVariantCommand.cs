using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Specifications;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;

namespace Application.Catalog.Variants.Commands.UpdateVariant;

public record UpdateVariantCommand(Guid Id, decimal RegularPrice, int Quantity, decimal? Percent) : IRequest<Unit>;

public class UpdateVariantCommandHandler : IRequestHandler<UpdateVariantCommand, Unit>
{
    private readonly IRepository<ProductVariant> _productVariantRepository;

    public UpdateVariantCommandHandler(IRepository<ProductVariant> productVariantRepository)
    {
        _productVariantRepository = productVariantRepository;
    }

    public async Task<Unit> Handle(UpdateVariantCommand request, CancellationToken cancellationToken)
    {
        var variant = await _productVariantRepository.FirstOrDefaultAsync(new ProductVariantFilterSpec(null, request.Id));
        Guard.Against.NotFound(request.Id, variant);

        variant.Price = request.RegularPrice;

        if(request.Quantity > 0)
        {
            variant.Quantity = request.Quantity;
        }
        else
        {
            throw new Exception("Quantity is not negative");
        }
        
        await _productVariantRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}