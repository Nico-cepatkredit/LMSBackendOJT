using LMSBackend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EmploymentDetailsConfiguration : IEntityTypeConfiguration<EmploymentDetails>
{
    public void Configure(EntityTypeBuilder<EmploymentDetails> builder)
    {
        builder.ToTable("EmploymentDetails", "CL");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(x => x.BorrowersId)
            .IsRequired();

        builder.Property(x => x.BorrowersType)
            .IsRequired();

        builder.Property(x => x.ValidID)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.ValidIDNumber)
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(x => x.Country)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.JobCategory)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.JobPosition)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Employer)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Agency)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Currency)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Salary)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.ContractDate);

        builder.Property(x => x.ContractDuration);

        builder.Property(x => x.YOEAsOFW);

        builder.Property(x => x.Remittance)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.RemittanceChannel)
            .IsRequired(false);

        builder.Property(x => x.Remittee)
            .HasMaxLength(250);

        builder.Property(x => x.VesselName)
            .HasMaxLength(150);

        builder.Property(x => x.VesselType)
            .HasMaxLength(150);

        builder.Property(x => x.VesselLocation)
            .HasMaxLength(150);

        builder.Property(x => x.IMOVessel)
            .HasMaxLength(150);

        builder.HasIndex(x => new { x.BorrowersId, x.BorrowersType });

        builder.Property(x => x.IsContractUnli)
            .IsRequired(false);

        builder.Property(x => x.SalaryInForiegn)
            .HasColumnType("decimal(18,2)")
            .IsRequired(false);

        builder.Property(x => x.SalaryInPeso)
            .HasColumnType("decimal(18,2)")
            .IsRequired(false);

        builder.Property(x => x.PossibleVacation)
            .HasColumnType("varchar(150)")
            .IsRequired(false);

        builder.Property(x => x.EmploymentStatus)
            .IsRequired(false);
    }
}
