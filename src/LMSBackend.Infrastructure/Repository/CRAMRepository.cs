using System.Collections;
using LMSBackend.Application.Common.Interfaces.IRepository;
using LMSBackend.Application.Features.CRAM.Dtos;
using LMSBackend.Infrastructure.Persistence.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LMSBackend.Infrastructure.Repository
{
    public class CRAMRepository : ICRAMRepository
    {
        private readonly LMSDbContext _context;

        public CRAMRepository(LMSDbContext context)
        {
            _context = context;
        }

        public async Task<LoanAppDetailsDto?> GetLoanAppDetailsAsync(
            string loanAppCode,
            string status,
            string? pnnumber = null
        )
        {
            var parameters = new[]
            {
                new SqlParameter("@loanappcode", loanAppCode ?? (object)DBNull.Value),
                new SqlParameter("@status", status ?? (object)DBNull.Value),
                new SqlParameter("@pnnumber", pnnumber ?? (object)DBNull.Value)
            };

            var result = await _context
                .Set<LoanAppDetailsDto>()
                .FromSqlRaw("EXEC [ILP].[sp_GetLoanAppDetails] @loanappcode, @status, @pnnumber", parameters)
                .ToListAsync();

            return result.FirstOrDefault();
        }
    }
}