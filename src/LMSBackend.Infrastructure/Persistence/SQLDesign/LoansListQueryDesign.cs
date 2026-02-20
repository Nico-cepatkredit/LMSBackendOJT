using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMSBackend.Domain.Entities;
using LMSBackend.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LMSBackend.Infrastructure.Persistence.SQLDesign
{
    public class LoansListQueryDesign
    {
        private readonly LMSDbContext _context;

        public LoansListQueryDesign(LMSDbContext context)
        {
            _context = context;
        }

        public IQueryable<object> Build(string? statusName)
        {
            return _context.Set<LoanDetailsApp>()
                .AsNoTracking()
                .Where(l =>
                    string.IsNullOrEmpty(statusName) ||
                    (
                        l.Assignment != null &&
                        l.Assignment.Remarks != null &&
                        l.Assignment.Remarks.Status.Name == statusName
                    ))
                .OrderByDescending(l => l.RecDate)
                .Select(l => new
                {
                    l.LMSLoanAppId,
                    l.LoanAppCode,
                    l.PNNumber,
                    l.ProductId,
                    l.BranchId,
                    l.Origin,
                    l.RecDate,

                    BorrowerFullName = l.Borrowers
                        .OrderBy(b => b.BorrowersType)
                        .Select(b =>
                            b.FirstName + " " +
                            b.MiddleName + " " +
                            b.LastName)
                        .FirstOrDefault(),

                    StatusName = l.Assignment != null &&
                                 l.Assignment.Remarks != null
                        ? l.Assignment.Remarks.Status.Name
                        : null,

                    PNCancelRequestBy = l.Assignment!.PNCancelRequestBy,
                    PNCancelApprovedBy = l.Assignment!.PNCancelApprovedBy,
                    PNCancelDeclinedBy = l.Assignment!.PNCancelDeclinedBy,
                    PNGeneratedBy = l.Assignment!.PNGeneratedBy
                });
        }
    }
}