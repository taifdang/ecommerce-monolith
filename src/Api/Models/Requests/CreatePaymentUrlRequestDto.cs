using Domain.Enums;

namespace Api.Models.Requests;

public record CreatePaymentUrlRequestDto(long OrderNumber, decimal Amount, PaymentProvider Provider);
