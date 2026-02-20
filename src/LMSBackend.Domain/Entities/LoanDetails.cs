namespace LMSBackend.Domain.Entities;

public class LoanDetailsApp
{
    public Guid LMSLoanAppId { get; set; }

    public string LoanAppCode { get; set; } = null!;
    public string LOSLoanAppId { get; set; } = null!;
    public string? PNNumber { get; set; }

    public DateTime DateOfApplication { get; set; }
    public string ProductId { get; set; } = null!;

    public DateTime? DepartureDate { get; set; }

    public int BranchId { get; set; }
    public int LoanTypeId { get; set; }
    public int PurposeId { get; set; }
    public int ChannelId { get; set; }

    public decimal? Amount { get; set; }
    public decimal? ApprovedAmount { get; set; }

    public int? ApprovedTerms { get; set; }
    public int? Terms { get; set; }
    public int? Origin { get; set; }

    public string? ReferredBy { get; set; }
    public Guid? LCId { get; set; }
    public string? LCMobileNumber { get; set; }
    public string? LCSocialMedia { get; set; }

    public string? CRARemarks { get; set; }
    public string? CRARecommendation { get; set; }
    public string? RecUser { get; set; }

    public DateTime RecDate { get; set; }
    public Guid? ModUser { get; set; }
    public DateTime? ModDate { get; set; }

    // Relationships
    public ICollection<BorrowerDetails> Borrowers { get; private set; }
        = new List<BorrowerDetails>();

    public Assignment? Assignment { get; set; }
    public Remarks? Remarks { get; set; }

    public virtual UserAccounts? UserAccount { get; set; }
}