using Application.Common.Models;

namespace Application.Catalog.Products.Queries.GetProductById;

public class ProductOptionValueDto
{
    public Guid Id { get; set; }
    public string Value { get; init; }
    public ImageLookupDto? Image { get; init; }
}
