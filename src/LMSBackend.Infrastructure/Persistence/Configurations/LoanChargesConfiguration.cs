using LMSBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSBackend.Infrastructure.Persistence.Configurations
{
    public class LoanChargesConfiguration : IEntityTypeConfiguration<LoanCharges>
    {
        public void Configure(EntityTypeBuilder<LoanCharges> builder)
        {
            builder.ToTable("LoanCharges", "ILP");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.LMSLoanAppId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.HasOne<LoanDetailsApp>()
                .WithMany()
                .HasForeignKey(x => x.LMSLoanAppId)
                .HasConstraintName("FK_LoanCharges_LoanDetails")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.LoanProduct)
                .HasMaxLength(50);

            builder.Property(x => x.LoanType)
                .HasMaxLength(50);

            builder.Property(x => x.AvailedAmount)
                .HasPrecision(18, 2);

            builder.Property(x => x.AvailedTerms);

            builder.Property(x => x.AmountFinance)
                .HasPrecision(18, 2);

            builder.Property(x => x.PreviousPNNumber)
                .HasMaxLength(50);

            builder.Property(x => x.PreviousLoanBalance)
                .HasPrecision(18, 2);

            builder.Property(x => x.ProcessingFeeRate)
                .HasPrecision(18, 2);

            builder.Property(x => x.InterestRate)
                .HasPrecision(18, 2);

            builder.Property(x => x.CreditRiskFeeRate)
                .HasPrecision(18, 2);

            builder.Property(x => x.GracePeriod);

            builder.Property(x => x.ChargeType);

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