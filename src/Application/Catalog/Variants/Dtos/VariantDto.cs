namespace Application.Catalog.Variants.Dtos;

public record VariantDto(
    Guid Id,
    Guid ProductId, 
    string ProductName, 
    string Title, 
    decimal RegularPrice, 
    decimal Percent,
    int Quantity,
    string Sku,
    VariantImageDto? Image,
    List<VariantOptionValueDto> Options);
//{
//    public Guid Id { get; set; }
//    public Guid ProductId { get; set; }
//    public string ProductName { get; set; }
//    public string Title { get; set; } = string.Empty;
//    public decimal RegularPrice { get; set; }
//    public decimal Percent { get; set; }
//    public int Quantity { get; set; }
//    public string Sku { get; set; } = string.Empty;
//    public VariantImageDto? Image { get; set; }
//    public List<VariantOptionValueDto> Options { get; set; }
//}
