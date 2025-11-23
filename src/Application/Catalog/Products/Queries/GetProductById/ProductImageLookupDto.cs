namespace Application.Catalog.Products.Queries.GetProductById;

public class ProductImageLookupDto
{
    public Guid Id { get; set; }
    public string? Url { get; set; }
    public Guid? OptionValueId { get; set; }
    public bool IsMain { get; set; }
}
