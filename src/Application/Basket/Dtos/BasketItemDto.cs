namespace Application.Basket.Dtos;

public record BasketItemDto
{
    public int ProductVariantId { get; set; }
    public string ProductName { get; set; }
    public decimal RegularPrice { get; set; }
    public string ImageUrl { get; set; }
    public int Quantity { get; set; }
}
