using Application.Common.Interfaces;

namespace Api.Services;

public class CurrentIPAddressProvider : ICurrentIPAddressProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentIPAddressProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetCurrentIPAddress()
    {
        var context = _httpContextAccessor?.HttpContext;
        if (context == null) return null;

        // Check for X-Forwarded-For header first (in case of proxy)
        if (context.Request.Headers.TryGetValue("X-Forwarded-For", out var forwarded))
        {
            // The X-Forwarded-For header can contain multiple IPs, the first one is the original client IP
            return forwarded.ToString().Split(',')[0].Trim();
        }

        // Fallback to RemoteIpAddress
        return context.Connection.RemoteIpAddress?.ToString();
    }
}
