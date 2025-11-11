using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class CookieService(IHttpContextAccessor httpContextAccessor) : ICookieService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string Get()
    {
        var token = _httpContextAccessor.HttpContext?.Request.Cookies["token_key"];
        return string.IsNullOrEmpty(token)
            ? throw new Exception("not exist token")
            : token;
    }

    public void Set(string token)
        => _httpContextAccessor.HttpContext?.Response.Cookies.Append(
            "token_key",
            token,
            new CookieOptions
            {
                SameSite = SameSiteMode.None,
                Secure = true,
                HttpOnly = true,
                MaxAge = TimeSpan.FromMinutes(30)
            });

    public void Delete() => _httpContextAccessor.HttpContext?.Response.Cookies.Delete("token_key");

    #region Guest_SessionId
    public string GetGuestId()
    {
        var sessionId = _httpContextAccessor.HttpContext?.Request.Cookies["guest_id"];
        return string.IsNullOrEmpty(sessionId)
            ? throw new Exception("not exist guest session id")
            : sessionId;
    }

    public void SetGuestId()
        => _httpContextAccessor.HttpContext?.Response.Cookies.Append(
            "guest_id",
            Guid.NewGuid().ToString("N"),
            new CookieOptions
            {
                SameSite = SameSiteMode.None,
                Secure = true,
                HttpOnly = true,
                MaxAge = TimeSpan.FromMinutes(30)
            });

    #endregion
}
