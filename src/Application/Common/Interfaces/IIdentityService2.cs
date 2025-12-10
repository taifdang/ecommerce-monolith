using Contracts.Requests;
using Contracts.Responses;

namespace Application.Common.Interfaces;

public interface IIdentityService2
{
    Task<TokenResult> Authenticate(AuthorizeRequest request, CancellationToken cancellationToken);
    Task<Guid> Register(RegisterRequest request, CancellationToken cancellationToken);
    Task Logout();
    Task<UserInfoResponse> GetProfile(CancellationToken cancellationToken);
    Task ConfirmEmail(CancellationToken cancellationToken);
    Task TwoFactor(CancellationToken cancellationToken);
    Task VerifyTwoFactor(CancellationToken cancellationToken);
    Task<TokenResult> RefreshToken(string token, CancellationToken cancellationToken);
    //Task SendResetPassword(SendResetPasswordRequest request, CancellationToken cancellationToken); // send request
    //Task ResetPassword(ResetPasswordRequest request, CancellationToken cancellationToken); // verify email address

    Task<Guid> RegisterNewUser(RegisterUserRequestDto request, CancellationToken cancellationToken);
}
