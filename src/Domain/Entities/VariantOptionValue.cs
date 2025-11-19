namespace Domain.Entities;

public class VariantOptionValue
{
    public Guid ProductVariantId { get; set; }
    public Guid OptionValueId { get; set; }
    public ProductVariant ProductVariant { get; set; }
    public OptionValue OptionValue { get; set; }
}
