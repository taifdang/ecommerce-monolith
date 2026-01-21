# EventBus.RabbitMQ

RabbitMQ-based implementation of the EventBus abstraction using .NET Aspire.

## Features

- Full integration with .NET Aspire for RabbitMQ hosting
- Durable message persistence
- Automatic retry handling
- Exchange-based fanout pattern for pub/sub
- Configurable queue and exchange names
- Background service for message consumption
- Easy switch between InMemory and RabbitMQ implementations

## Installation

The RabbitMQ event bus is already integrated into the project. You can switch between InMemory and RabbitMQ implementations using configuration.

### Prerequisites

1. **AppHost Configuration** - RabbitMQ is already configured in `src/AppHost/Program.cs`:
```csharp
var rabbitmq = builder.AddRabbitMQ("rabbitmq")
    .WithManagementPlugin();

var apiService = builder.AddProject<Projects.Api>("apiservice")
    .WithReference(rabbitmq);
```

2. **Infrastructure Configuration** - The event bus is configured in `src/Infrastructure/DependencyInjection.cs` to support both implementations.

## Configuration

### Switch Between InMemory and RabbitMQ

Add the following to your `appsettings.json` or `appsettings.Development.json`:

```json
{
  "EventBus": {
    "UseRabbitMQ": true,
    "ExchangeName": "event_bus",
    "QueueName": "api_events_queue",
    "PrefetchCount": 10,
    "MaxRetryCount": 3
  }
}
```

- Set `UseRabbitMQ` to `true` to use RabbitMQ
- Set `UseRabbitMQ` to `false` (or omit) to use InMemory event bus

### Configuration Options

| Option | Default | Description |
|--------|---------|-------------|
| UseRabbitMQ | false | Switch between RabbitMQ and InMemory implementations |
| ExchangeName | "event_bus" | RabbitMQ exchange name |
| QueueName | "api_events_queue" | RabbitMQ queue name |
| MaxRetryCount | 3 | Maximum retry attempts for failed messages |
| PrefetchCount | 10 | Number of messages to prefetch from RabbitMQ |

## Usage

### Publishing Events

Events are published through the `IEventPublisher` interface, regardless of the implementation:

```csharp
public class OrderService
{
    private readonly IEventPublisher _eventPublisher;

    public OrderService(IEventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }

    public async Task CreateOrder(Order order)
    {
        // ... create order logic ...

        // Publish event - works with both InMemory and RabbitMQ
        await _eventPublisher.PublishAsync(new OrderCreatedIntegrationEvent
        {
            OrderId = order.Id,
            CustomerId = order.CustomerId
        });
    }
}
```

### Handling Events

Event handlers are registered in `src/Infrastructure/DependencyInjection.cs`:

```csharp
eventBus
    .AddSubscription<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>()
    .AddSubscription<PaymentSucceededIntegrationEvent, PaymentSucceededIntegrationEventHandler>()
    // ... more subscriptions
```

Implement event handlers using `IIntegrationEventHandler<TEvent>`:

```csharp
public class OrderCreatedIntegrationEventHandler 
    : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
{
    private readonly ILogger<OrderCreatedIntegrationEventHandler> _logger;

    public OrderCreatedIntegrationEventHandler(
        ILogger<OrderCreatedIntegrationEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(OrderCreatedIntegrationEvent @event)
    {
        _logger.LogInformation("Order created: {OrderId}", @event.OrderId);
        
        // Handle the event
        await Task.CompletedTask;
    }
}
```

## Architecture

The implementation consists of:

- **RabbitMqSender**: Publishes events to the RabbitMQ exchange
- **RabbitMqReceiver**: Background service that consumes messages from the queue
- **RabbitMqOptions**: Configuration options
- **RabbitMqExtensions**: Service registration extensions

### Message Flow

1. **Publisher** ? Serializes event ? Wraps in MessageEnvelope ? Publishes to Exchange
2. **Exchange** ? Routes to Queue (fanout pattern)
3. **Receiver** ? Consumes from Queue ? Deserializes ? Invokes registered handlers

## RabbitMQ Management UI

When running with Aspire, the RabbitMQ Management UI is automatically available:
- URL: Check the Aspire Dashboard for the rabbitmq endpoint
- Default credentials: `guest` / `guest`

## Registered Event Subscriptions

The following events are currently registered:

- `OrderCreatedIntegrationEvent`
- `StockReservationRequestedIntegrationEvent`
- `GracePeriodConfirmedIntegrationEvent`
- `PaymentSucceededIntegrationEvent`
- `PaymentRejectedIntegrationEvent`
- `ReserveStockRejectedIntegrationEvent`
- `ReserveStockSucceededIntegrationEvent`
- `UserCreatedIntegrationEvent`

## Development vs Production

### Development (default)
```json
{
  "EventBus": {
    "UseRabbitMQ": false
  }
}
```
Uses in-memory event bus for fast local development without external dependencies.

### Production
```json
{
  "EventBus": {
    "UseRabbitMQ": true,
    "ExchangeName": "eshop_events",
    "QueueName": "api_events",
    "PrefetchCount": 20
  }
}
```
Uses RabbitMQ for reliable, distributed message processing.

## Troubleshooting

### Events not being received
1. Check that RabbitMQ is running (verify in Aspire Dashboard)
2. Verify `UseRabbitMQ` is set to `true` in configuration
3. Check event type registration in DependencyInjection.cs
4. Review logs for connection errors

### Message serialization errors
- Ensure event classes inherit from `IntegrationEvent`
- Check JSON serialization options in EventBusSubscriptionManager
- Verify property names match between publisher and subscriber

## Performance Considerations

- **PrefetchCount**: Higher values improve throughput but increase memory usage
- **MaxRetryCount**: Set based on acceptable failure tolerance
- **Queue Durability**: Messages persist through RabbitMQ restarts
- **Exchange Type**: Fanout pattern broadcasts to all bound queues (pub/sub)
