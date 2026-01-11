using Application.Common.Models;

namespace Application.Common.Interfaces;

public interface IPaymentGateway
{
    Task<CreatePaymentUrlResult> CreatePaymentUrl(CreatePaymentUrlRequest request);

    VerifyReturnUrlResult VerifyReturnUrl(IDictionary<string, string> parameters);

    VerifyIpnResult VerifyIpnCallback(IDictionary<string, string> parameters);

#if(deprecated)
    Task<PaymentReturnResult> VerifyReturnUrl(IDictionary<string, string> parameters);
    Task<IpnResult> VerifyIpnCallback(IDictionary<string, string> parameters);
#endif
}
