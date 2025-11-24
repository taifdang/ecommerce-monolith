namespace Domain.Events;

public record OrderConfirmedEvent(Guid OrderId, Guid CustomerId, decimal TotalAmount) : IDomainEvent;
