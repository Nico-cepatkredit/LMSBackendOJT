using LMSBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSBackend.Infrastructure.Persistence.Configurations
{
    public class LoanChargesTypeConfiguration : IEntityTypeConfiguration<LoanChargesType>
    {
        public void Configure(EntityTypeBuilder<LoanChargesType> builder)
        {
            builder.ToTable("LoanChargesType", "ILP");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.LMSLoanAppId)
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.HasOne<LoanDetailsApp>()
               .WithMany()
               .HasForeignKey(x => x.LMSLoanAppId)
               .HasConstraintName("FK_LoanChargesType_LoanDetails")
               .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Name)
                .HasMaxLength(100);

            builder.Property(x => x.Amount)
                .HasPrecision(18, 2);

            builder.Property(x => x.Type)
                .HasMaxLength(50);

            builder.Property(x => x.IsDeleted)
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

            builder.HasIndex(x => new { x.LMSLoanAppId, x.Name })
               .IsUnique();
        }
    }
}