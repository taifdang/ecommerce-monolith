using Application.Common.Models;

namespace Application.Catalog.Products.Queries.GetProductById;

public class ProductOptionVariantDto
{
    public string Title { get; init; }
    public string? Label { get; init; }
    public List<ImageLookupDto> Images { get; init; }
}
