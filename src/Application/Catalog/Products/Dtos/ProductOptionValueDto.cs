namespace Application.Catalog.Products.Dtos;

public class ProductOptionValueDto
{
    public string Title { get; set; }
    public List<ProductOptionVariantDto> Variants { get; set; } = new();
    public List<string> Values { get; set; } = new();
}
