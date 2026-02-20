using LMSBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSBackend.Infrastructure.Persistence.Configurations
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> builder)
        {
            builder.ToTable("Assignments", "ILP");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(x => x.LMSLoanAppId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(x => x.RemarksId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.HasOne<Remarks>()
               .WithMany()
               .HasForeignKey(x => x.RemarksId)
               .HasConstraintName("FK_Assignments_Remarks")
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Marketing)
                .HasColumnType("uniqueidentifier");

            builder.Property(x => x.MarketingOfficer)
                .HasColumnType("uniqueidentifier");

            builder.Property(x => x.Credit)
                .HasColumnType("uniqueidentifier");

            builder.Property(x => x.CreditOfficer)
                .HasColumnType("uniqueidentifier");
            builder.Property(x => x.AssignedCreditOfficer)
                .HasColumnType("uniqueidentifier");

            builder.Property(x => x.Loans)
                .HasColumnType("uniqueidentifier");

            builder.Property(x => x.LoansOfficer)
                .HasColumnType("uniqueidentifier");

            builder.Property(x => x.AssignedLoansOfficer)
                .HasColumnType("uniqueidentifier");

            builder.Property(x => x.ReleasedBy)
                .HasColumnType("uniqueidentifier");

            builder.Property(x => x.PNCancelRequestBy)
                .HasColumnType("uniqueidentifier");

            builder.Property(x => x.PNCancelApprovedBy)
                .HasColumnType("uniqueidentifier");

            builder.Property(x => x.PNCancelDeclinedBy)
                .HasColumnType("uniqueidentifier");

            builder.Property(x => x.PNGeneratedBy)
                .HasColumnType("uniqueidentifier");
            // Reference
            builder.HasOne(x => x.Loan)
                .WithOne(x => x.Assignment)
                .HasForeignKey<Assignment>(x => x.LMSLoanAppId)
                .HasConstraintName("FK_Assignments_LoanDetails")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Remarks)
                .WithMany()
                .HasForeignKey(x => x.RemarksId)
                .HasConstraintName("FK_Assignments_Remarks")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(x => x.LMSLoanAppId);
            builder.HasIndex(x => x.RemarksId);
        }
    }
}