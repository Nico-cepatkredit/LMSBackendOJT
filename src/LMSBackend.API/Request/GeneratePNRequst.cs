namespace LMSBackend.API.Request
{
    public class GeneratePNRequst
    {
        public Guid LMSLoanAppId { get; set; }
        public string? LOSLoanAppId { get; set; }
        public Guid UserId { get; set; }
    }
}