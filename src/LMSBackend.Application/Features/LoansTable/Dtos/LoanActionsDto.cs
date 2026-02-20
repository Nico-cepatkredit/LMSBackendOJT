namespace LMSBackend.Application.Features.LoansTable.Dtos
{
    public class LoanActionsDto
    {
        public LoanActionDto? GeneratePN { get; set; }
        public LoanActionDto? CancelRequest { get; set; }
        public LoanActionDto? CancelApproved { get; set; }
        public LoanActionDto? CancelDeclined { get; set; }
    }
}