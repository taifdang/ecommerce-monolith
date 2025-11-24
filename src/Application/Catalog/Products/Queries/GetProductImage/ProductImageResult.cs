using Application.Common.Models;

namespace Application.Catalog.Products.Queries.GetProductImage;

public class ProductImageResult
{
    public ImageLookupDto? MainImage { get; set; }
    public List<ImageLookupDto> CommonImages { get; set; } = new();
    public Dictionary<Guid, ImageLookupDto> VariantImages { get;set; } = new();
}
