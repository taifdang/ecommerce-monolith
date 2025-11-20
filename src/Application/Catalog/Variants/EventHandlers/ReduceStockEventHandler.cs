using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Specifications;
using Application.Order.Dtos;
using Domain.Entities;
using MediatR;

namespace Application.Catalog.Variants.EventHandlers;

public record ReduceStockCommand(IEnumerable<OrderItemDto> OrderItems) : IRequest<Unit>;

public class ReduceStockCommandHandler : IRequestHandler<ReduceStockCommand, Unit>
{
    private readonly IRepository<ProductVariant> _productVariantRepository;
    public ReduceStockCommandHandler(IRepository<ProductVariant> productVariantRepository)
    {
        _productVariantRepository = productVariantRepository;
    }

    public async Task<Unit> Handle(ReduceStockCommand request, CancellationToken cancellationToken)
    {
        if (request.OrderItems.Any())
        {
            foreach (var item in request.OrderItems)
            {
                var variantInStock = await _productVariantRepository
                    .FirstOrDefaultAsync(new ProductVariantInStockSpec(item.ProductVariantId), cancellationToken);

                if (variantInStock is null)
                {
                    throw new EntityNotFoundException();
                }
                if (item.Quantity > variantInStock.Quantity)
                {
                    throw new Exception("Not enough quantity");
                }
                var realStock = variantInStock.Quantity - item.Quantity;

                if (realStock < 0)
                {
                    throw new Exception("Stock negative");
                }

                variantInStock.Quantity = realStock;

                await _productVariantRepository.UpdateAsync(variantInStock, cancellationToken);
            }
        }
        return Unit.Value;
    }
}
