namespace LMSBackend.Domain.Entities
{
    public class Remarks
    {
        public Guid Id { get; set; }
        public Guid LMSLoanAppId { get; set; }
        public Guid StatusId { get; set; }
        public int LOSStatus { get; set; }
        public string? InternalRemarks { get; set; }
        public DateTime? InternalRemarksDate { get; set; }
        public string? ExternalRemarks { get; set; }
        public DateTime? ExternalRemarksDate { get; set; }
        public string? Urgent { get; set; }
        public string? UrgentApp { get; set; }
        public int? MasterList { get; set; }
        public int? FFCC { get; set; }
        public int? CRAF { get; set; }
        public int? Kaiser { get; set; }
        public int? VideoCall { get; set; }
        public int? ShareLocation { get; set; }
        public int? AgencyVerification { get; set; }

        // Navigation (lookup)
        public StatusList Status { get; set; } = null!;
        // relationship
        public LoanDetailsApp Loan { get; set; } = null!;
    }
}