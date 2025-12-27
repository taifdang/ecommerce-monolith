using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Identity.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, TokenResult>
{
    private readonly IIdentityService _identityService;

    public LoginCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<TokenResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.Authenticate(new LoginRequest(request.UserName, request.Password));
    }
}
