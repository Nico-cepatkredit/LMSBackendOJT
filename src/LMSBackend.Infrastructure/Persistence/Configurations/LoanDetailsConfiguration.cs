using LMSBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSBackend.Infrastructure.Persistence.Configurations
{
    public class LoanDetailsConfiguration : IEntityTypeConfiguration<LoanDetailsApp>
    {
        public void Configure(EntityTypeBuilder<LoanDetailsApp> builder)
        {
            builder.ToTable("LoanDetails", "CL");

            builder.HasKey(x => x.LMSLoanAppId);

            builder.Property(x => x.LMSLoanAppId)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(x => x.LoanAppCode)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.LOSLoanAppId)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.PNNumber)
                .HasColumnType("varchar(50)");

            builder.Property(x => x.ProductId)
                .HasColumnType("varchar(20)")
                .IsRequired();

            builder.Property(x => x.Amount)
                .HasPrecision(18, 2);

            builder.Property(x => x.ApprovedAmount)
                .HasPrecision(18, 2);

            builder.Property(x => x.CRARemarks)
                .HasColumnType("varchar(500)");

            builder.Property(x => x.CRARecommendation)
                .HasColumnType("varchar(500)");

            builder.Property(x => x.RecUser)
                .HasColumnType("varchar(50)");

            builder.Property(x => x.ModUser)
                .HasColumnType("uniqueidentifier");

            builder.Property(x => x.ModDate)
                .HasColumnType("datetime2");

            builder.Property(x => x.RecDate)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.HasOne(x => x.UserAccount)
                .WithMany()
                .HasForeignKey(x => x.LCId)
                .HasConstraintName("FK_LoanDetails_UserAccount_LCId");
        }


    }
}