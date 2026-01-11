using Application.Common.Models;
using Domain.Enums;
using MediatR;

namespace Application.Payment.Commands.CreatePaymentUrl;

public record CreatePaymentUrlCommand(
    long OrderNumber, 
    decimal Amount,
    PaymentProvider Provider, 
    DateTime OrderDate) 
    : IRequest<CreatePaymentUrlResult>;
