namespace Domain.Entities;

public class ProductImage
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int? OptionValueId { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsMain { get; set; } = false;
    public Product Product { get; set; }
    public OptionValue? OptionValue { get; set; }
}
