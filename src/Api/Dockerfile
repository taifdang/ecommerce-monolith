FROM mcr.microsoft.com/dotnet/sdk:9.0 AS builder
WORKDIR /src

# copy csproj files (use repo root context: docker-compose sets context to project root)
COPY ["src/Api/Api.csproj", "Api/"]
COPY ["src/Application/Application.csproj", "Application/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["src/ServiceDefaults/ServiceDefaults.csproj", "ServiceDefaults/"]
COPY ["src/Shared/Shared.csproj", "Shared/"]
COPY ["src/Domain/Domain.csproj", "Domain/"]
COPY ["src/Contracts/Contracts.csproj", "Contracts/"]
COPY ["src/EventBus/EventBus.csproj", "EventBus/"]
COPY ["src/EventBus.InMemory/EventBus.InMemory.csproj", "EventBus.InMemory/"]
COPY ["src/Outbox/Outbox.csproj", "Outbox/"]
COPY ["src/Outbox.EF/Outbox.EF.csproj", "Outbox.EF/"]

RUN dotnet restore "Api/Api.csproj"

# copy everything else and build
COPY src/ .
RUN dotnet build "Api/Api.csproj" -c Release --no-restore

# publish
RUN dotnet publish "Api/Api.csproj" -c Release --no-restore -o /app/publish

# final runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=builder /app/publish .

ENV ASPNETCORE_URLS http://+:80
ENV ASPNETCORE_ENVIRONMENT Docker

EXPOSE 80

ENTRYPOINT ["dotnet", "Api.dll"]
