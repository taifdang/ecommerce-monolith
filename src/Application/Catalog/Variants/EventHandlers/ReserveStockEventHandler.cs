using Application.Common.Interfaces;
using Application.Common.Specifications;
using Ardalis.GuardClauses;
using Domain.Entities;
using Domain.Events;
using MediatR;
using static Domain.Events.StockReservationFailedEvent;

namespace Application.Catalog.Variants.EventHandlers;

public class ReserveStockEventHandler : INotificationHandler<OrderCreatedEvent>
{
    private readonly IRepository<ProductVariant> _productVariantRepository;
    private readonly IMediator _mediator;

    public ReserveStockEventHandler(
        IRepository<ProductVariant> productVariantRepository,
        IMediator mediator)
    {
        _productVariantRepository = productVariantRepository;
        _mediator = mediator;
    }

    public async Task Handle(OrderCreatedEvent e, CancellationToken cancellationToken)
    {
        Guard.Against.Null(e);

        var failItems = new List<StockReservationFailedEvent.FailedItem>();

        foreach (var item in e.Items)
        {
            var variantInStock = await _productVariantRepository
                .FirstOrDefaultAsync(new ProductVariantInStockByIdSpec(item.ProductVariantId), cancellationToken);

            if (variantInStock is null || variantInStock.Quantity < item.Quantity)
            {
                failItems.Add(new FailedItem(
                    item.ProductVariantId,
                    item.Quantity, 
                    variantInStock?.Quantity ?? 0));
            }

            //if (item.Quantity > variantInStock.Quantity)
            //{
            //    throw new Exception("Not enough quantity");
            //}
            else
            {
                var currentStock = variantInStock.Quantity - item.Quantity;

                if (currentStock < 0)
                {
                    throw new Exception("Stock negative");
                }

                variantInStock.Quantity = currentStock;
                await _productVariantRepository.UpdateAsync(variantInStock, cancellationToken);
            }
        }

        if (failItems.Any())
        {
            await _mediator.Publish(new StockReservationFailedEvent(
                e.OrderId, e.CustomerId, failItems),
                cancellationToken);

            return;
        }

        await _mediator.Publish(new StockReservationSuccessedEvent(
            e.OrderId, e.CustomerId),
            cancellationToken);
    }
}