
using LMSBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSBackend.Infrastructure.Persistence.Configurations
{
    public class ApprovedDetailsConfiguration : IEntityTypeConfiguration<ApprovedDetails>
    {
        public void Configure(EntityTypeBuilder<ApprovedDetails> builder)
        {
            builder.ToTable("ApprovedDetails", "ILP");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(x => x.LMSLoanAppId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.HasOne<LoanDetailsApp>()
                .WithMany()
                .HasForeignKey(x => x.LMSLoanAppId)
                .HasConstraintName("FK_ApprovedDetails_LoanDetails")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Amount)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.InterestRate)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.Terms)
                .IsRequired();

            builder.Property(x => x.Amortization)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.OtherExposure)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.TotalExposure)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(x => x.CRORemarks)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.EncodedBy)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.EncodedDate)
                .HasDefaultValueSql("GETDATE()")
                .IsRequired();

            builder.Property(x => x.ApprovedBy)
                .HasColumnType("uniqueidentifier");

            builder.Property(x => x.ApprovedDate);

            builder.HasIndex(x => x.LMSLoanAppId)
                .IsUnique();
        }
    }
}