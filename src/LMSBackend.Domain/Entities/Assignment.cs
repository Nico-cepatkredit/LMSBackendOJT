namespace LMSBackend.Domain.Entities
{
    public class Assignment
    {
        public Guid Id { get; set; }
        public Guid LMSLoanAppId { get; set; }
        public Guid RemarksId { get; set; }
        public Guid? Marketing { get; set; }
        public Guid? MarketingOfficer { get; set; }
        public DateTime? MAAssignedDate { get; set; }
        public Guid? Credit { get; set; }
        public Guid? CreditOfficer { get; set; }
        public Guid? AssignedCreditOfficer { get; set; }
        public DateTime? CRAAssignedDate { get; set; }
        public Guid? Loans { get; set; }
        public Guid? LoansOfficer { get; set; }
        public Guid? AssignedLoansOfficer { get; set; }
        public DateTime? LPAAssignedDate { get; set; }
        public Guid? ReleasedBy { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public Guid? PNCancelRequestBy { get; set; }
        public DateTime? PNCancelRequestDate { get; set; }
        public Guid? PNCancelApprovedBy { get; set; }
        public DateTime? PNCancelApprovedDate { get; set; }
        public Guid? PNCancelDeclinedBy { get; set; }
        public DateTime? PNCancelDeclinedDate { get; set; }
        public Guid? PNGeneratedBy { get; set; }
        public DateTime? PNGeneratedDate { get; set; }
        // Navigation
        public LoanDetailsApp Loan { get; set; } = null!;
        public Remarks Remarks { get; set; } = null!;
    }
}