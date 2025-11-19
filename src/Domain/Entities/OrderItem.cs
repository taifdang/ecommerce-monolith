namespace Domain.Entities;

public class OrderItem
{
    public Guid OrderId { get; set; }
    public Guid ProductVariantId { get; set; }
    public string VariantName { get; set; }
    public string ProductName { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice => UnitPrice * Quantity;
    public string ImageUrl { get; set; }
}
