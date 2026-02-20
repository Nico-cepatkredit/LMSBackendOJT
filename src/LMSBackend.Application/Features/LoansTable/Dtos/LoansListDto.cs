namespace LMSBackend.Application.Features.LoansTable.Dtos
{
    public class LoansListDto
    {
        public required Guid LMSLoanAppId { get; set; }
        public required string LoanAppCode { get; set; }
        public string? PNNumber { get; set; }
        public required string ProductId { get; set; }
        public int BranchId { get; set; }
        public int? Origin { get; set; }
        public required string BorrowerFullName { get; set; }
        public Guid? RecUser{ get; set; }
        public DateTime? RecDate { get; set; }
        public Guid? ModUser{ get; set; }
        public DateTime? ModDate { get; set; }
        public string? StatusName { get; set; }
        public Guid? PNGeneratedBy { get; set; }
        public DateTime? PNGeneratedDate { get; set; }
        public Guid? PNCancelRequestBy { get; set; }
        public DateTime? PNCancelRequestDate { get; set; }
        public Guid? PNCancelApprovedBy { get; set; }
        public DateTime? PNCancelApprovedDate { get; set; }
        public Guid? PNCancelDeclinedBy { get; set; }
        public DateTime? PNCancelDeclinedDate { get; set; }
    }
}