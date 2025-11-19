namespace Domain.Entities;

public class ProductImage
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid? OptionValueId { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsMain { get; set; } = false;
    public Product Product { get; set; }
    public OptionValue? OptionValue { get; set; }
}
