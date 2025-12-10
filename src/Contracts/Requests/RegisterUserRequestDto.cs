namespace Contracts.Requests;

public record RegisterUserRequestDto(string UserName, string Email, string Password);
