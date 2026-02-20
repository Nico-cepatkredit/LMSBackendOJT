using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMSBackend.Domain.Constants;
using LMSBackend.Domain.Entities;
using LMSBackend.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using LMSBackend.Infrastructure.Tests.Helpers;

namespace LMSBackend.Infrastructure.Persistence.SQLDesign
{
    public class GeneratePNCommandDesign
    {
        private readonly LMSDbContext _context;

        public GeneratePNCommandDesign(LMSDbContext context)
        {
            _context = context;
        }

        public async Task<string> ExecuteAsync(
            Guid lmsLoanAppId,
            Guid userId,
            CancellationToken ct = default)
        {
            // 🔹 LINQ – authoring starts here (just like Build())

            var loan = await _context.Set<LoanDetailsApp>()
                .Include(l => l.Assignment)
                .Include(l => l.Remarks)
                .SingleAsync(l => l.LMSLoanAppId == lmsLoanAppId, ct);

            var branchCode = loan.BranchId.ToString("00");
            var year = DateTime.UtcNow.Year;
            var prefix = $"PN{branchCode}{year}";

            var lastPn = await _context.Set<LoanDetailsApp>()
                .Where(l => l.PNNumber != null &&
                            l.PNNumber.StartsWith(prefix))
                .OrderByDescending(l => l.PNNumber)
                .Select(l => l.PNNumber!)
                .FirstOrDefaultAsync(ct);

            var nextSeries = lastPn == null
                ? 1
                : int.Parse(lastPn[^6..]) + 1;

            var pnNumber = $"{prefix}{nextSeries:000000}";

            // 🔹 mutations (still LINQ/EF authored)

            loan.PNNumber = pnNumber;

            if (loan.Assignment == null)
            {
                loan.Assignment = new Assignment
                {
                    Id = Guid.NewGuid(),
                    LMSLoanAppId = loan.LMSLoanAppId
                };

                _context.Add(loan.Assignment);
            }

            loan.Assignment.PNGeneratedBy = userId;
            loan.Assignment.PNGeneratedDate = DateTime.UtcNow;

            if (loan.Remarks == null)
                throw new InvalidOperationException("Remarks must exist.");

            loan.Remarks.StatusId = StatusIds.UNPOSTED;

            EfCommandInspectionHelper.DumpPendingChanges(_context);
            await _context.SaveChangesAsync(ct);

            return pnNumber;
        }
    }
}