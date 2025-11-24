using static Domain.Events.StockReservationFailedEvent;

namespace Domain.Events;

public record StockReservationFailedEvent(
    Guid OrderId,
    Guid CustomerId,
    IReadOnlyList<FailedItem> FailedItems) : IDomainEvent
{
    public record FailedItem(Guid ProductVariantId, int Requested, int Available);
};
