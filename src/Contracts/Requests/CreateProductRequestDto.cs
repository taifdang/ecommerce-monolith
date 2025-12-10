namespace Contracts.Requests;

public record CreateProductRequestDto(Guid CategoryId, string Name, string UrlSlug, string Description);