using Application.Common.Interfaces;
using Contracts.Requests;
using Contracts.Responses;
using MediatR;

namespace Application.Identity.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResult>
{
    private readonly IIdentityService _identityService;

    public RegisterUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<RegisterUserResult> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        return await _identityService.Register(new RegisterUserRequestDto(command.UserName, command.Email, command.Password));
    }
}
