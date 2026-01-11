using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;

namespace Application.Payment.Commands.CreatePaymentUrl;

public class CreatePaymentUrlCommandHandler : IRequestHandler<CreatePaymentUrlCommand, CreatePaymentUrlResult>
{
    private readonly IPaymentGatewayFactory _factory;

    public CreatePaymentUrlCommandHandler(IPaymentGatewayFactory factory)
    {
        _factory = factory;
    }

    public Task<CreatePaymentUrlResult> Handle(CreatePaymentUrlCommand request, CancellationToken cancellationToken)
    {
        // warn: 
        var gateway = _factory.Resolve(request.Provider);
        return gateway.CreatePaymentUrl(new CreatePaymentUrlRequest
        {
            OrderNumber = request.OrderNumber,
            Amount = request.Amount,
            OrderDate = request.OrderDate,
        });
    }
}
