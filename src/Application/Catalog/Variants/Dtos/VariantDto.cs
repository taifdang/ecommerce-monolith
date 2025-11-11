namespace Application.Catalog.Variants.Dtos;

public record VariantDto
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal RegularPrice { get; set; }
    public decimal Percent { get; set; }
    public int Quantity { get; set; }
    public string Sku { get; set; } = string.Empty;
    public VariantImageDto? Image { get; set; }
    public List<VariantOptionValueDto> Options { get; set; }
}
