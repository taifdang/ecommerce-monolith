namespace Application.Common.Models;

public class VerifyIpnResult
{
    public bool CheckSignature { get; set; }
    public bool IsSuccess { get; set; }
    public bool IsNullEvent { get; set; }
    public string ResCode { get; set; } = null!;
    // destructure properties to Ipn 
    public long OrderNumber { get; set; }
    public string TransactionId { get; set; } = null!;
    public decimal Amount { get; set; }
    public string? CardBrand { get; set; } = "N/A";
}
