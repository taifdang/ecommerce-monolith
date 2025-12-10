using Microsoft.AspNetCore.Http;

namespace Contracts.Requests
{
    public record CreateOptionValueRequestDto(Guid OptionId, string Value, IFormFile? MediaFile = null);
}
