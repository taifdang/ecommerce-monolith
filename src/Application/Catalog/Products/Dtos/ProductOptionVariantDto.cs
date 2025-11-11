namespace Application.Catalog.Products.Dtos;

public class ProductOptionVariantDto
{
    public string Title { get; set; }
    public string? Label { get; set; }
    public List<ProductImageDto> Images { get; set; } = new();
}
