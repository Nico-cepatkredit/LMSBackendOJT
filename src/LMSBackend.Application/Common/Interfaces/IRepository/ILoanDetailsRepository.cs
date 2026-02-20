using LMSBackend.Domain.Entities;

namespace LMSBackend.Application.Common.Interfaces.IRepository
{
    public interface ILoanDetailsRepository
    {
        Task AddLoanAsync(LoanDetailsApp loan);
        Task AddApprovedDetailsAsync(ApprovedDetails approvedDetails);
        Task AddLoanChargesAsync(LoanCharges loanCharges);
        Task AddLoanChargeTypesAsync(IEnumerable<LoanChargesType> loanChargeTypes);
        Task AddBorrowerAsync(BorrowerDetails borrower);
        Task AddBorrowersAsync(IEnumerable<BorrowerDetails> borrowers);

        Task AddBorrowerAddressAsync(BorrowerAddress address);
        Task AddBorrowerAddressesAsync(IEnumerable<BorrowerAddress> addresses);
        Task AddRemarksAsync(Remarks remarks);
        Task AddAssignmentAsync(Assignment assignment);
        Task AddBorrowersBackgroundAsync(BorrowerBackground addresses);
        Task AddEmploymentDetailsAsync(EmploymentDetails remarks);
        Task AddLoanLCCommissionAsync(IEnumerable<LoanLCCommission> commission);
        Task AddMonthlyAmmortizationAsync(IEnumerable<MonthlyAmortization> amortization);

        Task<bool> ExistsUnGeneratedByLOSLoanAppIdAsync(string losLoanAppId);
    }
}