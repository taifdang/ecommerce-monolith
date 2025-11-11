using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Auth;

namespace Api.Controllers.Identity;

[Route(BaseApiPath + "/auth")]
public class AuthController(IIdentityService identityService) : BaseController
{
    private readonly IIdentityService _identityService = identityService;

    [HttpPost("login")]
    public async Task<IActionResult> Login(SignInRequest request, CancellationToken cancellationToken)
    {
        var tokenResult = await _identityService.SignIn(request, cancellationToken);
        return Ok(tokenResult); 
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(SignUpRequest request, CancellationToken cancellationToken)
    {
        await _identityService.SignUp(request, cancellationToken);
        return NoContent();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _identityService.SignOut();
        return NoContent();
    }
}
