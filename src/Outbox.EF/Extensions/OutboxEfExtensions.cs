using EventBus.InMemory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Outbox.Abstractions;
using Outbox.EF.Infrastructure;
using Outbox.EF.Infrastructure.Data;
using Outbox.EF.Infrastructure.Services;

namespace Outbox.EF.Extensions;

public static class OutboxEfExtensions
{
    public static void AddTransactionalOutbox(this IHostApplicationBuilder builder)
    {
        builder.AddNpgsqlDbContext<OutboxDbContext>("shopdb");

        builder.Services.AddScoped<IOutboxMessageProcessor, OutboxMessageProcessor>();
        builder.Services.AddScoped<IPollingOutboxMessageRepository, PollingOutboxMessageRepository>();
        builder.Services.AddSingleton<PollingOutboxMessageRepositoryOptions>();

        // In-memory event bus for demo/testing purposes
        // In production, consider using a more robust event bus like RabbitMQ, Azure Service Bus, etc.
        builder.AddInMemoryEventBus();

        // Hosted service to poll and process outbox messages
        builder.Services.AddHostedService<TransactionalOutboxPollingService>();
    }
}

