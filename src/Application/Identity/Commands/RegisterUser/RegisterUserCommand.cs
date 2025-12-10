using Contracts.Responses;
using MediatR;

namespace Application.Identity.Commands.RegisterUser;

public record RegisterUserCommand(string UserName, string Email, string Password) : IRequest<RegisterUserResult>;