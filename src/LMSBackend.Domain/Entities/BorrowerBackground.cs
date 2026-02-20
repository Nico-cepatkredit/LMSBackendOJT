using LMSBackend.Domain.Enums;

namespace LMSBackend.Domain.Entities
{
    public class BorrowerBackground
    {
        public Guid Id { get; set; }
        public Guid? BorrowersId { get; set; }
        public BorrowersType? BorrowersType { get; set; }
        public string? BackgroundType { get; set; }
        public string? Name { get; set; }
        public string? Suffix {get; set;} // String lang ito sa LOS
        public int? Age { get; set; }
        public Affiliation? Affiliation { get; set; }
        public Relationships? Relationship { get; set; }
        public string? Remarks { get; set; }
        public string? Position { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public decimal? LoanApproval { get; set; }
        public decimal? Amortization { get; set; }
        public Category? Category { get; set; }
        public string? ReferenceName { get; set; }
        public string? ReferenceYear { get; set; }
        public string? ContactNumber { get; set; }
        public string? Province { get; set; }
        public Guid EncodedBy { get; set; }
        public DateTime EncodedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
