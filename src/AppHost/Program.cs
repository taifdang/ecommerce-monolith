//ref: https://devblogs.microsoft.com/dotnet/new-aspire-app-with-react/
//ref: https://aspire.dev/fundamentals/networking-overview/#ports-and-proxies
var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Api>("apiservice")
    .WithExternalHttpEndpoints();
#if (setPort)
    .WithHttpEndpoint(port: 7129, env: "API_PORT", name: "api");
#endif

var reactVite = builder.AddViteApp("reactvite", "../Web/ClientApp")
    .WithReference(apiService)
    .WithEnvironment("BROWSER", "none");
#if (setPort)
    .WithEnvironment("VITE_API_URL", apiService.GetEndpoint("api"));
#endif

// Bundle the output of the Vite app into the wwwroot of the apiservice API
apiService.PublishWithContainerFiles(reactVite, "./wwwroot");

builder.Build().Run();
