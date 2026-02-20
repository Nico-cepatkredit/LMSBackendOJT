using LMSBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSBackend.Infrastructure.Persistence.Configurations
{
    public class LoanLCCommissionConfiguration : IEntityTypeConfiguration<LoanLCCommission>
    {
        public void Configure(EntityTypeBuilder<LoanLCCommission> builder)
        {
            builder.ToTable("LoanLCCommission", "ILP");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.LMSLoanAppId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.HasOne<LoanDetailsApp>()
               .WithMany()
               .HasForeignKey(x => x.LMSLoanAppId)
               .HasConstraintName("FK_LoanLCCommission_LoanDetails")
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.LoanLCCommissionId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.LCNo)
                .IsRequired();

            builder.Property(x => x.ConsultantId)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.ConsultantName)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.IsSeniorCitizen)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(x => x.IsLCSpecial)
                .HasDefaultValue(false)
                .IsRequired();

            builder.Property(x => x.AvailedTerm)
                .IsRequired();

            builder.Property(x => x.CommissionRate)
                .HasPrecision(18, 4)
                .IsRequired();

            builder.Property(x => x.CommissionAmount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.AvailedAmount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Status)
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(x => x.IsRenewal)
                .HasDefaultValue(false)
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
            builder.HasIndex(x => x.LoanLCCommissionId)
                .IsUnique();
        }
    }
}