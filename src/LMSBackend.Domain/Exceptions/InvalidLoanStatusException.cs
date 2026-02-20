
namespace LMSBackend.Domain.Exceptions
{
    public class InvalidLoanStatusException : DomainException
    {
        public Guid LoanId { get; }
        public Guid StatusId { get; }

        public InvalidLoanStatusException(Guid loanId, Guid statusId, string message)
            : base(message, "INVALID_LOAN_STATUS")
        {
            LoanId = loanId;
            StatusId = statusId;
        }
    }
}