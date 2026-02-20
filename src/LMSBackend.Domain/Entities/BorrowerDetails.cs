using System.ComponentModel.DataAnnotations.Schema;
using LMSBackend.Domain.Enums;

namespace LMSBackend.Domain.Entities
{
    public class BorrowerDetails
    {
        public Guid Id { get; set; }
        public BorrowersType BorrowersType { get; set; }
        public Guid LMSLoanAppID { get; set; }
        public string BorrowersCode { get; set; } = null!;
        public int IsOFW { get; set; }
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Suffix Suffix { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; } = null!;
        public string MobileNumber { get; set; } = null!;
        public string? MobileNumber2 { get; set; }
        public string? SocialMedia { get; set; }
        public Relationships Relationships { get; set; }
        public string? GroupChat { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public bool PEP { get; set; }
        public Religion Religion { get; set; }
        public string? School { get; set; }
        public string? Course { get; set; }
        public EducationStatus? EducationStatus { get; set; }
        public bool? isPBCBMarried { get; set; }
        public Relationships? RelationshipsToBene { get; set; }
        public Relationships? RelationshipsToACB { get; set; }
        public string? SpouseName { get; set; }
        public DateOnly? SpouseBirthdate { get; set; }
        public SpouseSourceOfIncome? SpouseSourceOfIncome { get; set; }
        public decimal? SpouseIncome { get; set; }
        public SpouseSourceOfIncome? CBACBSourceOfIncome { get; set; }
        public string AddressId { get; set; } = null!;

        // Navigation
        public LoanDetailsApp Loan { get; set; } = null!;
    }
}