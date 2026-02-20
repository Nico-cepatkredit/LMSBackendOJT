using LMSBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSBackend.Infrastructure.Persistence.Configurations
{
    public class RemarksConfiguration : IEntityTypeConfiguration<Remarks>
    {
        public void Configure(EntityTypeBuilder<Remarks> builder)
        {
            builder.ToTable("Remarks", "CL");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("uniqueidentifier")
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(x => x.LMSLoanAppId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.HasOne(x => x.Loan)
                .WithOne(x => x.Remarks)
                .HasForeignKey<Remarks>(x => x.LMSLoanAppId)
                .HasConstraintName("FK_Remarks_LoanDetails")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.StatusId)
                 .HasColumnType("uniqueidentifier")
                 .IsRequired();

            builder.HasOne(x => x.Status)
                .WithMany()
                .HasForeignKey(x => x.StatusId)
                .HasConstraintName("FK_Remarks_StatusList")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.LOSStatus);
            builder.Property(x => x.MasterList);
            builder.Property(x => x.FFCC);
            builder.Property(x => x.CRAF);
            builder.Property(x => x.Kaiser);
            builder.Property(x => x.VideoCall);
            builder.Property(x => x.ShareLocation);
            builder.Property(x => x.AgencyVerification);

            builder.Property(x => x.InternalRemarks)
                .HasColumnType("nvarchar(max)");

            builder.Property(x => x.InternalRemarksDate)
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.Property(x => x.ExternalRemarks)
                .HasColumnType("nvarchar(max)");

            builder.Property(x => x.ExternalRemarksDate)
                .HasColumnType("datetime")
                .IsRequired(false);

            builder.Property(x => x.Urgent)
                .HasColumnType("varchar(80)");

            builder.Property(x => x.UrgentApp)
                .HasColumnType("varchar(80)");

            builder.HasIndex(x => x.LMSLoanAppId);
            builder.HasIndex(x => x.StatusId);
        }
    }
}