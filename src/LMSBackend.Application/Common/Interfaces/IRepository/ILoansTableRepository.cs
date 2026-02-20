using LMSBackend.Application.Features.LoansTable.Dtos;

namespace LMSBackend.Application.Common.Interfaces.IRepository
{
    public interface ILoansTableRepository
    {
        Task<IEnumerable<LoansListDto>> GetLoansListAsync(string? statusName = null);
        Task<IEnumerable<StatusCountDto>> GetStatusCountsAsync();
    }
}