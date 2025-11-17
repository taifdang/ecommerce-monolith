using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Catalog.Variants.Commands.CreateVariant;

//Non option
public record CreateVariantCommand : IRequest<int>
{
    public int ProductId { get; set; }
    public decimal RegularPrice { get; set; }
    public int Quantity { get; set; }
}

public class CreateVariantCommandHandler : IRequestHandler<CreateVariantCommand, int>
{
    private readonly IRepository<ProductVariant> _productVariantRepository;

    public CreateVariantCommandHandler(IRepository<ProductVariant> productVariantRepository)
    {
        _productVariantRepository = productVariantRepository;
    }

    public async Task<int> Handle(CreateVariantCommand request, CancellationToken cancellationToken)
    {
        var variant = new ProductVariant
        {
            ProductId = request.ProductId,
            RegularPrice = request.RegularPrice,
            Quantity = request.Quantity,
            Status = Domain.Enums.IntentoryStatus.InStock,
            Percent = 0
        };

        await _productVariantRepository.AddAsync(variant, cancellationToken);

        return variant.ProductId;
    }
}
