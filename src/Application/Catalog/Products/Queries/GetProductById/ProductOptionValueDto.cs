namespace Application.Catalog.Products.Queries.GetProductById;

public class ProductOptionValueDto
{
    public string Title { get; init; }
    public List<ProductOptionVariantDto> Variants { get; init; }
    public List<string> Values { get; init; }
}
