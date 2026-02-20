using System.Text.Json.Serialization;
using LMSBackend.Application.Common.JsonConverters;
using LMSBackend.Application.Features.CRAM.Enum;

namespace LMSBackend.Application.Features.CRAM.Dtos
{
    public class LoanAppDetailsDto
    {
        public required Guid LMSLoanAppId { get; set; }
        public required string LoanAppCode { get; set; }

        [JsonConverter(typeof(DateTimeWithTimeConverter))]
        public DateTime? DateOfApplication { get; set; }

        public string? ProductId { get; set; }

        [JsonConverter(typeof(DateOnlyConverter))]
        public DateTime? DepartureDate { get; set; }

        public string? Branch { get; set; }
        public LoanTypeId LoanTypeId { get; set; }
        public string? LoanPurpose { get; set; }
        public decimal Amount { get; set; }
        public decimal ApprovedAmount { get; set; }
        public int ApprovedTerms { get; set; }
        public string? CRARecommendation { get; set; }
        public int? Origin { get; set; }
        public string? LCName { get; set; }
        public string? LCMobileNumber { get; set; }
        public string? LCSocialMedia { get; set; }
        public string? CRARemarks { get; set; }
    }
}