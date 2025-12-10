using MediatR;

namespace Contracts.Responses;

public record RegisterUserResult(Guid Id, string UserName, string Email) : IRequest<RegisterUserResult>;
