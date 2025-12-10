namespace Contracts.Requests;

public record CreateOptionRequestDto(Guid ProductId, string OptionName, bool AllowImage = false);
