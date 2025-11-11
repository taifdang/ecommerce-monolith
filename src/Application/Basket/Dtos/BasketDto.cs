namespace Application.Basket.Dtos;

public record BasketDto
{
    public int CustomerId { get; set; }
    public List<BasketItemDto> Items { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModified { get; set; }
}
