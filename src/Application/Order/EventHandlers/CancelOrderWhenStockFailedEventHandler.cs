using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using Domain.Events;
using MediatR;

namespace Application.Order.EventHandlers;

public class CancelOrderWhenStockFailedEventHandler : INotificationHandler<StockReservationFailedEvent>
{
    private readonly IRepository<Domain.Entities.Order> _orderRepo;

    public CancelOrderWhenStockFailedEventHandler(IRepository<Domain.Entities.Order> orderRepo)
    {
        _orderRepo = orderRepo;
    }

    public async Task Handle(StockReservationFailedEvent e, CancellationToken cancellationToken)
    {
        var order = await _orderRepo.GetByIdAsync(e.OrderId, cancellationToken);

        Guard.Against.NotFound(e.OrderId, order);

        order.Cancel();
        await _orderRepo.SaveChangesAsync(cancellationToken);
    }
}
