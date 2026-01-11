namespace Application.Common.Models;

public class CreatePaymentUrlResult
{
    public bool Status { get; set; }
    public string? Data { get; set; }
    public string? Error { get; set; }
}
