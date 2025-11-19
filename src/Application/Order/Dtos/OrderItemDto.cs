namespace Application.Order.Dtos;

public record OrderItemDto(Guid OrderId, Guid ProductVariantId, int Quantity);
