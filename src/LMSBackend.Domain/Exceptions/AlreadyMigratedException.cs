namespace LMSBackend.Domain.Exceptions;

public sealed class AlreadyMigratedException : DomainException
{
    public string LoanAppId { get; }

    public AlreadyMigratedException(string loanAppId)
        : base(
            $"Loan with LoanAppId '{loanAppId}' has already been migrated with status UNGENERATED.",
            "ALREADY_MIGRATED"
        )
    {
        LoanAppId = loanAppId;
    }
}