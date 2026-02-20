using LMSBackend.Domain.Enums;

namespace LMSBackend.Domain.Entities
{
    public class BorrowerAddress
    {
        public Guid Id { get; set; }
        public Guid BorrowersId { get; set; }
        public BorrowersType BorrowersType { get; set; }
        public AddressType AddressType { get; set; }
        public string Province { get; set; } = null!;
        public string Municipality { get; set; } = null!;
        public string Barangay { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string? LandMark { get; set; }
        public int? StayYears { get; set; }
        public int? StayMonths { get; set; }
        public string? CollectionArea { get; set; }
        public string? ProofOfBilling { get; set; }
        public TypeOfResidence TypeOfResidence { get; set; }
        public decimal? ResidenceAmount { get; set; }
    }
}