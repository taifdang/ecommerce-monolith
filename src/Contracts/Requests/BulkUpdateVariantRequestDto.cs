namespace Contracts.Requests;

public record BulkUpdateVariantRequestDto(Guid ProductId, decimal? Price, int? Quantity, string? Sku, bool IsActive);
