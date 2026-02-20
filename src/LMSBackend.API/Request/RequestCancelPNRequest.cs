namespace LMSBackend.API.Request
{
    public class RequestCancelPNRequest
    {
        public required string pnNumber { get; set; }
        public Guid UserId { get; set; }
        public string LOSLoanAppId { get; set; } = string.Empty;
    }
}