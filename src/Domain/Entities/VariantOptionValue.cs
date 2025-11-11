namespace Domain.Entities;

public class VariantOptionValue
{
    public int ProductVariantId { get; set; }
    public int OptionValueId { get; set; }
    public ProductVariant ProductVariant { get; set; }
    public OptionValue OptionValue { get; set; }
}
