using Application.Customer.Commands;
using Ardalis.GuardClauses;
using Contracts.IntegrationEvents;
using EventBus.Abstractions;
using MediatR;

namespace Application.Customer.EventHandlers;

public class UserCreatedIntegrationEventHandler(IMediator mediator) : IIntegrationEventHandler<UserCreatedIntegrationEvent>
{
    public async Task Handle(UserCreatedIntegrationEvent integrationEvent)
    {
        Guard.Against.Null(integrationEvent);

        await mediator.Send(new CreateCustomerCommand(integrationEvent.UserId, integrationEvent.Email));
    }
}
