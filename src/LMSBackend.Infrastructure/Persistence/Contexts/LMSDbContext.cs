using Microsoft.EntityFrameworkCore;
using LMSBackend.Domain.Entities;
using LMSBackend.Infrastructure.Persistence.Seed;
using LMSBackend.Application.Features.LoansTable.Dtos;
using LMSBackend.Application.Features.CRAM.Dtos;

namespace LMSBackend.Infrastructure.Persistence.Contexts;

public class LMSDbContext : DbContext
{
    public LMSDbContext(DbContextOptions<LMSDbContext> options)
        : base(options)
    {
    }
    public DbSet<ApprovedDetails> ApprovedDetails => Set<ApprovedDetails>();
    public DbSet<Assignment> Assignment => Set<Assignment>();
    public DbSet<LoanCharges> LoanCharges => Set<LoanCharges>();
    public DbSet<LoanChargesDetails> LoanChargesDetails => Set<LoanChargesDetails>();
    public DbSet<LoanChargesType> LoanChargesType => Set<LoanChargesType>();
    public DbSet<LoanDetailsApp> LoanDetailsApp => Set<LoanDetailsApp>();
    public DbSet<LoanLCCommission> LoanLCCommission => Set<LoanLCCommission>();
    public DbSet<MonthlyAmortization> MonthlyAmortizations => Set<MonthlyAmortization>();
    public DbSet<Remarks> Remarks => Set<Remarks>();
    public DbSet<StatusList> StatusList => Set<StatusList>();
    public DbSet<BorrowerDetails> BorrowerDetails => Set<BorrowerDetails>();
    public DbSet<BorrowerAddress> BorrowerAddress => Set<BorrowerAddress>();

    public DbSet<BorrowerBackground> BorrowerBackground => Set<BorrowerBackground>();
    public DbSet<EmploymentDetails> EmploymentDetails => Set<EmploymentDetails>();
    public DbSet<RefreshToken> RefreshToken => Set<RefreshToken>();
    public DbSet<UserAccounts> UserAccounts => Set<UserAccounts>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(LMSDbContext).Assembly);

        modelBuilder.Entity<LoansListDto>(eb =>
        {
            eb.HasNoKey();
            eb.ToView(null);
        });

        modelBuilder.Entity<StatusCountDto>(eb =>
        {
            eb.HasNoKey();
            eb.ToView(null);
        });

        modelBuilder.Entity<LoanAppDetailsDto>(eb =>
        {
            eb.HasNoKey();
            eb.ToView(null);
        });

        modelBuilder.ApplyAllEntitySeeds();
    }
}
