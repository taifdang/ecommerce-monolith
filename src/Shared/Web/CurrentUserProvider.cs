using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Shared.Web;

public interface ICurrentUserProdvider
{
    Guid? GetCurrentUserId();
}

public class CurrentUserProvider : ICurrentUserProdvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? GetCurrentUserId()
    {
        var nameIdentifier = _httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        if (Guid.TryParse(nameIdentifier, out var userId))
        {
            return userId;
        }
        return null;
    }
}

