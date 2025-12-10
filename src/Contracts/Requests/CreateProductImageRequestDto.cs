using Microsoft.AspNetCore.Http;


namespace Contracts.Requests;

public record CreateProductImageRequestDto(Guid ProductId, bool IsMain = false, IFormFile? MediaFile = null);
