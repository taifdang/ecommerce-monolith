using System.Diagnostics;

namespace Application.Common.Observability;

public static class ActivitySourceProvider
{
    public const string DefaultSourceName = "eshop";

    public static readonly ActivitySource Instance = new(DefaultSourceName, "v1");
}