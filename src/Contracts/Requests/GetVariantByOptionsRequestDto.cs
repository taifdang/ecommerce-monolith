namespace Contracts.Requests;

public record GetVariantByOptionsRequestDto(Guid ProductId, List<Guid> OptionValueMap);
