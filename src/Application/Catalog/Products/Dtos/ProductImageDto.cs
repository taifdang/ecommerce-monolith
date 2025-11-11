namespace Application.Catalog.Products.Dtos;

public record ProductImageDto
{
    public int Id { get; set; }
    public string? Url { get; set; }
}
