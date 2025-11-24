namespace Application.Catalog.Products.Queries.GetProductById;

public class ProductOptionDto
{
    public string Title { get; set; }
    public List<ProductOptionValueDto> OptionValues { get; set; }
}
