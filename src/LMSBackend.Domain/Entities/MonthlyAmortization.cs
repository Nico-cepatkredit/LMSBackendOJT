namespace LMSBackend.Domain.Entities
{
    public class MonthlyAmortization
    {
        public int Id { get; set; }
        public Guid LMSLoanAppId { get; set; }
        public int Term { get; set; }
        public Decimal InterestRate { get; set; }
        public Decimal Amortization { get; set; }
        public Decimal InterestAmount { get; set; }
        public Decimal Principal { get; set; }
        public Decimal OutstandingPrincipal { get; set; }
        public Decimal OutstandingReceivables { get; set; }
        public Guid EncodedBy { get; set; }
        public DateTime EncodedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}