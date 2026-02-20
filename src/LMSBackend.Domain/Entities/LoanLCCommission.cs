namespace LMSBackend.Domain.Entities
{
    public class LoanLCCommission
    {
        public int Id { get; set; }
        public Guid LMSLoanAppId { get; set; }
        public Guid LoanLCCommissionId { get; set; }
        public int LCNo { get; set; }
        public Guid? ConsultantId { get; set; }
        public string ConsultantName { get; set; } = String.Empty;
        public bool IsSeniorCitizen { get; set; }
        public bool IsLCSpecial { get; set; }
        public int AvailedTerm { get; set; }
        public Decimal CommissionRate { get; set; }
        public Decimal CommissionAmount { get; set; }
        public bool Status { get; set; }
        public bool IsRenewal { get; set; }
        public Decimal AvailedAmount { get; set; }
        public Guid EncodedBy { get; set; }
        public DateTime EncodedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}