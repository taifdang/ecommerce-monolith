using Application.Common.Interfaces;
using Application.Common.Models;
using Contracts.IntegrationEvents;
using MediatR;
using Outbox.Abstractions;
using System.Text.Json;

namespace Application.Payment.Commands.VerifyPaymentIpn;

public class VerifyPaymentIpnCommandHandler : IRequestHandler<VerifyPaymentIpnCommand, IpnResult>
{
    private readonly IPaymentGatewayFactory _factory;
    private readonly IApplicationDbContext _context;
    private readonly IPollingOutboxMessageRepository _outboxRepository;

    public VerifyPaymentIpnCommandHandler(
        IPaymentGatewayFactory factory,
        IApplicationDbContext context,
        IPollingOutboxMessageRepository outboxRepository)
    {
        _factory = factory;
        _context = context;
        _outboxRepository = outboxRepository;
    }
    public Task<IpnResult> Handle(VerifyPaymentIpnCommand request, CancellationToken cancellationToken)
    {
        var gateway = _factory.Resolve(request.Provider);
        var result = gateway.VerifyIpnCallback(request.Parameters);

        if(result.IsNullEvent)
        {
            return Task.FromResult(new IpnResult
            {
                RspCode = "99",
                Message = "System error",
            });
        }

        if (!result.CheckSignature)
        {
            return Task.FromResult(new IpnResult
            {
                RspCode = "97",
                Message = "Invalid signature",
            });
        }

        // get order from db
        var orderEntity = _context.Orders.FirstOrDefault(o => o.OrderNumber == result.OrderNumber);

        if (orderEntity == null)
        {
            return Task.FromResult(new IpnResult
            {
                RspCode = "01",
                Message = "Order not found",
            });
        }

        if (orderEntity.TotalAmount.Amount != result.Amount)
        {
            return Task.FromResult(new IpnResult
            {
                RspCode = "04",
                Message = "Invalid amount",
            });
        }

        // if order is already completed, no need to process again
        if (orderEntity.Status == Domain.Enums.OrderStatus.Completed)
        {
            return Task.FromResult(new IpnResult
            {
                RspCode = "02",
                Message = "Order already processed",
            });
        }

        if (result.IsSuccess)
        {
            var integrationEvent = new PaymentSucceededIntegrationEvent
            {
                OrderNumber = result.OrderNumber,
                TransactionId = result.TransactionId,
                CardBrand = result.CardBrand
            };

            var message = new PollingOutboxMessage
            {
                CreateDate = DateTime.UtcNow,
                PayloadType = typeof(PaymentSucceededIntegrationEvent).FullName ?? throw new Exception($"Could not get fullname of type {integrationEvent.GetType()}"),
                Payload = JsonSerializer.Serialize(integrationEvent),
                ProcessedDate = null
            };

            _outboxRepository.AddAsync(message);
        }
        else
        {
            // when response code and transaction status are not success, create payment rejected event
            var integrationEvent = new PaymentRejectedIntegrationEvent
            {
                OrderNumber = result.OrderNumber,
                TransactionId = result.TransactionId,
                CardBrand = result.CardBrand
            };

            var message = new PollingOutboxMessage
            {
                CreateDate = DateTime.UtcNow,
                PayloadType = typeof(PaymentRejectedIntegrationEvent).FullName ?? throw new Exception($"Could not get fullname of type {integrationEvent.GetType()}"),
                Payload = JsonSerializer.Serialize(integrationEvent),
                ProcessedDate = null
            };

            _outboxRepository.AddAsync(message);
        }

        _outboxRepository.SaveChangesAsync();

        return Task.FromResult(new IpnResult
        {
            RspCode = "00",
            Message = "Confirm Success",
        });
    }
}
