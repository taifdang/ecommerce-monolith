namespace Application.Basket.Queries.GetBasket;

public record BasketDto(
    Guid Id, 
    Guid CustomerId,
    List<BasketItemDto> Items,
    DateTime CreatedAt, 
    DateTime? LastModified);
