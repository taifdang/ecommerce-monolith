using Application.Common.Models;

namespace Application.Catalog.Products.Queries.GetProductById;

public class ProductItemDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public decimal MinPrice { get; init; }
    public decimal MaxPrice { get; init; }
    public string Description { get; init; }
    public string Category { get; init; }
    public List<ImageLookupDto> Images { get; init; }
    public List<ProductOptionValueDto> OptionValues { get; init; }
    public List<ProductOptionDto> Options { get; init; }
}
