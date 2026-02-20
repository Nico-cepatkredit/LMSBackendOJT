namespace LMSBackend.Application.Features.LoansTable.Dtos
{
    public class StatusCountDto
    {
        required
        public string Status { get; set; }
        public int Total { get; set; }
    }
}