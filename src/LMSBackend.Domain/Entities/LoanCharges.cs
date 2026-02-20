namespace LMSBackend.Domain.Entities
{
    public class LoanCharges
    {
        public int Id { get; set; }
        public Guid LMSLoanAppId { get; set; }
        public string? LoanProduct { get; set; }
        public string? LoanType { get; set; }
        public Decimal? AvailedAmount { get; set; }
        public int? AvailedTerms { get; set; }
        public Decimal? AmountFinance { get; set; }
        public string? PreviousPNNumber { get; set; }
        public Decimal? PreviousLoanBalance { get; set; }
        public Decimal? ProcessingFeeRate { get; set; }
        public Decimal? InterestRate { get; set; }
        public Decimal? CreditRiskFeeRate { get; set; }
        public int? GracePeriod { get; set; }
        public int? ChargeType { get; set; }
        public Guid EncodedBy { get; set; }
        public DateTime EncodedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}