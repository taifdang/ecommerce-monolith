namespace Contracts.Requests;

public record UpdateVariantRequestDto(Guid Id, decimal RegularPrice, int Quantity);
