using LMSBackend.Application.Features.CRAM.Dtos;

namespace LMSBackend.Application.Common.Interfaces.IRepository
{
    public interface ICRAMRepository
    {
        Task<LoanAppDetailsDto?> GetLoanAppDetailsAsync(
            string loanAppCode,
            string status,
            string? pnnumber = null
        );
    }
}