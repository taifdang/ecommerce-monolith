using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Specifications;
using Ardalis.GuardClauses;
using Domain.Entities;
using MediatR;

namespace Application.Catalog.Variants.Commands.DeleteVariant;

public record DeleteVariantCommand(Guid Id, Guid ProductId) : IRequest<Unit>;

public class DeleteVariantCommandHandler : IRequestHandler<DeleteVariantCommand, Unit>
{
    private readonly IRepository<ProductVariant> _productVariantRepository;
    public DeleteVariantCommandHandler(IRepository<ProductVariant> productVariantRepository)
    {
        _productVariantRepository = productVariantRepository;
    }

    public async Task<Unit> Handle(DeleteVariantCommand request, CancellationToken cancellationToken)
    {
        var variant = await _productVariantRepository.FirstOrDefaultAsync(new ProductVariantFilterSpec(request.ProductId, request.Id));
        Guard.Against.NotFound(request.Id, variant);

        await _productVariantRepository.DeleteAsync(variant, cancellationToken);

        return Unit.Value;
    }
}
