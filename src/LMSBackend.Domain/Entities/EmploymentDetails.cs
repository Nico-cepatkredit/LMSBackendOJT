using LMSBackend.Domain.Enums;

namespace LMSBackend.Domain.Entities
{
    public class EmploymentDetails
    {
        public Guid Id { get; set; }
        public Guid BorrowersId { get; set; }
        public BorrowersType BorrowersType { get; set; }
        public string? ValidID { get; set; }
        public string? ValidIDNumber { get; set; }
        public string Country { get; set; } = null!;
        public string JobCategory { get; set; } = null!;
        public string JobPosition { get; set; } = null!;
        public string Employer { get; set; } = null!;
        public string Agency { get; set; } = null!;
        public string Currency { get; set; } = null!;
        public Decimal? Salary { get; set; }
        public DateTime? ContractDate { get; set; }
        public int? ContractDuration { get; set; }
        public int? YOEAsOFW { get; set; }
        public Decimal? Remittance { get; set; }
        public RemittanceChannel? RemittanceChannel { get; set; }
        public string? Remittee { get; set; }
        public string? VesselName { get; set; }
        public string? VesselType { get; set; }
        public string? VesselLocation { get; set; }
        public string? IMOVessel { get; set; }
        public bool? IsContractUnli {get; set;}
        public decimal? SalaryInForiegn {get; set;}
        public decimal? SalaryInPeso {get; set;}
        public string? PossibleVacation { get; set; }
        public EmploymentStatus? EmploymentStatus { get; set;}
    }
}
