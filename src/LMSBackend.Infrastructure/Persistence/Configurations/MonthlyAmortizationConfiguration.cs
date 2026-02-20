using LMSBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSBackend.Infrastructure.Persistence.Configurations
{
    public class MonthlyAmortizationConfiguration : IEntityTypeConfiguration<MonthlyAmortization>
    {
        public void Configure(EntityTypeBuilder<MonthlyAmortization> builder)
        {
            builder.ToTable("MonthlyAmortization", "ILP");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.LMSLoanAppId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.HasOne<LoanDetailsApp>()
                .WithMany()
                .HasForeignKey(x => x.LMSLoanAppId)
                .HasConstraintName("FK_MonthlyAmortization_LoanDetails")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Term)
                .IsRequired();

            builder.Property(x => x.InterestRate)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Amortization)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.InterestAmount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Principal)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.OutstandingPrincipal)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.OutstandingReceivables)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.EncodedBy)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.EncodedDate)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder.Property(x => x.UpdatedBy)
                .HasColumnType("uniqueidentifier");

            builder.Property(x => x.UpdatedDate);

            builder.HasIndex(x => x.LMSLoanAppId);
        }
    }
}