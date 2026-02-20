namespace LMSBackend.Domain.Exceptions
{
    public class LOSLoanAppIdNotFoundException : DomainException
    {
        public LOSLoanAppIdNotFoundException(string loanAppId)
    : base(
        message: $"LoanAppId '{loanAppId}' was not found in LOS",
        code: "LOS_LOAN_NOT_FOUND")
        {
        }
    }
}