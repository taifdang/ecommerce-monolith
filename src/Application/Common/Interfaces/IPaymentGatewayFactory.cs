using Domain.Enums;

namespace Application.Common.Interfaces;

public interface IPaymentGatewayFactory
{
    IPaymentGateway Resolve(PaymentProvider provider);
}
