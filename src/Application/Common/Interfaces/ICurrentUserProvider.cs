namespace Application.Common.Interfaces;

public interface ICurrentUserProvider
{
    string? GetCurrentUserId();
}
