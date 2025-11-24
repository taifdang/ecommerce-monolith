using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using Domain.Events;
using MediatR;

namespace Application.Order.EventHandlers;

public class ComfirmOrderEventHandler : INotificationHandler<StockReservationSuccessedEvent>
{
    private readonly IRepository<Domain.Entities.Order> _orderRepo;

    public ComfirmOrderEventHandler(IRepository<Domain.Entities.Order> orderRepo)
    {
        _orderRepo = orderRepo;
    }

    public async Task Handle(StockReservationSuccessedEvent e, CancellationToken cancellationToken)
    {
        var order = await _orderRepo.GetByIdAsync(e.OrderId, cancellationToken);

        Guard.Against.NotFound(e.OrderId, order);

        order.Confirm();
        await _orderRepo.SaveChangesAsync(cancellationToken);
    }
}
