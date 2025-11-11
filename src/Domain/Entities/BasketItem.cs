namespace Domain.Entities;

public class BasketItem
{
    public int BasketId { get; set; }
    public int ProductVariantId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
