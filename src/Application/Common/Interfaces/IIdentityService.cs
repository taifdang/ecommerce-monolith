using Contracts.Requests;
using Contracts.Responses;

namespace Application.Common.Interfaces;

public interface IIdentityService
{
    Task<RegisterUserResult> Register(RegisterUserRequestDto request);
    Task<TokenResult> Authenticate(LoginRequestDto request);
    Task Logout();
    Task<TokenResult> RefreshToken();
    Task<UserInfoResponse> GetProfile();
}
