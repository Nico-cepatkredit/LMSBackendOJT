using LMSBackend.Application.Common.Interfaces.IRepository;
using LMSBackend.Application.Features.LoansTable.Dtos;
using LMSBackend.Infrastructure.Persistence.Contexts;
using LMSBackend.Infrastructure.Persistence.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LMSBackend.Infrastructure.Repository
{
    public class LoansTableRepository : ILoansTableRepository
    {
        private readonly LMSDbContext _context;
        public LoansTableRepository(LMSDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LoansListDto>> GetLoansListAsync(string? statusName = null)
        {
            var param = new SqlParameter(
                "@StatusName",
                statusName ?? (object)DBNull.Value);

            var rows = await _context.Set<LoansListDto>()
                .FromSqlRaw(
                    "EXEC [ILP].[sp_GetLoansList] @StatusName",
                    param)
                .AsNoTracking()
                .ToListAsync();

            return rows.Select(x => new LoansListDto
            {
                LMSLoanAppId = x.LMSLoanAppId,
                LoanAppCode = x.LoanAppCode,
                PNNumber = x.PNNumber,
                ProductId = x.ProductId,
                BranchId = x.BranchId,
                Origin = x.Origin,
                BorrowerFullName = x.BorrowerFullName,
                RecUser = x.RecUser,
                RecDate = x.RecDate,
                ModUser = x.ModUser,
                ModDate = x.ModDate,
                PNGeneratedBy = x.PNGeneratedBy,
                PNGeneratedDate = x.PNGeneratedDate,
                PNCancelRequestBy = x.PNCancelRequestBy,
                PNCancelRequestDate = x.PNCancelRequestDate,
                PNCancelApprovedBy = x.PNCancelApprovedBy,
                PNCancelApprovedDate = x.PNCancelApprovedDate,
                PNCancelDeclinedBy = x.PNCancelDeclinedBy,
                PNCancelDeclinedDate = x.PNCancelDeclinedDate,
                StatusName = x.StatusName
            });
        }

        public async Task<IEnumerable<StatusCountDto>> GetStatusCountsAsync()
        {
            var rows = await _context
                .Set<StatusCountDto>()
                .FromSqlRaw("EXEC [ILP].[sp_GetStatusCount]")
                .AsNoTracking()
                .ToListAsync();

            return rows.Select(x => new StatusCountDto
            {
                Status = x.Status,
                Total = x.Total
            });
        }
    }
}