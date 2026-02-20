namespace LMSBackend.Domain.Entities
{
    public class ApprovedDetails
    {
        public Guid Id { get; set; }
        public Guid LMSLoanAppId { get; set; }
        public Decimal Amount { get; set; }
        public Decimal InterestRate { get; set; }
        public int Terms { get; set; }
        public Decimal Amortization { get; set; }
        public Decimal OtherExposure { get; set; }
        public Decimal TotalExposure { get; set; }
        public string CRORemarks { get; set; } = null!;
        public Guid EncodedBy { get; set; }
        public DateTime EncodedDate { get; set; }
        public Guid? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
    }
}