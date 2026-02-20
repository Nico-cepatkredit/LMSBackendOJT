namespace LMSBackend.Domain.Entities
{
    public class LoanChargesDetails
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string? Name { get; set; }
        public Decimal? Amount { get; set; }
        public string? Type { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public int IsDropdown { get; set; }
    }
}