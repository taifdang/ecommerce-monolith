namespace Domain.Entities;

public class BasketItem
{
    public Guid BasketId { get; set; }
    public Guid ProductVariantId { get; set; }
    public int Quantity { get; set; }
}
