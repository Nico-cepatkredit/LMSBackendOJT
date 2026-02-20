namespace LMSBackend.Domain.Entities
{
    public class LoanChargesType
    {
        public int Id { get; set; }
        public Guid LMSLoanAppId { get; set; }
        public string? Name { get; set; }
        public Decimal? Amount { get; set; }
        public string? Type { get; set; }
        public bool IsDeleted { get; set; }
        public Guid EncodedBy { get; set; }
        public DateTime EncodedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}