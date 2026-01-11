namespace Application.Common.Models;

public class VerifyReturnUrlResult
{
    public bool CheckSignature { get; set; }
    public bool IsSuccess { get; set; }
    public string ResCode { get; set; } = null!;
}
