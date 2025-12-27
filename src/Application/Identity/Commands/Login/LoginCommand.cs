using Application.Common.Models;
using MediatR;

namespace Application.Identity.Commands.Login;

public record LoginCommand(string UserName, string Password) : IRequest<TokenResult>;
