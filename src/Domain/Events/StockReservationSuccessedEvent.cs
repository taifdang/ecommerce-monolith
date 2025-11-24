namespace Domain.Events;

public record StockReservationSuccessedEvent(Guid OrderId, Guid CustomerId) : IDomainEvent;

