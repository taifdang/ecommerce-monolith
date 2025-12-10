using MediatR;

namespace Application.Identity.Commands.Logout;

public record LogoutCommand() : IRequest<Unit>;