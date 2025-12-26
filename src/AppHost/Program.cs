//ref: https://devblogs.microsoft.com/dotnet/new-aspire-app-with-react/
//ref: https://aspire.dev/fundamentals/networking-overview/#ports-and-proxies
//ref: https://learn.microsoft.com/en-us/dotnet/aspire/architecture/overview
var builder = DistributedApplication.CreateBuilder(args);

var postgresql = builder.AddPostgres("postgresql")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithDataVolume()
    .WithPgWeb();

var database = postgresql.AddDatabase("shopdb");

var apiService = builder.AddProject<Projects.Api>("apiservice")
    .WithExternalHttpEndpoints()
    .WithReference(database)
    .WaitFor(database);
#if (setPort)
    .WithHttpEndpoint(port: 7129, env: "API_PORT", name: "api");
#endif

var reactVite = builder.AddViteApp("webfrontend", "../Web/ClientApp")
    .WithReference(apiService)
    .WaitFor(apiService)
    .WithEnvironment("BROWSER", "none");
#if (setPort)
    .WithEnvironment("VITE_API_URL", apiService.GetEndpoint("api"));
#endif

// Bundle the output of the Vite app into the wwwroot of the apiservice API
apiService.PublishWithContainerFiles(reactVite, "./wwwroot");

builder.Build().Run();
