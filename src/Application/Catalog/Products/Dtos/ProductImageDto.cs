namespace Application.Catalog.Products.Dtos;

public record ProductImageDto
{
    public Guid Id { get; set; }
    public string? Url { get; set; }
}
