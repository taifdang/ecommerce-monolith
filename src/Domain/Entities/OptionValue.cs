namespace Domain.Entities;

public class OptionValue
{
    public int Id { get; set; }
    public int ProductOptionId { get; set; }
    public string Value { get; set; }
    public string? Label { get; set; }
    public ProductOption ProductOption { get; set; }
    public ICollection<VariantOptionValue> VariantOptionValues { get; set; } = new List<VariantOptionValue>();
    public ICollection<ProductImage>? ProductImages { get; set; } = new List<ProductImage>();

}
